using System;
using System.Collections.Generic;

namespace Company.Services.EmployeeManagement
{
    public interface IEmployeeService : IDisposable
    {
        IEnumerable<EmployeeListItem> GetAll();

        Employee Get(int id);

        Employee Add(UpdateEmployeeRequest request);

        Employee Update(int id, UpdateEmployeeRequest request);

        Employee Delete(int id);
    }
}
