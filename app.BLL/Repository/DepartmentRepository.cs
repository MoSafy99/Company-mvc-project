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
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly CompanyContext _context;
        public DepartmentRepository(CompanyContext context)
        {
            _context = context;
        }
        public int Add(Department department)
		{
			_context.Add(department);
			return _context.SaveChanges();
		}

		public int Delete(Department department)
		{
			_context.Remove(department);
			return _context.SaveChanges();
		}

		public Department Get(int id)
			=> _context.Departments.Find(id);
		

		public IEnumerable<Department> GetAll()
		=> _context.Departments.ToList();
		

		public int Update(Department department)
		{
			_context.Update(department);
			return _context.SaveChanges();
		}
	}
}
