using HeroSimulator.Core.Enums;

namespace HeroSimulator.Core.Models.Items
{
    public class Weapon : Item
    {
        public Weapon(string name, ItemRarity rarity) : base(name, rarity, ItemSlot.Weapon)
        {
        }
    }
}