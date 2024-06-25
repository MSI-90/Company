using Company.Presentation.ActionFilters;
using Company.Presentation.ModelBinders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Company.Presentation.Controllers;


[Route("api/companies")]
[ApiController]
//[ResponseCache(CacheProfileName = "120secondsDuration")]
[OutputCache(PolicyName = "120secondsDuration")]
[ApiExplorerSettings(GroupName = "v1")]
public class CompaniesController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public CompaniesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    /// <summary>
    /// Получает список всех компаний
    /// </summary>
    /// <returns>Список компаний</returns>
    [HttpGet(Name = "GetCompanies")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetCompanies()
    {
        var companies = await _serviceManager.CompanyService.GetAllCompaniesAsync(trackChanges: false);
        return Ok(companies);
    }

    [HttpGet("{id:guid}", Name = "CompanyById")]
    [OutputCache(Duration = 60)]
    //[ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        var company = await _serviceManager.CompanyService.GetCompanyAsync(id, trackChanges: false);

        var etag = $"\"{Guid.NewGuid():n}\"";
        HttpContext.Response.Headers.ETag = etag ;

        return Ok(company);
    }

    [HttpGet("collection/({ids})", Name = "CompanyCollection")]
    public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        var companies = await _serviceManager.CompanyService.GetByIdsAsync(ids, trackChanges: false);
        return Ok(companies);
    }

    /// <summary>
    /// Создаёт новую компанию
    /// </summary>
    /// <param name="company"></param>
    /// <returns>Вновь созданная компания</returns>
    /// <response code="201">Возвращает только что созданный элемент</response>
    /// <response code="400">Если объект равен NULL</response>
    /// <response code="422">Если модель недействительна</response>
    [HttpPost(Name = "CreateCompany")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(422)]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
    {
        var createdCompany = await _serviceManager.CompanyService.CreateCompanyAsync(company);

        return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
    }

    [HttpPost("collection")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
    {
        var result = await _serviceManager.CompanyService.CreateCompanyCollectionAsync(companyCollection);

        return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        await _serviceManager.CompanyService.DeleteCompanyAsync(id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
    {
        await _serviceManager.CompanyService.UpdateCompanyAsync(id, company, trackChanges: true);

        return NoContent();
    }
}
