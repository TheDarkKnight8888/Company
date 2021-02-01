namespace Company.Services.EmployeeManagement
{
    public class EmployeeListItem
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? DepartmentId { get; set; } = null;
    }
}
