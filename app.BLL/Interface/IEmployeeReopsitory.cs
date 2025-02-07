using app.DAL.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.BLL.Interface
{
    public interface IEmployeeReopsitory : IGenricRepository<Employee>
    {
        IQueryable<Employee> GetEmpolyeeByAdress(string address);
        IQueryable<Employee> GetEmpolyeeByName (string name);


    }
}
