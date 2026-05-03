using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroSimulator.Core.Models.Entities
{
    public class Scout : Hero
    {
        public Scout(string name) : base(name)
        {
            Strength = 5;
            Dexterity = 12;
            Intelligence = 4;
            Armour = 3;
        }

        public override int CalculateDamage()
        {
            int equipmentBonus = Equipped.Sum(i => i.BonusDexterity);
            return (Dexterity + equipmentBonus) * 2;
        }
    }
}
