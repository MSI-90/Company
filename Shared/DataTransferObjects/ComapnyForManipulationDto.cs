using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record ComapnyForManipulationDto
{
    [Required(ErrorMessage = "Название компании является обязательным полем.")]
    [MaxLength(60, ErrorMessage = "Поле Название может содержать не более 60 символов")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Адресс компании является обязательным полем")]
    [MaxLength(60, ErrorMessage = "Поле Адресс может содержать не более 60 символов")]
    public string? Address { get; init; }

    [Required(ErrorMessage = "Страна является обязательным полем")]
    public string? Country { get; init; }
    public IEnumerable<EmployeeForCreationDto>? Employees { get; init; }
}
