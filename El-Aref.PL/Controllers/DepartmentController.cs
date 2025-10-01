using El_Aref.BLL.Interfaces;
using El_Aref.DAL.Model;
using El_Aref.PL.DTO;
using EL_Areff.Comapny.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace El_Aref.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _department;

        public DepartmentController( IDepartmentRepository department)
        {
            _department = department;
        }
        public IActionResult Index()
        {
            var departments = _department.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CraeteAt = model.CraeteAt,
                };
                var result = _department.Add(department);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");
            var result = _department.Get(id.Value);
            if (result is null) return NotFound(new { StatusCode = 400, Message = $"department with id {id} id not found" });
            return View(viewname, result);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest("invalid Id");
            var result = _department.Get(id.Value);
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
        public IActionResult Update([FromRoute] int id, Department model)
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
                var result = _department.Update(department);
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
            //if (id is null) return BadRequest("Invaid Id");
            //var department = _departmentRepository.Get(id.Value);
            //if(department is null ) return NotFound(new { StatusCode = 400, Message = $"department with id {id} id not found" });
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department model)
        {
            if (id != model.Id) return BadRequest();
            var result = _department.Delete(model);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
