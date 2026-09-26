using System.ComponentModel.DataAnnotations;

namespace employeeMVC.Models;

public class Employee
{
    public int Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; }
    [Required]
    public string Department { get; set; }
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value")]
    public decimal Salary { get; set; }
}