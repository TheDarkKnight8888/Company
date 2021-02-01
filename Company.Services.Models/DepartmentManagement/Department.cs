using System;

namespace Company.Services.DepartmentManagement
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ChangedAt { get; set; } = null;
    }
}
