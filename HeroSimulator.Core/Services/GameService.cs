using System;
using System.Collections.Generic;
using System.Linq;
using HeroSimulator.Core.Delegates;
using HeroSimulator.Core.Enums;
using HeroSimulator.Core.Exceptions;
using HeroSimulator.Core.Models;
using HeroSimulator.Core.Models.Entities;
using HeroSimulator.Core.Models.Items;

namespace HeroSimulator.Core.Services
{
    public class GameService
    {
        private readonly Hero _hero;
        private readonly Random _random;

        public event GameStateChangedHandler OnGameStateChanged;
        public event LogMessageHandler OnLogMessage;

        public GameService(Hero hero)
        {
            _hero = hero;
            _random = new Random();
        }

        public Hero GetHero()
        {
            return _hero;
        }

        public (int Hp, int Damage) GetEnemyStats(QuestDifficulty difficulty)
        {
            int hp = difficulty == QuestDifficulty.Easy ? _hero.Level * 20 : difficulty == QuestDifficulty.Normal ? _hero.Level * 40 : _hero.Level * 80;
            int dmg = difficulty == QuestDifficulty.Easy ? _hero.Level * 2 : difficulty == QuestDifficulty.Normal ? _hero.Level * 5 : _hero.Level * 10;
            return (hp, dmg);
        }

        public List<Quest> GenerateDailyQuests(int count = 3)
        {
            var possibleQuests = new List<Quest>
            {
                new Quest("Zlikwiduj szczury w piwnicy", QuestDifficulty.Easy, 10, _hero.Level * 5, _hero.Level * 10),
                new Quest("Zbierz rzadkie ziola", QuestDifficulty.Easy, 15, _hero.Level * 6, _hero.Level * 12),
                new Quest("Odnajdz zgubiony pierscien", QuestDifficulty.Easy, 12, _hero.Level * 8, _hero.Level * 15),
                new Quest("Eskortuj kupca", QuestDifficulty.Normal, 20, _hero.Level * 12, _hero.Level * 25),
                new Quest("Przegon bandytow z traktu", QuestDifficulty.Normal, 25, _hero.Level * 15, _hero.Level * 30),
                new Quest("Rozbij oboz goblinow", QuestDifficulty.Normal, 30, _hero.Level * 20, _hero.Level * 35),
                new Quest("Zbadaj nawiedzona krypte", QuestDifficulty.Hard, 40, _hero.Level * 30, _hero.Level * 50),
                new Quest("Zapoluj na lesnego trolla", QuestDifficulty.Hard, 50, _hero.Level * 40, _hero.Level * 60),
                new Quest("Pokonaj leze smoka", QuestDifficulty.Hard, 70, _hero.Level * 60, _hero.Level * 100)
            };

            return possibleQuests.OrderBy(q => _random.Next()).Take(count).ToList();
        }

        public List<Item> GenerateShopItems(int count = 5)
        {
            var items = new List<Item>();
            for (int i = 0; i < count; i++)
            {
                Item item;
                int rarityRoll = _random.Next(1, 101);
                ItemRarity rarity = rarityRoll > 85 ? ItemRarity.Rare : rarityRoll > 50 ? ItemRarity.Magic : ItemRarity.Common;
                int multiplier = rarity == ItemRarity.Rare ? 3 : rarity == ItemRarity.Magic ? 2 : 1;
                int statBonus = (_hero.Level + _random.Next(1, 5)) * multiplier;
                int price = statBonus * 10;
                int itemType = _random.Next(0, 6);

                switch (itemType)
                {
                    case 0:
                        if (_hero is Mage) item = new Weapon($"Kostur poziomu {_hero.Level}", rarity) { BonusIntelligence = statBonus, Price = price };
                        else if (_hero is Scout) item = new Weapon($"Luk poziomu {_hero.Level}", rarity) { BonusDexterity = statBonus, Price = price };
                        else item = new Weapon($"Miecz poziomu {_hero.Level}", rarity) { BonusStrength = statBonus, Price = price };
                        break;
                    case 1: item = new Armor($"Zbroja poziomu {_hero.Level}", rarity) { BonusArmour = statBonus, Price = price }; break;
                    case 2: item = new Pants($"Spodnie poziomu {_hero.Level}", rarity) { BonusArmour = statBonus / 2, BonusStrength = statBonus / 2, Price = price }; break;
                    case 3: item = new Boots($"Buty poziomu {_hero.Level}", rarity) { BonusArmour = statBonus / 2, BonusDexterity = statBonus / 2, Price = price }; break;
                    case 4: item = new Amulet($"Amulet poziomu {_hero.Level}", rarity) { BonusIntelligence = statBonus, Price = price }; break;
                    default: item = new Ring($"Pierscien poziomu {_hero.Level}", rarity) { BonusStrength = statBonus / 2, BonusDexterity = statBonus / 2, Price = price }; break;
                }
                items.Add(item);
            }
            return items;
        }

