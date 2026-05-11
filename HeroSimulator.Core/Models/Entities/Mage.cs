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
            int equipmentBonus = 0;

            if (EquippedWeapon != null)
            {
                equipmentBonus += EquippedWeapon.BonusIntelligence;
            }
            if (EquippedArmor != null)
            {
                equipmentBonus += EquippedArmor.BonusIntelligence;
            }
            if (EquippedPants != null)
            {
                equipmentBonus += EquippedPants.BonusIntelligence;
            }
            if (EquippedBoots != null)
            {
                equipmentBonus += EquippedBoots.BonusIntelligence;
            }
            if (EquippedAmulet != null)
            {
                equipmentBonus += EquippedAmulet.BonusIntelligence;
            }
            if (EquippedRing != null)
            {
                equipmentBonus += EquippedRing.BonusIntelligence;
            }

            return (Intelligence + equipmentBonus) * 3;
        }
    }
}