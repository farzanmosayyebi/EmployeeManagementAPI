using System.Runtime.Serialization;

namespace EmployeeManagement.Business.Exceptions
{
    [Serializable]
    public class InvalidAddOperationException : Exception
    {
        public Type ItemType;
        public int ItemId;

        public InvalidAddOperationException()
        {
        }

        public InvalidAddOperationException(string? message) : base(message)
        {
        }

        public InvalidAddOperationException(Type type, int employeeId)
        {
            ItemType = type;
            ItemId = employeeId;
        }

        public InvalidAddOperationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidAddOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}