        public void StartQuest(Quest quest)
        {
            if (_hero.Energy < quest.EnergyCost) throw new NotEnoughEnergyException("Brak energii.");
            _hero.Energy -= quest.EnergyCost;

            var enemyStats = GetEnemyStats(quest.Difficulty);
            int enemyHp = enemyStats.Hp;
            int enemyDamage = enemyStats.Damage;
            int heroDamage = _hero.CalculateDamage();

            int totalArmour = _hero.Armour;
            if (_hero.EquippedArmor != null) totalArmour += _hero.EquippedArmor.BonusArmour;
            if (_hero.EquippedPants != null) totalArmour += _hero.EquippedPants.BonusArmour;
            if (_hero.EquippedBoots != null) totalArmour += _hero.EquippedBoots.BonusArmour;

            bool won = false;
            while (true)
            {
                enemyHp -= Math.Max(1, heroDamage);
                if (enemyHp <= 0) { won = true; break; }
                int actualDamage = enemyDamage - (totalArmour / 2);
                _hero.CurrentHp -= Math.Max(1, actualDamage);
                if (_hero.CurrentHp <= 0) { _hero.CurrentHp = 1; won = false; break; }
            }

            if (won)
            {
                _hero.Gold += quest.GoldReward;
                _hero.Experience += quest.ExperienceReward;
                OnLogMessage?.Invoke($"Wygrana! {quest.Description}. Zdobyto {quest.GoldReward}g.");
                if (_hero.Experience >= _hero.ExperienceToNextLevel) LevelUp();
            }
            else OnLogMessage?.Invoke($"Porazka... Uciekasz z 1 HP.");

            OnGameStateChanged?.Invoke();
        }

        public void BuyItem(Item item)
        {
            if (_hero.Gold < item.Price) throw new NotEnoughGoldException("Brak zlota.");
            if (_hero.Backpack.Count >= 10) throw new InventoryFullException("Plecak pelen.");
            _hero.Gold -= item.Price;
            _hero.Backpack.Add(item);
            OnGameStateChanged?.Invoke();
        }

        public void SellItem(Item item)
        {
            if (_hero.Backpack.Contains(item))
            {
                int sellPrice = item.Price / 2;
                _hero.Gold += sellPrice;
                _hero.Backpack.Remove(item);
                OnLogMessage?.Invoke($"Sprzedano {item.Name} za {sellPrice}g.");
                OnGameStateChanged?.Invoke();
            }
        }

        public void EquipItem(Item item)
        {
            if (_hero.Backpack.Contains(item))
            {
                if (item is Weapon weapon) { if (_hero.EquippedWeapon != null) _hero.Backpack.Add(_hero.EquippedWeapon); _hero.EquippedWeapon = weapon; }
                else if (item is Armor armor) { if (_hero.EquippedArmor != null) _hero.Backpack.Add(_hero.EquippedArmor); _hero.EquippedArmor = armor; }
                else if (item is Pants pants) { if (_hero.EquippedPants != null) _hero.Backpack.Add(_hero.EquippedPants); _hero.EquippedPants = pants; }
                else if (item is Boots boots) { if (_hero.EquippedBoots != null) _hero.Backpack.Add(_hero.EquippedBoots); _hero.EquippedBoots = boots; }
                else if (item is Amulet amulet) { if (_hero.EquippedAmulet != null) _hero.Backpack.Add(_hero.EquippedAmulet); _hero.EquippedAmulet = amulet; }
                else if (item is Ring ring) { if (_hero.EquippedRing != null) _hero.Backpack.Add(_hero.EquippedRing); _hero.EquippedRing = ring; }
                _hero.Backpack.Remove(item);
                OnGameStateChanged?.Invoke();
            }
        }

        public void UnequipItem(Item item)
        {
            if (_hero.Backpack.Count >= 10) throw new InventoryFullException("Brak miejsca w plecaku.");
            if (_hero.EquippedWeapon == item) _hero.EquippedWeapon = null;
            else if (_hero.EquippedArmor == item) _hero.EquippedArmor = null;
            else if (_hero.EquippedPants == item) _hero.EquippedPants = null;
            else if (_hero.EquippedBoots == item) _hero.EquippedBoots = null;
            else if (_hero.EquippedAmulet == item) _hero.EquippedAmulet = null;
            else if (_hero.EquippedRing == item) _hero.EquippedRing = null;
            _hero.Backpack.Add(item);
            OnGameStateChanged?.Invoke();
        }

        public int GetAttributeUpgradeCost(int currentValue) => 10 + (currentValue * 5);

        public void UpgradeStrength()
        {
            int cost = GetAttributeUpgradeCost(_hero.Strength);
            if (_hero.Gold < cost) throw new NotEnoughGoldException("Brak zlota.");
            _hero.Gold -= cost; _hero.Strength++; OnGameStateChanged?.Invoke();
        }

        public void UpgradeDexterity()
        {
            int cost = GetAttributeUpgradeCost(_hero.Dexterity);
            if (_hero.Gold < cost) throw new NotEnoughGoldException("Brak zlota.");
            _hero.Gold -= cost; _hero.Dexterity++; OnGameStateChanged?.Invoke();
        }

        public void UpgradeIntelligence()
        {
            int cost = GetAttributeUpgradeCost(_hero.Intelligence);
            if (_hero.Gold < cost) throw new NotEnoughGoldException("Brak zlota.");
            _hero.Gold -= cost; _hero.Intelligence++; OnGameStateChanged?.Invoke();
        }

        public void EndDay()
        {
            _hero.CurrentDay++; _hero.Energy = _hero.MaxEnergy; _hero.CurrentHp = _hero.MaxHp; OnGameStateChanged?.Invoke();
        }

        private void LevelUp()
        {
            _hero.Level++; _hero.Experience -= _hero.ExperienceToNextLevel;
            _hero.ExperienceToNextLevel = (int)(_hero.ExperienceToNextLevel * 1.5);
            _hero.MaxHp += 20; _hero.CurrentHp = _hero.MaxHp;
        }
    }
}