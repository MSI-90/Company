using Contracts;
using Entities.Models;

namespace Repository;

public class CompanyRepository : RepositoryBase<Compani>, ICompanyRepository
{

    public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateCompany(Compani company) => Create(company);

    public IEnumerable<Compani> GetAllCompanies(bool trackChanges) =>
        FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();

    public IEnumerable<Compani> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
        FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();

    public Compani? GetCompany(Guid companyId, bool trackChanges) => FindByCondition(c => c.Id.Equals(companyId), trackChanges)
        .SingleOrDefault();
}
