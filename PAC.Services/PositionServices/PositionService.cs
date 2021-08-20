using PAC.DATA;
using PAC.Models.PositionModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Services.PositionServices
{
    public class PositionService
    {
        private readonly Guid _userId;
        public PositionService(Guid userId)
        {
            _userId = userId;
        }
        public async Task<IEnumerable<PositionListItem>> Get()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    await
                    ctx
                    .Positions
                //    .Where(p => p.OwnerID == _userId)
                    .Select(p => new PositionListItem
                    {
                        ID = p.ID,
                        PositionName = p.PositionName
                    }).ToListAsync();

                return query;
            }
        }
        public async Task<PositionDetail> Get(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var position =
                    await
                    ctx
                    .Positions
            //        .Where(p => p.OwnerID == _userId)
                    .SingleOrDefaultAsync(p => p.ID == id);
                if (position is null)
                {
                    return null;
                }
                return new PositionDetail
                {
                    ID = position.ID,
                    DepartmentID = position.DepartmentID,
                    PositionName = position.PositionName,
                    Department = position.Department,
                    CreatedDate = position.CreatedDate,
                    ModifiedDate = position.ModifiedDate
                };
            }
        }
        public async Task<bool> Post(PositionCreate position)
        {
            var entity = new Position
            {
                DepartmentID = position.DepartmentID,
                PositionName = position.PositionName,
                CreatedDate=DateTime.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                //find the department 
                var department = await ctx.Departments.FindAsync(position.DepartmentID);
                if (department is null)
                {
                    return false;
                }

                  entity.Department = department;
                //this adds position to department...
                 entity.Department.Positions.Add(entity);
                //add position to database...
                ctx.Positions.Add(entity);
                return await ctx.SaveChangesAsync() > 0;
            }
        }
        public async Task<bool> Put(PositionEdit position, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldPosData = await ctx.Positions.FindAsync(id);
                if (oldPosData is null)
                {
                    return false;
                }
                oldPosData.PositionName = position.PositionName;

                return await ctx.SaveChangesAsync() == 1;
            }
        }
        public async Task<bool> Delete(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldPosData = await ctx.Positions.FindAsync(id);
                if (oldPosData is null)
                {
                    return false;
                }
                ctx.Positions.Remove(oldPosData);
                return await ctx.SaveChangesAsync() > 0;
            }
        }
    }
}
