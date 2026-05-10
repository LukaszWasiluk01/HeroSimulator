using System;
using System.Collections.Generic;
using System.IO;
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
            if (File.Exists(_saveFilePath))
            {
                try
                {
                    var loadedHero = _saveLoadService.LoadGame(_saveFilePath);
                    if (loadedHero != null)
                    {
                        InitializeGameService(loadedHero);
                        AddLog("Wczytano zapis gry.");
                        return;
                    }
                }
                catch (Exception)
                {
                    AddLog("Blad wczytywania pliku. Utworz nowa postac.");
                }
            }

            ShowCharacterCreation();
        }

        private void ShowCharacterCreation()
        {
            using (var creationForm = new CharacterCreationForm())
            {
                if (creationForm.ShowDialog() == DialogResult.OK)
                {
                    InitializeGameService(creationForm.CreatedHero);
                    _saveLoadService.SaveGame(_gameService.GetHero(), _saveFilePath);
                }
                else
                {
                    Application.Exit();
                }
            }
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

        private string FormatItemStats(Item item)
        {
            var stats = new List<string>();
            if (item.BonusStrength > 0) stats.Add($"+{item.BonusStrength} STR");
            if (item.BonusDexterity > 0) stats.Add($"+{item.BonusDexterity} DEX");
            if (item.BonusIntelligence > 0) stats.Add($"+{item.BonusIntelligence} INT");
            if (item.BonusArmour > 0) stats.Add($"+{item.BonusArmour} PANCERZ");
            return string.Join(", ", stats);
        }

        private void UpdateUI()
        {
            var hero = _gameService.GetHero();

            lblName.Text = $"Imie: {hero.Name}";
            lblLevel.Text = $"Poziom: {hero.Level}";
            lblGold.Text = $"Zloto: {hero.Gold}";
            lblDay.Text = $"Dzien: {hero.CurrentDay}";

            if (tabControl1.TabPages.Count >= 3)
            {
                tabControl1.TabPages[2].Text = $"Sklep (Zloto: {hero.Gold})";
            }

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
                lbBackpack.Items.Add($"{item.Name} | {FormatItemStats(item)}");
            }

            lbEquipped.Items.Clear();
            lbEquipped.Items.Add(hero.EquippedWeapon != null ? $"[Bron] {hero.EquippedWeapon.Name} | {FormatItemStats(hero.EquippedWeapon)}" : "[Bron] Puste");
            lbEquipped.Items.Add(hero.EquippedArmor != null ? $"[Zbroja] {hero.EquippedArmor.Name} | {FormatItemStats(hero.EquippedArmor)}" : "[Zbroja] Puste");
            lbEquipped.Items.Add(hero.EquippedPants != null ? $"[Spodnie] {hero.EquippedPants.Name} | {FormatItemStats(hero.EquippedPants)}" : "[Spodnie] Puste");
            lbEquipped.Items.Add(hero.EquippedBoots != null ? $"[Buty] {hero.EquippedBoots.Name} | {FormatItemStats(hero.EquippedBoots)}" : "[Buty] Puste");
            lbEquipped.Items.Add(hero.EquippedAmulet != null ? $"[Amulet] {hero.EquippedAmulet.Name} | {FormatItemStats(hero.EquippedAmulet)}" : "[Amulet] Puste");
            lbEquipped.Items.Add(hero.EquippedRing != null ? $"[Pierscien] {hero.EquippedRing.Name} | {FormatItemStats(hero.EquippedRing)}" : "[Pierscien] Puste");
        }

        private void AddLog(string message)
        {
            lbLogs.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            lbLogs.TopIndex = lbLogs.Items.Count - 1;
        }

        private void RefreshTavernAndShop()
        {
            _currentQuests = _gameService.GenerateDailyQuests(3);
            lbQuests.Items.Clear();
            foreach (var quest in _currentQuests)
            {
                lbQuests.Items.Add($"[{quest.Difficulty}] {quest.Description} ({quest.EnergyCost} energii)");
            }

            _currentShopItems = _gameService.GenerateShopItems();
            lbShop.Items.Clear();
            foreach (var item in _currentShopItems)
            {
                lbShop.Items.Add($"{item.Name} [{item.Rarity}] | {FormatItemStats(item)} | Cena: {item.Price}g");
            }
        }

        private void btnBuyStr_Click(object sender, EventArgs e)
        {
            try { _gameService.UpgradeStrength(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnBuyDex_Click(object sender, EventArgs e)
        {
            try { _gameService.UpgradeDexterity(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnBuyInt_Click(object sender, EventArgs e)
        {
            try { _gameService.UpgradeIntelligence(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnEquip_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbBackpack.SelectedIndex != -1)
                {
                    _gameService.EquipItem(_gameService.GetHero().Backpack[lbBackpack.SelectedIndex]);
                }
                else if (lbEquipped.SelectedIndex != -1)
                {
                    var hero = _gameService.GetHero();
                    int index = lbEquipped.SelectedIndex;

                    if (index == 0 && hero.EquippedWeapon != null) _gameService.UnequipItem(hero.EquippedWeapon);
                    else if (index == 1 && hero.EquippedArmor != null) _gameService.UnequipItem(hero.EquippedArmor);
                    else if (index == 2 && hero.EquippedPants != null) _gameService.UnequipItem(hero.EquippedPants);
                    else if (index == 3 && hero.EquippedBoots != null) _gameService.UnequipItem(hero.EquippedBoots);
                    else if (index == 4 && hero.EquippedAmulet != null) _gameService.UnequipItem(hero.EquippedAmulet);
                    else if (index == 5 && hero.EquippedRing != null) _gameService.UnequipItem(hero.EquippedRing);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnStartQuest_Click(object sender, EventArgs e)
        {
            if (lbQuests.SelectedIndex == -1) return;
            try
            {
                _gameService.StartQuest(_currentQuests[lbQuests.SelectedIndex]);
                _currentQuests = _gameService.GenerateDailyQuests(3);
                lbQuests.Items.Clear();
                foreach (var quest in _currentQuests)
                {
                    lbQuests.Items.Add($"[{quest.Difficulty}] {quest.Description} ({quest.EnergyCost} energii)");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
                _gameService.BuyItem(_currentShopItems[lbShop.SelectedIndex]);
                _currentShopItems.RemoveAt(lbShop.SelectedIndex);
                lbShop.Items.RemoveAt(lbShop.SelectedIndex);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Czy na pewno usunac zapis?", "Reset", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (File.Exists(_saveFilePath)) File.Delete(_saveFilePath);
                Application.Restart();
            }
        }

        private void btnSaveGame_Click(object sender, EventArgs e)
        {
            if (_gameService != null)
            {
                _saveLoadService.SaveGame(_gameService.GetHero(), _saveFilePath);
                AddLog("Gra zostala zapisana.");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_gameService != null)
                _saveLoadService.SaveGame(_gameService.GetHero(), _saveFilePath);
        }
    }
}