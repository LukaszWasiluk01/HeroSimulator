namespace HeroSimulator.Core.Models.Entities
{
    public class Warrior : Hero
    {
        public Warrior(string name) : base(name)
        {
            Strength = 15;
            Dexterity = 5;
            Intelligence = 2;
            Armour = 5;
        }

        public override int CalculateDamage()
        {
            int equipmentBonus = 0;

            if (EquippedWeapon != null)
            {
                equipmentBonus += EquippedWeapon.BonusStrength;
            }
            if (EquippedArmor != null)
            {
                equipmentBonus += EquippedArmor.BonusStrength;
            }
            if (EquippedPants != null)
            {
                equipmentBonus += EquippedPants.BonusStrength;
            }
            if (EquippedBoots != null)
            {
                equipmentBonus += EquippedBoots.BonusStrength;
            }
            if (EquippedAmulet != null)
            {
                equipmentBonus += EquippedAmulet.BonusStrength;
            }
            if (EquippedRing != null)
            {
                equipmentBonus += EquippedRing.BonusStrength;
            }

            return (Strength + equipmentBonus) * 2;
        }
    }
}