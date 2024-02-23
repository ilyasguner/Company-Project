using DataAccess.Abstract;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public  class AdminProcess
    {
        static AdminProcess adminProcess;
        SqlService sqlService;
        public AdminProcess()
        {
            sqlService = SqlDatabase.GetInstance();
        }

        public void Backup(string filePath,string fileName)
        {
            try
            {
                sqlService.Stored("YedekAl", new SqlParameter("@dosyaYolu", filePath), new SqlParameter("@dosyaIsmi", fileName));
            }
            catch 
            {

            }
        }

        public static AdminProcess GetInstance()
        {
            if (adminProcess==null)
            {
                adminProcess = new AdminProcess();
            }
            return adminProcess;
        }
    }
}
