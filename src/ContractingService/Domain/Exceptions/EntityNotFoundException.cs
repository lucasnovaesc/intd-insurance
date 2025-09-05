

namespace Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string messageException, Exception inner) : base(messageException, inner)
        {

        }
        public EntityNotFoundException(string messageException) : base(messageException)
        {

        }
    }
}
