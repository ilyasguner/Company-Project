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
    public partial class FrmJobDetailProcess : Form
    {
        JobDetailManager detailManager;//kullanacağımız nesneleri oluşturuyoruz
        JobManager jobManager;
        Job job;
        bool start;
        string workerNo;
        public string employerNo;
        public FrmJobDetailProcess()//oluşturduğumuz nesneleri constructor içinde newliyoruz
        {
            InitializeComponent();
            jobManager = JobManager.GetInstance();
            detailManager = JobDetailManager.GetInstance();
        }

        private void FrmJobDetailProcess_Load(object sender, EventArgs e)//form yüklendiğinde otomatik çalışcak methodumuz
        {
            start = true;
            CmbPersonnelLoad();
            JobList();
            start = false;
        }
        void CmbPersonnelLoad()//personelleri listeleyen comboboxsun verikaynağını yüklüyoruz
        {
            CmbPersonnels.DataSource = PersonnelManager.GetInstance().GetList();//direkt olarak sınıfı kullanarakta nesneyi newleyip metohud kullanabiliriz
            CmbPersonnels.ValueMember = "PersonNo";//comboboxta işlem yapacağımız değerimiz
            CmbPersonnels.DisplayMember = "Name";//comboboxta gözükecek değer
            CmbPersonnels.SelectedValue = 0;//ilk yüklemede seçili olarak 0 değerini veriyoruz
        }

        void JobList()
        {
            DtgJobs.DataSource = jobManager.GetList();
            job = null;
            Clear();
        }

        private void DtgJobs_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (DtgJobs.CurrentRow == null)
            {
                MessageBox.Show("Öncelikle İş Bilgisi Seçiniz");
                return;
            }

            DataGridViewRow row = DtgJobs.CurrentRow;//seçilen satırı Dtg nesnesine eşitliyoruz

            job = new Job(//seçilen satırın hücrelerini entity kısmında oluşturduğumuz constructor yardımı ile ilk başta oluşturduğumuz job nesnesine atıyoruz
                row.Cells["Id"].Value.ConInt(),
                row.Cells["JobName"].Value.ToString(),
                row.Cells["JobContent"].Value.ToString(),
                row.Cells["Date"].Value.ConDate()
                );

            TxtJobName.Text = job.JobName;//seçilen satırın bilgilerini aşağıdaki textboxların içine yazdırıyoruz
            RchJobContent.Text = job.JobContent;
        }
        void Clear()//satırlarımızı temizleme methodumuz
        {
            TxtJobName.Text = ""; RchJobContent.Text = ""; CmbPersonnels.SelectedValue = 0; TxtDepartment.Text = "";
        }

        private void CmbPersonnels_SelectedValueChanged(object sender, EventArgs e)
        {//comboboxtan seçilen personeli
            if (start || CmbPersonnels.SelectedValue == null)//start true veya seçili değer yoksa direkt methottan çıkmasını istiyoruz
            {
                return;
            }

            workerNo = CmbPersonnels.SelectedValue.ToString();
            TxtDepartment.Text = DepartmentManager.GetInstance().GetDepartmentNameByPersonNo(workerNo);
        }

        private void TxtJobName_KeyPress(object sender, KeyPressEventArgs e)//kullanıcı textboxlarımıza müdahele edememesi için yazılan methotlar
        {
            e.Handled = true;
        }
        private void TxtJobName_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
        private void RchJobContent_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void RchJobContent_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            
        }
        private void TxtDepartment_KeyPress(object sender, KeyPressEventArgs e)//klavye tuşlarınız çalıştırmaz
        {
            e.Handled = true;
        }
        private void TxtDepartment_KeyDown(object sender, KeyEventArgs e)//backspace tuşunu çalıştırmaz
        {
            e.Handled = true;
        }

        private void BtnSendJob_Click(object sender, EventArgs e)
        {
            JobDetail jobDetail = new JobDetail(job.Id, employerNo, workerNo);//eklene işin entity değerlerini alıyoruz ve bir iş detay nesnesine atıyoruz
            string controlText = detailManager.IsJobDetailComplete(jobDetail);//control methodumuz

            if (controlText != "")
            {
                MessageBox.Show(controlText, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            DialogResult dr = MessageBox.Show(CmbPersonnels.Text + " Personeli İçin " + TxtJobName.Text + " Görevi Atanacaktır. Onaylıyor Musunuz", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                MessageBox.Show(detailManager.Add(jobDetail));//jobdetailmanagera oradanda jobdala yolluyoruz ve oradanda veri tabanına kaydediyoruz
                Clear();
            }
        }
    }
}
