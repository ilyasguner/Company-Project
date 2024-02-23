using Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    internal class LogService
    {
        
            static LogService logService;
            UserInfo userInfo;
            string logFilePath, logInfoPath, logErrorPath;

            public LogService()
            {
                userInfo = new UserInfo(UserRealName.realName, Environment.MachineName, Environment.UserName);//kullanıcı bilgileri
                logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Company_Log";//log kayıtlarımızı tutacağımız bir klasör oluşruturyoruz
                //environment sınıfından öncelikle klasörümüzün nereye oluşturulcağını seçiyoruz sonra da da klasörümüzün adını giriyoruz

                if (!Directory.Exists(logFilePath))//exist==var mı    logfilePath ile klasör oluşrutulmuş mu onu kontrol ediyor
                {
                    Directory.CreateDirectory(logFilePath);//eğer yoksa klasörü oluşturuyoruz klasör işlemlerinde directory kullanıyoruz
                }
                
                logInfoPath = logFilePath + "\\info.txt";//yapılan işlemlerin tutulacağı dosya oluşturduğumuz klasöre addını girerek dosyayı oluşruyoruz
               /* if (!File.Exists(logInfoPath))
                {
                    File.Create(logInfoPath);//creat komutu hatalı çalışıyor
                }*/

                logErrorPath = logFilePath + "\\error.txt";//hata kayıtlarımızı tutacağımız dosyamız
               /* if (!File.Exists(logErrorPath))
                {
                    File.Create(logErrorPath);  dosya işlemlerinde File kullanılıyor
                }*/
            }

            string UserInfos()//ilk başta kullanıcı ve bilgisayar bilgileri
            {
                return userInfo.Name + " " + userInfo.MachineName + " " + userInfo.UserName + " " + DateTime.Now.ToLocalTime() + " Method Name : ";
            }

            string GetParameters(params object[] parameters)//gelen bilgileri string bir ifade halinde yazdırığ döndüren method
            {
                string text = "";
                foreach (var item in parameters)
                {
                    text += item + " / ";
                }
                return text;
            }

            public string Info(string methodName, params object[] parameters)//dosyaya yazdıran methot
            {
                File.AppendAllText(logInfoPath, "\n" + UserInfos() + "(" + methodName + ") " + GetParameters(parameters));//eğer dosya yoksa onuda direkt oluşturuyor
            
                return "1";
            }

            public string Error(string errorMessage, string methodName, params object[] parameters)
            {
                File.AppendAllText(logErrorPath, "\n" + UserInfos() + "(" + methodName + ") " + GetParameters(parameters) + " Error Message: " + errorMessage);
                return "-1";
            }


            public static LogService GetInstance()//getınstace methodumuz
            {
                if (logService == null)
                {
                    logService = new LogService();
                }
                return logService;
            }

        }

        internal class UserInfo//constructor oluşrutduğumuz sınıf
        {
            string name, machineName, userName;

            public string Name { get => name; set => name = value; }
            public string MachineName { get => machineName; set => machineName = value; }
            public string UserName { get => userName; set => userName = value; }

            public UserInfo(string name, string machineName, string userName)
            {
                this.name = name;
                this.machineName = machineName;
                this.userName = userName;
            }
        }
    
}
