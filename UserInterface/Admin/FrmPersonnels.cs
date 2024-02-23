using Business.Concrete;
using DataAccess.Concrete;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface.Admin
{
    public partial class FrmPersonnels : Form
    {
        PersonnelManager personnelManager;
        DepartmentManager departmentManager;
        AuthManager authManager;
        Personnel personnel;
        List<Personnel> listPersonnels;
        int outNumber = 0;

        public FrmPersonnels()//constructor sınıf oluşturulduğunda çalışan method 
        {
            InitializeComponent();
            personnelManager = PersonnelManager.GetInstance();//buradaki nesneler bir kere newleniyor daha sonra oluşturulmuyor
            departmentManager = DepartmentManager.GetInstance();
            authManager = AuthManager.GetInstance();
        }

        private void FrmPersonnels_Load(object sender, EventArgs e)
        {
            ComboboxLoad();
            PersonnelList();
        }

        void ComboboxLoad()//comboboxlarımızın veri alacağı yerleri ve kullanacağı verileri seçiyoruz
        {
            CmbDepartment.DataSource = departmentManager.GetList();
            CmbDepartment.ValueMember = "Id";
            CmbDepartment.DisplayMember = "Name";
            CmbDepartment.SelectedValue = 0;

            CmbAuth.DataSource = authManager.GetList();
            CmbAuth.ValueMember = "Id";
            CmbAuth.DisplayMember = "Name";
            CmbAuth.SelectedValue = 0;
        }

        public void PersonnelList()
        {
            listPersonnels= personnelManager.GetList();

            DtgPersonnels.DataSource = listPersonnels;
            //DtgPersonnels.DataSource = personnelManager.GetTable();
            personnel = null;
            Clear();
        }

        private void DtgPersonnels_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (DtgPersonnels.CurrentRow == null)
            {
                MessageBox.Show("Öncelikle Listeden Personel Seçiniz");
                return;
            }

            personnel = new Personnel(//seçilen satırın bilgilerini bir personel listesine atıyoruz
                DtgPersonnels.CurrentRow.Cells["Id"].Value.ConInt(),
                DtgPersonnels.CurrentRow.Cells["DepartmentId"].Value.ConInt(),
                DtgPersonnels.CurrentRow.Cells["AuthId"].Value.ConInt(),
                DtgPersonnels.CurrentRow.Cells["PersonNo"].Value.ToString(),
                DtgPersonnels.CurrentRow.Cells["Name"].Value.ToString(),
                DtgPersonnels.CurrentRow.Cells["DepartmentName"].Value.ToString(),
                DtgPersonnels.CurrentRow.Cells["AuthName"].Value.ToString()
                );

            TxtPersonNo.Text = personnel.PersonNo;
            TxtName.Text = personnel.Name;
            CmbDepartment.SelectedValue = personnel.DepartmentId;
            CmbAuth.SelectedValue = personnel.AuthId;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (personnel == null)
            {
                MessageBox.Show("Öncelikle Personel Seçiniz");
                return;
            }
            DialogResult dr = MessageBox.Show(personnel.Name + " Personeli İçin Güncelleme Yapmak İstediğinize Emin Misiniz ?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)//personel bilgilerini yeni girilen bilgiler ile değiştiriyoruz
            {
                personnel.Name = TxtName.Text;
                personnel.DepartmentId = CmbDepartment.SelectedValue.ConInt();
                personnel.AuthId = CmbAuth.SelectedValue.ConInt();

                MessageBox.Show(personnelManager.Update(personnel));//personel nesnesini PersonelManagerı oradan PersonelDal yolluyoruz
                PersonnelList();
            }
        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonnelList();
        }

        void Clear()
        {
            TxtName.Text = ""; TxtPersonNo.Text = ""; CmbDepartment.SelectedValue = 0; CmbAuth.SelectedValue = 0;
        }

        private void TxtPersonNo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void BenDelete_Click(object sender, EventArgs e)
        {
            if (personnel == null)
            {
                MessageBox.Show("Öncelikle Personel Seçiniz");
                return;
            }
            DialogResult dr = MessageBox.Show(personnel.Name + " Personeli İçin Silme İşlemi Yapmak İstediğinize Emin Misiniz ?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                personnelManager.Delete(personnel.Id);
                PersonnelList();
            }
        }




        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FrmAddPerson frmAddPerson = new FrmAddPerson();
            frmAddPerson.ShowDialog();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)//kullanıcının arama yerine yazdığı değere göre arama yapan method
        {
            if (listPersonnels==null)
            {
                return;
            }
            if (listPersonnels.Count==0)
            {
                return;
            }

            List<Personnel> exampleList;
            string searchtext = TxtSearch.Text.Trim();

            if (int.TryParse(searchtext,out outNumber))//try parse içine aldığı ifadeyi inte çevirmeye çalışır çevirirse true çeviremezse false döndürür
            {
                exampleList = listPersonnels.Where(i => i.PersonNo.Contains(searchtext)).ToList();
                //eğer ifade inte çevrilirse demekki kullanıcı sayısal karakter girdi o zaman sicil noya göre arama yap
            }
            else 
            {
                exampleList = listPersonnels.Where(i => i.Name.ToLower().Contains(searchtext.ToLower())).ToList();
                //inte çevrilmezse false gelir ve else girer demek ki kullanıcı string ifade girdi
            }

            DtgPersonnels.DataSource = null;//önce listeyi sıfırlıyoruz
            DtgPersonnels.DataSource = exampleList;//filtrelediğimiz listeyi datagriedvieve yansıtıyoruz

            personnel = null;
            Clear();
        }
    }
}
