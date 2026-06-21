using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specifications.EmployeeSpecification;

namespace Talabat.API.Controllers
{

    public class EmployeeController : BaseController
    {
        private readonly IGenaricRepository<Employee> _employeeRepository;

        public EmployeeController(IGenaricRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }





        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllAsync()
        {
            var spec = new EmployeeWithDepartmentWithSpecification();
            var employees = await _employeeRepository.GettAllWithSpecAsync(spec);
            return Ok(employees);
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {

            var spec = new EmployeeWithDepartmentWithSpecification(id);
            var employee = await _employeeRepository.GetByIdWithSpecAsync(spec);
            if (employee is null) return NotFound(new { msg = "Not Found Employee" });
            return Ok(employee);
        }



    }
}
