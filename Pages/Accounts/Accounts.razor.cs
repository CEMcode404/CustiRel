using crm.Shared.Component.BinaryDialog;
using crm.Shared.Component.OffCanvas;
using crm.Shared.Component.SearchBar;
using crm.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace crm.Pages.Accounts
{
    public partial class Accounts : ComponentBase
    {
        private readonly OffCanvasControl _viewAccountControl = new();
        private readonly OffCanvasControl _editAccountControl = new();
        private readonly BinaryDialogControl _removeAccountControl = new();
        private List<Account> Data =
        [
            new Account("Don Belle", "Tech", "fdfd", "US", "New York"),
            new Account("Johnny Test", "Food", "fdfd", "Japan", "Tokyo"),
        ];

        private void ViewAccount()
        {
            _viewAccountControl.ShowOffCanvas();
        }

        private void EditAccount()
        {
            _editAccountControl.ShowOffCanvas();
        }

        private void RemoveAccount()
        {
            _removeAccountControl.ShowBinaryDialog();
        }

        async private Task<string[]> HandleOnChange(string inputString)
        {
            await Task.CompletedTask; // use this if there is no await to avoid compiler warning;
            return ["dfd", "fdfd"];
        }

    }
}