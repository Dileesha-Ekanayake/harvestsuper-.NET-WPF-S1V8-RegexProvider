using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1V8
{
    internal class GenderController
    {
        public static List<Gender> Get()
        {
            return GenderDao.GetAll();
        }
    }
}
