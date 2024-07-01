using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record EmployeeForManipulationDto
{
    [Required(ErrorMessage = "Имя сотрудника - обязательное поле.")]
    [MaxLength(30, ErrorMessage = "Максимальная длина поля Имя - 30 символов.")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Возраст - обязательное поле.")]
    [Range(18, int.MaxValue, ErrorMessage = "Возраст обязателен и он не может быть меньше 18.")]
    public int Age { get; init; }

    [Required(ErrorMessage = "Позиция - обязательное поле.")]
    [MaxLength(20, ErrorMessage = "Максимальная длина поля Позиция - 20 символов")]
    public string? Position { get; init; }
}
