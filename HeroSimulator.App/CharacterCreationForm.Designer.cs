namespace HeroSimulator.App
{
    partial class CharacterCreationForm
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
            label1 = new Label();
            label2 = new Label();
            tbName = new TextBox();
            label3 = new Label();
            cbClass = new ComboBox();
            btnCreate = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(74, 18);
            label1.Name = "label1";
            label1.Padding = new Padding(0, 0, 0, 10);
            label1.Size = new Size(177, 31);
            label1.TabIndex = 4;
            label1.Text = "TWORZENIE BOHATERA";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(12, 49);
            label2.Name = "label2";
            label2.Padding = new Padding(0, 0, 0, 10);
            label2.Size = new Size(85, 31);
            label2.TabIndex = 5;
            label2.Text = "Podaj imię:";
            // 
            // tbName
            // 
            tbName.Location = new Point(12, 83);
            tbName.Name = "tbName";
            tbName.Size = new Size(296, 23);
            tbName.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(12, 120);
            label3.Name = "label3";
            label3.Padding = new Padding(0, 0, 0, 10);
            label3.Size = new Size(109, 31);
            label3.TabIndex = 7;
            label3.Text = "Wybierz klasę:";
            // 
            // cbClass
            // 
            cbClass.FormattingEnabled = true;
            cbClass.Items.AddRange(new object[] { "Wojownik", "Zwiadowca", "Mag" });
            cbClass.Location = new Point(12, 154);
            cbClass.Name = "cbClass";
            cbClass.Size = new Size(296, 23);
            cbClass.TabIndex = 8;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(12, 203);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(296, 23);
            btnCreate.TabIndex = 12;
            btnCreate.Text = "Rozpocznij przygodę";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // CharacterCreationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(320, 262);
            Controls.Add(btnCreate);
            Controls.Add(cbClass);
            Controls.Add(label3);
            Controls.Add(tbName);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "CharacterCreationForm";
            Text = "CharacterCreationForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox tbName;
        private Label label3;
        private ComboBox cbClass;
        private Button btnCreate;
    }
}