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

        //private readonly IEmployeeReopsitory _employeeReopsitory;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_employeeReopsitory = employeeReopsitory;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(string SearchName)
        {
            IEnumerable<Employee> employess;
            if (string.IsNullOrEmpty(SearchName)) 
            {
                 employess = _unitOfWork.employeeReopsitory.GetAll();
                
            }
            else
            {
               employess=  _unitOfWork.employeeReopsitory.GetEmpolyeeByName(SearchName);
            }

                var MapedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employess);
                return View(MapedEmp);
        }
        [HttpGet]
        public IActionResult Create()
        {
          ViewBag.Departments= _unitOfWork.departmentRepository.GetAll();
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
                _unitOfWork.employeeReopsitory.Add(mapedEmp);
                _unitOfWork.Complete();
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
            var employee = _unitOfWork.employeeReopsitory.Get(id.Value);
            if (employee is null)
                return NotFound();
                var mapedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);
            
            return View(ViewName, mapedEmp);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Departments = _unitOfWork.departmentRepository.GetAll();


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
                    _unitOfWork.employeeReopsitory.Update(mapedEmp);
                    _unitOfWork.Complete();
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
                    _unitOfWork.employeeReopsitory.Delete(mapedEmp);
                    _unitOfWork.Complete();
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
