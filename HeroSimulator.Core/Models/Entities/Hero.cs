using System.Collections.Generic;
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

        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }
        public Pants EquippedPants { get; set; }
        public Boots EquippedBoots { get; set; }
        public Amulet EquippedAmulet { get; set; }
        public Ring EquippedRing { get; set; }

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
        }

        public override int CalculateTotalPower()
        {
            int basePower = base.CalculateTotalPower();
            int equipmentPower = 0;

            if (EquippedWeapon != null) equipmentPower += EquippedWeapon.BonusStrength + EquippedWeapon.BonusDexterity + EquippedWeapon.BonusIntelligence + EquippedWeapon.BonusArmour;
            if (EquippedArmor != null) equipmentPower += EquippedArmor.BonusStrength + EquippedArmor.BonusDexterity + EquippedArmor.BonusIntelligence + EquippedArmor.BonusArmour;
            if (EquippedPants != null) equipmentPower += EquippedPants.BonusStrength + EquippedPants.BonusDexterity + EquippedPants.BonusIntelligence + EquippedPants.BonusArmour;
            if (EquippedBoots != null) equipmentPower += EquippedBoots.BonusStrength + EquippedBoots.BonusDexterity + EquippedBoots.BonusIntelligence + EquippedBoots.BonusArmour;
            if (EquippedAmulet != null) equipmentPower += EquippedAmulet.BonusStrength + EquippedAmulet.BonusDexterity + EquippedAmulet.BonusIntelligence + EquippedAmulet.BonusArmour;
            if (EquippedRing != null) equipmentPower += EquippedRing.BonusStrength + EquippedRing.BonusDexterity + EquippedRing.BonusIntelligence + EquippedRing.BonusArmour;

            return basePower + equipmentPower;
        }

        public abstract int CalculateDamage();
    }
}