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
            Luck = 8;
        }

        public override int CalculateDamage()
        {
            int equipmentBonus = 0;

            foreach (var item in Equipment.Values)
            {
                equipmentBonus += item.BonusDexterity;
            }

            return (Dexterity + equipmentBonus) * 2;
        }
    }
}