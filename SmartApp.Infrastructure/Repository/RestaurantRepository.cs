using Microsoft.EntityFrameworkCore;
using SmartApp.Application.Interface;
using SmartApp.Domain.ModelTemp;
using SmartApp.Domain.ViewModels;
using SmartApp.Infrastructure.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartApp.Infrastructure.Repository
{

    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantRepository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<User?> GetUserAsync(string emailId)
        {
            return _dbContext.Users.FirstOrDefaultAsync(f => f.Email.Equals(emailId));


        }

        public async Task<RestaurantReservationDetails> GetRestaurantReservationDetailsAsync(int timeSlotId)
        {

            var query = await (from diningTable in _dbContext.DiningTables
                               join restaurantBranch in _dbContext.RestaurantBranches on diningTable.RestaurantBranchId equals restaurantBranch.Id
                               join restaurant in _dbContext.Restaurants on restaurantBranch.RestaurantId equals restaurant.Id
                               join timeSlot in _dbContext.TimeSlots on diningTable.Id equals timeSlot.DiningTableId
                               where timeSlot.Id == timeSlotId
                               select new RestaurantReservationDetails()
                               {
                                   RestaurantName = restaurant.Name,
                                   BranchName = restaurantBranch.Name,
                                   Address = restaurantBranch.Address,
                                   TableName = diningTable.TableName,
                                   Capacity = diningTable.Capacity,
                                   MealType = timeSlot.MealType,
                                   ReservationDay = timeSlot.ReservationDay.ToDateTime(TimeOnly.MinValue)
                               }).FirstOrDefaultAsync();

            return query;
        }

        public Task<List<RestaurantModel>> GetAllRestaurantsAsync()
        {
            var restaurants = _dbContext.Restaurants
                 .OrderBy(x => x.Name)
                 .Select(r => new RestaurantModel()
                 {
                     Id = r.Id,
                     Name = r.Name,
                     Address = r.Address,
                     phone = r.Phone,
                     Email = r.Email,
                     ImageUrl = r.ImageUrl
                 }).ToListAsync();
            return restaurants;
        }

        public async Task<IEnumerable<DiningTableWithTimeSlotModel>> GetDiningTableByBranchAsync(int branchId)
        {
            var data = await (
                from rb in _dbContext.RestaurantBranches
                join dt in _dbContext.DiningTables on rb.Id equals dt.RestaurantBranchId
                join ts in _dbContext.TimeSlots on dt.Id equals ts.DiningTableId
                where dt.RestaurantBranchId == branchId 
                orderby ts.Id, ts.MealType
                select new DiningTableWithTimeSlotModel()
                {
                    BranchId = rb.Id,
                    ReservationDay = ts.ReservationDay.ToDateTime(new TimeOnly(0, 0)),
                    TableName = dt.TableName,
                    Capacity = dt.Capacity,
                    MealType = ts.MealType,
                    TableStatus = ts.TableStatus,
                    TimeSlotId = ts.Id,
                    UserEmailId = (from r in _dbContext.Reservations
                                   join u in _dbContext.Users on r.UserId equals u.Id
                                   where r.TimeSlotId == ts.Id
                                   select u.Email.ToLower()).FirstOrDefault()

                })
                .ToListAsync();
            return data;




        }

        public async Task<IEnumerable<DiningTableWithTimeSlotModel>> GetDiningTablesByBranchAsync(int branchId, DateTime date)
        {
            var targetDate = DateOnly.FromDateTime(date);
            var diningTables = await _dbContext.DiningTables
                .Where(dt => dt.RestaurantBranchId == branchId)
                .SelectMany(dt => dt.TimeSlots, (dt, ts) => new
                {

                    dt.RestaurantBranchId,
                    dt.TableName,
                    dt.Capacity,
                    ts.ReservationDay,
                    ts.MealType,
                    ts.TableStatus,
                    ts.Id

                    // You might want to include more properties here as needed
                })
                .Where(ts => ts.ReservationDay == targetDate)
                .OrderBy(ts => ts.Id)
                .ThenBy(ts => ts.MealType)
                .ToListAsync();

            return diningTables.Select(dt => new DiningTableWithTimeSlotModel
            {
                BranchId = dt.RestaurantBranchId,
                ReservationDay = dt.ReservationDay.ToDateTime(new TimeOnly(0, 0)),
                TableName = dt.TableName,
                Capacity = dt.Capacity,
                MealType = dt.MealType,
                TableStatus = dt.TableStatus,
                TimeSlotId = dt.Id

            });
        }

        public async Task<IEnumerable<RestaurantBranchModel>> GetRestaurantBranchByRestaurantIdAsync(int restaurantId)
        {
            var branches = await _dbContext.RestaurantBranches
                .Where(b => b.RestaurantId == restaurantId)
                .Select(b => new RestaurantBranchModel()
                {
                    Id = b.Id,
                    RestaurantId = b.RestaurantId,
                    Name = b.Name,
                    Address = b.Address,
                    Phone = b.Phone,
                    Email = b.Email,
                    ImageUrl = b.ImageUrl
                }).ToListAsync();

            return branches;
        }


        
    }
}
