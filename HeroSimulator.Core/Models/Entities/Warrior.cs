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
            Luck = 5;
        }

        public override int CalculateDamage()
        {
            int equipmentBonus = 0;

            foreach (var item in Equipment.Values)
            {
                equipmentBonus += item.BonusStrength;
            }

            return (Strength + equipmentBonus) * 2;
        }
    }
}