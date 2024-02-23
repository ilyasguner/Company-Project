using DataAccess.Abstract;
using DataAccess.Database;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class PersonnelDal : IRepository<Personnel>//IRepositoryden kalıtım aldırıyoruz böylece moethotlar Personnel sınıfına göre geliyor direkt
    {
        static PersonnelDal personnelDal;
        LogService logService;
        SqlService sqlService;//sql işlemerini yaptığımız sınıftan nesne oluşturuyoruz
        SqlDataReader dataReader;//sql'den gelen verileri okumamızı sağlayan sınıf
        SecurityProcess securityProcess;
        bool result;//kontrol değişkenimiz

        public PersonnelDal()
        {
            sqlService = SqlDatabase.GetInstance();//nesnemizi constuructar içinde newliyoruz
            logService = LogService.GetInstance();
            securityProcess = SecurityProcess.GetInstance();
        }
        public string Add(Personnel entity)
        {
            try
            {
                dataReader = sqlService.StoreReader("PersonelEkle", new SqlParameter("@sicilno", entity.PersonNo), new SqlParameter("@adsoyad", entity.Name),
                    new SqlParameter("@departman", entity.DepartmentId), new SqlParameter("@yetkiid", entity.AuthId));
                if (dataReader.Read())
                {
                    result = dataReader[0].ConBool();
                }
                dataReader.Close();
                if (result)
                {
                    return entity.PersonNo + " Sicil Numarası Daha Önce Kullanılmış";
                }
                //return entity.Name + " Personel Kaydı Başarıyla Tamamlanmıştır";
                return logService.Info("Personel Ekleme", "Sicil No: " + entity.PersonNo, "İsim: " + entity.Name, "Departman Id: " + entity.DepartmentId, "Yetki Id: " + entity.AuthId);

            }
            catch (Exception ex)
            {
               return logService.Error(ex.Message, "Personel Ekleme", "Sicil No: " + entity.PersonNo, "İsim: " + entity.Name, "Departman Id: " + entity.DepartmentId, "Yetki Id: " + entity.AuthId);

            }
        }

        public string Delete(int id)
        {
            try
            {
                sqlService.Stored("PersonelSil", new SqlParameter("@id", id));
                return "Personel Başarıyla Silindi";
            }
            catch (Exception ex)
            {
                return ex.Message; 
            }
        }

        public Personnel Get(int id)
        {
            return null;
        }

        public List<Personnel> GetList()
        {
            try
            {
                List<Personnel> personnels = new List<Personnel>();
                dataReader = sqlService.StoreReader("PersonelListesi");
                while (dataReader.Read())
                {
                    personnels.Add(new Personnel(dataReader["ID"].ConInt(), dataReader["DEPARTMANID"].ConInt(), dataReader["YETKIID"].ConInt(), dataReader["SICILNO"].ToString(), dataReader["AD_SOYAD"].ToString(), dataReader["DEPARTMAN_AD"].ToString(), dataReader["YETKI_AD"].ToString()));
                }//personel bilgilerini çekip entity katmanında oluşturduğumuz constructor ile newleyip listemize ekliyoruz
                dataReader.Close();
                return personnels;
            }
            catch
            {
                return new List<Personnel>();
            }
        }

        public DataTable GetTable()
        {
            try
            {
                DataTable dataTable = sqlService.GetDataTable("PersonelListesi");
                return dataTable;
            }
            catch 
            {

                return null;
            }
        }

        public string Update(Personnel entity, string oldName)
        {
            try
            {
                sqlService.Stored("PersonelGuncelle", new SqlParameter("@sicilno", entity.PersonNo), new SqlParameter("@adsoyad", entity.Name), new SqlParameter("@departmanId", entity.DepartmentId), new SqlParameter("@yetkiId", entity.AuthId));
                return entity.Name + " Personeli Başarıyla Güncellendi";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static PersonnelDal GetInstance()//bu method sayesinde personelmanagerdan gelen nesne burada bir kere newleniyor
        {
            if (personnelDal == null)//nullsa newle değilse direkt döndür böylece fazladan nesne oluşturup program performansını azaltmayalım
            {
                personnelDal = new PersonnelDal();
            }
            return personnelDal;
        }

        public object[] Login(string personNo, string password)
        {//giriş yapmak için personel nosu ve şifresini alıp veri tabanında eğer o personel varsa bilgilerini çekiyoruz
            try
            {
                //string value = securityProcess.Encrypt("key", "Server=DESKTOP-DLPFOKN; Database=DBKURUMSAL; Integrated Security=True;");

                object[] infos = null;
                string password2 = securityProcess.Encrypt("key", password);
                dataReader = sqlService.StoreReader("PersonelLogin", new SqlParameter("@sicilno", personNo), new SqlParameter("@sifre", password2));
                if (dataReader.Read())
                {
                    string name, departmentName, authName; int id, departmentId, authId;

                    id = dataReader["ID"].ConInt();
                    name = dataReader["AD_SOYAD"].ToString();
                    departmentId = dataReader["DEPARTMANID"].ConInt();
                    departmentName = dataReader["DEPARTMAN_AD"].ToString();
                    authId = dataReader["YETKIID"].ConInt();
                    authName = dataReader["YETKI_AD"].ToString();

                    infos = new object[] { id, personNo, name, departmentId, departmentName, authId, authName };
                }
                dataReader.Close();
                return infos;//farklı değerler olduğu için object tipinde bir dizi oluşturduk eğer şifre veya no yanlışsa null değer olacak
            }
            catch
            {
                return null;
            }
        }

        public string GetPersonnelNameByPersonNo(string personNo)//sicil noya göre personel ismi getiren methodumuz
        {
            try
            {
                string personnelName = "";
                dataReader = sqlService.StoreReader("PersonelIsmi", new SqlParameter("@sicilno", personNo));
                if (dataReader.Read())
                {
                    personnelName = dataReader["AD_SOYAD"].ToString();//gelen değeri eşitleyip döndürüyoruz
                }
                dataReader.Close();
                return personnelName;
            }
            catch
            {
                return "";
            }
        }

        public string UpdatePassword(string personNo,string password)
        {
            try
            {
                string password2 = securityProcess.Encrypt("key", password);//öncelikle girilen şifreyi kriptoluyoruz
                dataReader = sqlService.StoreReader("SifreGuncelle", new SqlParameter("@sicilno", personNo), new SqlParameter("@sifre", password2));
                dataReader.Close();
                return logService.Info("Şifre Güncelleme", "Sicil No:" + personNo);//şifreyi log dosyamıza tabiki yazdırmıyoruz
            }
            catch (Exception ex)
            {

                return logService.Error(ex.Message, "Şifre Güncelleme", "Sicil No:" + personNo);
            }
        }
    }
}
