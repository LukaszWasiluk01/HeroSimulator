namespace HeroSimulator.Core.Models
{
    public class DungeonEnemy
    {
        public string Name
        {
            get; set;
        }
        public int Level
        {
            get; set;
        }
        public int Hp
        {
            get; set;
        }
        public int Damage
        {
            get; set;
        }
        public int GoldReward
        {
            get; set;
        }
        public int ExpReward
        {
            get; set;
        }

        public DungeonEnemy(string name, int level, int hp, int damage, int goldReward, int expReward)
        {
            Name = name;
            Level = level;
            Hp = hp;
            Damage = damage;
            GoldReward = goldReward;
            ExpReward = expReward;
        }
    }
}