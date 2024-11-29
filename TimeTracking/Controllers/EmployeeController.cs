using Mapster;
using Microsoft.AspNetCore.Mvc;
using TimeTracking.Models;


namespace TimeTracking.Controllers;

[Route("api/employee")]
[ApiController]
public class EmployeeController(TimeTrackingDbContext dbContext): ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<Resources.Employee>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var response = dbContext.Employees.ProjectToType<Resources.Employee>().AsEnumerable();
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<Resources.Employee>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var dbEmployee = await dbContext.Employees.FindAsync(id);
        if (dbEmployee == null)
        {
            return NotFound();
        }
        return Ok(dbEmployee.Adapt<Resources.Employee>());
    }
    
}