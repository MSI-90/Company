using Entities.Models;

namespace Contracts;

public interface ICompanyRepository
{
    IEnumerable<Compani> GetAllCompanies(bool trackChanges);
    Compani? GetCompany(Guid companyId, bool trackChanges);
    void CreateCompany(Compani company);
    IEnumerable<Compani> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
}
