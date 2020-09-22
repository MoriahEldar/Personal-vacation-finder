using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class FactorySingletonDal
    {
        private static Idal instance = null;
        static FactorySingletonDal() { }
        public static Idal Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Dal_XML_imp();
                }
                return instance;
            }
        }
    }
}