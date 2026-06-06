using HeroSimulator.Core.Enums;
using HeroSimulator.Core.Models.Buildings;
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
        public int CurrentDungeonFloor
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
        public Castle HeroCastle
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
            CurrentDungeonFloor = 1;
            Backpack = new List<Item>();
            Equipment = new Dictionary<ItemSlot, Item>();
            HeroCastle = new Castle();
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

        public int CalculateTotalLuck()
        {
            int totalLuck = Luck;
            foreach (var item in Equipment.Values)
            {
                totalLuck += item.BonusLuck;

                foreach (var gem in item.SocketedGems)
                {
                    totalLuck += gem.BonusLuck;
                }
            }
            return totalLuck;
        }

        public int CalculateCriticalChance()
        {
            return Math.Min(100, CalculateTotalLuck() * 2);
        }

        public abstract int CalculateDamage();
    }
}