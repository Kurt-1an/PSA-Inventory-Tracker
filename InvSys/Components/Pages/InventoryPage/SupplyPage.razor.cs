using InvSys.Components.Account.Pages.Manage;
using InvSys.Data;
using InvSys.Models;
using InvSys.Services;
using InvSys.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;


namespace InvSys.Components.Pages.InventoryPage;


public partial class SupplyPage : ComponentBase
{
    #region INJECTIONS
    [Inject] SupplyService SupplyService { get; set; }
    [Inject] IJSRuntime JSRuntime { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }

    #endregion

    #region FIELDS
    private List<Supply> SupplyList { get; set; } = [];
    private bool isLoading = false;
    private Supply SelectedSupply { get; set; } = new();
    private string? SupplySearchInput { get; set; } = string.Empty;

    #endregion

    #region DISPLAY CONTENTS
    protected override async Task OnInitializedAsync()
    {
        await LoadSupplies();
        isLoading = false;
    }
    private async Task LoadSupplies()
    {

        // Retrieve properties from the service
        var _supply = SupplyService.GetAll();

        foreach (var supply in _supply)
        {
            if (supply.supTotalAmount == null || supply.supBalance <= 0)
            {
                supply.supBalance = supply.supBeginningInventoryQty + supply.supDelivery;
                supply.supEndingInventoryQty = supply.supBalance - supply.supIssuance;
                supply.supTotalAmount = supply.supAverageUnitCost * supply.supEndingInventoryQty;

                SupplyService.Update(supply);
            }
        }

        // Set the PropertyList after calculating totals
        SupplyList = _supply;

    }

    #endregion

    #region CRUD OPERATIONS
    private async Task Add()
    {

        //Adds new entity to DB
        Supply newSupply = new Supply();
        newSupply = SelectedSupply;


        bool isDuplicate = SupplyService.IsStockNumberDuplicate(newSupply.supStockNumber);
        if (isDuplicate)
        {
            string alertMessage = $"Warning: Duplicate Stock Number!\n\nA stock with the number '{newSupply.supStockNumber}' already exists. Please enter a different stock number.";
            await JSRuntime.InvokeVoidAsync("alert", alertMessage);
            return;
        }


        newSupply.supBalance = newSupply.supBeginningInventoryQty + newSupply.supDelivery;
        newSupply.supEndingInventoryQty = newSupply.supBalance - newSupply.supIssuance;
        newSupply.supTotalAmount = newSupply.supAverageUnitCost * newSupply.supEndingInventoryQty;

        SupplyService.Add(newSupply);

        //Sets the total amount (diplayed in UI)
        //supTotalAmount = newSupply.supTotalAmount;

        await Dismiss();
    }



    //EDIT
    private async Task PopulateEdit(int SupId)
    {
        SelectedSupply = SupplyService.GetById(SupId);
    }
    private async Task Edit()
    {
        SupplyService.Update(SelectedSupply);
    }



    //MERGE
    private async Task Merge(int SupId)
    {
        SelectedSupply = SupplyService.GetById(SupId);
        SelectedSupply.IsRemoved = false;
        SupplyService.Update(SelectedSupply);
        StateHasChanged();
    }



    //DELETE
    private async Task PopulateSupply(int SupId)
    {
        SelectedSupply = SupplyService.GetById(SupId);
    }
    private async Task Remove()
    {
        if (SelectedSupply.IsRemoved == true)
        {
            SupplyService.Delete(SelectedSupply.ID);
        }
        else
        {
            SelectedSupply.IsRemoved = true;
            SupplyService.Update(SelectedSupply);
        }
        StateHasChanged();
    }
    #endregion

    #region COMPONENTS
    private async Task Dismiss()
    {
        SelectedSupply = new Supply();
        await JSRuntime.InvokeVoidAsync("reloadPage");
    }
    private void Print() //PRINT
    {
        JSRuntime.InvokeVoidAsync("Print");
    }
    private async Task GetSupplyList(ChangeEventArgs e) //SEARCH
    {
        SupplySearchInput = e?.Value?.ToString();

        var result = SupplyService.GetAll();
        if (result is not null && result.Count > 0 && !String.IsNullOrEmpty(SupplySearchInput))
        {
            SupplyList = result.Where(x =>
                x.supArticle?.ToString()?.Contains(SupplySearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.category?.ToString()?.Contains(SupplySearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.supAverageUnitCost?.ToString()?.Contains(SupplySearchInput, StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
        else
        {
            SupplyList = result?.ToList();
        }
        StateHasChanged();
    }
    #endregion

    #region EXPORT EXCEL
    private void ExportToExcel()
    {
        var excelBytes = SupplyService.GenerateExcelFile(SupplyList);
        var fileName = "Supplies.xlsx";
        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        var fileStream = new MemoryStream(excelBytes);
        var base64String = Convert.ToBase64String(fileStream.ToArray());
        var uri = $"data:{mimeType};base64,{base64String}";
        NavigationManager.NavigateTo(uri);
    }
    #endregion

    #region IMPORT EXCEL
    private string filePath;
    private async Task HandleFileChange(InputFileChangeEventArgs e)
    {
        var file = e.File;

        using (var memoryStream = new MemoryStream())
        {
            await file.OpenReadStream().CopyToAsync(memoryStream);
            filePath = Convert.ToBase64String(memoryStream.ToArray());
        }
    }

    private async Task ImportSupplies()
    {
        try
        {
            isLoading = true; // Set loading flag to true

            if (!string.IsNullOrEmpty(filePath))
            {
                var bytes = Convert.FromBase64String(filePath);
                var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".xlsx");
                await File.WriteAllBytesAsync(tempFilePath, bytes);
                SupplyService.ImportSuppliesFromExcel(tempFilePath); // Await the import operation
            }
            else
            {
                // Handle case where no file is selected
            }
        }
        finally
        {
            isLoading = false; // Reset loading flag
            await LoadSupplies(); // Reload the employee list after import
        }
    }
    #endregion

    #region BTN FILTERS
    private void ShowButtonALL()
    {
        var result = SupplyService.GetAll();
        SupplyList = result;

    }

    private void ShowButtonOffice()
    {
        var result = SupplyService.GetAll();
        if (result is not null && result.Count > 0)
        {
            SupplyList = result.Where(x =>
                x.category?.ToString()?.Contains("Office", StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
    }

    private void ShowButtonOther()
    {
        var result = SupplyService.GetAll();
        if (result is not null && result.Count > 0)
        {
            SupplyList = result.Where(x =>
                x.category?.ToString()?.Contains("Other", StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
    }
    #endregion
}
