using System;
using System.Windows.Forms;
using HeroSimulator.Core.Models.Entities;

namespace HeroSimulator.App
{
    public partial class CharacterCreationForm : Form
    {
        public Hero CreatedHero { get; private set; }

        public CharacterCreationForm()
        {
            InitializeComponent();
            cbClass.SelectedIndex = 0;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string name = string.IsNullOrWhiteSpace(tbName.Text) ? "Bohater" : tbName.Text;
            string selectedClass = cbClass.SelectedItem.ToString();

            if (selectedClass == "Mag")
                CreatedHero = new Mage(name);
            else if (selectedClass == "Zwiadowca")
                CreatedHero = new Scout(name);
            else
                CreatedHero = new Warrior(name);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}