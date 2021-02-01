using System;
using System.Linq;
using System.Collections.Generic;
using Company.DataAccess.Departments;
using Company.DataAccess;
using Company.Services.EmployeeManagement;

namespace Company.Services.DepartmentManagement
{
    public sealed class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentContext context;
        private bool isDisposed = false;

        public DepartmentService(IDepartmentContext context)
        {
            this.context = context;
        }

        public Department Add(UpdateDepartmentRequest request)
        {
            var item = this.context.Departments.FirstOrDefaultItem(item => item.Name == request.Name);
            if (item != null)
            {
                throw new RequestedResourceHasConflictException();
            }

            var department = this.MapToDataAccess(request);
            var added = this.context.Departments.Add(department);
            this.context.Departments.SaveChanges();
            var result = this.MapToResponse(added);
            return result;
        }

        public Department Delete(int id)
        {
            var item = this.context.Departments.FirstOrDefaultItem(item => item.Id == id);
            if (item == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            var deleted = this.context.Departments.Remove(item);
            this.context.Departments.SaveChanges();
            var result = this.MapToResponse(deleted);
            return result;
        }

        

        public Department Get(int id)
        {
            var item = this.context.Departments.FirstOrDefaultItem(item => item.Id == id);
            if (item == null)
            {
                throw new RequestedResourceNotFoundException();
            }
            var result = this.MapToResponse(item);
            return result;
        }

        public IEnumerable<DepartmentListItem> GetAll()
        {
            foreach (var item in this.context.Departments)
            {
                var result = this.MapToListItem(item);
                yield return result;
            }
        }

        public Department Update(int id, UpdateDepartmentRequest request)
        {
            var item = this.context.Departments.FirstOrDefaultItem(item => item.Id == id);
            if (item == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            item.Name = request.Name;
            item.ChangedAt = DateTime.Now;

            var updated = this.context.Departments.Update(item);
            this.context.Departments.SaveChanges();
            var result = this.MapToResponse(updated);
            return result;
        }

        public IEnumerable<EmployeeListItem> GetFreeEmployees()
        {
            var freeEmployees = this.context.Employees.Where(item => item.DepartmentId == null);
            foreach (var item in freeEmployees)
            {
                EmployeeListItem employee = this.MapEmploee(item);
                yield return employee;
            }
        }

        public int AssignEmployeeToDepartment(int employeeId, int departmentId)
        {
            var employee = this.context.Employees.FirstOrDefaultItem(item => item.Id == employeeId);
            if (employee == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            employee.DepartmentId = departmentId;
            this.context.Employees.Update(employee);
            this.context.Employees.SaveChanges();

            return departmentId;
        }

        public void UnassignEmployeeFromDepartment(int employeeId)
        {
            var employee = this.context.Employees.FirstOrDefaultItem(item => item.Id == employeeId);
            if (employee == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            employee.DepartmentId = null;
            this.context.Employees.Update(employee);
            this.context.Employees.SaveChanges();
        }

        public IEnumerable<EmployeeListItem> GetDepartmentEmployees(int departmentId)
        {
            var freeEmployees = this.context.Employees.Where(item => item.DepartmentId == departmentId);
            foreach (var item in freeEmployees)
            {
                EmployeeListItem employee = this.MapEmploee(item);
                yield return employee;
            }
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

        private DataAccess.Departments.Department MapToDataAccess(UpdateDepartmentRequest request)
        {
            return new DataAccess.Departments.Department
            {
                Name = request.Name,
            };
        }

        private Department MapToResponse(DataAccess.Departments.Department response)
        {
            return new Department
            {
                Id = response.Id,
                Name = response.Name,
                CreatedAt = response.CreatedAt,
                ChangedAt = response.ChangedAt
            };
        }

        private DepartmentListItem MapToListItem(DataAccess.Departments.Department response)
        {
            return new DepartmentListItem
            {
                Id = response.Id,
                Name = response.Name
            };
        }

        private EmployeeListItem MapEmploee(DataAccess.Employees.Employee employee)
        {
            return new EmployeeListItem
            {
                Id = employee.Id,
                DepartmentId = employee.DepartmentId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
            };
        }
    }
}
