using System.Linq;
using Company.Services.DepartmentManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Company.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService departmentService;

        public DepartmentsController(IDepartmentService service)
        {
            this.departmentService = service;
        }


        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult OnGetAll()
        {
            var result = this.departmentService.GetAll().ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult OnGet(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var result = this.departmentService.Get(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult OnUpdate(int id, UpdateDepartmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = this.departmentService.Update(id, request);
            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult OnCreate(UpdateDepartmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = this.departmentService.Add(request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult OnDelete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = this.departmentService.Delete(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("freestaff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult OnGetFreeEmployees()
        {
            var employees = this.departmentService.GetFreeEmployees().ToList();

            return Ok(employees);
        }

        [HttpGet]
        [Route("{departmentId}/assignemployee/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult OnAssignEmployeeToDepartment(int departmentId, int employeeId)
        {
            if (departmentId <= 0 || employeeId <= 0)
            {
                return NotFound();
            }

            var result = this.departmentService.AssignEmployeeToDepartment(employeeId, departmentId);

            return Ok(result);
        }

        [HttpGet]
        [Route("unassignemployee/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult OnUnaasignEmployeeFromDepartment(int employeeId)
        {
            if ( employeeId <= 0)
            {
                return NotFound();
            }

            this.departmentService.UnassignEmployeeFromDepartment(employeeId);

            return Ok();
        }

        [HttpGet]
        [Route("{departmentId}/employees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult OnGetDepartmentEmployee(int departmentId)
        {
            if (departmentId <= 0)
            {
                return NotFound();
            }

            var result = this.departmentService.GetDepartmentEmployees(departmentId).ToList();

            return Ok(result);
        }
    }
}
