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
                        return;
                    }
                }
                catch (Exception) { }
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
                }
                else
                    Application.Exit();
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

            RefreshTavern();
            RefreshShop();
            UpdateUI();
        }

        private string GetItemStatsInfo(Item item)
        {
            if (item == null)
                return string.Empty;
            var s = new List<string>();
            if (item.BonusStrength > 0)
                s.Add($"+{item.BonusStrength} STR");
            if (item.BonusDexterity > 0)
                s.Add($"+{item.BonusDexterity} DEX");
            if (item.BonusIntelligence > 0)
                s.Add($"+{item.BonusIntelligence} INT");
            if (item.BonusArmour > 0)
                s.Add($"+{item.BonusArmour} PANC");
            return string.Join(", ", s);
        }

        private void UpdateUI()
        {
            var h = _gameService.GetHero();
            string className = h is Warrior ? "Wojownik" : h is Mage ? "Mag" : "Zwiadowca";

            int bStr = 0, bDex = 0, bInt = 0, bArm = 0;
            var eq = new List<Item> { h.EquippedWeapon, h.EquippedArmor, h.EquippedPants, h.EquippedBoots, h.EquippedAmulet, h.EquippedRing };
            foreach (var i in eq.Where(x => x != null))
            {
                bStr += i.BonusStrength;
                bDex += i.BonusDexterity;
                bInt += i.BonusIntelligence;
                bArm += i.BonusArmour;
            }

            lblName.Text = $"[{className.ToUpper()}] {h.Name} | DMG: {h.CalculateDamage()} | Pancerz: {h.Armour + bArm}";
            lblLevel.Text = $"Poziom: {h.Level} | HP: {h.CurrentHp}/{h.MaxHp}";
            lblGold.Text = $"Zloto: {h.Gold}";
            lblDay.Text = $"Dzien: {h.CurrentDay}";

            if (tabControl1.TabPages.Count >= 3)
                tabControl1.TabPages[2].Text = $"Sklep ({h.Gold}g)";

            pbHp.Maximum = h.MaxHp;
            pbHp.Value = Math.Min(h.CurrentHp, h.MaxHp);
            lblEnergy.Text = $"Energia: {h.Energy}/{h.MaxEnergy}";
            pbEnergy.Maximum = h.MaxEnergy;
            pbEnergy.Value = Math.Min(h.Energy, h.MaxEnergy);

            lblStr.Text = $"STR: {h.Strength + bStr} ({h.Strength}+{bStr}) " + (h is Warrior ? "[+2 DMG/pkt]" : "");
            lblDex.Text = $"DEX: {h.Dexterity + bDex} ({h.Dexterity}+{bDex}) " + (h is Scout ? "[+2 DMG/pkt]" : "");
            lblInt.Text = $"INT: {h.Intelligence + bInt} ({h.Intelligence}+{bInt}) " + (h is Mage ? "[+3 DMG/pkt]" : "");

            btnBuyStr.Text = $"+1 ({_gameService.GetAttributeUpgradeCost(h.Strength)}g)";
            btnBuyDex.Text = $"+1 ({_gameService.GetAttributeUpgradeCost(h.Dexterity)}g)";
            btnBuyInt.Text = $"+1 ({_gameService.GetAttributeUpgradeCost(h.Intelligence)}g)";

            lbBackpack.Items.Clear();
            foreach (var item in h.Backpack)
                lbBackpack.Items.Add($"{item.Name} | {GetItemStatsInfo(item)} | Sprzedaj: {item.Price / 2}g");

            lbEquipped.Items.Clear();
            lbEquipped.Items.Add(h.EquippedWeapon != null ? $"[Bron] {h.EquippedWeapon.Name} ({GetItemStatsInfo(h.EquippedWeapon)})" : "[Bron] Puste");
            lbEquipped.Items.Add(h.EquippedArmor != null ? $"[Zbroja] {h.EquippedArmor.Name} ({GetItemStatsInfo(h.EquippedArmor)})" : "[Zbroja] Puste");
            lbEquipped.Items.Add(h.EquippedPants != null ? $"[Spodnie] {h.EquippedPants.Name} ({GetItemStatsInfo(h.EquippedPants)})" : "[Spodnie] Puste");
            lbEquipped.Items.Add(h.EquippedBoots != null ? $"[Buty] {h.EquippedBoots.Name} ({GetItemStatsInfo(h.EquippedBoots)})" : "[Buty] Puste");
            lbEquipped.Items.Add(h.EquippedAmulet != null ? $"[Amulet] {h.EquippedAmulet.Name} ({GetItemStatsInfo(h.EquippedAmulet)})" : "[Amulet] Puste");
            lbEquipped.Items.Add(h.EquippedRing != null ? $"[Pierscien] {h.EquippedRing.Name} ({GetItemStatsInfo(h.EquippedRing)})" : "[Pierscien] Puste");
        }

        private void AddLog(string message)
        {
            lbLogs.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            lbLogs.TopIndex = lbLogs.Items.Count - 1;
        }

        private void RefreshTavern()
        {
            _currentQuests = _gameService.GenerateDailyQuests(3);
            lbQuests.Items.Clear();
            foreach (var q in _currentQuests)
            {
                var s = _gameService.GetEnemyStats(q.Difficulty);
                lbQuests.Items.Add($"[{q.Difficulty}] {q.Description} | Wrog: {s.Hp}HP, {s.Damage}DMG ({q.EnergyCost} energii)");
            }
        }

        private void RefreshShop()
        {
            _currentShopItems = _gameService.GenerateShopItems(5);
            lbShop.Items.Clear();
            foreach (var i in _currentShopItems)
                lbShop.Items.Add($"{i.Name} [{i.Rarity}] | {GetItemStatsInfo(i)} | Cena: {i.Price}g");
        }

        private void btnBuyStr_Click(object sender, EventArgs e)
        {
            try
            {
                _gameService.UpgradeStrength();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnBuyDex_Click(object sender, EventArgs e)
        {
            try
            {
                _gameService.UpgradeDexterity();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnBuyInt_Click(object sender, EventArgs e)
        {
            try
            {
                _gameService.UpgradeIntelligence();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            if (lbBackpack.SelectedIndex != -1)
                _gameService.SellItem(_gameService.GetHero().Backpack[lbBackpack.SelectedIndex]);
        }

        private void btnEquip_Click(object sender, EventArgs e)
        {
            try
            {
                var h = _gameService.GetHero();
                if (lbBackpack.SelectedIndex != -1)
                    _gameService.EquipItem(h.Backpack[lbBackpack.SelectedIndex]);
                else if (lbEquipped.SelectedIndex != -1)
                {
                    int idx = lbEquipped.SelectedIndex;
                    if (idx == 0 && h.EquippedWeapon != null)
                        _gameService.UnequipItem(h.EquippedWeapon);
                    else if (idx == 1 && h.EquippedArmor != null)
                        _gameService.UnequipItem(h.EquippedArmor);
                    else if (idx == 2 && h.EquippedPants != null)
                        _gameService.UnequipItem(h.EquippedPants);
                    else if (idx == 3 && h.EquippedBoots != null)
                        _gameService.UnequipItem(h.EquippedBoots);
                    else if (idx == 4 && h.EquippedAmulet != null)
                        _gameService.UnequipItem(h.EquippedAmulet);
                    else if (idx == 5 && h.EquippedRing != null)
                        _gameService.UnequipItem(h.EquippedRing);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnStartQuest_Click(object sender, EventArgs e)
        {
            if (lbQuests.SelectedIndex == -1)
                return;
            try
            {
                _gameService.StartQuest(_currentQuests[lbQuests.SelectedIndex]);
                RefreshTavern();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnEndDay_Click(object sender, EventArgs e)
        {
            _gameService.EndDay();
            RefreshTavern();
            RefreshShop();
        }

        private void btnBuyItem_Click(object sender, EventArgs e)
        {
            if (lbShop.SelectedIndex == -1)
                return;
            try
            {
                var item = _currentShopItems[lbShop.SelectedIndex];
                _gameService.BuyItem(item);
                _currentShopItems.RemoveAt(lbShop.SelectedIndex);
                lbShop.Items.RemoveAt(lbShop.SelectedIndex);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            if (File.Exists(_saveFilePath))
                File.Delete(_saveFilePath);
            Application.Restart();
        }

        private void btnSaveGame_Click(object sender, EventArgs e)
        {
            _saveLoadService.SaveGame(_gameService.GetHero(), _saveFilePath);
            AddLog("Zapisano.");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_gameService != null)
                _saveLoadService.SaveGame(_gameService.GetHero(), _saveFilePath);
        }
    }
}