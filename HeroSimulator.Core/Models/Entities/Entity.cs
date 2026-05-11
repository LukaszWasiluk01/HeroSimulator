namespace HeroSimulator.Core.Models.Entities
{
    public abstract class Entity
    {
        public string Name
        {
            get; set;
        }
        public int Level
        {
            get; set;
        }
        public int MaxHp
        {
            get; set;
        }
        public int CurrentHp
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

        protected Entity(string name, int level)
        {
            Name = name;
            Level = level;
        }

        public virtual int CalculateTotalPower()
        {
            return Strength + Dexterity + Intelligence + Armour + MaxHp;
        }
    }
}
