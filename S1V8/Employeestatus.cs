using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1V8
{

    [Table("statusemployee", Schema = "harvest")]
    internal class Employeestatus
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
