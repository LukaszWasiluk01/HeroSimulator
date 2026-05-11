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
            int equipmentBonus = 0;
            if (EquippedWeapon != null)
                equipmentBonus += EquippedWeapon.BonusDexterity;
            if (EquippedArmor != null)
                equipmentBonus += EquippedArmor.BonusDexterity;
            return (Dexterity + equipmentBonus) * 2;
        }
    }
}