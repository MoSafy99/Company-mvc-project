using app.BLL.Interface;
using app.BLL.Repository;
using app.DAL.model;
using app.Pl.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace app.Pl.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeReopsitory _employeeReopsitory;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeReopsitory employeeReopsitory ,
            IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _employeeReopsitory = employeeReopsitory;
            _departmentRepository = departmentRepository;
           _mapper = mapper;
        }
        public IActionResult Index()
        {
            var employess = _employeeReopsitory.GetAll();
            var MapedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employess);

            return View(MapedEmp);
        }
        [HttpGet]
        public IActionResult Create()
        {
          ViewBag.Departments= _departmentRepository.GetAll();
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM )
        {
            if(ModelState.IsValid)
            {
                ///manual mapping 
                ///var mapedEmployee = new Employee()
                ///{
                ///    Name = employeeVM.Name,
                ///    Age = employeeVM.Age,
                ///    PhoneNumbers = employeeVM.PhoneNumbers
                ///};

                var mapedEmp = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                _employeeReopsitory.Add(mapedEmp);
                return RedirectToAction(nameof(Index));

            }
            return View(employeeVM);

        }
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }
            var employee = _employeeReopsitory.Get(id.Value);
            if (employee is null)
                return NotFound();
                var mapedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);
            
            return View(ViewName, mapedEmp);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Departments = _departmentRepository.GetAll();


            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mapedEmp =_mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                    _employeeReopsitory.Update(mapedEmp);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(employeeVM);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {

            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, EmployeeViewModel employeeVm)
        {
            if (id != employeeVm.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mapedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                    _employeeReopsitory.Delete(mapedEmp);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(employeeVm);
        }
    }
}
