using CinemAPI.Data;
using CinemAPI.Data.Implementation;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Input.Projection;
using System;
using System.Linq;
using System.Web.Http;

namespace CinemAPI.Controllers
{

    public class ProjectionController : ApiController
    {
        private readonly INewProjection newProj;
        private readonly IProjectionRepository projectionRepository;

        public ProjectionController(INewProjection newProj, IProjectionRepository projectionRepository)
        {
            this.newProj = newProj;
            this.projectionRepository = projectionRepository;
        }

        [HttpPost]
        public IHttpActionResult Index(ProjectionCreationModel model)
        {
            NewProjectionSummary summary = newProj.New(new Projection(model.MovieId, model.RoomId, model.StartDate, model.AvailableSeatsCount));

            if (summary.IsCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        public IHttpActionResult GetProjection(int id) {
            var projection = this.projectionRepository.GetProjectionById(id);

            if (projection == null || projection.StartDate < DateTime.Now)
            {
                return BadRequest("This projection is started or finished");
            }

            return Ok(projection.AvailableSeatsCount);
        }


       
    }
}