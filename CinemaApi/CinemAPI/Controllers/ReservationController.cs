using CinemAPI.Data;
using CinemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ReservationController : ApiController
    {
        private readonly IProjectionRepository projectionRepository;
        private readonly IReservationRepository reservationRepository;
        public ReservationController(IProjectionRepository projectionRepository, IReservationRepository reservationRepository)
        {
            this.projectionRepository = projectionRepository;
            this.reservationRepository = reservationRepository;
        }

        [HttpPost]
        public IHttpActionResult ReserveSeats(int id, int row, int col)
        {
            var projection =  (Projection)this.projectionRepository.GetProjectionById(id);

            if (projection == null)
            {
                return BadRequest("Projection with given id does not exist");
            }

            TimeSpan span = projection.StartDate.Subtract(DateTime.Now);
            if (projection.StartDate < DateTime.Now)
            {
                return BadRequest("Sorry, you can not reserve a seat !The projection is finished.");
            }

            if (span.Minutes < 10)
            {
                return BadRequest("Sorry, you can not reserve a seat !The projection starts in less than 10 minutes.");
            }

            if (projection.Reservations.Any(x => x.Row == row && x.Column == col))
            {
                return BadRequest("These seats are already reserved");
            }

            if (projection.Room.Rows < row || projection.Room.SeatsPerRow < col)
            {
                return BadRequest("Seats not existing in the room");
            }

            var reservation = new Reservation(projection.StartDate,
                projection.Movie.Name, projection.Room.Cinema.Name, projection.Room.Number, row, col, projection.Id);

            var reservationTicket =  this.reservationRepository.Insert(reservation);

            return Ok(reservationTicket);
        }
    }
}
