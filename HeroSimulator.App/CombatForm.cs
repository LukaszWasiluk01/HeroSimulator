using HeroSimulator.Core.Models;
using HeroSimulator.Core.Services;

namespace HeroSimulator.App
{
    public partial class CombatForm : Form
    {
        private readonly GameService _gameService;
        private readonly CombatInfo _combatInfo;

        private int _heroHp;
        private int _enemyHp;
        private int _enemyMaxHp;
        private int _enemyDamage;

        private int _indicatorSpeed = 15;
        private int _indicatorDirection = 1;
        private bool _combatResolved = false;

        public CombatForm(GameService gameService, CombatInfo combatInfo)
        {
            InitializeComponent();
            _gameService = gameService;
            _combatInfo = combatInfo;

            pnlTimingBar.BackColor = Color.LightGray;

            pnlTimingBar.Controls.Add(pnlTargetArea);
            pnlTargetArea.BackColor = Color.LightBlue;
            pnlTargetArea.Top = 0;
            pnlTargetArea.Left = (pnlTimingBar.Width - pnlTargetArea.Width) / 2;
            pnlTargetArea.Height = pnlTimingBar.Height;

            pnlTimingBar.Controls.Add(pnlIndicator);
            pnlIndicator.BackColor = Color.Black;
            pnlIndicator.Top = 0;
            pnlIndicator.Left = 0;
            pnlIndicator.Height = pnlTimingBar.Height;

            pnlIndicator.BringToFront();

            InitializeCombat();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space)
            {
                if (btnAttack.Enabled)
                {
                    btnAttack.PerformClick();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void InitializeCombat()
        {
            var hero = _gameService.GetHero();
            _heroHp = hero.CurrentHp;

            _enemyHp = _combatInfo.EnemyMaxHp;
            _enemyMaxHp = _combatInfo.EnemyMaxHp;
            _enemyDamage = _combatInfo.EnemyDamage;

            pbHeroHp.Maximum = hero.MaxHp;
            pbHeroHp.Value = _heroHp;
            lblHeroInfo.Text = $"{hero.Name} (HP: {_heroHp}/{hero.MaxHp})";

            pbEnemyHp.Maximum = _enemyMaxHp;
            pbEnemyHp.Value = _enemyHp;
            lblEnemyInfo.Text = $"{_combatInfo.EnemyName} (HP: {_enemyHp}/{_enemyMaxHp})";

            tmrIndicator.Start();
        }

        private void tmrIndicator_Tick(object sender, EventArgs e)
        {
            pnlIndicator.Left += _indicatorSpeed * _indicatorDirection;

            if (pnlIndicator.Right >= pnlTimingBar.Width)
            {
                _indicatorDirection = -1;
            }
            else if (pnlIndicator.Left <= 0)
            {
                _indicatorDirection = 1;
            }
        }

        private async void btnAttack_Click(object sender, EventArgs e)
        {
            if (!btnAttack.Enabled)
            {
                return;
            }

            bool shouldPause = chkPauseIndicator.Checked;
            if (shouldPause)
            {
                tmrIndicator.Stop();
                btnAttack.Enabled = false;
            }

            int indicatorCenter = pnlIndicator.Left + (pnlIndicator.Width / 2);
            int targetLeftEdge = pnlTargetArea.Left;
            int targetRightEdge = pnlTargetArea.Right;

            bool isPerfectHit = indicatorCenter >= (targetLeftEdge - 5) && indicatorCenter <= (targetRightEdge + 5);

            CombatTurnResult turnResult = _gameService.ExecuteCombatTurn(_heroHp, _enemyHp, _enemyDamage, isPerfectHit);

            _heroHp = turnResult.HeroRemainingHp;
            _enemyHp = turnResult.EnemyRemainingHp;

            UpdateUIStats();

            string logMessage = "";
            if (turnResult.IsPerfectHit)
            {
                logMessage += "IDEALNE TRAFIENIE! ";
            }
            else
            {
                logMessage += "Zwykly cios. ";
            }

            if (turnResult.IsCriticalHit)
            {
                logMessage += $"KRYTYK! Zadasz {turnResult.HeroDamageDealt} dmg. ";
            }
            else
            {
                logMessage += $"Zadasz {turnResult.HeroDamageDealt} dmg. ";
            }

            logMessage += $"Otrzymujesz {turnResult.EnemyDamageDealt} dmg.";
            lblCombatLog.Text = logMessage;

            bool isCombatEnded = CheckCombatEnd();

            if (shouldPause && !isCombatEnded)
            {
                await Task.Delay(500);

                if (!this.IsDisposed)
                {
                    tmrIndicator.Start();
                    btnAttack.Enabled = true;
                    btnAttack.Focus();
                }
            }
        }

        private void UpdateUIStats()
        {
            var hero = _gameService.GetHero();

            pbHeroHp.Value = Math.Max(0, _heroHp);
            lblHeroInfo.Text = $"{hero.Name} (HP: {Math.Max(0, _heroHp)}/{hero.MaxHp})";

            pbEnemyHp.Value = Math.Max(0, _enemyHp);
            lblEnemyInfo.Text = $"{_combatInfo.EnemyName} (HP: {Math.Max(0, _enemyHp)}/{_enemyMaxHp})";
        }

        private bool CheckCombatEnd()
        {
            if (_enemyHp <= 0)
            {
                tmrIndicator.Stop();
                btnAttack.Enabled = false;
                MessageBox.Show("Zwyciestwo! Pokonales przeciwnika.", "Koniec Walki", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _gameService.ResolveCombat(_combatInfo, true, _heroHp);
                _combatResolved = true;
                this.Close();
                return true;
            }
            else if (_heroHp <= 0)
            {
                tmrIndicator.Stop();
                btnAttack.Enabled = false;
                MessageBox.Show("Porazka... Zostales pokonany.", "Koniec Walki", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _gameService.ResolveCombat(_combatInfo, false, 1);
                _combatResolved = true;
                this.Close();
                return true;
            }

            return false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_combatResolved)
            {
                tmrIndicator.Stop();
                _gameService.ResolveCombat(_combatInfo, false, Math.Max(1, _heroHp));
                _combatResolved = true;
            }
            base.OnFormClosing(e);
        }
    }
}