using SmartApp.Domain.ModelTemp;
using SmartApp.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Application.Interface
{
    public interface IRestaurantRepository
    {
        Task<List<RestaurantModel>> GetAllRestaurantsAsync();
        Task<IEnumerable<RestaurantBranchModel>> GetRestaurantBranchByRestaurantIdAsync(int restaurantId);

        Task<IEnumerable<DiningTableWithTimeSlotModel>> GetDiningTablesByBranchAsync(int branchId, DateTime date);

        Task<IEnumerable<DiningTableWithTimeSlotModel>> GetDiningTableByBranchAsync(int branchId);

        Task<RestaurantReservationDetails> GetRestaurantReservationDetailsAsync(int timeSlotId);

        Task<User?> GetUserAsync(string emailId);

    }
}
