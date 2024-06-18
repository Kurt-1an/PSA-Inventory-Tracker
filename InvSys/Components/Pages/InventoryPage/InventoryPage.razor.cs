using InvSys.Data;
using InvSys.Models;
using InvSys.Services;
using InvSys.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace InvSys.Components.Pages.InventoryPage;

public partial class InventoryPage
{
    #region FIELDS
    [Inject] IJSRuntime JSRuntime { get; set; }

    
    #endregion

    private void Print()
    {
        JSRuntime.InvokeVoidAsync("Print");
    }
}






