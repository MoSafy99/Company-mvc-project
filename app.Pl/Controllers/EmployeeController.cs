using app.BLL.Interface;
using app.BLL.Repository;
using app.DAL.model;
using Microsoft.AspNetCore.Mvc;

namespace app.Pl.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeReopsitory _employeeReopsitory;
        public EmployeeController(IEmployeeReopsitory employeeReopsitory)
        {
            _employeeReopsitory = employeeReopsitory;
            
        }
        public IActionResult Index()
        {
            var employess = _employeeReopsitory.GetAll();
            return View(employess);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee )
        {
            if(ModelState.IsValid)
            {
                _employeeReopsitory.Add(employee);
                return RedirectToAction(nameof(Index));

            }
            return View(employee);

        }
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }
            var department = _employeeReopsitory.Get(id.Value);
            if (department is null)
            {
                return NotFound();
            }
            return View(ViewName, department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeReopsitory.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {

            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeReopsitory.Delete(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(employee);
        }
    }
}
