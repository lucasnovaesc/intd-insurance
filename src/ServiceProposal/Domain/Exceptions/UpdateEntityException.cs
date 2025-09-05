

namespace Domain.Exceptions
{
    public class UpdateEntityException : Exception
    {

        public UpdateEntityException()
        {
        }

        public UpdateEntityException(string message)
            : base(message)
        {
        }

        public UpdateEntityException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
