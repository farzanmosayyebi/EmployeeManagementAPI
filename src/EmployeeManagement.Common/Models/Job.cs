namespace EmployeeManagement.Common.Models;
public class Job : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}
