using Business.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface.Admin
{
    public partial class FrmBackUp : Form
    {
        public FrmBackUp()
        {
            InitializeComponent();
        }

        private void BtnGozAt_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = folderBrowserDialog1.SelectedPath;
                TxtPath.Text = filePath;
            }
        }

        private void FrmBackUp_Load(object sender, EventArgs e)
        {

        }

        private void BtnYedekle_Click(object sender, EventArgs e)
        {
            if (TxtPath.Text == @"C:\")
            {
                MessageBox.Show("Lütfen C dizini içinde bir klasör seçiniz");
                return;
            }
            if (string.IsNullOrEmpty(TxtPath.Text))
            {
                MessageBox.Show("Dosya Yolu Seçiniz");
                return;
            }
            if (string.IsNullOrEmpty(TxtName.Text))
            {
                MessageBox.Show("Dosya İsmi Belirtiniz");
                return;
            }

            string fileName = TxtName.Text.Trim();
            fileName = Path.GetFileNameWithoutExtension(fileName);
            fileName = @"\" + fileName + ".bak";

            AdminManager.GetInstance().Backup(TxtPath.Text.Trim(), fileName);
            System.Diagnostics.Process.Start(TxtPath.Text.Trim());
        }


    }
}
