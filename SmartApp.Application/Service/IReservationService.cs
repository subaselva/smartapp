using SmartApp.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Application.Service
{
    public interface IReservationService
    {
        Task<int> CreateOrUpdateReservationAsync(ReservationModel reservation);
        Task<bool> TimeSlotIdExistAsync(int timeSlotId);
        Task<DiningTableWithTimeSlotModel> CheckInReservationAsync(DiningTableWithTimeSlotModel reservation);
        Task<List<ReservationDetailsModel>> GetReservationDetailsAsync();
    }
}
