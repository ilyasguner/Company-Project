using DataAccess;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AdminManager
    {
        static AdminManager adminManager;
        AdminProcess adminProcess;

        public AdminManager()
        {
            adminProcess = AdminProcess.GetInstance();
        }

        

        public void Backup(string filePath, string fileName)
        {
            try
            {
                adminProcess.Backup(filePath, fileName);
            }
            catch
            {

                
            }
        }

        public static AdminManager GetInstance()
        {
            if (adminManager==null)
            {
                adminManager = new AdminManager();
            }
            return adminManager;
        }

    }
}
