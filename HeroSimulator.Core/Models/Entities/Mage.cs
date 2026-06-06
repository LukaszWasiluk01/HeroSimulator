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
            Luck = 5;
        }

        public override int CalculateDamage()
        {
            int equipmentBonus = 0;

            foreach (var item in Equipment.Values)
            {
                equipmentBonus += item.BonusIntelligence;

                foreach (var gem in item.SocketedGems)
                {
                    equipmentBonus += gem.BonusIntelligence;
                }
            }

            return (Intelligence + equipmentBonus) * 3;
        }
    }
}