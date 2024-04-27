using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
        public CompanyDto GetCompany(Guid id, bool trackChanges);
    }
}
