using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using HeroSimulator.Core.Models.Items;

namespace HeroSimulator.Core.Models.Entities
{
    [JsonDerivedType(typeof(Warrior), "Warrior")]
    [JsonDerivedType(typeof(Mage), "Mage")]
    [JsonDerivedType(typeof(Scout), "Scout")]
    public abstract class Hero : Entity
    {
        public int Gold { get; set; }
        public int Energy { get; set; }
        public int MaxEnergy { get; set; }
        public int CurrentDay { get; set; }
        public int Experience { get; set; }
        public int ExperienceToNextLevel { get; set; }
        public List<Item> Backpack { get; set; }
        public List<Item> Equipped { get; set; }

        protected Hero(string name) : base(name, 1)
        {
            Gold = 0;
            Energy = 100;
            MaxEnergy = 100;
            CurrentDay = 1;
            MaxHp = 100;
            CurrentHp = 100;
            Experience = 0;
            ExperienceToNextLevel = 100;
            Backpack = new List<Item>();
            Equipped = new List<Item>();
        }

        public override int CalculateTotalPower()
        {
            int basePower = base.CalculateTotalPower();
            int equipmentPower = Equipped.Sum(i => i.BonusStrength + i.BonusDexterity + i.BonusIntelligence + i.BonusArmour);
            return basePower + equipmentPower;
        }

        public abstract int CalculateDamage();
    }
}