
using SmartApp.Application.Interface;
using SmartApp.Domain.ViewModels;

namespace SmartApp.Application.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
            
        }
        public async Task<List<RestaurantModel>> GetAllRestaurantsAsync()
        {
            return await _restaurantRepository.GetAllRestaurantsAsync();
        }

        public async Task<IEnumerable<DiningTableWithTimeSlotModel>> GetDiningTableByBranchAsync(int branchId)
        {
            return await _restaurantRepository.GetDiningTableByBranchAsync(branchId);
        }

        public async Task<IEnumerable<DiningTableWithTimeSlotModel>> GetDiningTablesByBranchAsync(int branchId, DateTime date)
        {
            return await _restaurantRepository.GetDiningTablesByBranchAsync(branchId, date);
        }

        public async Task<IEnumerable<RestaurantBranchModel>> GetRestaurantBranchByRestaurantIdAsync(int restaurantId)
        {
            return await _restaurantRepository.GetRestaurantBranchByRestaurantIdAsync((int)restaurantId);
        }
    }
}
