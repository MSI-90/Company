namespace Entities.Exceptions;

public sealed class MaxAgeRangeBadRequestException : BadRequestException
{
    public MaxAgeRangeBadRequestException() : base ("Максимальный возраст не может быть меньше минимального.")
    {
    }
}
