using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography;

namespace S1V8
{
    internal class EmployeeDao
    {
        
        public static List<Employee> Get(string query)
        {
            //Ensures that each operation gets its own isolated instance of the database context,
            //which helps in managing transactions and ensuring data consistency.
            using (var dbContext = new DatabaseUtil())
            {
                return dbContext.employee.Include(e => e.Gender).Include(e => e.Designation).Include(e => e.Employeestatus).ToList();
            }
        }
        public static List<Employee> GetAll()
        {

            return Get("SELECT * FROM employee");
            
        }

        public static List<Employee> GetAllByName(string name)
        {
            using (var dbContext = new DatabaseUtil())
            {
                return dbContext.employee
                    .Where(e => e.Name.ToLower().StartsWith(name.ToLower()))
                    .Select(e => new Employee 
                    {   
                        Id = e.Id, 
                        Name = e.Name, 
                        Nic = e.Nic,
                        DOB = e.DOB,
                        Mobile = e.Mobile,
                        Email = e.Email,
                        Gender = e.Gender,
                        Designation = e.Designation,
                        Employeestatus = e.Employeestatus,
                    })
                    .ToList();
            }
        }

        public static List<Employee> GetAllByGender(Gender gender)
        {
            using(var dbContext = new DatabaseUtil())
            {
                return dbContext.employee
                    .Where(e => e.Gender == gender)
                    .Select(e => new Employee
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Nic = e.Nic,
                        DOB = e.DOB,
                        Mobile = e.Mobile,
                        Email = e.Email,
                        Gender = e.Gender,
                        Designation = e.Designation,
                        Employeestatus = e.Employeestatus,
                    })
                    .ToList();
            }
        }

        public static List<Employee> GetAllByNameAndGender(string name, Gender gender)
        {
            using(var dbContext = new DatabaseUtil())
            {
                return dbContext.employee
                    .Where(e => e.Name.ToLower().StartsWith(name.ToLower()) && e.Gender == gender)
                    .Select(e => new Employee
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Nic = e.Nic,
                        DOB = e.DOB,
                        Mobile = e.Mobile,
                        Email = e.Email,
                        Gender = e.Gender,
                        Designation = e.Designation,
                        Employeestatus = e.Employeestatus,
                    })
                    .ToList();
            }
        }

        public static List<Employee> GetByNic(string nic)
        {
            using (var dbContext = new DatabaseUtil())
            {
                return dbContext.employee
                    .Where(e => e.Nic == nic)
                    .Select(e => new Employee
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Nic = e.Nic,
                        DOB = e.DOB,
                        Mobile = e.Mobile,
                        Email = e.Email,
                        Gender = e.Gender,
                        Designation = e.Designation,
                        Employeestatus = e.Employeestatus,
                    })
                    .ToList();
            }
        }


        public static string Save(Employee employee)
        {
            string msg = "";

            using (var dbContext = new DatabaseUtil())
            {
                dbContext.Database.EnsureCreated();
                /*
                To save an Employee entity with foreign key relationships to Gender, Designation, 
                and Employeestatus entities, you need to ensure that the referenced entities 
                (Gender, Designation, Employeestatus) are correctly associated with the 
                Employee entity before saving it to the database.              
                */
                Gender gender = dbContext.gender.FirstOrDefault(g => g.Id == employee.Gender.Id);
                Designation designation = dbContext.designation.FirstOrDefault(d => d.Id == employee.Designation.Id);
                Employeestatus employeestatus = dbContext.statusemployee.FirstOrDefault(s => s.Id == employee.Employeestatus.Id);

                if (gender != null && designation != null && employeestatus != null)
                {
                    Employee newEmployee = new Employee
                    {
                        Name = employee.Name,
                        Nic = employee.Nic,
                        DOB = employee.DOB,
                        Mobile = employee.Mobile,
                        Email = employee.Email,
                        Gender = gender,
                        Designation = designation,
                        Employeestatus = employeestatus
                    };

                    dbContext.employee.Add(newEmployee);
                    dbContext.SaveChanges();

                    msg = "1";
                }
                else
                {
                    msg = "Database error";
                }
            }

            return msg;
        }

        public static string Update(Employee employee)
        {
            string msg = "";

            using (var dbContext = new DatabaseUtil())
            {
                Gender gender = dbContext.gender.FirstOrDefault(g => g.Id == employee.Gender.Id);
                Designation designation = dbContext.designation.FirstOrDefault(d => d.Id == employee.Designation.Id);
                Employeestatus employeestatus = dbContext.statusemployee.FirstOrDefault(s => s.Id == employee.Employeestatus.Id);

                var emp = dbContext.employee.FirstOrDefault(e => e.Id == employee.Id);

                if (emp != null)
                {
                    emp.Name = employee.Name;
                    emp.Nic = employee.Nic;
                    emp.DOB = employee.DOB;
                    emp.Mobile = employee.Mobile;
                    emp.Email = employee.Email;
                    emp.Gender = gender;
                    emp.Designation = designation;
                    emp.Employeestatus = employeestatus;

                    dbContext.SaveChanges();

                    msg = "1";
                }
                else
                {
                    msg = "Employee not found.";
                }
            }

            return msg;
        }
        public static string Delete(Employee employee)
        {
            string msg = "";

            using (var dbContext = new DatabaseUtil())
            {
                var emp = dbContext.employee.FirstOrDefault(e => e.Id == employee.Id);

                if (emp != null)
                {
                    dbContext.employee.Remove(emp);
                    dbContext.SaveChanges();

                    msg = "1";
                }
                else
                {
                    msg = "Employee not found.";
                }
            }

            return msg;
        }


        //public static List<Employee> GetAllByName(string name)
        //{
        //   using (var dbContext = new DatabaseUtil())
        //   {
        //       string sqlQuery = @"
        //   SELECT *
        //   FROM employee
        //   WHERE LOWER(name) LIKE LOWER({0} + '%')";

        //       return dbContext.employee
        //           .FromSqlRaw(sqlQuery, name)
        //           .ToList();
        //   }
        //}

    }
}
