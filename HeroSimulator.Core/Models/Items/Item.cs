using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroSimulator.Core.Enums;

namespace HeroSimulator.Core.Models.Items
{
    public abstract class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ItemRarity Rarity { get; set; }
        public int BonusStrength { get; set; }
        public int BonusDexterity { get; set; }
        public int BonusIntelligence { get; set; }
        public int BonusArmour { get; set; }
        public int Price { get; set; }

        protected Item(string name, ItemRarity rarity)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Rarity = rarity;
        }
    }
}
