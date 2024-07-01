using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record UserForAuthenticationDto
{
    [Required(ErrorMessage = "Имя пользователя обязательно")]
    public string UserName {  get; init; } = string.Empty;

    [Required(ErrorMessage = "Пароль обязателен")]
    public string Password { get; init; } = string.Empty;
}
