namespace HeroSimulator.App
{
    partial class CombatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblHeroInfo = new Label();
            pbHeroHp = new ProgressBar();
            lblEnemyInfo = new Label();
            pbEnemyHp = new ProgressBar();
            pnlTimingBar = new Panel();
            pnlTargetArea = new Panel();
            pnlIndicator = new Panel();
            btnAttack = new Button();
            lblCombatLog = new Label();
            tmrIndicator = new System.Windows.Forms.Timer(components);
            chkPauseIndicator = new CheckBox();
            pnlTimingBar.SuspendLayout();
            SuspendLayout();
            // 
            // lblHeroInfo
            // 
            lblHeroInfo.AutoSize = true;
            lblHeroInfo.Font = new Font("Segoe UI", 12F);
            lblHeroInfo.Location = new Point(23, 20);
            lblHeroInfo.Name = "lblHeroInfo";
            lblHeroInfo.Padding = new Padding(0, 0, 0, 10);
            lblHeroInfo.Size = new Size(95, 31);
            lblHeroInfo.TabIndex = 1;
            lblHeroInfo.Text = "Bohater Info";
            // 
            // pbHeroHp
            // 
            pbHeroHp.ForeColor = Color.Green;
            pbHeroHp.Location = new Point(23, 56);
            pbHeroHp.Name = "pbHeroHp";
            pbHeroHp.Size = new Size(327, 35);
            pbHeroHp.TabIndex = 15;
            // 
            // lblEnemyInfo
            // 
            lblEnemyInfo.AutoSize = true;
            lblEnemyInfo.Font = new Font("Segoe UI", 12F);
            lblEnemyInfo.Location = new Point(450, 20);
            lblEnemyInfo.Name = "lblEnemyInfo";
            lblEnemyInfo.Padding = new Padding(0, 0, 0, 10);
            lblEnemyInfo.Size = new Size(115, 31);
            lblEnemyInfo.TabIndex = 16;
            lblEnemyInfo.Text = "Przeciwnik Info";
            // 
            // pbEnemyHp
            // 
            pbEnemyHp.ForeColor = Color.Green;
            pbEnemyHp.Location = new Point(450, 56);
            pbEnemyHp.Name = "pbEnemyHp";
            pbEnemyHp.Size = new Size(327, 35);
            pbEnemyHp.TabIndex = 17;
            // 
            // pnlTimingBar
            // 
            pnlTimingBar.Controls.Add(pnlTargetArea);
            pnlTimingBar.Location = new Point(198, 115);
            pnlTimingBar.Name = "pnlTimingBar";
            pnlTimingBar.Size = new Size(400, 50);
            pnlTimingBar.TabIndex = 18;
            // 
            // pnlTargetArea
            // 
            pnlTargetArea.BackColor = SystemColors.ActiveCaption;
            pnlTargetArea.Location = new Point(171, 0);
            pnlTargetArea.Name = "pnlTargetArea";
            pnlTargetArea.Size = new Size(60, 50);
            pnlTargetArea.TabIndex = 0;
            // 
            // pnlIndicator
            // 
            pnlIndicator.BackColor = SystemColors.ActiveCaptionText;
            pnlIndicator.Location = new Point(201, 115);
            pnlIndicator.Name = "pnlIndicator";
            pnlIndicator.Size = new Size(5, 50);
            pnlIndicator.TabIndex = 0;
            // 
            // btnAttack
            // 
            btnAttack.Location = new Point(215, 181);
            btnAttack.Name = "btnAttack";
            btnAttack.Size = new Size(367, 31);
            btnAttack.TabIndex = 19;
            btnAttack.Text = "ATAK (Spacja)";
            btnAttack.UseVisualStyleBackColor = true;
            btnAttack.Click += btnAttack_Click;
            // 
            // lblCombatLog
            // 
            lblCombatLog.Font = new Font("Segoe UI", 12F);
            lblCombatLog.Location = new Point(12, 224);
            lblCombatLog.Name = "lblCombatLog";
            lblCombatLog.Padding = new Padding(0, 0, 0, 10);
            lblCombatLog.Size = new Size(776, 31);
            lblCombatLog.TabIndex = 20;
            lblCombatLog.Text = "Rozpocznij walkę!";
            lblCombatLog.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tmrIndicator
            // 
            tmrIndicator.Interval = 15;
            tmrIndicator.Tick += tmrIndicator_Tick;
            // 
            // chkPauseIndicator
            // 
            chkPauseIndicator.AutoSize = true;
            chkPauseIndicator.Checked = true;
            chkPauseIndicator.CheckState = CheckState.Checked;
            chkPauseIndicator.Location = new Point(12, 258);
            chkPauseIndicator.Name = "chkPauseIndicator";
            chkPauseIndicator.Size = new Size(191, 19);
            chkPauseIndicator.TabIndex = 21;
            chkPauseIndicator.Text = "Zatrzymaj pasek po ataku (0.5s)";
            chkPauseIndicator.UseVisualStyleBackColor = true;
            // 
            // CombatForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 285);
            Controls.Add(chkPauseIndicator);
            Controls.Add(pnlIndicator);
            Controls.Add(lblCombatLog);
            Controls.Add(btnAttack);
            Controls.Add(pnlTimingBar);
            Controls.Add(pbEnemyHp);
            Controls.Add(lblEnemyInfo);
            Controls.Add(pbHeroHp);
            Controls.Add(lblHeroInfo);
            Name = "CombatForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Walka z przeciwnikiem";
            pnlTimingBar.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHeroInfo;
        private ProgressBar pbHeroHp;
        private Label lblEnemyInfo;
        private ProgressBar pbEnemyHp;
        private Panel pnlTimingBar;
        private Panel pnlIndicator;
        private Panel pnlTargetArea;
        private Button btnAttack;
        private Label lblCombatLog;
        private System.Windows.Forms.Timer tmrIndicator;
        private CheckBox chkPauseIndicator;
    }
}