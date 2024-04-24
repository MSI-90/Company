using Contracts;
using Entities.Models;
using Service.Contracts;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
            try
            {
                var companies = _repositoryManager.Company.GetAllCompanies(trackChanges);
                return companies;
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Что-то пошло не так в {nameof(GetAllCompanies)} методе сервиса {ex}");
                throw;
            }
        }
    }
}
