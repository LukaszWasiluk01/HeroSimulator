using HeroSimulator.Core.Models.Items;

namespace HeroSimulator.Core.Models.Buildings
{
    public class Mine
    {
        public int Level
        {
            get; set;
        }
        public List<Gem> StoredGems
        {
            get; set;
        }
        public int MaxCapacity
        {
            get; set;
        }

        public Mine()
        {
            Level = 1;
            StoredGems = new List<Gem>();
            MaxCapacity = 5;
        }

        public int GetUpgradeCost()
        {
            return Level * Level * 50;
        }
    }
}