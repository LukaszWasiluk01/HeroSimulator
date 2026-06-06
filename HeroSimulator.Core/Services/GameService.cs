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

        public DungeonEnemy GetDungeonBoss(int floor)
        {
            switch (floor)
            {
                case 1:
                    return new DungeonEnemy("Szlam z Kanalow", 1, 250, 15, 100, 50);
                case 2:
                    return new DungeonEnemy("Wodz Goblinow", 5, 600, 35, 250, 150);
                case 3:
                    return new DungeonEnemy("Wskrzeszony Szkielet", 10, 1200, 60, 500, 300);
                case 4:
                    return new DungeonEnemy("Krol Orkow", 15, 2500, 100, 1000, 600);
                case 5:
                    return new DungeonEnemy("Mroczny Rycerz", 20, 5000, 180, 2000, 1200);
                case 6:
                    return new DungeonEnemy("Wielki Golem", 25, 10000, 300, 4000, 2500);
                case 7:
                    return new DungeonEnemy("Lisz", 30, 20000, 500, 7500, 5000);
                case 8:
                    return new DungeonEnemy("Demon z Otchlani", 40, 40000, 800, 15000, 10000);
                case 9:
                    return new DungeonEnemy("Smoczy Wladca", 50, 80000, 1200, 30000, 20000);
                case 10:
                    return new DungeonEnemy("Mroczny Pan", 60, 150000, 2000, 100000, 50000);
                default:
                    return null;
            }
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
                int itemType = _random.Next(0, 7);

                if (itemType == 6)
                {
                    item = new Gem($"Klejnot poziomu {_hero.Level}", rarity) { Price = price / 2 };
                    int gemType = _random.Next(0, 4);

                    if (gemType == 0)
                        item.BonusStrength = statBonus;
                    else if (gemType == 1)
                        item.BonusDexterity = statBonus;
                    else if (gemType == 2)
                        item.BonusIntelligence = statBonus;
                    else
                        item.BonusLuck = statBonus;
                }
                else
                {
                    switch (itemType)
                    {
                        case 0:
                            if (_hero is Mage)
                                item = new Weapon($"Kostur poziomu {_hero.Level}", rarity) { BonusIntelligence = statBonus, Price = price };
                            else if (_hero is Scout)
                                item = new Weapon($"Luk poziomu {_hero.Level}", rarity) { BonusDexterity = statBonus, Price = price };
                            else
                                item = new Weapon($"Miecz poziomu {_hero.Level}", rarity) { BonusStrength = statBonus, Price = price };
                            break;
                        case 1:
                            item = new Armor($"Zbroja poziomu {_hero.Level}", rarity) { BonusArmour = statBonus, Price = price };
                            break;
                        case 2:
                            item = new Pants($"Spodnie poziomu {_hero.Level}", rarity) { BonusArmour = statBonus / 2, BonusStrength = statBonus / 2, Price = price };
                            break;
                        case 3:
                            item = new Boots($"Buty poziomu {_hero.Level}", rarity) { BonusArmour = statBonus / 2, BonusDexterity = statBonus / 2, Price = price };
                            break;
                        case 4:
                            item = new Amulet($"Amulet poziomu {_hero.Level}", rarity) { BonusIntelligence = statBonus, BonusLuck = statBonus / 2, Price = price + (statBonus * 5) };
                            break;
                        default:
                            item = new Ring($"Pierscien poziomu {_hero.Level}", rarity) { BonusStrength = statBonus / 2, BonusDexterity = statBonus / 2, BonusLuck = statBonus / 2, Price = price + (statBonus * 5) };
                            break;
                    }
                }
                items.Add(item);
            }
            return items;
        }

        public void PayEnergyForQuest(Quest quest)
        {
            if (_hero.Energy < quest.EnergyCost)
            {
                throw new NotEnoughEnergyException("Brak energii.");
            }

            _hero.Energy -= quest.EnergyCost;
            OnGameStateChanged?.Invoke();
        }

        public CombatInfo PrepareQuestCombat(Quest quest)
        {
            var enemyStats = GetEnemyStats(quest.Difficulty);
            return new CombatInfo
            {
                EnemyName = "Potwor",
                EnemyMaxHp = enemyStats.Hp,
                EnemyDamage = enemyStats.Damage,
                GoldReward = quest.GoldReward,
                ExpReward = quest.ExperienceReward,
                IsDungeon = false,
                Description = quest.Description
            };
        }

        public CombatInfo PrepareDungeonCombat()
        {
            var boss = GetDungeonBoss(_hero.CurrentDungeonFloor);
            if (boss == null)
            {
                throw new InvalidOperationException("Pokonales juz wszystkich bossow! Gratulacje!");
            }

            return new CombatInfo
            {
                EnemyName = boss.Name,
                EnemyMaxHp = boss.Hp,
                EnemyDamage = boss.Damage,
                GoldReward = boss.GoldReward,
                ExpReward = boss.ExpReward,
                IsDungeon = true,
                Description = $"Walka z Bossem: {boss.Name} (Pietro {_hero.CurrentDungeonFloor})"
            };
        }

        public CombatTurnResult ExecuteCombatTurn(int currentHeroHp, int currentEnemyHp, int enemyDamage, bool isPerfectHit)
        {
            int baseDamage = _hero.CalculateDamage();
            double randomVariance = _random.Next(80, 121) / 100.0;
            int randomizedDamage = (int)(baseDamage * randomVariance);

            if (isPerfectHit)
            {
                randomizedDamage = (int)(randomizedDamage * 1.5);
            }

            bool isCrit = false;
            int critChance = _hero.CalculateCriticalChance();

            if (_random.Next(1, 101) <= critChance)
            {
                randomizedDamage *= 2;
                isCrit = true;
            }

            int finalEnemyHp = Math.Max(0, currentEnemyHp - Math.Max(1, randomizedDamage));

            int totalArmour = _hero.TotalArmour;
            double enemyVariance = _random.Next(80, 121) / 100.0;
            int randomizedEnemyDamage = (int)(enemyDamage * enemyVariance);
            int finalEnemyDamage = Math.Max(1, randomizedEnemyDamage - (totalArmour / 2));

            if (isPerfectHit)
            {
                finalEnemyDamage /= 2;
            }

            int finalHeroHp = currentHeroHp;

            if (finalEnemyHp > 0)
            {
                finalHeroHp = Math.Max(0, currentHeroHp - finalEnemyDamage);
            }
            else
            {
                finalEnemyDamage = 0;
            }

            return new CombatTurnResult
            {
                HeroDamageDealt = randomizedDamage,
                EnemyDamageDealt = finalEnemyDamage,
                IsCriticalHit = isCrit,
                IsPerfectHit = isPerfectHit,
                HeroRemainingHp = finalHeroHp,
                EnemyRemainingHp = finalEnemyHp
            };
        }

        public void ResolveCombat(CombatInfo combatInfo, bool won, int remainingHp)
        {
            _hero.CurrentHp = Math.Max(1, remainingHp);

            if (won)
            {
                _hero.Gold += combatInfo.GoldReward;
                _hero.Experience += combatInfo.ExpReward;
                OnLogMessage?.Invoke($"Wygrana! {combatInfo.Description}. Zdobyto {combatInfo.GoldReward}g.");

                if (combatInfo.IsDungeon)
                {
                    int currentFloor = _hero.CurrentDungeonFloor;
                    _hero.CurrentDungeonFloor++;
                    OnLogMessage?.Invoke($"Pokonano Bossa Lochow! Odblokowano pietro {_hero.CurrentDungeonFloor}.");

                    int statBonus = currentFloor * 5;
                    Gem rewardGem = new Gem($"Klejnot Bossa (Pietro {currentFloor})", ItemRarity.Rare) { Price = statBonus * 10 };

                    int gemType = _random.Next(0, 4);
                    if (gemType == 0)
                        rewardGem.BonusStrength = statBonus;
                    else if (gemType == 1)
                        rewardGem.BonusDexterity = statBonus;
                    else if (gemType == 2)
                        rewardGem.BonusIntelligence = statBonus;
                    else
                        rewardGem.BonusLuck = statBonus;

                    if (_hero.Backpack.Count < 10)
                    {
                        _hero.Backpack.Add(rewardGem);
                        OnLogMessage?.Invoke($"Otrzymujesz rzadki artefakt: {rewardGem.Name}!");
                    }
                    else
                    {
                        _hero.Gold += rewardGem.Price;
                        OnLogMessage?.Invoke($"Plecak pelen! Bossa upuscil zamiast tego {rewardGem.Price}g.");
                    }
                }

                while (_hero.Experience >= _hero.ExperienceToNextLevel)
                {
                    LevelUp();
                }
            }
            else
            {
                OnLogMessage?.Invoke($"Porazka... Uciekasz z {remainingHp} HP z walki: {combatInfo.Description}.");
            }

            OnGameStateChanged?.Invoke();
        }

        public void BuyItem(Item item)
        {
            if (_hero.Gold < item.Price)
            {
                throw new NotEnoughGoldException("Brak zlota.");
            }

            if (_hero.Backpack.Count >= 10)
            {
                throw new InventoryFullException("Plecak pelen.");
            }

            _hero.Gold -= item.Price;
            _hero.Backpack.Add(item);
            OnLogMessage?.Invoke($"Kupiono przedmiot: {item.Name} za {item.Price}g.");
            OnGameStateChanged?.Invoke();
        }

        public void SellItem(Item item)
        {
            if (_hero.Backpack.Contains(item))
            {
                int sellPrice = item.TotalPrice / 2;
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
                if (item is Gem)
                {
                    throw new ArgumentException("Nie mozna zalozyc klejnotu na siebie. Musisz go osadzic w przedmiocie.");
                }

                if (_hero.Equipment.ContainsKey(item.Slot))
                {
                    _hero.Backpack.Add(_hero.Equipment[item.Slot]);
                }

                _hero.Equipment[item.Slot] = item;
                _hero.Backpack.Remove(item);

                OnLogMessage?.Invoke($"Zalozono przedmiot: {item.Name}.");
                OnGameStateChanged?.Invoke();
            }
        }

        public void UnequipItem(Item item)
        {
            if (_hero.Backpack.Count >= 10)
            {
                throw new InventoryFullException("Brak miejsca w plecaku.");
            }

            if (_hero.Equipment.ContainsKey(item.Slot) && _hero.Equipment[item.Slot] == item)
            {
                _hero.Equipment.Remove(item.Slot);
                _hero.Backpack.Add(item);

                OnLogMessage?.Invoke($"Zdjete: {item.Name}.");
                OnGameStateChanged?.Invoke();
            }
        }

        public void SocketGem(Item targetItem, Gem gem)
        {
            if (!_hero.Backpack.Contains(gem))
            {
                throw new ArgumentException("Klejnot musi znajdowac sie w plecaku.");
            }

            bool isOwned = _hero.Backpack.Contains(targetItem) || _hero.Equipment.Values.Contains(targetItem);
            if (!isOwned)
            {
                throw new ArgumentException("Nie posiadasz wybranego przedmiotu docelowego.");
            }

            if (targetItem is Gem)
            {
                throw new ArgumentException("Nie mozna wlozyc klejnotu do innego klejnotu.");
            }

            if (targetItem.SocketedGems.Count >= targetItem.MaxSockets)
            {
                throw new ArgumentException($"Przedmiot {targetItem.Name} nie ma juz wolnych gniazd.");
            }

            targetItem.SocketedGems.Add(gem);
            _hero.Backpack.Remove(gem);

            OnLogMessage?.Invoke($"Osadzono {gem.Name} w {targetItem.Name}.");
            OnGameStateChanged?.Invoke();
        }

        public int GetAttributeUpgradeCost(int currentValue)
        {
            return 10 + (currentValue * 5);
        }

        public void UpgradeStrength()
        {
            int cost = GetAttributeUpgradeCost(_hero.Strength);
            if (_hero.Gold < cost)
            {
                throw new NotEnoughGoldException("Brak zlota.");
            }

            _hero.Gold -= cost;
            _hero.Strength++;
            OnLogMessage?.Invoke($"Ulepszono Sile.");
            OnGameStateChanged?.Invoke();
        }

        public void UpgradeDexterity()
        {
            int cost = GetAttributeUpgradeCost(_hero.Dexterity);
            if (_hero.Gold < cost)
            {
                throw new NotEnoughGoldException("Brak zlota.");
            }

            _hero.Gold -= cost;
            _hero.Dexterity++;
            OnLogMessage?.Invoke($"Ulepszono Zrecznosc.");
            OnGameStateChanged?.Invoke();
        }

        public void UpgradeIntelligence()
        {
            int cost = GetAttributeUpgradeCost(_hero.Intelligence);
            if (_hero.Gold < cost)
            {
                throw new NotEnoughGoldException("Brak zlota.");
            }

            _hero.Gold -= cost;
            _hero.Intelligence++;
            OnLogMessage?.Invoke($"Ulepszono Inteligencje.");
            OnGameStateChanged?.Invoke();
        }

        public void UpgradeLuck()
        {
            int cost = GetAttributeUpgradeCost(_hero.Luck);
            if (_hero.Gold < cost)
            {
                throw new NotEnoughGoldException("Brak zlota.");
            }

            _hero.Gold -= cost;
            _hero.Luck++;
            OnLogMessage?.Invoke($"Ulepszono Szczescie.");
            OnGameStateChanged?.Invoke();
        }

        public void UpgradeMine()
        {
            int cost = _hero.HeroCastle.GemMine.GetUpgradeCost();
            if (_hero.Gold < cost)
            {
                throw new NotEnoughGoldException("Brak zlota na ulepszenie kopalni.");
            }

            _hero.Gold -= cost;
            _hero.HeroCastle.GemMine.Level++;
            OnLogMessage?.Invoke($"Ulepszono Kopalnie Klejnotow na poziom {_hero.HeroCastle.GemMine.Level}.");
            OnGameStateChanged?.Invoke();
        }

        public void CollectGemsFromMine()
        {
            var mine = _hero.HeroCastle.GemMine;
            if (mine.StoredGems.Count == 0)
            {
                throw new ArgumentException("Brak wykopanych klejnotow w magazynie kopalni.");
            }

            List<Gem> collected = new List<Gem>();
            foreach (var gem in mine.StoredGems.ToList())
            {
                if (_hero.Backpack.Count >= 10)
                {
                    break;
                }
                _hero.Backpack.Add(gem);
                mine.StoredGems.Remove(gem);
                collected.Add(gem);
            }

            if (collected.Count > 0)
            {
                OnLogMessage?.Invoke($"Odebrano {collected.Count} klejnotow z kopalni do plecaka.");
                OnGameStateChanged?.Invoke();
            }

            if (mine.StoredGems.Count > 0 && _hero.Backpack.Count >= 10)
            {
                throw new InventoryFullException("Plecak pelen, reszta klejnotow zostala w magazynie kopalni.");
            }
        }

        public void EndDay()
        {
            _hero.CurrentDay++;
            _hero.Energy = _hero.MaxEnergy;
            _hero.CurrentHp = _hero.MaxHp;

            MineGems();

            OnLogMessage?.Invoke($"Rozpoczeto nowy dzien ({_hero.CurrentDay}). Zregenerowano sily.");
            OnGameStateChanged?.Invoke();
        }

        private void MineGems()
        {
            var mine = _hero.HeroCastle.GemMine;

            if (mine.StoredGems.Count >= mine.MaxCapacity)
            {
                return;
            }

            int effectiveRoll = _random.Next(1, 101) + (mine.Level * 2);

            ItemRarity rarity = effectiveRoll > 95 ? ItemRarity.Rare : effectiveRoll > 60 ? ItemRarity.Magic : ItemRarity.Common;
            int multiplier = rarity == ItemRarity.Rare ? 3 : rarity == ItemRarity.Magic ? 2 : 1;

            int statBonus = (mine.Level + _random.Next(1, 4)) * multiplier;

            Gem newGem = new Gem($"Klejnot Kopalniany", rarity) { Price = statBonus * 5 };

            int gemType = _random.Next(0, 4);
            if (gemType == 0)
                newGem.BonusStrength = statBonus;
            else if (gemType == 1)
                newGem.BonusDexterity = statBonus;
            else if (gemType == 2)
                newGem.BonusIntelligence = statBonus;
            else
                newGem.BonusLuck = statBonus;

            mine.StoredGems.Add(newGem);
        }

        private void LevelUp()
        {
            _hero.Level++;
            _hero.Experience -= _hero.ExperienceToNextLevel;

            long nextExp = (long)(_hero.ExperienceToNextLevel * 1.2);
            if (nextExp > int.MaxValue)
            {
                _hero.ExperienceToNextLevel = int.MaxValue;
            }
            else
            {
                _hero.ExperienceToNextLevel = (int)nextExp;
            }

            _hero.MaxHp += 50;
            _hero.CurrentHp = _hero.MaxHp;
            OnLogMessage?.Invoke($"Awans na {_hero.Level} poziom! Odnowiono maksymalne zdrowie.");
        }
    }
}