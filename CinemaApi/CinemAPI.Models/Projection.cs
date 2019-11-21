using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Room;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemAPI.Models
{
    public class Projection : IProjection, IProjectionCreation
    {
        public Projection()
        {
            this.Reservations = new List<Reservation>();
            this.Tickets = new List<Ticket>();
        }

        public Projection(int movieId, int roomId, DateTime startdate, int availableSeats)
            : this()
        {
            this.MovieId = movieId;
            this.RoomId = roomId;
            this.StartDate = startdate;
            this.AvailableSeatsCount = availableSeats;
        }

        public long Id { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        public int MovieId { get; set; }
        
        public virtual Movie Movie { get; set; }

        public DateTime StartDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Available seats should be greater than zero")]
        public int AvailableSeatsCount { get; set; } 

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

     
    }
}