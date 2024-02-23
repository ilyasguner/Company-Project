using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Auth
    {
        int id; string name;//propertylerimizi yazıyoruz

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }//encapsüle işlemi ile get ve set atıyoruz

        public Auth(string name)
        {
            this.name = name;
        }                             //farklı amaçlar için kullnacağımız constructorlarımızı yazıyoruz

        public Auth(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
