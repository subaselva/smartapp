using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Domain.ViewModels
{
    public class ReservationModel
    {
        public string? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public required string EmailId { get; set; }
        public string? PhoneNumber { get; set; }
        public required int TimeSlotId { get; set; }
        public required DateTime ReservationDate { get; set; }
        public string ReservationStatus { get; set; }
    }
}
