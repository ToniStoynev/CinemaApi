using CinemAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data.EF.ModelConfigurations
{
    internal sealed class ReservationModelConfiguration : IModelConfiguration
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<Reservation> projectionModel = modelBuilder.Entity<Reservation>();
            projectionModel.HasKey(model => model.Id);
            projectionModel.Property(model => model.ProjectionStartDate).IsRequired();
            projectionModel.Property(model => model.MovieName).IsRequired();
            projectionModel.Property(model => model.CinemaName).IsRequired();
            projectionModel.Property(model => model.RoomNumber).IsRequired();
            projectionModel.Property(model => model.Row).IsRequired();
            projectionModel.Property(model => model.Column).IsRequired();
            projectionModel.Property(model => model.IsCancelled);
            projectionModel.Property(model => model.ProjectionId).IsRequired();
        }
    }
}
