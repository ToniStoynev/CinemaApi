using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Ouput;

namespace CinemAPI.Data.Implementation
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly CinemaDbContext db;
        private readonly IProjectionRepository projectionRepository;

        public ReservationRepository(CinemaDbContext db, IProjectionRepository projectionRepository)
        {
            this.db = db;
            this.projectionRepository = projectionRepository;
        }
        public ReservationTicket Insert(IReservationCreation reservation)
        {
            Reservation  newReservaton = new Reservation(reservation.ProjectionStartDate, 
                reservation.MovieName, reservation.CinemaName, reservation.RoomNumber, reservation
                .Row, reservation.Column, reservation.ProjectionId);

            var projection = this.projectionRepository.GetProjectionById(reservation.ProjectionId);

            db.Reservations.Add(newReservaton);

            projection.AvailableSeatsCount--;
            db.SaveChanges();

            return new ReservationTicket
            {
                UniqueKeyOfReservation = newReservaton.Id,
                ProjectionStartDate = newReservaton.ProjectionStartDate,
                MovieName = newReservaton.MovieName,
                CinemaName = newReservaton.CinemaName,
                RoomNumber = newReservaton.RoomNumber,
                Row = newReservaton.Row,
                Column = newReservaton.Column
            };
        }

        public void CancelReservations(long id)
        {
            var reservations = db.Reservations.Where(x => x.ProjectionId == id);

            foreach (var reservation in reservations)
            {
                TimeSpan span = reservation.ProjectionStartDate.Subtract(DateTime.Now);
                if (span.Minutes < 10 && reservation.IsCancelled == false)
                {
                    reservation.IsCancelled = true;
                    reservation.Projection.AvailableSeatsCount++;
                }
            }
            db.SaveChanges();
        }

        public IReservation GetReservationById(int id)
        {
            return db.Reservations.FirstOrDefault(x => x.Id == id);
        }
    }
}
