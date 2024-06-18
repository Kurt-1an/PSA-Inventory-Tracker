using System;
using Serilog;
using InvSys.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using InvSys.Data;
using InvSys.Services.Interface;
using ClosedXML.Excel;


namespace InvSys.Services;

public class EmployeeService : ICrudService<Employee>
{
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }


    public List<Employee> GetAll()
    {
        return _context.Employees.ToList();
    }

    public Employee GetById(int id)
    {
        return _context.Employees.Find(id);
    }

    public void Add(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public void Update(Employee employee)
    {
        _context.Entry(employee).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var employee = _context.Employees.Find(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }

    public void ImportEmployeesFromExcel(string filePath)
    {
        try
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Assuming data is on the first worksheet

                foreach (var row in worksheet.RowsUsed().Skip(1)) // Skip header row
                {
                    var employee = new Employee
                    {
                        empName = row.Cell(1).GetString(), // Adjust column indexes as per your Excel file
                        empPos = row.Cell(2).GetString(),
                        empMail = row.Cell(3).GetString()
                    };

                    _context.Employees.Add(employee);
                }

                _context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Log.Error($"Error importing employees from Excel: {ex.Message}");
            throw; // Rethrow the exception to handle it at a higher level if necessary
        }
    }

    public byte[] GenerateExcelFile(List<Employee> employees)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Employees");

            // Add headers
            worksheet.Cell(1, 1).Value = "Name";
            worksheet.Cell(1, 2).Value = "Position";
            worksheet.Cell(1, 3).Value = "Email";

            // Add other headers similarly

            int row = 2;

            // Populate data
            foreach (var emp in employees)
            {
                //var supply = supplies[i];
                worksheet.Cell(row, 1).Value = emp.empName;
                worksheet.Cell(row, 2).Value = emp.empPos;
                worksheet.Cell(row, 3).Value = emp.empMail;

                row++;
                // Add other properties similarly
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }
}
