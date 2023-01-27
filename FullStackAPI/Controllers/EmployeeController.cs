using FullStackAPI.Data;
using FullStackAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly FullStackDBContext _fullStackDBContext;
        public EmployeeController(FullStackDBContext fullStackDBContext)
        {
            _fullStackDBContext = fullStackDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
           var employees=  await _fullStackDBContext.Employees.ToListAsync();
            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employees employee)
        {
            employee.Id=Guid.NewGuid();
            await _fullStackDBContext.Employees.AddAsync(employee);
            await _fullStackDBContext.SaveChangesAsync();
            return Ok(employee);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute]Guid id)
        {
            var employee=  await _fullStackDBContext.Employees.FirstOrDefaultAsync(x=> x.Id==id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id,Employees updateEmployeeRequest)
        {
            var employee = await _fullStackDBContext.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;

            await _fullStackDBContext.SaveChangesAsync();

            return Ok(employee);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDBContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _fullStackDBContext.Employees.Remove(employee);
            await _fullStackDBContext.SaveChangesAsync();
            return Ok(employee);
        }
    }
}
