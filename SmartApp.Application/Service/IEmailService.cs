using SendGrid;
using SmartApp.Domain.ModelTemp;


namespace SmartApp.Application.Service
{
   
        public interface IEmailService
        {
        Task<Response> SendContactEmailAsync(ContactModel model, string restaurantEmail);

    }


}
