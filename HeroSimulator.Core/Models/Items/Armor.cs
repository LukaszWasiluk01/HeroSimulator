using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroSimulator.Core.Enums;

namespace HeroSimulator.Core.Models.Items
{
    public class Armor : Item
    {
        public Armor(string name, ItemRarity rarity) : base(name, rarity)
        {
        }
    }
}
