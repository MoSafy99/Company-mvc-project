using app.DAL.model;
using Microsoft.Build.Framework;

namespace app.Pl.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "Name is requird")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Code is requird ")]

        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
