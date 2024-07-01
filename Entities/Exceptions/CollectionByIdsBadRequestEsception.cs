namespace Entities.Exceptions;

public sealed class CollectionByIdsBadRequestEsception : BadRequestException
{
    public CollectionByIdsBadRequestEsception() : base("Несоответствие количества коллекций при сравнении с идентификаторами") 
    {
    }
}
