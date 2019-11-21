using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Ticket;
using CinemAPI.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaDbContext db;
        private readonly IProjectionRepository projectionRepository;
        public TicketRepository(CinemaDbContext db,  IProjectionRepository projectionRepository)
        {
            this.db = db;
            this.projectionRepository = projectionRepository;
        }

        public TicketReturnObject Insert(ITicketCreation ticket)
        {
            Ticket newTicket = new Ticket(ticket.ProjectionStartDate,
                ticket.MovieName, ticket.CinemaName, ticket.RoomNumber, ticket
                .Row, ticket.Col, ticket.ProjectionId);

            var projection = (Projection)this.projectionRepository.GetProjectionById(ticket.ProjectionId);

            db.Tickets.Add(newTicket);

            projection.AvailableSeatsCount--;
            db.SaveChanges();

            return new TicketReturnObject
            {
                TicketUniqueKey = newTicket.Id,
                ProjectionStartDate = newTicket.ProjectionStartDate,
                MovieName = newTicket.MovieName,
                CinemaName = newTicket.CinemaName,
                RoomNumber = newTicket.RoomNumber,
                Row = newTicket.Row,
                Column = newTicket.Col
            };
        }
    }
}
