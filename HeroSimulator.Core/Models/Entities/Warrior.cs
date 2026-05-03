using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroSimulator.Core.Models.Entities
{
    public class Warrior : Hero
    {
        public Warrior(string name) : base(name)
        {
            Strength = 12;
            Dexterity = 5;
            Intelligence = 2;
            Armour = 8;
        }

        public override int CalculateDamage()
        {
            int equipmentBonus = Equipped.Sum(i => i.BonusStrength);
            return (Strength + equipmentBonus) * 2;
        }
    }
}
