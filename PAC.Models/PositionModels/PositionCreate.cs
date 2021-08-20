using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Models.PositionModels
{
    public class PositionCreate
    {
        public int DepartmentID { get; set; }
        [Required]
        public string PositionName { get; set; }
       
    }
}
