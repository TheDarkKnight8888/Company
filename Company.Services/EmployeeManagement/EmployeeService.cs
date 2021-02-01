using System;
using System.Collections.Generic;
using Company.DataAccess;
using Company.DataAccess.Employees;

namespace Company.Services.EmployeeManagement
{
    public sealed class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeContext context;
        private bool isDisposed = false;

        public EmployeeService(IEmployeeContext context)
        {
            this.context = context;
        }

        public Employee Add(UpdateEmployeeRequest request)
        {
            var department = this.MapToDataAccess(request);
            var added = this.context.Employees.Add(department);
            this.context.Employees.SaveChanges();
            var result = this.MapToResponse(added);
            return result;
        }

        public Employee Delete(int id)
        {
            var item = this.context.Employees.FirstOrDefaultItem(item => item.Id == id);
            if (item == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            var deleted = this.context.Employees.Remove(item);
            this.context.Employees.SaveChanges();
            var result = this.MapToResponse(deleted);
            return result;
        }



        public Employee Get(int id)
        {
            var item = this.context.Employees.FirstOrDefaultItem(item => item.Id == id);
            if (item == null)
            {
                throw new RequestedResourceNotFoundException();
            }
            var result = this.MapToResponse(item);
            return result;
        }

        public IEnumerable<EmployeeListItem> GetAll()
        {
            foreach (var item in this.context.Employees)
            {
                var result = this.MapToListItem(item);
                yield return result;
            }
        }

        public Employee Update(int id, UpdateEmployeeRequest request)
        {
            var item = this.context.Employees.FirstOrDefaultItem(item => item.Id == id);
            if (item == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            item.ChangedAt = DateTime.Now;
            item.HiredAt = request.HiredAt;
            item.LastName = request.LastName;
            item.FirstName = request.FirstName;
            item.MiddleName = request.MiddleName;
            item.Position = request.Position;

            var updated = this.context.Employees.Update(item);
            this.context.Employees.SaveChanges();
            var result = this.MapToResponse(updated);
            return result;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }
        }

        private DataAccess.Employees.Employee MapToDataAccess(UpdateEmployeeRequest request)
        {
            return new DataAccess.Employees.Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                HiredAt = request.HiredAt,
                Position = request.Position
            };
        }

        private Employee MapToResponse(DataAccess.Employees.Employee response)
        {
            return new Employee
            {
                Id = response.Id,
                CreatedAt = response.CreatedAt,
                ChangedAt = response.ChangedAt,
                FirstName = response.FirstName,
                LastName = response.LastName,
                MiddleName = response.MiddleName,
                HiredAt = response.HiredAt,
                DepartmentId = response.DepartmentId,
                Position = response.Position
            };
        }

        private EmployeeListItem MapToListItem(DataAccess.Employees.Employee response)
        {
            return new EmployeeListItem
            {
                Id = response.Id,
                FirstName = response.FirstName,
                LastName = response.LastName,
                DepartmentId = response.DepartmentId,
            };
        }
    }
}
