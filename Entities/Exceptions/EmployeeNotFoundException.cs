namespace Entities.Exceptions;

public class EmployeeNotFoundException : NotFoundException
{
    public EmployeeNotFoundException(Guid emplooyeeId) : base($"Сотрудник с id: {emplooyeeId} не существует в базе данных")
    {
    }
}
