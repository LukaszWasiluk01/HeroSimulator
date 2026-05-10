namespace HeroSimulator.Core.Models.Entities
{
    public class Warrior : Hero
    {
        public Warrior(string name) : base(name)
        {
            Strength = 12;
            Dexterity = 5;
            Intelligence = 2;
            Armour = 8;
        }

        public override int CalculateDamage()
        {
            int equipmentBonus = 0;
            if (EquippedWeapon != null) equipmentBonus += EquippedWeapon.BonusStrength;
            if (EquippedArmor != null) equipmentBonus += EquippedArmor.BonusStrength;
            return (Strength + equipmentBonus) * 2;
        }
    }
}