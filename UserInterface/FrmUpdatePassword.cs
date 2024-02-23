using Business.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface
{
    public partial class FrmUpdatePassword : Form
    {
        PersonnelManager personnelManager;
         public string personNo;

        public FrmUpdatePassword()
        {
            InitializeComponent();
            personnelManager = PersonnelManager.GetInstance();
        }

        private void ChkLookPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkLookPassword.Checked)
            {
                TxtPassword1.UseSystemPasswordChar = false;
                TxtPassword2.UseSystemPasswordChar = false;
            }
            else
            {
                TxtPassword1.UseSystemPasswordChar = true;
                TxtPassword2.UseSystemPasswordChar = true;
            }
        }

        private void BtnNewPassword_Click(object sender, EventArgs e)
        {
            string controlText = personnelManager.IsPasswordComplete(TxtPassword1.Text, TxtPassword2.Text);
            if (controlText!="")
            {
                MessageBox.Show(controlText);
                return;
            }

            controlText = personnelManager.UpdatePassword(personNo, TxtPassword1.Text.Trim());
            if (controlText=="1")
            {
                MessageBox.Show("Şifre Başarıyla Güncellenmiştir","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                Close();
            }
            else if (controlText=="-1")
            {
                MessageBox.Show("Bir Hata Oldu Lütfen Yetkili İle Görüşünüz","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            
        }

        private void FrmUpdatePassword_Load(object sender, EventArgs e)
        {

        }
    }
}
