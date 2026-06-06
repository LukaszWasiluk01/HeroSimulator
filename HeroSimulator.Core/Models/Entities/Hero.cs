using HeroSimulator.Core.Enums;
using HeroSimulator.Core.Models.Items;
using System.Text.Json.Serialization;

namespace HeroSimulator.Core.Models.Entities
{
    [JsonDerivedType(typeof(Warrior), "Warrior")]
    [JsonDerivedType(typeof(Mage), "Mage")]
    [JsonDerivedType(typeof(Scout), "Scout")]
    public abstract class Hero : Entity
    {
        public int Gold
        {
            get; set;
        }
        public int Energy
        {
            get; set;
        }
        public int MaxEnergy
        {
            get; set;
        }
        public int CurrentDay
        {
            get; set;
        }
        public int Experience
        {
            get; set;
        }
        public int ExperienceToNextLevel
        {
            get; set;
        }
        public List<Item> Backpack
        {
            get; set;
        }
        public Dictionary<ItemSlot, Item> Equipment
        {
            get; set;
        }

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
            Equipment = new Dictionary<ItemSlot, Item>();
        }

        public override int CalculateTotalPower()
        {
            int basePower = base.CalculateTotalPower();
            int equipmentPower = 0;

            foreach (var item in Equipment.Values)
            {
                equipmentPower += item.BonusStrength + item.BonusDexterity + item.BonusIntelligence + item.BonusArmour + item.BonusLuck;

                foreach (var gem in item.SocketedGems)
                {
                    equipmentPower += gem.BonusStrength + gem.BonusDexterity + gem.BonusIntelligence + gem.BonusArmour + gem.BonusLuck;
                }
            }

            return basePower + equipmentPower;
        }

        public abstract int CalculateDamage();
    }
}