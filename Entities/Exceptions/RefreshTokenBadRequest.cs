namespace Entities.Exceptions;

public sealed class RefreshTokenBadRequest : BadRequestException
{
    public RefreshTokenBadRequest() : base("Неверный запрос клиента. TokenDto имеет недопустимые значения.")
    {
    }
}
