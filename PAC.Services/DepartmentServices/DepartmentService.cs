using PAC.DATA;
using PAC.Models.DepartmentModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Services.DepartmentServices
{
    public class DepartmentService
    {
        private readonly Guid _userId;
        public DepartmentService(Guid userId)
        {
            _userId = userId;
        }
        public async Task<bool> Post(DepartmentCreate department)
        {
            var entity = new Department
            {
                DepartmentName = department.DepartmentName,
                CreatedDate=DateTime.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Departments.Add(entity);
                return await ctx.SaveChangesAsync()>0;
            }
        }
        public async Task<IEnumerable<DepartmentListItem>> Get()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    await
                    ctx
                    .Departments
                 //   .Where(d => d.OwnerID == _userId)
                    .Select(d => new DepartmentListItem
                    {
                        ID=d.ID,
                        DepartmentName=d.DepartmentName
                    }).ToListAsync();
                
                return query;
            }
        }
        public async Task<DepartmentDetail> Get(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var department =
                    await
                    ctx
                    .Departments
                  //  .Where(d => d.OwnerID == _userId)
                    .SingleOrDefaultAsync(d => d.ID == id);

                if (department is null)
                {
                    return null;
                }

                //this is TRANSFORMING A DEPARTMENT TO A DEPARTMETN DETAIL -> THE EASY WAY.....
                return new DepartmentDetail
                {
                    ID=department.ID,
                    DepartmentName=department.DepartmentName,
                    CreatedDate=department.CreatedDate,
                    ModifiedDate=department.ModifiedDate,
                    Employees=department.Employees,
                    Positions=department.Positions
                };
            }
        }
        public async Task<bool> Put(DepartmentEdit department, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldDepData = await ctx.Departments.FindAsync(id);
                if (oldDepData is null)
                {
                    return false;
                }
                oldDepData.DepartmentName = department.DepartmentName;
                oldDepData.ModifiedDate = DateTime.Now;

                return await ctx.SaveChangesAsync()>0;
            }
        }
        public async Task<bool> Delete(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldDepData = await ctx.Departments.FindAsync(id);
                if (oldDepData is null)
                {
                    return false;
                }
                ctx.Departments.Remove(oldDepData);
                return await ctx.SaveChangesAsync() == 1;
            }
        }
    }
}
