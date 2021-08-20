using Microsoft.AspNet.Identity;
using PAC.Models.DogCatcherModels;
using PAC.Services.DogCatcherServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PAC_API.Controllers.DogCatcher_Controller
{
    public class DogCatcherController : ApiController
    {
        private DogCatcherService CreateDC_Service()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new DogCatcherService(userId);
            return svc;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var svc = CreateDC_Service();
            var dogcatchers = await svc.Get();
            return Ok(dogcatchers);
        }
        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri] int id)
        {
            if (id<1)
            {
                return BadRequest();
            }
            var svc = CreateDC_Service();
            var dc = await svc.Get(id);
            return Ok(dc);
        }
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] DogCatcherCreate dogCatcher)
        {
            if (dogCatcher is null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var svc = CreateDC_Service();
            var success = await svc.Post(dogCatcher);
            if (success)
            {
                return Ok();
            }
            return InternalServerError();
        }
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] DogCatcherEdit dogCatcher, [FromUri] int id)
        {
            if (id<1 || id!=dogCatcher.ID || dogCatcher is null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var svc = CreateDC_Service();
            var success = await svc.Put(dogCatcher, id);
            if (success)
            {
                return Ok();
            }
            return InternalServerError();
        }
        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            if (id<1)
            {
                return BadRequest();
            }
            var svc = CreateDC_Service();
            var success = svc.Delete(id);
            if (await success)
            {
                return Ok();
            }
            return InternalServerError();
        }

    }
}
