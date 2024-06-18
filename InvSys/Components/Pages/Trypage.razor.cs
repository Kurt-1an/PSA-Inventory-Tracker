using System;
using System.IO;
using System.Threading.Tasks;
using InvSys.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using InvSys.Services;
using InvSys.Models;
using Microsoft.JSInterop;

namespace InvSys.Components.Pages;

public partial class Trypage : ComponentBase
{
    #region INJECTIONS
    [Inject] private EmployeeService EmployeeService { get; set; }
    [Inject] private IJSRuntime JSRuntime { get; set; }
    #endregion

    #region FIELDS
    private List<Employee> EmployeeList { get; set; } = new List<Employee>();
    private Employee SelectedEmployee { get; set; } = new Employee();

    private bool isLoading = false;
    private string filePath;
    #endregion

    #region DISPLAY CONTENTS
    protected override async Task OnInitializedAsync()
    {
        await LoadEmployees();
    }

    private async Task LoadEmployees()
    {
        EmployeeList = EmployeeService.GetAll();
    }
    #endregion

    private async Task HandleFileChange(InputFileChangeEventArgs e)
    {
        var file = e.File;

        using (var memoryStream = new MemoryStream())
        {
            await file.OpenReadStream().CopyToAsync(memoryStream);
            filePath = Convert.ToBase64String(memoryStream.ToArray());
        }
    }

    private async Task ImportEmployees()
    {
        try
        {
            isLoading = true; // Set loading flag to true

            if (!string.IsNullOrEmpty(filePath))
            {
                var bytes = Convert.FromBase64String(filePath);
                var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".xlsx");

                await File.WriteAllBytesAsync(tempFilePath, bytes);

                EmployeeService.ImportEmployeesFromExcel(tempFilePath); // Await the import operation
            }
            else
            {
                // Handle case where no file is selected
            }
        }
        finally
        {
            isLoading = false; // Reset loading flag
            await LoadEmployees(); // Reload the employee list after import
        }
    }

}
