using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S1V8
{
    [Table("employee", Schema = "harvest")]
    internal class Employee
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [RegularExpression("^([A-Z][a-z]*[.]?[\\s]?)*([A-Z][a-z]*)$", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }

        [Column("dob")]
        public DateTime DOB { get; set; }

        [Column("nic")]
        [RegularExpression("^(([\\d]{9}[vVxX])|([\\d]{12}))$", ErrorMessage = "Invalid NIC")]
        public string Nic { get; set; }

        [Column("email")]
        [RegularExpression("^[a-z]*@[a-z]*.[a-z]*$" , ErrorMessage = "Invlid Email")]
        public string Email { get; set; }

        [Column("mobile")]
        [RegularExpression("^0[0-9]{9}$" , ErrorMessage = "Invalid Mobile")]
        public string Mobile { get; set; }

        [ForeignKey("Gender")]
        [Column("gender_id")]
        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        [ForeignKey("Designation")]
        [Column("designation_id")]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }

        [ForeignKey("Employeestatus")]
        [Column("statusemployee_id")]
        public int EmployeeStatusId { get; set; }
        public Employeestatus Employeestatus { get; set; }
    }
}
