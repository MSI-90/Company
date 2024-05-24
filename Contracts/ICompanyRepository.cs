using Entities.Models;

namespace Contracts;

public interface ICompanyRepository
{
    Task<IEnumerable<Compani>> GetAllCompaniesAsync(bool trackChanges);
    Task<Compani?> GetCompanyAsync(Guid companyId, bool trackChanges);
    void CreateCompany(Compani company);
    Task<IEnumerable<Compani>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    void DeleteCompany(Compani company);
}
