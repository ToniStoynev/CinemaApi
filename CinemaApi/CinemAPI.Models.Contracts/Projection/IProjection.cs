﻿
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;

namespace CinemAPI.Models.Contracts.Projection
{
    public interface IProjection
    {
        long Id { get; }

        int RoomId { get; }

        int MovieId { get; }

        DateTime StartDate { get; }

        int AvailableSeatsCount { get; set; }
    }
}