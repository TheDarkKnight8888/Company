using Company.DataAccess.Departments;
using Company.DataAccess.Employees;

namespace Company.DataAccess
{
    internal sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(string connectionString) : base(connectionString) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}
