
using Microsoft.AspNetCore.Mvc;
using SmartApp.Application.Service;
using SmartApp.Domain.ModelTemp;

namespace SmartApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
        public class ContactController : ControllerBase
        {
            private readonly IEmailService emailService;

            public ContactController(IEmailService emailService)
            {
                this.emailService = emailService;
            }

        [HttpPost("send")]
        public async Task<IActionResult> SendContactMessage([FromBody] ContactModel model)
        {
            if (string.IsNullOrEmpty(model.Message)) return BadRequest("Message is required");

            // Here you decide restaurant’s email (from config or DB)
            string restaurantEmail = "subalakshmi74soft@gmail.com";

            await emailService.SendContactEmailAsync(model, restaurantEmail);

            return Ok(new { success = true, message = "Message sent successfully" });
        }
    }
    }

