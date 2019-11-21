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
    public class TicketController : ApiController
    {
        private readonly ITicketRepository ticketRepository;
        private readonly IProjectionRepository projectionResosiory;
        private readonly IReservationRepository reservationRepository;
        public TicketController(ITicketRepository ticketRepository, IProjectionRepository projectionResosiory,
            IReservationRepository reservationRepository)
        {
            this.projectionResosiory = projectionResosiory;
            this.ticketRepository = ticketRepository;
            this.reservationRepository = reservationRepository;
        }

        [HttpPost]
        public IHttpActionResult BuyTicket(int id, int row, int col) {

            var projection =  (Projection)projectionResosiory.GetProjectionById(id);

            if (projection == null)
            {
                return BadRequest("Projection with given id doesn't exist");
            }

            if (projection.StartDate < DateTime.Now)
            {
                return BadRequest("Cannot buy ticket for finished or started projection");
            }

            reservationRepository.CancelReservations(id);

            if (projection.Reservations.Any(x => x.Row == row && x.Column == col && x.IsCancelled == false))
            {
                return BadRequest("Sorry! These seats are already reserved.");
            }

            if (projection.Tickets.Any(x => x.Row == row && x.Col == col))
            {
                return BadRequest("Sorry! These seats are already bought.");
            }

            Ticket ticket = new Ticket(projection.StartDate,
               projection.Movie.Name, projection.Room.Cinema.Name, projection.Room.Number, row, col, projection.Id);

            var returnTicket = this.ticketRepository.Insert(ticket);

            return Ok(returnTicket);
        }

        [HttpPost]
        public IHttpActionResult BuyTicketWithReservation(int id) {

            var reservation = (Reservation)reservationRepository.GetReservationById(id);

            if (reservation == null)
            {
                return BadRequest("Reservation with given id doesn't exist");
            }

            reservationRepository.CancelReservations(reservation.ProjectionId);

            TimeSpan span = reservation.ProjectionStartDate.Subtract(DateTime.Now);

            if (span.Minutes < 10)
            {
                return BadRequest("Can not buy a ticket with reservation 10 minutes before projection starts.");
            }

            

            if (reservation.IsCancelled == true)
            {
                return BadRequest("Sorry, your reservation is cancelled");
            }

            if (reservation.Projection.Tickets.Any(x => x.Row == reservation.Row && x.Col == reservation.Column))
            {
                return BadRequest("Тhe places for this reservation have already been purchased");
            }

            Ticket ticket = new Ticket(reservation.ProjectionStartDate,
               reservation.MovieName, reservation.CinemaName, reservation.RoomNumber, 
               reservation.Row, reservation.Column, reservation.ProjectionId);

            var returnTicket = this.ticketRepository.Insert(ticket);

            return Ok(returnTicket);
        }
    }
}
