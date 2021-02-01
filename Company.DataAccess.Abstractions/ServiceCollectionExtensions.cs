using Company.DataAccess.Departments;
using Company.DataAccess.Employees;
using Microsoft.Extensions.DependencyInjection;

namespace Company.DataAccess.Abstractions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<ApplicationDbContext>((provider) => new ApplicationDbContext(connectionString));
            services.AddScoped<IDepartmentContext>((provider) => new DepartmetContext(provider.GetService<ApplicationDbContext>()));
            services.AddScoped<IEmployeeContext>((provider) => new EmployeeContext(provider.GetService<ApplicationDbContext>()));

            return services;
        }
    }
}
