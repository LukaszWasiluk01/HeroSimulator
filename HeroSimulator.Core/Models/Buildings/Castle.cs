namespace HeroSimulator.Core.Models.Buildings
{
    public class Castle
    {
        public Mine GemMine
        {
            get; set;
        }

        public Castle()
        {
            GemMine = new Mine();
        }
    }
}