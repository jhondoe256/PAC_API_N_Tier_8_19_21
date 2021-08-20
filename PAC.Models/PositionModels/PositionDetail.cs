using PAC.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Models.PositionModels
{
    public class PositionDetail
    {
        public int ID { get; set; }
        public string PositionName { get; set; }
        public int DepartmentID { get; set; }
        public Department Department { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
