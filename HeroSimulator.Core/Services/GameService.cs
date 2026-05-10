using HeroSimulator.Core.Enums;
using HeroSimulator.Core.Exceptions;
using HeroSimulator.Core.Models;
using HeroSimulator.Core.Models.Entities;
using HeroSimulator.Core.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroSimulator.Core.Services
{
    public class GameService
    {
        private readonly Hero _hero;
        private readonly Random _random;

        public GameService(Hero hero)
        {
            _hero = hero;
            _random = new Random();
        }

        public List<Quest> GenerateDailyQuests(int count = 3)
        {
            var possibleQuests = new List<Quest>
            {
                new Quest("Zlikwiduj szczury w piwnicy", QuestDifficulty.Easy, 10, _hero.Level * 5, _hero.Level * 10),
                new Quest("Eskortuj kupca", QuestDifficulty.Normal, 20, _hero.Level * 12, _hero.Level * 25),
                new Quest("Zapoluj na lesnego trolla", QuestDifficulty.Hard, 40, _hero.Level * 30, _hero.Level * 50),
                new Quest("Odzyskaj skradziony artefakt", QuestDifficulty.Normal, 25, _hero.Level * 15, _hero.Level * 30),
                new Quest("Oczysc oboz goblinow", QuestDifficulty.Hard, 50, _hero.Level * 40, _hero.Level * 60)
            };

            return possibleQuests.OrderBy(q => _random.Next()).Take(count).ToList();
        }

        public List<Item> GenerateShopItems(int count = 3)
        {
            var items = new List<Item>();
            for (int i = 0; i < count; i++)
            {
                Item item;
                int rarityRoll = _random.Next(1, 101);
                ItemRarity rarity = rarityRoll > 80 ? ItemRarity.Rare : rarityRoll > 40 ? ItemRarity.Magic : ItemRarity.Common;

                int statBonus = _hero.Level + _random.Next(_hero.Level, _hero.Level * 2);
                int price = statBonus * 5;

                if (_random.Next(0, 2) == 0)
                {
                    item = new Weapon($"Miecz poziomu {_hero.Level}", rarity) { BonusStrength = statBonus, Price = price };
                }
                else
                {
                    item = new Armor($"Zbroja poziomu {_hero.Level}", rarity) { BonusArmour = statBonus, Price = price };
                }

                items.Add(item);
            }
            return items;
        }

        public void StartQuest(Quest quest)
        {
            if (_hero.Energy < quest.EnergyCost)
            {
                throw new NotEnoughEnergyException("Brak energii na te misje.");
            }

            _hero.Energy -= quest.EnergyCost;
            _hero.Gold += quest.GoldReward;
            _hero.Experience += quest.ExperienceReward;

            if (_hero.Experience >= _hero.ExperienceToNextLevel)
            {
                LevelUp();
            }
        }

        public void BuyItem(Item item)
        {
            if (_hero.Gold < item.Price)
            {
                throw new NotEnoughGoldException("Brak zlota na zakup przedmiotu.");
            }
            if (_hero.Backpack.Count >= 10)
            {
                throw new InventoryFullException("Plecak jest pelen.");
            }

            _hero.Gold -= item.Price;
            _hero.Backpack.Add(item);
        }

        public void EndDay()
        {
            _hero.CurrentDay++;
            _hero.Energy = _hero.MaxEnergy;
        }

        private void LevelUp()
        {
            _hero.Level++;
            _hero.Experience -= _hero.ExperienceToNextLevel;
            _hero.ExperienceToNextLevel = (int)(_hero.ExperienceToNextLevel * 1.5);
            _hero.MaxHp += 20;
            _hero.CurrentHp = _hero.MaxHp;
            _hero.MaxEnergy += 10;
            _hero.Energy = _hero.MaxEnergy;
        }
    }
}