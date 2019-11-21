﻿using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models
{
    public class Reservation : IReservation, IReservationCreation
    {
        public Reservation()
        {

        }
        public Reservation(DateTime projectionStartDate, string movieName, string cinemaName, int roomNumber,
            int row, int column, long projectionId)
        {
            this.ProjectionStartDate = projectionStartDate;
            this.MovieName = movieName;
            this.CinemaName = cinemaName;
            this.RoomNumber = roomNumber;
            this.Row = row;
            this.Column = column;
            this.ProjectionId = projectionId;
        }
        public int Id { get; set; }

        public DateTime ProjectionStartDate { get; set; }

        public string MovieName { get; set; }

        public string CinemaName { get; set; }

        public int RoomNumber { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public long ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }

        public bool IsCancelled { get; set; }
    }
}
