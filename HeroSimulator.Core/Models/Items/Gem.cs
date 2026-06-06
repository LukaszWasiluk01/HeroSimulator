using HeroSimulator.Core.Enums;

namespace HeroSimulator.Core.Models.Items
{
    public class Gem : Item
    {
        public Gem(string name, ItemRarity rarity) : base(name, rarity, ItemSlot.None)
        {
        }
    }
}