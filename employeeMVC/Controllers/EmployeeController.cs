using employeeMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace employeeMVC.Controllers;

public class EmployeeController : Controller
{
    // =============================================================

    // 1. Db set up
    // db context
    private readonly Data.ApplicationDbContext _context;

    // constructor for Db context DI
    public EmployeeController(Data.ApplicationDbContext context)
    {
        _context = context;
    }
    // =============================================================

    // 2. Get list all employees. Very first view
    //public async Task<IActionResult> Index()
    //{
    //    // get all employees from db
    //    var employees = await _context.Employees.ToListAsync();
    //    return View(employees);
    //}
    // Mock Data for testing purpose
    public IActionResult Index()
    {
        var employee = new Employee
        {
            Id = 1,
            Name = "John Doe",
            Department = "IT",
            Salary = 50000
        };
        return View(employee);
    }
    // =============================================================

    // 3. Create employee
    // Get reate employee UI
    public IActionResult Create()
    {
        return View();
    }

    // Save input employee
    [HttpPost]
    public async Task<IActionResult> Create(Employee employee)
    {
        // Check if input is valid
        if (ModelState.IsValid)
        {
            // Add to db
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            // redirect to index
            return RedirectToAction("Index");
        }

        return View(employee);
    }

    // =============================================================
    // 4. Edit Employee
    // Get edit UI
    public async Task<IActionResult> Edit(int id)
    {
        // Get employee by id
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Employee employee)
    {
        // Prevent attacking when user is trying to edit another employee
        if (id != employee.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(employee);
    }
}
    
