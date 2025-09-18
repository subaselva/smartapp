using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Domain.ViewModels
{
    public partial class RestaurantModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;

        public string? phone { get; set; }

        public string? Email { get; set; }

        public string? ImageUrl { get; set; }
    }
}
