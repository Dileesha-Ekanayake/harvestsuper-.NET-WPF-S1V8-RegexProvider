using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace S1V8
{
    internal class DesignationDao
    {
        public static List<Designation> GetAll()
        {
            using(DatabaseUtil dbContext =  new DatabaseUtil())
            {
                return dbContext.designation.ToList();
            }
        }
    }
}
