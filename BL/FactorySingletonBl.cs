using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class FactorySingletonBl
    {
        private static Ibl instance = null;
        static FactorySingletonBl() { }
        public static Ibl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Bl_imp();
                }
                return instance;
            }
        }
    }
}