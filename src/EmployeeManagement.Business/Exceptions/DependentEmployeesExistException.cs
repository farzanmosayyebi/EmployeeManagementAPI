using EmployeeManagement.Common.Models;
using System.Runtime.Serialization;

namespace EmployeeManagement.Business.Exceptions;

[Serializable]
public class DependentEmployeesExistException : Exception
{
    public List<Employee>? Employees;

    public DependentEmployeesExistException()
    {
    }

    public DependentEmployeesExistException(List<Employee> employees)
    {
        Employees = employees;
    }

    public DependentEmployeesExistException(string? message) : base(message)
    {
    }

    public DependentEmployeesExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DependentEmployeesExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}