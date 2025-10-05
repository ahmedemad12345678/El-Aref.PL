using El_Aref.BLL.Interfaces;
using El_Aref.DAL.Model;
using El_Aref.PL.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using AutoMapper;


namespace El_Aref.PL.Controllers
{
    public class EmployeeController : Controller
    {
       private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository, 
                                  IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

            [HttpGet]
            public IActionResult Index(string? SearchInput)
            {
                IEnumerable<Employee> employees;
                if (string.IsNullOrEmpty(SearchInput))
                {
                    employees = _employeeRepository.GetAll();
                
                }
                else
                {
                    employees = _employeeRepository.GetByName (SearchInput);
                }
                // Dictionary :
                // 1.ViewData : Transfer Exrta Information From Controller (Action) To View 
                // 2.ViewBag  : Transfer Exrta Information From Controller (Action) To View 
                // 3.TempData : Transfer Exrta Information From One Request To Another Request

                // 1.ViewData //ViewData["Message"] = "Welcome To Employee Page => ViewData";
                // 2.ViewBag //ViewBag.Message = "Welcome To Employee Page => ViewBag";



                return View(employees);
            }
            [HttpGet]
            public IActionResult Create()
            {
                var deprtments = _departmentRepository.GetAll();
                ViewData["deprtments"] = deprtments;
                return View();
            }
            [HttpPost]
            public IActionResult Create(CreateEmployeeTDO model)
            {
                if (ModelState.IsValid)
                {
                    //var employee = new Employee()
                    //{
                    //    Name = model.Name,
                    //    Age = model.Age,
                    //    Email = model.Email,
                    //    Address = model.Address,
                    //    CreateAt = model.CreateAt,
                    //    HiringDate = model.HiringDate,
                    //    IsActive = model.IsActive,
                    //    IsDeleted = model.IsDeleted,
                    //    Phone = model.Phone,
                    //    Salary = model.Salary,
                        
                        
                    //};
                    var employee= _mapper.Map<Employee>(model);


                var count = _employeeRepository.Add(employee);
                    if (count > 0)
                    {
                            TempData["Message"] = "Employee Added Successfully";
                            return RedirectToAction(nameof(Index));
                    }
                    
                }
                return View(model);
            }
            [HttpGet]
            public IActionResult Details(int? id, string viewname = "Details")
            {
                if (id is null) return BadRequest("Invalid Id");
                var Emp = _employeeRepository.Get(id.Value);
                if (Emp is null) return NotFound("Not Found");
                return View(viewname, Emp);

            }
            

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var emp = _employeeRepository.Get(id.Value);
            if (emp is null) return NotFound("Not Found");

            var employeeDTO = new CreateEmployeeTDO()
            {
                Name = emp.Name,
                Age = emp.Age,
                Email = emp.Email,
                Address = emp.Address,
                CreateAt = emp.CreateAt,
                HiringDate = emp.HiringDate,
                IsActive = emp.IsActive,
                IsDeleted = emp.IsDeleted,
                Phone = emp.Phone,
                Salary = emp.Salary,
                DepartmentId = emp.DepartmentId
            };

            // ✅ أضف هذه السطور
            var departments = _departmentRepository.GetAll();
            ViewData["deprtments"] = departments;

            return View(employeeDTO);
        }

        [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Update([FromRoute] int id, CreateEmployeeTDO model)
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employee()
                    {
                        Id = id,
                        Name = model.Name,
                        Age = model.Age,
                        Email = model.Email,
                        Address = model.Address,
                        CreateAt = model.CreateAt,
                        HiringDate = model.HiringDate,
                        IsActive = model.IsActive,
                        IsDeleted = model.IsDeleted,
                        Phone = model.Phone,
                        Salary = model.Salary
                    };
                    var result = _employeeRepository.Update(employee);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(model);
            }

            [HttpGet]
            public IActionResult Delete(int? id)
            {

                return Details(id, "Delete");
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Delete([FromRoute] int id, Employee model)
            {
                if (id != model.Id) return BadRequest();
                var result = _employeeRepository.Delete(model);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }

        }
    }
