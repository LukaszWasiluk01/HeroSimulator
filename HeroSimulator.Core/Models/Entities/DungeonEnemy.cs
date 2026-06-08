namespace HeroSimulator.Core.Models.Entities
{
    public class DungeonEnemy : Entity
    {
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
            : base(name, level)
        {
            MaxHp = hp;
            CurrentHp = hp;
            Damage = damage;
            GoldReward = goldReward;
            ExpReward = expReward;
        }
    }
}