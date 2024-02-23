using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Database
{
    class SqlDatabase : SqlService//SqlDatabase sınıfını SqlService den kalıtım aldırıyoruz
    {
        static SqlDatabase sqlDatabase;//oluşturduğumuz sqlDatabase nesnesi sqlsevice methotlarını kullanabilir

        public static SqlDatabase GetInstance()//bu şekilde yapmamız aslında bir nevi güvenlik önlemi
        {
            if (sqlDatabase == null)
            {
                sqlDatabase = new SqlDatabase();//sqlDatabase nesnesini burada newleyip geri döndürüyoruz            
            }
            return sqlDatabase;
        }

    }
}
