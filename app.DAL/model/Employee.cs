using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.DAL.model
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int? Age { get; set; }
        
        public string Adress { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        public string Email { get; set; }
       
        public string PhoneNumbers { get; set; }
        public DateTime HirDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; } 
        public Department Department { get; set; }
    }
}
