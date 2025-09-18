﻿using SmartApp.Application.Interface;
using SmartApp.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Application.Service
{
    public class ReservationService: IReservationService
    {
        private readonly IReservationRepository reservationRepository;
        public ReservationService(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        public Task<DiningTableWithTimeSlotModel> CheckInReservationAsync(DiningTableWithTimeSlotModel reservation)
        {
            return reservationRepository.CheckInReservationAsync(reservation);
        }

        public Task<int> CreateOrUpdateReservationAsync(ReservationModel reservation)
        {
            return reservationRepository.CreateOrUpdateReservationAsync(reservation);
        }


        public Task<List<ReservationDetailsModel>> GetReservationDetailsAsync()
        {
            return reservationRepository.GetReservationDetailsAsync();
        }

        public async Task<bool> TimeSlotIdExistAsync(int timeSlotId)
        {
            var timeSlot = await reservationRepository.GetTimeSlotByIdAsync(timeSlotId);
            // You might need to map the TimeSlot entity to a TimeSlotModel here
            return timeSlot != null;
        }
    }
}
