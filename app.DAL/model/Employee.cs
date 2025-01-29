using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.DAL.model
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Name is Required")]
        [MaxLength(50, ErrorMessage = "Max length is 50 chars ")]
        [MinLength(5, ErrorMessage = "Max length is 5 chars ")]

        public string Name { get; set; }
        [Range(22,35,ErrorMessage ="Age must be between 20 to 35")]
        public int? Age { get; set; }
        //[RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}[a-zA-Z]{4,10}[a-zA-Z]{5,10}$",
            //ErrorMessage ="Adress must be like 123-street-city-country")]
        public string Adress { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumbers { get; set; }
        public DateTime HirDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;










    }
}
