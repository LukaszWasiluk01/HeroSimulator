using HeroSimulator.Core.Enums;
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
                catch (Exception)
                {
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
                    AddLog("Utworzono nowa postac.");
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

            RefreshTavern();
            RefreshShop();
            UpdateUI();
        }

        private string GetItemStatsInfo(Item item)
        {
            if (item == null)
            {
                return string.Empty;
            }

            var s = new List<string>();
            if (item.BonusStrength > 0)
                s.Add($"+{item.BonusStrength} STR");
            if (item.BonusDexterity > 0)
                s.Add($"+{item.BonusDexterity} DEX");
            if (item.BonusIntelligence > 0)
                s.Add($"+{item.BonusIntelligence} INT");
            if (item.BonusArmour > 0)
                s.Add($"+{item.BonusArmour} PANC");
            if (item.BonusLuck > 0)
                s.Add($"+{item.BonusLuck} LUCK");

            string stats = string.Join(", ", s);

            if (item.MaxSockets > 0)
            {
                stats += $" [Gniazda: {item.SocketedGems.Count}/{item.MaxSockets}]";

                if (item.SocketedGems.Count > 0)
                {
                    int gStr = 0, gDex = 0, gInt = 0, gArm = 0, gLuck = 0;

                    foreach (var gem in item.SocketedGems)
                    {
                        gStr += gem.BonusStrength;
                        gDex += gem.BonusDexterity;
                        gInt += gem.BonusIntelligence;
                        gArm += gem.BonusArmour;
                        gLuck += gem.BonusLuck;
                    }

                    var gs = new List<string>();
                    if (gStr > 0)
                        gs.Add($"+{gStr} STR");
                    if (gDex > 0)
                        gs.Add($"+{gDex} DEX");
                    if (gInt > 0)
                        gs.Add($"+{gInt} INT");
                    if (gArm > 0)
                        gs.Add($"+{gArm} PANC");
                    if (gLuck > 0)
                        gs.Add($"+{gLuck} LUCK");

                    if (gs.Count > 0)
                    {
                        stats += $" (Klejnoty: {string.Join(", ", gs)})";
                    }
                }
            }
            else if (item is Gem)
            {
                stats += $" [Klejnot]";
            }

            return stats;
        }

        private string GetSlotPolishName(ItemSlot slot)
        {
            return slot switch
            {
                ItemSlot.Weapon => "Bron",
                ItemSlot.Armor => "Zbroja",
                ItemSlot.Pants => "Spodnie",
                ItemSlot.Boots => "Buty",
                ItemSlot.Amulet => "Amulet",
                ItemSlot.Ring => "Pierscien",
                ItemSlot.None => "Brak",
                _ => slot.ToString()
            };
        }

        private void UpdateUI()
        {
            var h = _gameService.GetHero();
            string className = h is Warrior ? "Wojownik" : h is Mage ? "Mag" : "Zwiadowca";

            int bStr = 0, bDex = 0, bInt = 0, bArm = 0, bLuck = 0;

            foreach (var i in h.Equipment.Values)
            {
                bStr += i.BonusStrength;
                bDex += i.BonusDexterity;
                bInt += i.BonusIntelligence;
                bArm += i.BonusArmour;
                bLuck += i.BonusLuck;

                foreach (var g in i.SocketedGems)
                {
                    bStr += g.BonusStrength;
                    bDex += g.BonusDexterity;
                    bInt += g.BonusIntelligence;
                    bArm += g.BonusArmour;
                    bLuck += g.BonusLuck;
                }
            }

            lblName.Text = $"[{className.ToUpper()}] {h.Name} | DMG: {h.CalculateDamage()} | Pancerz: {h.Armour + bArm}";
            lblLevel.Text = $"Poziom: {h.Level} | HP: {h.CurrentHp}/{h.MaxHp} | POTEGA: {h.CalculateTotalPower()}";
            lblGold.Text = $"Zloto: {h.Gold}";
            lblDay.Text = $"Dzien: {h.CurrentDay}";

            if (tabControl1.TabPages.Count >= 3)
            {
                tabControl1.TabPages[2].Text = $"Sklep ({h.Gold}g)";
            }

            pbHp.Maximum = h.MaxHp;
            pbHp.Value = Math.Min(h.CurrentHp, h.MaxHp);
            lblEnergy.Text = $"Energia: {h.Energy}/{h.MaxEnergy}";
            pbEnergy.Maximum = h.MaxEnergy;
            pbEnergy.Value = Math.Min(h.Energy, h.MaxEnergy);

            lblStr.Text = $"STR: {h.Strength + bStr} ({h.Strength}+{bStr}) " + (h is Warrior ? "[+2 DMG/pkt]" : "");
            lblDex.Text = $"DEX: {h.Dexterity + bDex} ({h.Dexterity}+{bDex}) " + (h is Scout ? "[+2 DMG/pkt]" : "");
            lblInt.Text = $"INT: {h.Intelligence + bInt} ({h.Intelligence}+{bInt}) " + (h is Mage ? "[+3 DMG/pkt]" : "");
            lblLuck.Text = $"LUCK: {h.Luck + bLuck} ({h.Luck}+{bLuck}) [Szansa na kryt.]";

            btnBuyStr.Text = $"+1 ({_gameService.GetAttributeUpgradeCost(h.Strength)}g)";
            btnBuyDex.Text = $"+1 ({_gameService.GetAttributeUpgradeCost(h.Dexterity)}g)";
            btnBuyInt.Text = $"+1 ({_gameService.GetAttributeUpgradeCost(h.Intelligence)}g)";
            btnBuyLuck.Text = $"+1 ({_gameService.GetAttributeUpgradeCost(h.Luck)}g)";

            if (h.HeroCastle != null && h.HeroCastle.GemMine != null)
            {
                lblMineInfo.Text = $"Kopalnia Klejnotow (Poziom: {h.HeroCastle.GemMine.Level})";
                lblMineStorage.Text = $"Wykopane klejnoty: {h.HeroCastle.GemMine.StoredGems.Count}";
                btnUpgradeMine.Text = $"Ulepsz kopalnie ({h.HeroCastle.GemMine.GetUpgradeCost()}g)";
                btnCollectGems.Enabled = h.HeroCastle.GemMine.StoredGems.Count > 0;
            }

            lbBackpack.Items.Clear();
            foreach (var item in h.Backpack)
            {
                lbBackpack.Items.Add($"{item.Name} | {GetItemStatsInfo(item)} | Sprzedaj: {item.Price / 2}g");
            }

            lbEquipped.Items.Clear();
            foreach (ItemSlot slot in Enum.GetValues(typeof(ItemSlot)))
            {
                if (slot == ItemSlot.None)
                    continue;

                string slotName = GetSlotPolishName(slot);
                if (h.Equipment.TryGetValue(slot, out Item equippedItem))
                {
                    lbEquipped.Items.Add($"[{slotName}] {equippedItem.Name} ({GetItemStatsInfo(equippedItem)})");
                }
                else
                {
                    lbEquipped.Items.Add($"[{slotName}] Puste");
                }
            }
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
            {
                lbShop.Items.Add($"{i.Name} [{i.Rarity}] | {GetItemStatsInfo(i)} | Cena: {i.Price}g");
            }
        }

        private void btnBuyStr_Click(object sender, EventArgs e)
        {
            try
            {
                _gameService.UpgradeStrength();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuyDex_Click(object sender, EventArgs e)
        {
            try
            {
                _gameService.UpgradeDexterity();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuyInt_Click(object sender, EventArgs e)
        {
            try
            {
                _gameService.UpgradeIntelligence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuyLuck_Click(object sender, EventArgs e)
        {
            try
            {
                _gameService.UpgradeLuck();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            if (lbBackpack.SelectedIndex != -1)
            {
                _gameService.SellItem(_gameService.GetHero().Backpack[lbBackpack.SelectedIndex]);
            }
        }

        private void btnEquip_Click(object sender, EventArgs e)
        {
            try
            {
                var h = _gameService.GetHero();
                if (lbBackpack.SelectedIndex != -1)
                {
                    _gameService.EquipItem(h.Backpack[lbBackpack.SelectedIndex]);
                    lbBackpack.ClearSelected();
                }
                else if (lbEquipped.SelectedIndex != -1)
                {
                    var slots = Enum.GetValues(typeof(ItemSlot)).Cast<ItemSlot>().Where(s => s != ItemSlot.None).ToList();
                    ItemSlot selectedSlot = slots[lbEquipped.SelectedIndex];

                    if (h.Equipment.TryGetValue(selectedSlot, out Item itemToUnequip))
                    {
                        _gameService.UnequipItem(itemToUnequip);
                    }
                    lbEquipped.ClearSelected();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Blad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSocketGem_Click(object sender, EventArgs e)
        {
            try
            {
                var h = _gameService.GetHero();

                if (lbBackpack.SelectedIndex == -1)
                {
                    MessageBox.Show("Najpierw zaznacz klejnot w plecaku.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var selectedBackpackItem = h.Backpack[lbBackpack.SelectedIndex];
                if (!(selectedBackpackItem is Gem gem))
                {
                    MessageBox.Show("Zaznaczony przedmiot w plecaku nie jest klejnotem.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lbEquipped.SelectedIndex == -1)
                {
                    MessageBox.Show("Zaznacz zalozony przedmiot, w ktorym chcesz osadzic klejnot.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var slots = Enum.GetValues(typeof(ItemSlot)).Cast<ItemSlot>().Where(s => s != ItemSlot.None).ToList();
                ItemSlot selectedSlot = slots[lbEquipped.SelectedIndex];

                if (!h.Equipment.TryGetValue(selectedSlot, out Item targetItem))
                {
                    MessageBox.Show("W wybranym slocie nie ma zadnego przedmiotu.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _gameService.SocketGem(targetItem, gem);

                lbBackpack.ClearSelected();
                lbEquipped.ClearSelected();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Blad osadzania", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpgradeMine_Click(object sender, EventArgs e)
        {
            try
            {
                _gameService.UpgradeMine();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Blad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCollectGems_Click(object sender, EventArgs e)
        {
            try
            {
                _gameService.CollectGemsFromMine();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnStartQuest_Click(object sender, EventArgs e)
        {
            if (lbQuests.SelectedIndex == -1)
            {
                return;
            }

            try
            {
                var quest = _currentQuests[lbQuests.SelectedIndex];

                _gameService.PayEnergyForQuest(quest);

                using (var combatForm = new CombatForm(_gameService, quest))
                {
                    combatForm.ShowDialog();
                }

                RefreshTavern();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEndDay_Click(object sender, EventArgs e)
        {
            lbLogs.Items.Clear();
            _gameService.EndDay();
            RefreshTavern();
            RefreshShop();
        }

        private void btnBuyItem_Click(object sender, EventArgs e)
        {
            if (lbShop.SelectedIndex == -1)
            {
                return;
            }

            try
            {
                var item = _currentShopItems[lbShop.SelectedIndex];
                _gameService.BuyItem(item);
                _currentShopItems.RemoveAt(lbShop.SelectedIndex);
                lbShop.Items.RemoveAt(lbShop.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            if (File.Exists(_saveFilePath))
            {
                File.Delete(_saveFilePath);
            }
            Application.Restart();
        }

        private void btnSaveGame_Click(object sender, EventArgs e)
        {
            _saveLoadService.SaveGame(_gameService.GetHero(), _saveFilePath);
            AddLog("Zapisano gre.");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_gameService != null)
            {
                _saveLoadService.SaveGame(_gameService.GetHero(), _saveFilePath);
            }
        }
    }
}