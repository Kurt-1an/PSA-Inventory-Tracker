using InvSys.Data;
using InvSys.Models;
using InvSys.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;

namespace InvSys.Components.Pages;
public partial class Lease: ComponentBase
{
    #region INJECTIONS
    [Inject] PropertyService PropertyService { get; set; }
    [Inject] SupplyService SupplyService { get; set; }
    [Inject] EmployeeService EmployeeService { get; set; }
    [Inject] IJSRuntime JSRuntime { get; set; }
    #endregion

    #region FIELDS

    //MAIN Item Attributes
    private List<Property> PropertyList { get; set; } = [];
    private List<Property> PropertyNoList { get; set; } = [];
    private Property SelectedProperty { get; set; } = new();

    private List<Supply> SupplyList { get; set; } = [];
    private List<Supply> SupplyNoList { get; set; } = [];
    private Supply SelectedSupply { get; set; } = new();

    private List<Employee> EmployeeList { get; set; } = [];
    private string PropertySearchInput { get; set; } = string.Empty;

    string comment = string.Empty;
    string location = string.Empty;
    string search;

    private bool ShowDropdown = false;
    private bool ShowDropdownSupply = false;
    private bool showprop = false;
    private bool showsupply = false;

    private int? bal;

    private Dictionary<string, object> attributes = new Dictionary<string, object>();
    #endregion

    #region DISPLAY CONTENTS
    protected override async Task OnInitializedAsync() // LOADS WHEN APPLICATION RUNS
    {
        await LoadProperties();
    }

    private async Task LoadProperties() // LOADS THE PROPERTIES
    {
        PropertyList = PropertyService.GetAll();
        PropertyNoList = PropertyService.GetAll();
        SupplyList = SupplyService.GetAll();
        SupplyNoList = SupplyService.GetAll();
        EmployeeList = EmployeeService.GetAll();
    }

    #endregion

    #region COMPONENTS
    private async Task Dismiss()
    {
        SelectedProperty = new Property();
        SelectedSupply = new Supply();

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
                x.propCustodian?.ToString()?.Contains(PropertySearchInput, StringComparison.CurrentCultureIgnoreCase) == true ||
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

    private async Task ShowModal() //PROPERTY MODAL
    {
        if (PropertyList != null && PropertyList.All(item => !string.IsNullOrEmpty(item.propRemarks)))
        {
            string alertMessage = "Warning: No Property Available for Issue\n\nMake sure a property is available for issuance.";
            await JSRuntime.InvokeVoidAsync("alert", alertMessage);
            return;
        }
        else
        {
            attributes["data-bs-toggle"] = "modal";
        }
    }

    private async Task ShowModalSupply() //SUPPLY MODAL
    {
        if (SupplyList != null && SupplyList.All(item => !string.IsNullOrEmpty(item.supRemark)))
        {
            string alertMessage = "Warning: No Supply Available for Issue\n\nMake sure a property is available for issuance.";
            await JSRuntime.InvokeVoidAsync("alert", alertMessage);
            return;
        }
        else
        {
            attributes["data-bs-toggle"] = "modal";
        }
    }
    #endregion

    #region LEASE PROPERTY
    private async Task ShowDropdownHandler(bool show)
    {
        await Task.Delay(500);
        ShowDropdown = show;
    }

    private async Task GetPropertyListNo(ChangeEventArgs e)
    {
        SelectedProperty.propPropertyNo = e?.Value?.ToString();
        PropertyNoList.Clear();

        var result = PropertyNoList;

        if (result is not null && result.Count > 0 && !String.IsNullOrEmpty(SelectedProperty.propPropertyNo))
        {
            PropertyNoList = result.Where(x =>
                x.propArticle?.ToString()?.Contains(SelectedProperty.propPropertyNo, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.propDescription?.ToString()?.Contains(SelectedProperty.propPropertyNo, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.propPropertyNo?.ToString()?.Contains(SelectedProperty.propPropertyNo, StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
        else
        {
            PropertyNoList = result;
        }
    }

    private async Task PopulateFormData(int id)
    {
        SelectedProperty = new Property();
        var result = PropertyService.GetById(id);

        SelectedProperty = result;
    }

    private async Task LeaseItem()
    {
        Property newEntry = new Property();
        newEntry = SelectedProperty;

        List<string> selectedReasons = new List<string>();

        if (!string.IsNullOrEmpty(location))
        {
            selectedReasons.Add(location);
        }
        if (!string.IsNullOrEmpty(comment))
        {
            selectedReasons.Add(comment);
        }
        else
        {
            selectedReasons.Add("N/A");
        }

        newEntry.propRemarks = string.Join(", ", selectedReasons);

        PropertyService.Update(newEntry);

        await Dismiss();
    }
    #endregion 

    #region LEASE SUPPLY
    private async Task SupplyShowDropdownHandler(bool show)
    {
        await Task.Delay(500);
        ShowDropdownSupply = show;
    }

    private async Task GetSupplyListNo(ChangeEventArgs e)
    {
        search = e.Value.ToString();
        SupplyNoList.Clear();

        var result = SupplyNoList;

        if (result is not null && result.Count > 0 && !String.IsNullOrEmpty(search))
        {
            SupplyNoList = result.Where(x =>
                x.supArticle?.ToString()?.Contains(search, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.supDescription?.ToString()?.Contains(search, StringComparison.CurrentCultureIgnoreCase) == true ||
                x.supStockNumber?.ToString()?.Contains(search, StringComparison.CurrentCultureIgnoreCase) == true
            ).ToList();
        }
        else
        {
            SupplyNoList = result;
        }
    }

    private async Task SupplyPopulateFormData(int id)
    {
        SelectedSupply = new Supply();
        var result = SupplyService.GetById(id);


        SelectedSupply = result;

        search = Convert.ToString(SelectedSupply.supStockNumber);
    }

    private async Task SupplyLeaseItem()
    {

        List<string> selectedReasons = new List<string>();

        if (!string.IsNullOrEmpty(location))
        {
            selectedReasons.Add(location);
        }
        if (!string.IsNullOrEmpty(comment))
        {
            selectedReasons.Add(comment);
        }
        else
        {
            selectedReasons.Add("N/A");
        }

        SelectedSupply.supRemark = string.Join(", ", selectedReasons);

        SupplyService.Update(SelectedSupply);

        await Dismiss();
    }
    #endregion

    #region BTN FILTERS
    private async Task buttonProp()
    {
        showprop = true;
        showsupply = false;
        var result = PropertyService.GetAll();
        if (result is not null && result.Count > 0)
        {
            PropertyList = result.Where(x =>
                !string.IsNullOrEmpty(x.propRemarks)
            ).ToList();
        }
    }


    private async Task buttonSupply()
    {
        showprop = false;
        showsupply = true;
        var result = SupplyService.GetAll();
        if (result is not null && result.Count > 0)
        {
            SupplyList = result.Where(x =>
                !string.IsNullOrEmpty(x.supRemark)
            ).ToList();
        }
    }
    #endregion
}