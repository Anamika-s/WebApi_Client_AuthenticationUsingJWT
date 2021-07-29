
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
   
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Batch { get; set; }
        public int Marks { get; set; }

    }
}
