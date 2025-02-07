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
    public class EmployeeReopsitory : GenricRepository<Employee>, IEmployeeReopsitory
    {
        private readonly CompanyContext _context;

        public EmployeeReopsitory(CompanyContext context):base(context)
        {
            _context = context;
        }
        public IQueryable<Employee> GetEmpolyeeByAdress(string address)
        
          =>  _context.Employees.Where(E => E.Adress == address);

        public IQueryable<Employee> GetEmpolyeeByName(string name)
        
            => _context.Employees.Where(E=>E.Name.ToLower().Contains(name.ToLower()));
        
    }
}
