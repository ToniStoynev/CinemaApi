﻿using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IProjectionRepository
    {
        IProjection Get(int movieId, int roomId, DateTime startDate);

        void Insert(IProjectionCreation projection);

        IEnumerable<IProjection> GetActiveProjections(int roomId);

        IProjection GetProjectionById(long id);
    }
}