using PAC.DATA;
using PAC.Models.DogCatcherModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Services.DogCatcherServices
{
    public class DogCatcherService
    {
        private readonly Guid _userId;
        public DogCatcherService(Guid userId)
        {
            _userId = userId;
        }
        public async Task<IEnumerable<DogCatcherListItem>> Get()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    await
                    ctx
                    .DogCatchers
               //     .Where(d => d.OwnerId == _userId)
                    .Select(d => new DogCatcherListItem
                    {
                        ID = d.ID,
                        EmployeeBadgeID = d.EmployeeBadgeID,
                        FirstName = d.FirstName,
                        LastName = d.LastName
                    }).ToListAsync();

                return query;
            }
        }
        public async Task<DogCatcherDetail> Get(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var dogcatcher =
                    await
                    ctx
                    .DogCatchers
                    .Include(d=>d.Position)
                    .Include(d=>d.Position.Department)
                  //  .Where(d => d.OwnerId == _userId)
                    .SingleOrDefaultAsync(d => d.ID == id);
                if (dogcatcher is null)
                {
                    return null;
                }

                return new DogCatcherDetail
                {
                    ID = dogcatcher.ID,
                    EmployeeBadgeID = dogcatcher.EmployeeBadgeID,
                    FirstName = dogcatcher.FirstName,
                    LastName = dogcatcher.LastName,
                    OwnerId = dogcatcher.OwnerId,
                    Position = dogcatcher.Position,
                    PositionID = dogcatcher.PositionID,
                    CreatedDate = dogcatcher.CreatedDate,
                    ModifiedDate = dogcatcher.ModifiedDate
                };
            }
        }
        public async Task<bool> Post(DogCatcherCreate dogCatcher)
        {
            var entity = new DogCatcher
            {
                EmployeeBadgeID = dogCatcher.EmployeeBadgeID,
                FirstName = dogCatcher.FirstName,
                LastName = dogCatcher.LastName,
                PositionID = dogCatcher.PositionID,
                CreatedDate=DateTime.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                //find position 
                var position = await ctx.Positions.FindAsync(dogCatcher.PositionID);
                if (position is null)
                {
                    return false;
                }
                entity.Position = position;

                //find department 
                var department = ctx.Departments.SingleOrDefault(d => d.ID == position.DepartmentID);
                entity.Position.Department = department;
                //add dogcater to department 
                entity.Position.Department.Employees.Add(entity);

                ctx.DogCatchers.Add(entity);

                return await ctx.SaveChangesAsync() > 0;
            }
        }
        public async Task<bool> Put(DogCatcherEdit dogCatcher, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldDcData = await ctx.DogCatchers.FindAsync(id);
                if (oldDcData is null)
                {
                    return false;
                }
                oldDcData.EmployeeBadgeID = dogCatcher.EmployeeBadgeID;
                oldDcData.FirstName = dogCatcher.FirstName;
                oldDcData.LastName = dogCatcher.LastName;
                oldDcData.PositionID = dogCatcher.PositionID;

                return await ctx.SaveChangesAsync() > 0;
            }
        }
        public async Task<bool> Delete(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldDcData = await ctx.DogCatchers.FindAsync(id);
                if (oldDcData is null)
                {
                    return false;
                }

                ctx.DogCatchers.Remove(oldDcData);
                return await ctx.SaveChangesAsync() > 0;
            }
        }
    }
}
