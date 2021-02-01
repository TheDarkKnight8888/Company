using System;

namespace Company.DataAccess.Departments
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ChangedAt { get; set; } = null;
    }
}
