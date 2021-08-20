using PAC.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Models.DepartmentModels
{
    public class DepartmentDetail
    {
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<Position> Positions { get; set; } = new List<Position>();
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
