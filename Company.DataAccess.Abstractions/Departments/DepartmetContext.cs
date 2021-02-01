using Company.DataAccess.Employees;

namespace Company.DataAccess.Departments
{
    internal sealed class DepartmetContext : DomainContextBase, IDepartmentContext
    {
        public DepartmetContext(ApplicationDbContext dbContext) : base(dbContext) { }

        public IEntitySet<Department> Departments => GetEntitySet<Department>();

        public IEntitySet<Employee> Employees => GetEntitySet<Employee>();
    }
}
