using System;

namespace Company.DataAccess.Employees
{
    internal sealed class EmployeeContext : DomainContextBase, IEmployeeContext, IDisposable
    {
        public EmployeeContext(ApplicationDbContext dbContext) : base(dbContext) { }

        public IEntitySet<Employee> Employees => this.GetEntitySet<Employee>();
    }
}
