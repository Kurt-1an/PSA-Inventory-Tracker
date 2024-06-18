using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using InvSys.Data;
using InvSys.Models;
using InvSys.Services;
using InvSys.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace InvSys.Components.Pages.EmployeePage;

public partial class EmployeePage : ComponentBase
{
    #region INJECTIONS
    [Inject] private EmployeeService EmployeeService { get; set; }
    [Inject] private IJSRuntime JSRuntime { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    #endregion

    #region FIELDS
    private List<Employee> EmployeeList { get; set; } = new List<Employee>();
    private Employee SelectedEmployee { get; set; } = new Employee();
    private bool isLoading = false;

    public string empName { get; set; }
    public string empPos { get; set; }
    public string empMail { get; set; }
    private string ProductSearchInput { get; set; } = string.Empty;
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

    #region CRUD OPERATION
    //ADD
    private async Task Add()
    {
        Employee newEmployee = new Employee();
        newEmployee = SelectedEmployee;
        EmployeeService.Add(newEmployee);

        await LoadEmployees();
    }



    //EDIT
    private async Task PopulateEdit(int EmpId)
    {
        SelectedEmployee = EmployeeService.GetById(EmpId);
        empName = SelectedEmployee.empName;
        empPos = SelectedEmployee.empPos;
        empMail = SelectedEmployee.empMail;
        StateHasChanged();
    }
    private async Task Edit()
    {
        SelectedEmployee.empName = empName;
        SelectedEmployee.empPos = empPos;
        SelectedEmployee.empMail = empMail;

        EmployeeService.Update(SelectedEmployee);
        await Dismiss();
        StateHasChanged();
    }



    //DELETE
    private async Task PopulateEmployee(int EmpId)
    {
        SelectedEmployee = EmployeeService.GetById(EmpId);
        StateHasChanged();
    }
    private async Task Remove()
    {
        if (SelectedEmployee.IsRemoved == true)
        {
            EmployeeService.Delete(SelectedEmployee.ID);
        }
        else
        {
            SelectedEmployee.IsRemoved = true;
            EmployeeService.Update(SelectedEmployee);
        }
        await LoadEmployees();
    }



    //MERGE
    private async Task Merge(int EmpId)
    {
        SelectedEmployee = EmployeeService.GetById(EmpId);
        SelectedEmployee.IsRemoved = false;
        EmployeeService.Update(SelectedEmployee);
        StateHasChanged();
    }
    #endregion

    #region COMPONENTS
    List<string> PositionList = new List<string> {
        SD.Role_CSS,
        SD.Role_SSS,
        SD.Role_SS2,
        SD.Role_SA,
        SD.Role_RO1,
        SD.Role_RO2,
        SD.Role_ACC1,
        SD.Role_AO1,
        SD.Role_AO2,
        SD.Role_AO3,
        SD.Role_ISA1};

    private void Print()
    {
        JSRuntime.InvokeVoidAsync("Print");
    }

    private async Task GetEmployeeList(ChangeEventArgs e)
    {
        ProductSearchInput = e?.Value?.ToString();

        var result = EmployeeService.GetAll();
        if (result is not null && result.Count > 0 && !String.IsNullOrEmpty(ProductSearchInput))
        {
            EmployeeList = result.Where(x =>
                x.empName?.ToString()?.Contains(ProductSearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.empPos?.ToString()?.Contains(ProductSearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.empMail?.ToString()?.Contains(ProductSearchInput, StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
        else
        {
            EmployeeList = result.ToList();
        }
        StateHasChanged();
    }

    private async Task Dismiss()
    {
        SelectedEmployee = new Employee();
    }
    #endregion

    #region IMPORT EMPLOYEE
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
    #endregion

    #region EXPORT EXCEL
    private void ExportEmployeesToExcel()
    {
        var excelBytes = EmployeeService.GenerateExcelFile(EmployeeList);
        //var fileName = "Employees.xlsx";
        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        var fileStream = new MemoryStream(excelBytes);
        var base64String = Convert.ToBase64String(fileStream.ToArray());
        var uri = $"data:{mimeType};base64,{base64String}";
        NavigationManager.NavigateTo(uri);
    }
    #endregion
}
