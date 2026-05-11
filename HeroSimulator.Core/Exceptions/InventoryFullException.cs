namespace HeroSimulator.Core.Exceptions
{
    public class InventoryFullException : Exception
    {
        public InventoryFullException(string message) : base(message)
        {
        }
    }
}
