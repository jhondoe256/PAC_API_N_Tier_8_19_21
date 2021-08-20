using PAC.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Models.DogCatcherModels
{
    public class DogCatcherDetail
    {
        public Guid OwnerId { get; set; }
       
        public int ID { get; set; }
        public string EmployeeBadgeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PositionID { get; set; }
        public Position Position { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
