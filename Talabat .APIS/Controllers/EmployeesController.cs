using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Core.Specifications.EmployeeSpec;
using Talabat_.APIS.Errors;

namespace Talabat_.APIS.Controllers
{
    public class EmployeesController : BaseAPIController
    {
        private readonly IGenericRepository<Employee> _genericRepository;

        public EmployeesController(IGenericRepository<Employee> genericRepository) : base()
        {
            _genericRepository = genericRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            var spec = new EmployeeWithDepartmentSpec();
            var employees = await _genericRepository.GetAllWithSpecAsync(spec);
            return Ok(employees);
        }

        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]

        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var spec = new EmployeeWithDepartmentSpec(id);

            var employees = await _genericRepository.GetWithSpecAsync(spec);

            if (employees is null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(employees);
        }

    }
}
