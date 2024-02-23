using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public abstract class SqlService
    {
        //readonly ile yalnızca okunabilir yani dışardan müdahele edilemez string bir bağlantı adresi oluşturuyoruz
        readonly string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        SqlConnection connection;//sql bağlantı sınıfından bir nesne oluşturuyoruz
        public SqlService()
        {
            //connectionString = SecurityProcess.GetInstance().Decrypt("key", connectionString);//kriptoladığımız connection cümlesini burada çözüyoruz

            connection = new SqlConnection();//başlantıyı newliyoruz
            connection.ConnectionString = connectionString;//daha sonra bu bağlantıya adres olarak yukarıda aldığımız adres yolunu gösteriyoruz
        }

        SqlConnection OpenConnection()//readonly değişkenlere constructor içinde değer atıyabiliriz
        {
            if (connection.State == ConnectionState.Closed)//bağlantı durumu kapalı ise
            {
                connection.Open();//bağlantıyı aç
            }
            return connection;//ve bağlantıyı geri döndür
        }

        void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)//bağlantı açık ise
            {
                connection.Close();//bağlantıyı kapat
            }
        }

        public SqlCommand Execute(string commandText, params SqlParameter[] parameters) // update table set Age=15
        {

            //bir sqlkomutu oluşturuyoruz
            using (SqlCommand command = new SqlCommand())
            {

                command.CommandText = commandText;//dışardan gelen komut yazısını gönderiyoruz
                command.Connection = OpenConnection();//sql ile bağlantıyı açıyoruz
                command.CommandType = CommandType.Text;//komut tipini belirtebiliriz
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);//yolladığımız komut için parametreleri ekliyoruz
                }
                command.ExecuteNonQuery();//komut olarak yolladığımız sorguyu sqlde çalıştırıyoruz
                CloseConnection();//bağlantıyı kapatıyoruz
                return command;//sqlden bize bir değer dönmğyor aslında burada void de kullanabiliriz
            }
        }

        public SqlDataReader Reader(string commandText, params SqlParameter[] parameters) //select ....
        {
            using (SqlCommand command = new SqlCommand())
            {


                command.CommandText = commandText;
                command.Connection = OpenConnection();
                command.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                SqlDataReader dataReader = command.ExecuteReader();//dataReader sorgu sonrası sqlde gelen verileri okumamızı sağlıyor
                CloseConnection();
                return dataReader;//bu verileri geri döndürüyoruz
            }
        }

        public SqlCommand Stored(string commandText, params SqlParameter[] parameters)// exec PersonelGuncelle      //aslında yukarıda ki Execute methodunun aynısı
        {

            using (SqlCommand command = new SqlCommand())
            {

                command.CommandText = commandText;//ancak burada sql de yazdığımız bir storeprosedürü çalıştırıyoruz
                command.Connection = OpenConnection();
                command.CommandType = CommandType.StoredProcedure;//burada yolladğımız komutun tipini yazıyoruz storeprosedüre
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                command.ExecuteNonQuery();
                CloseConnection();
                return command;//buradan da gferiye değer dönmüyor dönse bile ...Dal kısmında onu almıyoruz aslında gereksiz void methotlar olabilir
            }
        }

        public SqlDataReader StoreReader(string commandText, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand())
            {


                command.CommandText = commandText;
                command.Connection = OpenConnection();
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                SqlDataReader dataReader = command.ExecuteReader();
                return dataReader;
            }
        }

        public DataTable GetDataTable(string commandText, params SqlParameter[] parameters)
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {

                dataAdapter.SelectCommand = Stored(commandText, parameters);

                using (DataTable dataTable = new DataTable())
                {

                    dataAdapter.Fill(dataTable);
                    return dataTable;

                }

            }
        }

    }
}
