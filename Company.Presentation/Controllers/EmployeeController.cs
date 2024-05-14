
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Company.Presentation.Controllers;

[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public EmployeeController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet]
    public IActionResult GetEmployeesForCompany(Guid companyId)
    {
        var employees = _serviceManager.EmployeeService.GetEmployees(companyId, trackChanges: false);
        return Ok(employees);
    }

    [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
    public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
    {
        var employee = _serviceManager.EmployeeService.GetEmployee(companyId, id, trackChanges: false);
        return Ok(employee);
    }

    [HttpPost]
    public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
    {
        if (employee is null)
            return BadRequest("Объект EmployeeForCreationDto равен null");

        var employeeToReturn = _serviceManager.EmployeeService.CreateEmployeeForCompany(companyId, employee, trackChanges: false);

        return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id)
    {
        _serviceManager.EmployeeService.DeleteEmployeeForCompany(companyId, id, trackChanges: false);
        return NoContent();
    }
}
