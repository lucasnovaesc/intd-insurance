

namespace Domain.Exceptions
{
    public class InsertEntityException : Exception
    {
        public InsertEntityException() { }
        public InsertEntityException(string message) : base(message) { }
    }
}
