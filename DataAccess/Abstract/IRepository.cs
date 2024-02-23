using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRepository<T>//interface bir sınıf oluşturduk buradaki<T> ne yazarsak aşağıdada T o olur
    {
        List<T> GetList();//bu methotlar bütün sınıflarda kullanacağımız methotlar 
                          //bundan dolayı tekrar tekrar yazmayıp IRepositoryden kalıtım aldırıp T yerine o sınıfı koyarak
                          //diğer sınıflarda kolayca kullanabiliriz
        T Get(int id);

        string Add(T entity);

        string Update(T entity, string oldName);

        string Delete(int id);

    }
}
