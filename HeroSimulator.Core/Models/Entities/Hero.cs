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

        public int Strength
        {
            get; set;
        }
        public int Dexterity
        {
            get; set;
        }
        public int Intelligence
        {
            get; set;
        }
        public int Armour
        {
            get; set;
        }
        public int Luck
        {
            get; set;
        }

        [JsonIgnore]
        public int TotalStrength
        {
            get
            {
                int total = Strength;
                foreach (var item in Equipment.Values)
                {
                    total += item.BonusStrength;
                    foreach (var gem in item.SocketedGems)
                        total += gem.BonusStrength;
                }
                return total;
            }
        }

        [JsonIgnore]
        public int TotalDexterity
        {
            get
            {
                int total = Dexterity;
                foreach (var item in Equipment.Values)
                {
                    total += item.BonusDexterity;
                    foreach (var gem in item.SocketedGems)
                        total += gem.BonusDexterity;
                }
                return total;
            }
        }

        [JsonIgnore]
        public int TotalIntelligence
        {
            get
            {
                int total = Intelligence;
                foreach (var item in Equipment.Values)
                {
                    total += item.BonusIntelligence;
                    foreach (var gem in item.SocketedGems)
                        total += gem.BonusIntelligence;
                }
                return total;
            }
        }

        [JsonIgnore]
        public int TotalArmour
        {
            get
            {
                int total = Armour;
                foreach (var item in Equipment.Values)
                {
                    total += item.BonusArmour;
                    foreach (var gem in item.SocketedGems)
                        total += gem.BonusArmour;
                }
                return total;
            }
        }

        [JsonIgnore]
        public int TotalLuck
        {
            get
            {
                int total = Luck;
                foreach (var item in Equipment.Values)
                {
                    total += item.BonusLuck;
                    foreach (var gem in item.SocketedGems)
                        total += gem.BonusLuck;
                }
                return total;
            }
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

        public virtual int CalculateTotalPower()
        {
            return TotalStrength + TotalDexterity + TotalIntelligence + TotalArmour + TotalLuck;
        }

        public int CalculateCriticalChance()
        {
            return Math.Min(100, TotalLuck * 2);
        }

        public abstract int CalculateDamage();
    }
}