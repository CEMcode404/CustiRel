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
        private readonly OffCanvasControl _removeAccountControl = new();
        private List<Account> Data =
        [
            new Account("Don Belle", "Tech", "fdfd", "US", "New York"),
            new Account("Johnny Test", "Food", "fdfd", "Japan", "Tokyo"),
        ];
        private IEnumerable<SearchBarItem> Vocabulary { get; set; } = [new SearchBarItem(1, "dd"), new SearchBarItem(1, "a")];
        private void OnAutoCompleteChanged(SearchBarItem searchTerm)
        {

        }

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
            _removeAccountControl.ShowOffCanvas();
        }

    }
}