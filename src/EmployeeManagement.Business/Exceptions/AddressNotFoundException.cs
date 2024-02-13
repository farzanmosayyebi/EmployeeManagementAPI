using System.Runtime.Serialization;

namespace EmployeeManagement.Business.Exceptions;

[Serializable]
public class AddressNotFoundException : Exception
{
    public int Id { get; }
    public AddressNotFoundException()
    {
    }
    public AddressNotFoundException(int id)
    {
        Id = id;
    }

    public AddressNotFoundException(string? message) : base(message)
    {
    }

    public AddressNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected AddressNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

}