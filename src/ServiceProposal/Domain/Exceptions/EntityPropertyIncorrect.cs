

namespace Domain.Exceptions
{
    public class EntityPropertyIncorrect : Exception
    {
        public EntityPropertyIncorrect() { }
        public EntityPropertyIncorrect(string message) : base(message) { }
    }
}
