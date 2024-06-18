using InvSys.Data;
using InvSys.Models;
using InvSys.Services;
using InvSys.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;

namespace InvSys.Components.Pages.InventoryPage;

public partial class PropertyPage : ComponentBase
{
    #region INJECTIONS
    [Inject] PropertyService PropertyService { get; set; }
    [Inject] EmployeeService EmployeeService { get; set; }
    [Inject] IJSRuntime JSRuntime { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    #endregion

    #region FIELDS

    //MAIN Item Attributes
    private List<Property> PropertyList { get; set; } = [];
    private List<Employee> EmployeeList { get; set; } = [];
    private Property SelectedProperty { get; set; } = new();
    private bool isLoading = false;
    private string PropertySearchInput { get; set; } = string.Empty;
    private string buttonRPCSPDisplay = "none";
    private string buttonRPCSPALLDisplay = "none";
    private string buttonALLDisplay = "none";
    private string buttonRPCPPEDisplay = "none";
    private string buttonRPCPPEALLDisplay = "none";
    string comment = "";
    string location = "";
    string person = "";
    private string filePath;

    #endregion

    #region DISPLAY CONTENTS
    protected override async Task OnInitializedAsync() // LOADS WHEN APPLICATION RUNS
    {
        await LoadProperties();
        ShowButtonALL();
    }

    private async Task LoadProperties() // LOADS THE PROPERTIES
    {
        PropertyList = PropertyService.GetAll();
        EmployeeList = EmployeeService.GetAll();
    }

    #endregion

    #region CRUD OPERATIONS
    //ADD
    private async Task Add()
    {
        Property newProperty = new Property();
        newProperty = SelectedProperty;

        bool isDuplicate = PropertyService.IsStockNumberDuplicate(newProperty.propPropertyNo);

        if (isDuplicate)
        {
            string alertMessage = $"Warning: Duplicate Property Number!\n\nA property with the number '{newProperty.propPropertyNo}' already exists. Please enter a different stock number.";
            await JSRuntime.InvokeVoidAsync("alert", alertMessage);
            return;
        }

        PropertyService.Add(newProperty);

        await Dismiss();
    }


    //EDIT
    private async Task PopulateEdit(int SupId)
    {
        SelectedProperty = PropertyService.GetById(SupId);
    }
    private async Task Edit()
    {
        PropertyService.Update(SelectedProperty);
    }


    //DELETE
    private async Task PopulateProperty(int PropId)
    {
        SelectedProperty = PropertyService.GetById(PropId);
    }

    private async Task Remove()
    {
        if (SelectedProperty.IsRemoved == true)
        {
            PropertyService.Delete(SelectedProperty.ID);
        }
        else
        {
            SelectedProperty.IsRemoved = true;
            PropertyService.Update(SelectedProperty);
        }
        await Dismiss();
    }


    //MERGE
    private async Task Merge(int PropId)
    {
        SelectedProperty = PropertyService.GetById(PropId);
        SelectedProperty.IsRemoved = false;
        PropertyService.Update(SelectedProperty);
        StateHasChanged();
    }
    #endregion

    #region COMPONENTS
    private async Task Dismiss()
    {
        SelectedProperty = new Property();
        await JSRuntime.InvokeVoidAsync("reloadPage");
    }
    private void Print() 
    {
        JSRuntime.InvokeVoidAsync("Print");
    }
    private async Task GetPropertyList(ChangeEventArgs e)
    {
        PropertySearchInput = e?.Value?.ToString();

        var result = PropertyService.GetAll();
        if (result is not null && result.Count > 0 && !string.IsNullOrEmpty(PropertySearchInput))
        {
            PropertyList = result.Where(x =>
                x.propArticle?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.propDescription?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.propPropertyNo?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.propUnitOfMeasure?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.propUnitValue?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.propBalancePerCardQty?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.propOnHandPerCardQty?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.propShortageOverageQty?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.propShortageOverageValue?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.propRemarks?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.propYrAcquired.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
        else
        {
            PropertyList = result.ToList();
        }
        StateHasChanged();
    }

    private void ShowButtonALL()
    {
        buttonRPCSPDisplay = "block";
        buttonRPCPPEDisplay = "block";
        buttonRPCSPALLDisplay = "inline";


        buttonRPCPPEALLDisplay = "none";
    }

    private void ShowButtonRPCSP()
    {
        buttonRPCSPDisplay = "block";
        buttonRPCSPALLDisplay = "inline";

        buttonRPCPPEDisplay = "none";
        buttonALLDisplay = "none";
    }

    private void ShowButtonRPCPPE()
    {
        buttonRPCPPEDisplay = "block";
        buttonRPCPPEALLDisplay = "inline";

        buttonRPCSPDisplay = "none";
        buttonALLDisplay = "none";
    }


    #endregion

    #region IMPORT PROPERTY
    private async Task HandleFileChange(InputFileChangeEventArgs e)
    {
        var file = e.File;

        using (var memoryStream = new MemoryStream())
        {
            await file.OpenReadStream().CopyToAsync(memoryStream);
            filePath = Convert.ToBase64String(memoryStream.ToArray());
        }
    }

    private async Task ImportProperties()
    {
        try
        {
            isLoading = true; // Set loading flag to true

            if (!string.IsNullOrEmpty(filePath))
            {
                var bytes = Convert.FromBase64String(filePath);
                var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".xlsx");
                await File.WriteAllBytesAsync(tempFilePath, bytes);
                PropertyService.ImportPropertiesFromExcel(tempFilePath); // Await the import operation
            }
            else
            {
                // Handle case where no file is selected
            }
        }
        finally
        {
            isLoading = false; // Reset loading flag
            await LoadProperties(); // Reload the employee list after import
        }
    }
    #endregion

    #region EXPORT EXCEL
    private void ExportToExcel()
    {
        var excelBytes = PropertyService.GenerateExcelFile(PropertyList);
        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        var fileStream = new MemoryStream(excelBytes);
        var base64String = Convert.ToBase64String(fileStream.ToArray());
        var uri = $"data:{mimeType};base64,{base64String}";
        NavigationManager.NavigateTo(uri);
    }
    #endregion

    #region BTN FILTERS
    private async Task buttonRPCSPAIT()
    {
        PropertySearchInput = "IT";

        var result = PropertyService.GetAll();
        if (result is not null && result.Count > 0 && !string.IsNullOrEmpty(PropertySearchInput))
        {
            PropertyList = result.Where(x =>
                x.category?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
    }

    private async Task buttonRPCSPAFF()
    {
        PropertySearchInput = "Furniture & Fixtures";

        var result = PropertyService.GetAll();
        if (result is not null && result.Count > 0 && !string.IsNullOrEmpty(PropertySearchInput))
        {
            PropertyList = result.Where(x =>
                x.category?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
    }

    private async Task buttonRPCSPAcomm()
    {
        PropertySearchInput = "Communication";

        var result = PropertyService.GetAll();
        if (result is not null && result.Count > 0 && !string.IsNullOrEmpty(PropertySearchInput))
        {
            PropertyList = result.Where(x =>
                x.category?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
    }

    private async Task buttonRPCSPAbook()
    {
        PropertySearchInput = "Books";

        var result = PropertyService.GetAll();
        if (result is not null && result.Count > 0 && !string.IsNullOrEmpty(PropertySearchInput))
        {
            PropertyList = result.Where(x =>
                x.category?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
    }

    private async Task buttonRPCSPAspts()
    {
        PropertySearchInput = "Sports";

        var result = PropertyService.GetAll();
        if (result is not null && result.Count > 0 && !string.IsNullOrEmpty(PropertySearchInput))
        {
            PropertyList = result.Where(x =>
                x.category?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
    }

    private async Task buttonRPCSPAothrs()
    {
        PropertySearchInput = "Others";

        var result = PropertyService.GetAll();
        if (result is not null && result.Count > 0 && !string.IsNullOrEmpty(PropertySearchInput))
        {
            PropertyList = result.Where(x =>
                x.category?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
    }

    private async Task buttonRPCPPEoffice()
    {
        PropertySearchInput = "Office Equipment";

        var result = PropertyService.GetAll();
        if (result is not null && result.Count > 0 && !string.IsNullOrEmpty(PropertySearchInput))
        {
            PropertyList = result.Where(x =>
                x.category?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
    }

    private async Task buttonRPCPPEequip()
    {
        PropertySearchInput = "It Equipment";

        var result = PropertyService.GetAll();
        if (result is not null && result.Count > 0 && !string.IsNullOrEmpty(PropertySearchInput))
        {
            PropertyList = result.Where(x =>
                x.category?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
    }

    private async Task buttonRPCPPEmtr()
    {
        PropertySearchInput = "Motor Vehicle";

        var result = PropertyService.GetAll();
        if (result is not null && result.Count > 0 && !string.IsNullOrEmpty(PropertySearchInput))
        {
            PropertyList = result.Where(x =>
                x.category?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
    }
    #endregion
}