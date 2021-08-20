using Microsoft.AspNet.Identity;
using PAC.Models.PositionModels;
using PAC.Services.PositionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PAC_API.Controllers.Position_Controller
{
    public class PositionController : ApiController
    {
        private PositionService CreatePositionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new PositionService(userId);
            return svc;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var svc = CreatePositionService();
            var positions = await svc.Get();
            return Ok(positions);
        }
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            if (id<1)
            {
                return BadRequest();
            }
            var svc = CreatePositionService();
            var position = await svc.Get(id);
            return Ok(position);
        }
        [HttpPost]
        public async Task<IHttpActionResult> Post(PositionCreate position)
        {
            if (position is null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var svc = CreatePositionService();
            var success = await svc.Post(position);
            if (success)
            {
                return Ok();
            }
            return InternalServerError();
        }
        [HttpPut]
        public async Task<IHttpActionResult> Put(PositionEdit position, int id)
        {
            if (id<1 || id!=position.ID || position is null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var svc = CreatePositionService();
            var success = await svc.Put(position,id);
            if (success)
            {
                return Ok();
            }
            return InternalServerError();
        }
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id<1)
            {
                return BadRequest();
            }
            var svc = CreatePositionService();
            var success = await svc.Delete(id);
            if (success)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}
