using System;

namespace Company.DataAccess.Employees
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ChangedAt { get; set; } = null;

        public DateTime? HiredAt { get; set; } = null;

        public int? DepartmentId { get; set; } = null;
    }
}
