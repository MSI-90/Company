namespace Entities.Exceptions;

public sealed class CompanyCollectionBadRequest : BadRequestException
{
    public CompanyCollectionBadRequest() : base("Коллекция компаний, отправленная от клиента, равна null.") 
    {
    }
}
