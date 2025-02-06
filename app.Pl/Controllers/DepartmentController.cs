using app.BLL.Interface;
using app.DAL.model;
using app.Pl.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace app.Pl.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository,IMapper mapper )
        {
			_departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
		{
			var Departments = _departmentRepository.GetAll();
			var MapedDepartment = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(Departments);
			return View(MapedDepartment);
		}
		[HttpGet]
		public IActionResult Create() 
		{ 
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
			if(ModelState.IsValid) // server side validation 
			{
				var MapedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);	

                _departmentRepository.Add(MapedDepartment);
				return RedirectToAction(nameof(Index));
			}
            return View(departmentVM);
        }
		public IActionResult Details (int? id , string ViewName= "Details")
		{
			if (id is null)
			{
				return BadRequest();
			}
			var department = _departmentRepository.Get(id.Value);
			if (department is null)
				return NotFound();
            var MapedDepartment = _mapper.Map<Department, DepartmentViewModel>(department);


            return View( ViewName, MapedDepartment);
		}
		[HttpGet]
		public IActionResult Edit(int? id)
		{
			///if (id is null)
			///	return BadRequest();
			///var department = _departmentRepository.Get(id.Value);
			///if (department is null)
			///	return NotFound();
			///return View(department);
			return Details(id, "Edit");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit ([FromRoute]int id, DepartmentViewModel departmentVM)
		{
			if (id != departmentVM.Id)
				return BadRequest();
			if (ModelState.IsValid)
			{
				try 
				{
                    var MapedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                    _departmentRepository.Update(MapedDepartment);
                    return RedirectToAction(nameof(Index));
                }
				catch (Exception ex)
				{
					ModelState.AddModelError(string .Empty, ex.Message);
				}
				
			}
			return View(departmentVM);
		}
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete ([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MapedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                    _departmentRepository.Delete(MapedDepartment);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(departmentVM);
        }
    }
}
