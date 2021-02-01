using System;
using System.Collections.Generic;
using Company.Services.EmployeeManagement;

namespace Company.Services.DepartmentManagement
{
    public interface IDepartmentService : IDisposable
    {
        IEnumerable<DepartmentListItem> GetAll();

        Department Get(int id);

        Department Add(UpdateDepartmentRequest request);

        Department Update(int id, UpdateDepartmentRequest request);

        Department Delete(int id);

        IEnumerable<EmployeeListItem> GetFreeEmployees();

        IEnumerable<EmployeeListItem> GetDepartmentEmployees(int departmentId);

        int AssignEmployeeToDepartment(int employeeId, int departmentId);

        void UnassignEmployeeFromDepartment(int employeeId);
    }
}
