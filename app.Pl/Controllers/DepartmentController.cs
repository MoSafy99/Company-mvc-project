using app.BLL.Interface;
using app.DAL.model;
using Microsoft.AspNetCore.Mvc;

namespace app.Pl.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IDepartmentRepository _departmentRepository;

		public DepartmentController(IDepartmentRepository departmentRepository )
        {
			_departmentRepository = departmentRepository;
		}
        public IActionResult Index()
		{
			var Departments = _departmentRepository.GetAll();
			return View(Departments);
		}
		[HttpGet]
		public IActionResult Create() 
		{ 
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
			if(ModelState.IsValid) // server side validation 
			{
				_departmentRepository.Add(department);
				return RedirectToAction(nameof(Index));
			}
            return View(department);
        }
		public IActionResult Details (int? id , string ViewName= "Details")
		{
			if (id is null)
			{
				return BadRequest();
			}
			var department = _departmentRepository.Get(id.Value);
			if (department is null)
			{
				return NotFound();
			}
			return View( ViewName, department);
		}
		[HttpGet]
		public IActionResult Edit(int? id)
		{
			//if (id is null)
			//	return BadRequest();
			//var department = _departmentRepository.Get(id.Value);
			//if (department is null)
			//	return NotFound();
			//return View(department);
			return Details(id, "Edit");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit ([FromRoute]int id,Department department)
		{
			if (id != department.Id)
				return BadRequest();
			if (ModelState.IsValid)
			{
				try 
				{
                    _departmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
				catch (Exception ex)
				{
					ModelState.AddModelError(string .Empty, ex.Message);
				}
				
			}
			return View(department);
		}
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete ([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentRepository.Delete(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(department);
        }
    }
}
