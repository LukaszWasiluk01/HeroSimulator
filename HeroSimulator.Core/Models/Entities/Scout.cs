namespace HeroSimulator.Core.Models.Entities
{
    public class Scout : Hero
    {
        public Scout(string name) : base(name)
        {
            Strength = 5;
            Dexterity = 15;
            Intelligence = 5;
            Armour = 2;
        }

        public override int CalculateDamage()
        {
            int equipmentBonus = 0;

            if (EquippedWeapon != null)
            {
                equipmentBonus += EquippedWeapon.BonusDexterity;
            }
            if (EquippedArmor != null)
            {
                equipmentBonus += EquippedArmor.BonusDexterity;
            }
            if (EquippedPants != null)
            {
                equipmentBonus += EquippedPants.BonusDexterity;
            }
            if (EquippedBoots != null)
            {
                equipmentBonus += EquippedBoots.BonusDexterity;
            }
            if (EquippedAmulet != null)
            {
                equipmentBonus += EquippedAmulet.BonusDexterity;
            }
            if (EquippedRing != null)
            {
                equipmentBonus += EquippedRing.BonusDexterity;
            }

            return (Dexterity + equipmentBonus) * 2;
        }
    }
}