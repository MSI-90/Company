using Contracts;
using Entities.Models;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Compani>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Compani> GetAllCompanies(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();

        public Compani GetCompany(Guid companyId, bool trackChanges) => FindByConditoin(c => c.Id.Equals(companyId), trackChanges)
            .SingleOrDefault();
    }
}
