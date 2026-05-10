namespace HeroSimulator.App
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            plikToolStripMenuItem = new ToolStripMenuItem();
            btnRestartGame = new ToolStripMenuItem();
            btnSaveGame = new ToolStripMenuItem();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            btnBuyInt = new Button();
            btnBuyDex = new Button();
            btnBuyStr = new Button();
            label11 = new Label();
            pbHp = new ProgressBar();
            lbEquipped = new ListBox();
            label10 = new Label();
            btnEquip = new Button();
            lbBackpack = new ListBox();
            label9 = new Label();
            lblInt = new Label();
            lblDex = new Label();
            lblStr = new Label();
            label5 = new Label();
            lblDay = new Label();
            lblGold = new Label();
            lblLevel = new Label();
            lblName = new Label();
            tabPage2 = new TabPage();
            label2 = new Label();
            btnEndDay = new Button();
            btnStartQuest = new Button();
            lbLogs = new ListBox();
            lbQuests = new ListBox();
            label1 = new Label();
            pbEnergy = new ProgressBar();
            lblEnergy = new Label();
            tabPage3 = new TabPage();
            btnBuyItem = new Button();
            lbShop = new ListBox();
            label3 = new Label();
            menuStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { plikToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // plikToolStripMenuItem
            // 
            plikToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { btnRestartGame, btnSaveGame });
            plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            plikToolStripMenuItem.Size = new Size(38, 20);
            plikToolStripMenuItem.Text = "Plik";
            // 
            // btnRestartGame
            // 
            btnRestartGame.Name = "btnRestartGame";
            btnRestartGame.Size = new Size(180, 22);
            btnRestartGame.Text = "Zacznij od nowa";
            // 
            // btnSaveGame
            // 
            btnSaveGame.Name = "btnSaveGame";
            btnSaveGame.Size = new Size(180, 22);
            btnSaveGame.Text = "Zapisz grę";
            btnSaveGame.Click += btnSaveGame_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(12, 27);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(776, 411);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnBuyInt);
            tabPage1.Controls.Add(btnBuyDex);
            tabPage1.Controls.Add(btnBuyStr);
            tabPage1.Controls.Add(label11);
            tabPage1.Controls.Add(pbHp);
            tabPage1.Controls.Add(lbEquipped);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(btnEquip);
            tabPage1.Controls.Add(lbBackpack);
            tabPage1.Controls.Add(label9);
            tabPage1.Controls.Add(lblInt);
            tabPage1.Controls.Add(lblDex);
            tabPage1.Controls.Add(lblStr);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(lblDay);
            tabPage1.Controls.Add(lblGold);
            tabPage1.Controls.Add(lblLevel);
            tabPage1.Controls.Add(lblName);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(768, 383);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Postać";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnBuyInt
            // 
            btnBuyInt.Location = new Point(667, 107);
            btnBuyInt.Name = "btnBuyInt";
            btnBuyInt.Size = new Size(87, 23);
            btnBuyInt.TabIndex = 18;
            btnBuyInt.Text = "+1 (Koszt: 10)";
            btnBuyInt.UseVisualStyleBackColor = true;
            btnBuyInt.Click += btnBuyInt_Click;
            // 
            // btnBuyDex
            // 
            btnBuyDex.Location = new Point(667, 76);
            btnBuyDex.Name = "btnBuyDex";
            btnBuyDex.Size = new Size(87, 23);
            btnBuyDex.TabIndex = 17;
            btnBuyDex.Text = "+1 (Koszt: 10)";
            btnBuyDex.UseVisualStyleBackColor = true;
            btnBuyDex.Click += btnBuyDex_Click;
            // 
            // btnBuyStr
            // 
            btnBuyStr.Location = new Point(667, 45);
            btnBuyStr.Name = "btnBuyStr";
            btnBuyStr.Size = new Size(87, 23);
            btnBuyStr.TabIndex = 16;
            btnBuyStr.Text = "+1 (Koszt: 10)";
            btnBuyStr.UseVisualStyleBackColor = true;
            btnBuyStr.Click += btnBuyStr_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F);
            label11.Location = new Point(16, 134);
            label11.Name = "label11";
            label11.Padding = new Padding(0, 0, 0, 10);
            label11.Size = new Size(33, 31);
            label11.TabIndex = 15;
            label11.Text = "HP:";
            // 
            // pbHp
            // 
            pbHp.Location = new Point(55, 134);
            pbHp.Name = "pbHp";
            pbHp.Size = new Size(327, 23);
            pbHp.TabIndex = 14;
            // 
            // lbEquipped
            // 
            lbEquipped.FormattingEnabled = true;
            lbEquipped.ItemHeight = 15;
            lbEquipped.Location = new Point(388, 199);
            lbEquipped.Name = "lbEquipped";
            lbEquipped.Size = new Size(366, 169);
            lbEquipped.TabIndex = 13;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F);
            label10.Location = new Point(388, 165);
            label10.Name = "label10";
            label10.Padding = new Padding(0, 0, 0, 10);
            label10.Size = new Size(73, 31);
            label10.TabIndex = 11;
            label10.Text = "Założone";
            // 
            // btnEquip
            // 
            btnEquip.Location = new Point(107, 165);
            btnEquip.Name = "btnEquip";
            btnEquip.Size = new Size(96, 23);
            btnEquip.TabIndex = 9;
            btnEquip.Text = "Załóż / Zdejmij";
            btnEquip.UseVisualStyleBackColor = true;
            btnEquip.Click += btnEquip_Click;
            // 
            // lbBackpack
            // 
            lbBackpack.FormattingEnabled = true;
            lbBackpack.ItemHeight = 15;
            lbBackpack.Location = new Point(16, 199);
            lbBackpack.Name = "lbBackpack";
            lbBackpack.Size = new Size(366, 169);
            lbBackpack.TabIndex = 8;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F);
            label9.Location = new Point(16, 165);
            label9.Name = "label9";
            label9.Padding = new Padding(0, 0, 0, 10);
            label9.Size = new Size(85, 31);
            label9.TabIndex = 6;
            label9.Text = "Ekwipunek";
            // 
            // lblInt
            // 
            lblInt.AutoSize = true;
            lblInt.Font = new Font("Segoe UI", 12F);
            lblInt.Location = new Point(388, 106);
            lblInt.Name = "lblInt";
            lblInt.Padding = new Padding(0, 0, 0, 10);
            lblInt.Size = new Size(37, 31);
            lblInt.TabIndex = 7;
            lblInt.Text = "INT:";
            // 
            // lblDex
            // 
            lblDex.AutoSize = true;
            lblDex.Font = new Font("Segoe UI", 12F);
            lblDex.Location = new Point(388, 75);
            lblDex.Name = "lblDex";
            lblDex.Padding = new Padding(0, 0, 0, 10);
            lblDex.Size = new Size(41, 31);
            lblDex.TabIndex = 6;
            lblDex.Text = "DEX:";
            // 
            // lblStr
            // 
            lblStr.AutoSize = true;
            lblStr.Font = new Font("Segoe UI", 12F);
            lblStr.Location = new Point(388, 44);
            lblStr.Name = "lblStr";
            lblStr.Padding = new Padding(0, 0, 0, 10);
            lblStr.Size = new Size(40, 31);
            lblStr.TabIndex = 5;
            lblStr.Text = "STR:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(388, 13);
            label5.Name = "label5";
            label5.Padding = new Padding(0, 0, 0, 10);
            label5.Size = new Size(70, 31);
            label5.TabIndex = 4;
            label5.Text = "Atrybuty";
            // 
            // lblDay
            // 
            lblDay.AutoSize = true;
            lblDay.Font = new Font("Segoe UI", 12F);
            lblDay.Location = new Point(16, 106);
            lblDay.Name = "lblDay";
            lblDay.Padding = new Padding(0, 0, 0, 10);
            lblDay.Size = new Size(52, 31);
            lblDay.TabIndex = 3;
            lblDay.Text = "Dzień:";
            // 
            // lblGold
            // 
            lblGold.AutoSize = true;
            lblGold.Font = new Font("Segoe UI", 12F);
            lblGold.Location = new Point(16, 75);
            lblGold.Name = "lblGold";
            lblGold.Padding = new Padding(0, 0, 0, 10);
            lblGold.Size = new Size(49, 31);
            lblGold.TabIndex = 2;
            lblGold.Text = "Złoto:";
            // 
            // lblLevel
            // 
            lblLevel.AutoSize = true;
            lblLevel.Font = new Font("Segoe UI", 12F);
            lblLevel.Location = new Point(16, 44);
            lblLevel.Name = "lblLevel";
            lblLevel.Padding = new Padding(0, 0, 0, 10);
            lblLevel.Size = new Size(64, 31);
            lblLevel.TabIndex = 1;
            lblLevel.Text = "Poziom:";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 12F);
            lblName.Location = new Point(16, 13);
            lblName.Name = "lblName";
            lblName.Padding = new Padding(0, 0, 0, 10);
            lblName.Size = new Size(43, 31);
            lblName.TabIndex = 0;
            lblName.Text = "Imię:";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(btnEndDay);
            tabPage2.Controls.Add(btnStartQuest);
            tabPage2.Controls.Add(lbLogs);
            tabPage2.Controls.Add(lbQuests);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(pbEnergy);
            tabPage2.Controls.Add(lblEnergy);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(768, 383);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Karczma";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(384, 55);
            label2.Name = "label2";
            label2.Padding = new Padding(0, 0, 0, 10);
            label2.Size = new Size(131, 31);
            label2.TabIndex = 13;
            label2.Text = "Dziennik zdarzeń:";
            // 
            // btnEndDay
            // 
            btnEndDay.Location = new Point(384, 354);
            btnEndDay.Name = "btnEndDay";
            btnEndDay.Size = new Size(366, 23);
            btnEndDay.TabIndex = 12;
            btnEndDay.Text = "ZAKOŃCZ DZIEŃ";
            btnEndDay.UseVisualStyleBackColor = true;
            btnEndDay.Click += btnEndDay_Click;
            // 
            // btnStartQuest
            // 
            btnStartQuest.Location = new Point(15, 354);
            btnStartQuest.Name = "btnStartQuest";
            btnStartQuest.Size = new Size(366, 23);
            btnStartQuest.TabIndex = 11;
            btnStartQuest.Text = "Wyrusz na misję";
            btnStartQuest.UseVisualStyleBackColor = true;
            btnStartQuest.Click += btnStartQuest_Click;
            // 
            // lbLogs
            // 
            lbLogs.FormattingEnabled = true;
            lbLogs.ItemHeight = 15;
            lbLogs.Location = new Point(384, 89);
            lbLogs.Name = "lbLogs";
            lbLogs.Size = new Size(366, 259);
            lbLogs.TabIndex = 10;
            // 
            // lbQuests
            // 
            lbQuests.FormattingEnabled = true;
            lbQuests.ItemHeight = 15;
            lbQuests.Location = new Point(15, 89);
            lbQuests.Name = "lbQuests";
            lbQuests.Size = new Size(366, 259);
            lbQuests.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(15, 55);
            label1.Name = "label1";
            label1.Padding = new Padding(0, 0, 0, 10);
            label1.Size = new Size(120, 31);
            label1.TabIndex = 3;
            label1.Text = "Dostępne misje:";
            // 
            // pbEnergy
            // 
            pbEnergy.Location = new Point(154, 13);
            pbEnergy.Name = "pbEnergy";
            pbEnergy.Size = new Size(596, 23);
            pbEnergy.TabIndex = 2;
            // 
            // lblEnergy
            // 
            lblEnergy.AutoSize = true;
            lblEnergy.Font = new Font("Segoe UI", 12F);
            lblEnergy.Location = new Point(15, 13);
            lblEnergy.Name = "lblEnergy";
            lblEnergy.Padding = new Padding(0, 0, 0, 10);
            lblEnergy.Size = new Size(65, 31);
            lblEnergy.TabIndex = 1;
            lblEnergy.Text = "Energia:";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(btnBuyItem);
            tabPage3.Controls.Add(lbShop);
            tabPage3.Controls.Add(label3);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(768, 383);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Sklep";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnBuyItem
            // 
            btnBuyItem.Location = new Point(15, 354);
            btnBuyItem.Name = "btnBuyItem";
            btnBuyItem.Size = new Size(747, 23);
            btnBuyItem.TabIndex = 13;
            btnBuyItem.Text = "Kup wybrany przedmiot";
            btnBuyItem.UseVisualStyleBackColor = true;
            btnBuyItem.Click += btnBuyItem_Click;
            // 
            // lbShop
            // 
            lbShop.FormattingEnabled = true;
            lbShop.ItemHeight = 15;
            lbShop.Location = new Point(15, 46);
            lbShop.Name = "lbShop";
            lbShop.Size = new Size(747, 304);
            lbShop.TabIndex = 12;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(15, 12);
            label3.Name = "label3";
            label3.Padding = new Padding(0, 0, 0, 10);
            label3.Size = new Size(197, 31);
            label3.TabIndex = 2;
            label3.Text = "Dzisiejsza oferta handlarza:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Hero Simulator";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem plikToolStripMenuItem;
        private ToolStripMenuItem btnRestartGame;
        private ToolStripMenuItem btnSaveGame;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label lblName;
        private TabPage tabPage3;
        private Label label9;
        private Label lblInt;
        private Label lblDex;
        private Label lblStr;
        private Label label5;
        private Label lblDay;
        private Label lblGold;
        private Label lblLevel;
        private Label label10;
        private Button btnEquip;
        private ListBox lbBackpack;
        private Label label11;
        private ProgressBar pbHp;
        private ListBox lbEquipped;
        private Button btnBuyInt;
        private Button btnBuyDex;
        private Button btnBuyStr;
        private ProgressBar pbEnergy;
        private Label lblEnergy;
        private Label label1;
        private Label label2;
        private Button btnEndDay;
        private Button btnStartQuest;
        private ListBox lbLogs;
        private ListBox lbQuests;
        private Button btnBuyItem;
        private ListBox lbShop;
        private Label label3;
    }
}
