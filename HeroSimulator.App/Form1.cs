using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HeroSimulator.Core.Models;
using HeroSimulator.Core.Models.Entities;
using HeroSimulator.Core.Models.Items;
using HeroSimulator.Core.Services;

namespace HeroSimulator.App
{
    public partial class Form1 : Form
    {
        private GameService _gameService;
        private readonly SaveLoadService _saveLoadService;
        private readonly string _saveFilePath = "savegame.json";

        private List<Quest> _currentQuests;
        private List<Item> _currentShopItems;

        public Form1()
        {
            InitializeComponent();
            _saveLoadService = new SaveLoadService();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            var defaultHero = new Warrior("Student");
            InitializeGameService(defaultHero);
        }

        private void InitializeGameService(Hero hero)
        {
            if (_gameService != null)
            {
                _gameService.OnGameStateChanged -= UpdateUI;
                _gameService.OnLogMessage -= AddLog;
            }

            _gameService = new GameService(hero);
            _gameService.OnGameStateChanged += UpdateUI;
            _gameService.OnLogMessage += AddLog;

            RefreshTavernAndShop();
            UpdateUI();
        }

        private void UpdateUI()
        {
            var hero = _gameService.GetHero();

            lblName.Text = $"Imie: {hero.Name}";
            lblLevel.Text = $"Poziom: {hero.Level}";
            lblGold.Text = $"Zloto: {hero.Gold}";
            lblDay.Text = $"Dzien: {hero.CurrentDay}";

            pbHp.Maximum = hero.MaxHp;
            pbHp.Value = Math.Min(hero.CurrentHp, hero.MaxHp);

            lblEnergy.Text = $"Energia: {hero.Energy}/{hero.MaxEnergy}";
            pbEnergy.Maximum = hero.MaxEnergy;
            pbEnergy.Value = Math.Min(hero.Energy, hero.MaxEnergy);

            lblStr.Text = $"STR: {hero.Strength}";
            lblDex.Text = $"DEX: {hero.Dexterity}";
            lblInt.Text = $"INT: {hero.Intelligence}";

            btnBuyStr.Text = $"+1 (Koszt: {_gameService.GetAttributeUpgradeCost(hero.Strength)})";
            btnBuyDex.Text = $"+1 (Koszt: {_gameService.GetAttributeUpgradeCost(hero.Dexterity)})";
            btnBuyInt.Text = $"+1 (Koszt: {_gameService.GetAttributeUpgradeCost(hero.Intelligence)})";

            lbBackpack.Items.Clear();
            foreach (var item in hero.Backpack)
            {
                lbBackpack.Items.Add($"{item.Name} | +{item.BonusStrength + item.BonusDexterity + item.BonusIntelligence + item.BonusArmour} Statystyki");
            }

            lbEquipped.Items.Clear();
            foreach (var item in hero.Equipped)
            {
                lbEquipped.Items.Add($"{item.Name} | +{item.BonusStrength + item.BonusDexterity + item.BonusIntelligence + item.BonusArmour} Statystyki");
            }
        }

        private void AddLog(string message)
        {
            lbLogs.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            lbLogs.TopIndex = lbLogs.Items.Count - 1;
        }

        private void RefreshTavernAndShop()
        {
            _currentQuests = _gameService.GenerateDailyQuests();
            lbQuests.Items.Clear();
            foreach (var quest in _currentQuests)
            {
                lbQuests.Items.Add($"[{quest.Difficulty}] {quest.Description} | Koszt: {quest.EnergyCost} | Nagroda: {quest.GoldReward}g");
            }

            _currentShopItems = _gameService.GenerateShopItems();
            lbShop.Items.Clear();
            foreach (var item in _currentShopItems)
            {
                lbShop.Items.Add($"{item.Name} [{item.Rarity}] | Cena: {item.Price} zlota");
            }
        }

        private void zapiszGręToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _saveLoadService.SaveGame(_gameService.GetHero(), _saveFilePath);
            AddLog("Stan gry zostal zapisany na dysku.");
        }

        private void wczytajGręToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var loadedHero = _saveLoadService.LoadGame(_saveFilePath);
            if (loadedHero != null)
            {
                InitializeGameService(loadedHero);
                AddLog("Stan gry zostal wczytany.");
            }
            else
            {
                MessageBox.Show("Nie znaleziono pliku zapisu.", "Blad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuyStr_Click(object sender, EventArgs e)
        {
            try { _gameService.UpgradeStrength(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Blad", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void btnBuyDex_Click(object sender, EventArgs e)
        {
            try { _gameService.UpgradeDexterity(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Blad", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void btnBuyInt_Click(object sender, EventArgs e)
        {
            try { _gameService.UpgradeIntelligence(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Blad", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void btnEquip_Click(object sender, EventArgs e)
        {
            var hero = _gameService.GetHero();
            try
            {
                if (lbBackpack.SelectedIndex != -1)
                {
                    _gameService.EquipItem(hero.Backpack[lbBackpack.SelectedIndex]);
                }
                else if (lbEquipped.SelectedIndex != -1)
                {
                    _gameService.UnequipItem(hero.Equipped[lbEquipped.SelectedIndex]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Blad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStartQuest_Click(object sender, EventArgs e)
        {
            if (lbQuests.SelectedIndex == -1) return;

            try
            {
                var selectedQuest = _currentQuests[lbQuests.SelectedIndex];
                _gameService.StartQuest(selectedQuest);
                _currentQuests.RemoveAt(lbQuests.SelectedIndex);
                lbQuests.Items.RemoveAt(lbQuests.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Blad energii", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEndDay_Click(object sender, EventArgs e)
        {
            _gameService.EndDay();
            RefreshTavernAndShop();
        }

        private void btnBuyItem_Click(object sender, EventArgs e)
        {
            if (lbShop.SelectedIndex == -1) return;

            try
            {
                var selectedItem = _currentShopItems[lbShop.SelectedIndex];
                _gameService.BuyItem(selectedItem);
                _currentShopItems.RemoveAt(lbShop.SelectedIndex);
                lbShop.Items.RemoveAt(lbShop.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Blad sklepu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}