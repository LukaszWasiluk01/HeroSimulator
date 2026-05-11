using HeroSimulator.Core.Enums;

namespace HeroSimulator.Core.Models.Items
{
    public class Weapon : Item
    {
        public int MinDamage
        {
            get; set;
        }
        public int MaxDamage
        {
            get; set;
        }

        public Weapon(string name, ItemRarity rarity) : base(name, rarity)
        {
        }
    }
}
