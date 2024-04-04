using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Название компании является обязательным полем.")]
        [MaxLength(60, ErrorMessage = "Поле Название может содержать не более 60 символов")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Адресс компании является обязателньым полем")]
        [MaxLength(60, ErrorMessage = "Поле Адресс может содержать не более 60 символов")]
        public string? Address { get; set; }
        public string? Country { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
