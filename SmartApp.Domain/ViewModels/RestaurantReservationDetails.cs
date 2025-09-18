using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Domain.ViewModels
{
    public class RestaurantReservationDetails
    {
        public string RestaurantName { get; set; }
        public string BranchName { get; set; }
        public string TableName { get; set; }
        public int Capacity { get; set; }
        public string MealType { get; set; }
        public DateTime ReservationDay { get; set; }
        public string Address { get; set; }
    }
}
