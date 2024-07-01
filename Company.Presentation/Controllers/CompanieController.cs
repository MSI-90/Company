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
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetCompanies()
    {
        var companies = await _serviceManager.CompanyService.GetAllCompaniesAsync(trackChanges: false);
        return Ok(companies);
    }

    /// <summary>
    /// Получает компанию по её идентификтору 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Определённая компания по id</returns>
    /// <response code="200">Возвращает объект</response>
    /// <response code="404">Объект не найден</response>
    [HttpGet("{id:guid}", Name = "CompanyById")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [OutputCache(Duration = 60)]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        var company = await _serviceManager.CompanyService.GetCompanyAsync(id, trackChanges: false);

        var etag = $"\"{Guid.NewGuid():n}\"";
        HttpContext.Response.Headers.ETag = etag ;

        return Ok(company);
    }

    /// <summary>
    /// Получает набор коллекций по указаным идентификаторам
    /// </summary>
    /// <param name="ids"></param>
    /// <returns>Список компаний</returns>
    /// <response code="200">Возвращает список компаний по списку идентификаторов</response>
    /// <response code="400">Если идентификатор коллекции или коллекций не найден в БД</response>
    [HttpGet("collection/", Name = "CompanyCollection")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
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

    /// <summary>
    /// Создаёт несколько компаний, с возможностью включить в себя сотрудников
    /// </summary>
    /// <param name="companyCollection"></param>
    /// <returns>Список компаний с сотрудниками</returns>
    /// <response code="201">Возвращает список созданных компаний</response>
    /// <response code="400">Если объект для создания равен NULL</response>
    /// <response code="422">Если модель недействительна</response>
    [HttpPost("Collection")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(422)]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
    {
        var result = await _serviceManager.CompanyService.CreateCompanyCollectionAsync(companyCollection);

        return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
    }

    /// <summary>
    /// Удаляет указаную по идентификатору компанию
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Удаляет компанию</returns>
    /// <response code="204"></response>
    /// <response code="404"></response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        await _serviceManager.CompanyService.DeleteCompanyAsync(id, trackChanges: false);
        return NoContent();
    }

    /// <summary>
    /// Обновляет указаную по идентификатору компанию
    /// </summary>
    /// <param name="id"></param>
    /// <param name="company"></param>
    /// <returns>Обновляет данные о компании</returns>
    /// <response code="204"></response>
    /// <response code="404"></response>
    /// <response code="422"></response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(422)]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
    {
        await _serviceManager.CompanyService.UpdateCompanyAsync(id, company, trackChanges: true);

        return NoContent();
    }
}
