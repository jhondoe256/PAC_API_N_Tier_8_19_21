using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.DATA
{
    public class Department
    {
        public Guid OwnerID { get; set; }

        [Required]
        public int ID { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        public List<Position> Positions { get; set; } = new List<Position>();
        public List<Employee> Employees { get; set; } = new List<Employee>();

    }
}
