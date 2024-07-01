
namespace Entities.Exceptions;

public sealed class CompanyNotFoundException : NotFoundException
{
    public CompanyNotFoundException(Guid companyId) : base($"Компания с id: {companyId} не существует в базе данных")
    {
    }
}
