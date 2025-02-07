using app.BLL.Interface;
using app.DAL.Context;
using app.DAL.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.BLL.Repository
{
    public class GenricRepository<T> : IGenricRepository<T> where T : class
    {
        private readonly CompanyContext _context;

        public GenricRepository(CompanyContext context)
        {
            _context = context;
        }
        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        public void Delete(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public T Get(int id)
       => _context.Set<T>().Find(id);
        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) _context.Employees.Include(E => E.Department).ToList();
            }else
              return _context.Set<T>().ToList();
        }
        

        public void Update(T item)
        {
            _context.Set<T>().Update(item);
        }
    }
}
