namespace HeroSimulator.Core.Models
{
    public class CombatTurnResult
    {
        public int HeroDamageDealt
        {
            get; set;
        }
        public int EnemyDamageDealt
        {
            get; set;
        }
        public bool IsCriticalHit
        {
            get; set;
        }
        public bool IsPerfectHit
        {
            get; set;
        }
        public int HeroRemainingHp
        {
            get; set;
        }
        public int EnemyRemainingHp
        {
            get; set;
        }
    }
}