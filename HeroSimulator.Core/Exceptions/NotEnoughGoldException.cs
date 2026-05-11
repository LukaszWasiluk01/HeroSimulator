namespace HeroSimulator.Core.Exceptions
{
    public class NotEnoughGoldException : Exception
    {
        public NotEnoughGoldException(string message) : base(message)
        {
        }
    }
}
