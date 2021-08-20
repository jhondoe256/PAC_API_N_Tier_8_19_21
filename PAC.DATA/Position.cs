using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAC.DATA
{
    public class Position
    {
        public Guid OwnerID{ get; set; }
        [Key]
        public int ID { get; set; }

        public string PositionName { get; set; }

        [ForeignKey(nameof(Department))]
        public int DepartmentID { get; set; }
        public Department Department { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}