using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartApp.Application.Service;
using SmartApp.Domain.ModelTemp;
using SmartApp.Domain.ViewModels;

namespace SmartApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailNotification emailNotification;

        public ReservationController(IReservationService reservationService, IHttpContextAccessor contextAccessor, IEmailNotification emailNotification)
        {
            this.reservationService = reservationService;
            _contextAccessor = contextAccessor;
            this.emailNotification = emailNotification;
        }

        [HttpGet("{id}", Name = "GetReservation")]
        //[RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Read")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            // Your logic to retrieve and return a reservation
            return Ok();
        }


        [HttpPost("CheckIn")]
        [ProducesResponseType(200, Type = typeof(ReservationModel))]
        [ProducesResponseType(400)]
      //  [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Write")]
        public async Task<ActionResult<ReservationModel>> CheckInReservationAsync(DiningTableWithTimeSlotModel reservation)
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
            
            var response = await reservationService.CheckInReservationAsync(reservation);
            if (response == null)
            {
                return NotFound($"User with email {reservation.UserEmailId} not found.");
            }

            await emailNotification.SendCheckInEmailAsync(reservation);
            return Ok(response);
        }
    }
}
