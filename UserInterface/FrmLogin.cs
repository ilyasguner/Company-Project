using Business.Concrete;
using DataAccess.Concrete;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserInterface.Admin;
using UserInterface.Worker;

namespace UserInterface
{
    public partial class FrmLogin : Form
    {
        PersonnelManager personnelManager;
        public FrmLogin()
        {
            InitializeComponent();
            personnelManager = PersonnelManager.GetInstance();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//app config dosyasına yazdığımız bilgileri alıyoruz
            string version2 = config.AppSettings.Settings["version"].Value;

            //config.AppSettings.Settings["version"].Value = "test";
            //config.Save(ConfigurationSaveMode.Modified, true); config dosyasındaki bir verinin değerini değiştirirsek bu ekilde kaydedebiliriz
            string version = ConfigurationManager.AppSettings["version"];
            LblVersion.Text = version;
        }
        private void FrmLogin_Shown(object sender, EventArgs e)//form sayfası açıldığı gibi yapılacak olayları yazıyoruz
        {
            //MskSicil.Text = "902569";
            //MskSicil.Text = "563217";
            TxtPassword.Text = "123456";
            //BtnLogin.PerformClick();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (MskSicil.Text.Trim() == "" || TxtPassword.Text.Trim() == "")//kullanıcı adı ve şifresini alıyoruz
            {
                MessageBox.Show("Lütfen Sicil Numaranızla Birlikte Şifrenizi Giriniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            object[] infos = personnelManager.Login(MskSicil.Text, TxtPassword.Text);//kullanıcı adı ve şifreyi personnel dala yollayarak öyle bir kullanıcı varsa değerlerini alıyoruz
            
            if (infos == null)//eğer null ise kullanıcı adı veya şifre yanlıştır
            {
                MessageBox.Show("Hatalı Sicil No Veya Şifre Girdiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Sayın " + infos[2] + " Hoşgeldiniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (infos[5].ConInt() == 1)//yetki id 1 ise yetkili sayfasına değilse çalışan sayfasına yönlendiriyoruz
            {
                DialogResult dr = MessageBox.Show("Admin Pencerenizi Açmak İster Misiniz ? ", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    FrmAdminDashboard frmAdminDashboard = new FrmAdminDashboard();
                    frmAdminDashboard.infos = infos;//veri tabanından dönen kullanıcı bilgilerini diğer sayfalardaki infos ile o formlara yönlendiriyoruz
                    frmAdminDashboard.Show();
                }
                else
                {
                    ShowWorkerDahsboard(infos);
                }
                this.Hide();//bu pencereyi gizliyoruz kapatırsak eğer uygulama da kapanır
            }
            else
            {
                ShowWorkerDahsboard(infos);
                this.Hide();
            }
        }

        void ShowWorkerDahsboard(object[] infos)
        { 
            FrmWorkerDashboard frmWorkerDashboard = new FrmWorkerDashboard();
            frmWorkerDashboard.infos = infos;
            frmWorkerDashboard.Show();
        }

        private void LnkUpdatePassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MskSicil.Text.Trim()=="")
            {
                MessageBox.Show("Öncelikle Sicil Numarınızı Giriniz");
                return;
            }
            FrmUpdatePassword frmUpdatePassword = new FrmUpdatePassword();
            frmUpdatePassword.personNo = MskSicil.Text;
            frmUpdatePassword.ShowDialog();
        }
    }
}
