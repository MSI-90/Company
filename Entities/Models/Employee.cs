using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Employee
{
    [Column("EmployeeId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Поле Имя является обязательным.")]
    [MaxLength(30, ErrorMessage = "Поле Имя может содержать не более 30 символов.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Поле Возраст является обязательным полем.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Поле Позиция является обязательным полем.")]
    [MaxLength(20, ErrorMessage = "Поле Позиция может содержать не более 20 символов.")]
    public string? Position { get; set; }


    //[ForeignKey(nameof(Company))]
    public Guid CompanyId { get; set; }
    public Compani? Company { get; set; }
}