using Company.Services.DepartmentManagement;
using Microsoft.Extensions.DependencyInjection;
using Company.DataAccess.Abstractions;
using Company.Services.EmployeeManagement;

namespace Company.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCompanyServices(this IServiceCollection services, string connectinString)
        {
            services.AddApplicationDbContext(connectinString);
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
