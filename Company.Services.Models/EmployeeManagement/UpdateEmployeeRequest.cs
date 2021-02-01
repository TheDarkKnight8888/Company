using System;

namespace Company.Services.EmployeeManagement
{
    public class UpdateEmployeeRequest
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public DateTime? HiredAt { get; set; } = null;
    }
}
