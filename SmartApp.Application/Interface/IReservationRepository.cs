using SmartApp.Domain.ModelTemp;
using SmartApp.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Application.Interface
{
    public interface IReservationRepository
    {
        Task<int> CreateOrUpdateReservationAsync(ReservationModel reservation);
        Task<TimeSlot> GetTimeSlotByIdAsync(int timeSlotId);
        Task<DiningTableWithTimeSlotModel> CheckInReservationAsync(DiningTableWithTimeSlotModel reservation);
        Task<List<ReservationDetailsModel>> GetReservationDetailsAsync();
    }

}
