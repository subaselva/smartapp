using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using SmartApp.Application.Service;
using SmartApp.Domain.ViewModels;

namespace SmartApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IReservationService reservationService;
        private readonly IEmailNotification emailNotification;


        public RestaurantController(IRestaurantService restaurantService, IReservationService reservationService,
            IEmailNotification emailNotification)
        {
            _restaurantService = restaurantService;
            this.reservationService = reservationService;
            this.emailNotification = emailNotification;
        }
        [HttpGet("restaurants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet("branches/{restaurantId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RestaurantBranchModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RestaurantBranchModel>>> GetRestaurantBranchByRestaurantId(int restaurantId)
        {
            var branches = await _restaurantService.GetRestaurantBranchByRestaurantIdAsync(restaurantId);
            if (branches == null)
            {
                return NotFound();
            }
            return Ok(branches);

        }
        [HttpGet("diningtables/{branchId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DiningTableWithTimeSlotModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<DiningTableWithTimeSlotModel>>> GetDiningTableByBranch(int branchId)
        {
            var diningTables = await _restaurantService.GetDiningTableByBranchAsync(branchId);
            if (diningTables == null || !diningTables.Any())
            {
                return NotFound();
            }
            return Ok(diningTables);
        }

        [HttpGet("diningtables/{branchId}/{date}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DiningTableWithTimeSlotModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<DiningTableWithTimeSlotModel>>> GetDiningTablesByBranchAndDateAsync(int branchId, DateTime date)
        {
            var diningTables = await _restaurantService.GetDiningTablesByBranchAsync(branchId, date);
            if (diningTables == null)
            {
                return NotFound();
            }
            return Ok(diningTables);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ReservationModel))]
        [ProducesResponseType(400)]
        
        [AllowAnonymous]
        public async Task<ActionResult<ReservationModel>> CreateReservationAsync(ReservationModel reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the selected time slot exists
            var timeSlot = await reservationService.TimeSlotIdExistAsync(reservation.TimeSlotId);
            if (!timeSlot)
            {
                return NotFound("Selected time slot not found.");
            }

            // Create a new reservation
            var newReservation = new ReservationModel
            {
                UserId = reservation.UserId,
                FirstName = reservation.FirstName,
                LastName = reservation.LastName,
                EmailId = reservation.EmailId,
                PhoneNumber = reservation.PhoneNumber,
                TimeSlotId = reservation.TimeSlotId,
                ReservationDate = reservation.ReservationDate,
                ReservationStatus = reservation.ReservationStatus
            };

            var createdReservation = await reservationService.CreateOrUpdateReservationAsync(newReservation);
            await emailNotification.SendBookingEmailAsync(newReservation);

            return new CreatedResult("GetReservation", new { id = createdReservation });
        }

        [HttpGet("getreservations")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReservationDetailsModel>))]
        [ProducesResponseType(404)]
        
        public async Task<ActionResult<IEnumerable<ReservationDetailsModel>>> GetReservationDetails(int branchId, DateTime date)
        {
            var reservations = await reservationService.GetReservationDetailsAsync();

            return Ok(reservations);
        }

    }


}

