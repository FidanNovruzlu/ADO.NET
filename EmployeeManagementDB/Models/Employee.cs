using Azure;
using System.Drawing;

namespace EmployeeManagementDB.Models
{
    internal class Employee
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public override string ToString()
        {
            return $"Name: {Name}   Surname: {Surname}";
        }
    }
}
