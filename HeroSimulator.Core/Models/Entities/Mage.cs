using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroSimulator.Core.Models.Entities
{
    public class Mage : Hero
    {
        public Mage(string name) : base(name)
        {
            Strength = 2;
            Dexterity = 3;
            Intelligence = 15;
            Armour = 1;
        }

        public override int CalculateDamage()
        {
            int equipmentBonus = Equipped.Sum(i => i.BonusIntelligence);
            return (Intelligence + equipmentBonus) * 3;
        }
    }
}
