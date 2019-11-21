using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Ouput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IReservationRepository
    {
        ReservationTicket Insert(IReservationCreation reservation);

        void CancelReservations(long id);

        IReservation GetReservationById(int id);
    }
}
