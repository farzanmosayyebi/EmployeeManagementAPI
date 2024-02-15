using System.Runtime.Serialization;

namespace EmployeeManagement.Business.Exceptions
{
    [Serializable]
    public class ItemNotFoundException : Exception
    {
        public Type ItemType { get; }
        public int ItemId { get; }

        public ItemNotFoundException()
        {
        }

        public ItemNotFoundException(string? message) : base(message)
        {
        }

        public ItemNotFoundException(Type type, int id)
        {
            ItemType = type;
            ItemId = id;
        }

        public ItemNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}