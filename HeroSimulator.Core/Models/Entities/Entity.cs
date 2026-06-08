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

        protected Entity(string name, int level)
        {
            Name = name;
            Level = level;
        }
    }
}