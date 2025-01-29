using app.BLL.Interface;
using app.DAL.Context;
using app.DAL.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.BLL.Repository
{
	public class DepartmentRepository :GenricRepository<Department>, IDepartmentRepository
	{
        public DepartmentRepository(CompanyContext context):base(context)
        {
            
        }


    }
}
