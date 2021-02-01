using System;

namespace Company.DataAccess.Employees
{
    public interface IEmployeeContext : IDisposable
    {
        IEntitySet<Employee> Employees { get; }
    }
}
