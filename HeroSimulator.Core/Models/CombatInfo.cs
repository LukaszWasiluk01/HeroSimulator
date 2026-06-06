namespace HeroSimulator.Core.Models
{
    public class CombatInfo
    {
        public string EnemyName
        {
            get; set;
        }
        public int EnemyMaxHp
        {
            get; set;
        }
        public int EnemyDamage
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
        public bool IsDungeon
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
    }
}