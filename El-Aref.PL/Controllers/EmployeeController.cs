using El_Aref.BLL.Interfaces;
using El_Aref.DAL.Model;
using El_Aref.PL.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using El_Aref.PL.Helper;
using System.Threading.Tasks;


namespace El_Aref.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        // private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
                                    //IEmployeeRepository employeeRepository,
                                    //IDepartmentRepository departmentRepository,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }

            [HttpGet]
            public async Task<IActionResult> Index(string? SearchInput)
            {
                IEnumerable<Employee> employees;
                if (string.IsNullOrEmpty(SearchInput))
                {
                    employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
                
                }
                else
                {
                    employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
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
            public async Task<IActionResult> Create()
            {
                var deprtments = await _unitOfWork.DepartmentRepository.GetAllAsync();
                ViewData["deprtments"] = deprtments;
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Create(CreateEmployeeTDO model)
            {
                if (ModelState.IsValid)
                {
                    if (model.Image is not null)
                    {
                        model.ImageName = DocumentSettings.UpLoadFile(model.Image, "images");
                    }

                    var employee = _mapper.Map<Employee>(model);
                    await _unitOfWork.EmployeeRepository.AddAsync(employee);

                     var count =await _unitOfWork.ComleteAsync();
                     if (count > 0)
                     {
                         
                        TempData["Message"] = "Employee Added Successfully";
                                 return RedirectToAction(nameof(Index));
                     }
                         
                     }
                    return View(model);
                }
            [HttpGet]
            public async Task<IActionResult> Details(int? id, string viewname = "Details")
            {
                if (id is null) return BadRequest("Invalid Id");
                var Emp =await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
                if (Emp is null) return NotFound("Not Found");
                return View(viewname, Emp);

            }
            

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var emp =await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            var department =await _unitOfWork.DepartmentRepository.GetAllAsync();
            if (emp is null) return NotFound("Not Found");

           

            // ✅ أضف هذه السطور
            var employeeDTO = _mapper.Map<CreateEmployeeTDO>(emp);
            ViewData["deprtments"] = department;

            return View(employeeDTO);
        }

        [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Update([FromRoute] int id, CreateEmployeeTDO model)
            {
                if (ModelState.IsValid)
                {

                    if (model.ImageName is not null && model.Image is not null)
                    {
                        DocumentSettings.DeleteFile(model.ImageName, "images");
                    }
                    if (model.Image is not null)
                    {
                        model.ImageName = DocumentSettings.UpLoadFile(model.Image, "images");
                    }

                    var employee = _mapper.Map<Employee>(model);
                    _unitOfWork.EmployeeRepository.Update(employee);
                    var result =await _unitOfWork.ComleteAsync();
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(model);
            }

            [HttpGet]
            public async Task<IActionResult> Delete(int? id)
            {

                return await Details(id, "Delete");
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Delete([FromRoute] int id, Employee model)
            {
                
                

                if (id != model.Id) return BadRequest();
                _unitOfWork.EmployeeRepository.Delete(model);
                var result = await _unitOfWork.ComleteAsync();

                if (result > 0)
                {

                    if(model.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(model.ImageName, "images");
                }

                return RedirectToAction(nameof(Index));
                }
                return View(model);
            }

        }
    }
