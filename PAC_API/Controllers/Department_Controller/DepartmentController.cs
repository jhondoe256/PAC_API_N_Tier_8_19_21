using Microsoft.AspNet.Identity;
using PAC.Models.DepartmentModels;
using PAC.Services.DepartmentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PAC_API.Controllers.Department_Controller
{
    public class DepartmentController : ApiController
    {
        private DepartmentService CreateDepartmentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new DepartmentService(userId);
            return svc;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var svc = CreateDepartmentService();
            var depts = await svc.Get();
            return Ok(depts);
        }
        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri] int id)
        {
            var svc = CreateDepartmentService();
            var dept = await svc.Get(id);
            return Ok(dept);
        }
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]DepartmentCreate department)
        {
            if (department is null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var svc = CreateDepartmentService();
            var success = await svc.Post(department);
            if (success)
            {
                return Ok();
            }
            return InternalServerError();
        }
        [HttpPut]
        public async Task<IHttpActionResult>Put([FromBody] DepartmentEdit department, [FromUri]int id)
        {
            if (id<1 || id!=department.ID || department is null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var svc = CreateDepartmentService();
            var success = await svc.Put(department, id);
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
            var svc = CreateDepartmentService();
            var success = await svc.Delete(id);
            if (success)
            {
                return Ok();
            }
            return InternalServerError();
        }
             
    }
}
