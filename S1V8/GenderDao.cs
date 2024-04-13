using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace S1V8
{
    internal class GenderDao
    {
        public static List<Gender> GetAll()
        {
            using(DatabaseUtil dbContext =  new DatabaseUtil())
            {
                return dbContext.gender.ToList();
            }
        }

    }
}
