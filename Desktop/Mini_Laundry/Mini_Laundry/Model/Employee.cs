using Mini_Laundry.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Laundry.Model
{
    class Employee
    {
        private const string pk = "id_employee";
        public static DataRow find(int id)
        {
            return DataConnection.getData($"SELECT * FROM employees WHERE {Employee.pk} = {id}").Rows[0];
        }
        public static void create(string name, string email, string password, string date, string job, string address, int salary)
        {
            DataConnection.execQuery($"INSERT INTO employees (name, email, password, date_of_birth, job, address, salary) VALUES ('{name}', '{email}', '{password}', '{date}', '{job}', '{address}', {salary})");
        }
        public static void delete(int id)
        {
            DataConnection.execQuery($"DELETE FROM employees WHERE {Employee.pk} = {id}");
        }
        public static void update(string name, string email, string password, string date, string job, string address, int salary, int id)
        {
            DataConnection.execQuery($"UPDATE employees SET name = '{name}', email = '{email}', password = '{password}', date_of_birth = '{date}', job = '{job}', address = '{address}', salary = '{salary}' WHERE {Employee.pk} = {id}");
        }
    }
}
