using InvSys.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using InvSys.Models;

namespace InvSys.Components.Pages;
public partial class Home
{
    #region INJECTIONS
    [Inject] PropertyService PropertyService { get; set; }
    [Inject] SupplyService SupplyyService { get; set; }

    [Inject] IJSRuntime JSRuntime { get; set; }
    #endregion

    #region FIELDS

    //MAIN Item Attributes
    private bool isLoading = true;
    private List<Property> PropertyList { get; set; } = [];
    private List<Supply> SupplyList { get; set; } = [];

    private string PropertySearchInput { get; set; } = string.Empty;
    #endregion

    #region DISPLAY CONTENTS
    protected override async Task OnInitializedAsync() // LOADS WHEN APPLICATION RUNS
    {
        await LoadProperties();
        isLoading = false;
    }

    private async Task LoadProperties() // LOADS THE PROPERTIES
    {
        PropertyList = PropertyService.GetAll();
        SupplyList = SupplyyService.GetAll();
    }

    #endregion
}

