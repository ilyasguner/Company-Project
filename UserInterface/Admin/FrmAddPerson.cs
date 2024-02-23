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
    public partial class FrmAddPerson : Form
    {
        PersonnelManager personnelManager;
        DepartmentManager departmentManager;
        AuthManager authManager;
        public FrmAddPerson()//diğer sınıflarda kullanacağımız nesneleri new liyoruz
        {
            InitializeComponent();
            personnelManager = PersonnelManager.GetInstance();
            departmentManager = DepartmentManager.GetInstance();
            authManager = AuthManager.GetInstance();
        }

        private void FrmAddPerson_Load(object sender, EventArgs e)
        {
            ComboboxLoad();
        }
        void ComboboxLoad()
        {
            CmbDepartment.DataSource = departmentManager.GetList();//comboboxumuza bir adet veri kaynağı atıyoruz
            CmbDepartment.ValueMember = "Id";//comboboxta bizim işlem yapacağımız deper
            CmbDepartment.DisplayMember = "Name";//kullanıcının göreceği değer
            CmbDepartment.SelectedValue = 0;

            CmbAuth.DataSource = authManager.GetList();
            CmbAuth.ValueMember = "Id";
            CmbAuth.DisplayMember = "Name";
            CmbAuth.SelectedValue = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(TxtName.Text + " Personelini Kaydetmek İstiyor Musunuz ?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                //yeni personel eklemek için değerleri constructor ile bir nesneye atıyoruz nesneyi daha sonra veri tabanına göndereceğiz
                Personnel personnel = new Personnel(CmbDepartment.SelectedValue.ConInt(), CmbAuth.SelectedValue.ConInt(), MskSicilNo.Text, TxtName.Text);
                MessageBox.Show(personnelManager.Add(personnel));//dönen mesajı yansıtıyoruz

                var form =(FrmPersonnels) Application.OpenForms["FrmPersonnels"];//başka bir forma bağlanıp ona ait methodu çalıştırmaya yarıyor
                form.PersonnelList();
            }
        }
    }
}
