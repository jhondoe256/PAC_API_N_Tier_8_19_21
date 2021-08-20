using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.DATA
{
    public class Employee
    {
        public Guid OwnerId { get; set; }

        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey(nameof(Position))]
        public int PositionID { get; set; }
        public Position Position { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate{ get; set; }


    }
}
