using AutoMapper;
using El_Aref.BLL.Interfaces;
using El_Aref.DAL.Model;
using El_Aref.PL.DTO;
using EL_Areff.Comapny.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace El_Aref.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _department;

        public DepartmentController( /*IDepartmentRepository department*/
                                     IUnitOfWork unitOfWork,
                                     IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_department = department;
        }
        public async Task<IActionResult> Index(string? SearchInput)
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            if (string.IsNullOrEmpty(SearchInput))
            {
                departments =await _unitOfWork.DepartmentRepository.GetAllAsync();
                return View(departments);
            }
            else
            {
                departments = _unitOfWork.DepartmentRepository.GetByName(SearchInput);
                return View(departments);
            }
            

            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CraeteAt = model.CraeteAt,
                    
                };
                await _unitOfWork.DepartmentRepository.AddAsync(department);
                
                var result =await _unitOfWork.ComleteAsync();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewname = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");
            var result =await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (result is null) return NotFound(new { StatusCode = 400, Message = $"department with id {id} id not found" });
            return View(viewname, result);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest("invalid Id");
            var result =await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (result is null) return NotFound(new { StatusCode = 400, Message = $"department with id {id} id not found" });

            var department = new CreateDepartmentDto()
            {
                Code = result.Code,
                Name = result.Name,
                CraeteAt = result.CraeteAt,
            };
            


            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int id, CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                //if (id != model.Id) return BadRequest();
                var department = new Department()
                {
                    Id = id,
                    Code = model.Code,
                    Name = model.Name,
                    CraeteAt = model.CraeteAt,
                };
                _unitOfWork.DepartmentRepository.Update(department);
                var result = await _unitOfWork.ComleteAsync();
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
            //if (id is null) return BadRequest("Invaid Id");
            //var department = _departmentRepository.Get(id.Value);
            //if(department is null ) return NotFound(new { StatusCode = 400, Message = $"department with id {id} id not found" });
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, Department model)
        {
            if (id != model.Id) return BadRequest();
            _unitOfWork.DepartmentRepository.Delete(model);
            var result =await _unitOfWork.ComleteAsync();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
