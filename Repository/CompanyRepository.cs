using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class CompanyRepository : RepositoryBase<Compani>, ICompanyRepository
{
    public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateCompany(Compani company) => Create(company);

    public async Task<IEnumerable<Compani>> GetAllCompaniesAsync(bool trackChanges) => 
        await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();

    public async Task<IEnumerable<Compani>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
        .ToListAsync();

    public async Task<Compani?> GetCompanyAsync(Guid companyId, bool trackChanges) => 
        await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
        .SingleOrDefaultAsync();

    public void DeleteCompany(Compani company) => Delete(company);
}
