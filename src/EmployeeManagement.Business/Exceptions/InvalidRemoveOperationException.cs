using System.Runtime.Serialization;

namespace EmployeeManagement.Business.Exceptions
{
    [Serializable]
    public class InvalidRemoveOperationException : Exception
    {
        public Type ItemType;
        public int ItemId;

        public InvalidRemoveOperationException()
        {
        }

        public InvalidRemoveOperationException(string? message) : base(message)
        {
        }

        public InvalidRemoveOperationException(Type type, int employeeId)
        {
            ItemType = type;
            ItemId = employeeId;
        }

        public InvalidRemoveOperationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidRemoveOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}