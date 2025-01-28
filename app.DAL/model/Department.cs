using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.DAL.model
{
	public class Department
	{
        public int Id { get; set; }
		[Required (ErrorMessage ="Name is requird ")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Code is requird ")]

		public string Code { get; set; }	
		public DateTime DateOfCreation { get; set; }


    }
}
