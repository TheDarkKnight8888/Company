using Company.DataAccess.Employees;
using System;

namespace Company.DataAccess.Departments
{
    public interface IDepartmentContext : IDisposable
    {
        IEntitySet<Department> Departments { get; }

        IEntitySet<Employee> Employees { get; }
    }
}
