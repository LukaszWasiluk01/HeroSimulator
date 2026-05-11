using HeroSimulator.Core.Enums;

namespace HeroSimulator.Core.Models
{
    public class Quest
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public QuestDifficulty Difficulty { get; set; }
        public int EnergyCost { get; set; }
        public int GoldReward { get; set; }
        public int ExperienceReward { get; set; }

        public Quest(string description, QuestDifficulty difficulty, int energyCost, int goldReward, int experienceReward)
        {
            Id = Guid.NewGuid().ToString();
            Description = description;
            Difficulty = difficulty;
            EnergyCost = energyCost;
            GoldReward = goldReward;
            ExperienceReward = experienceReward;
        }
    }
}
