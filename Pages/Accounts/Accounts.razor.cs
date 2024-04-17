using crm.Shared.Component.SearchBar;
using crm.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace crm.Pages.Accounts
{
    public partial class Accounts : ComponentBase
    {

        List<Account> Data =
        [
            new Account("Don Belle", "Tech", "fdfd", "US", "New York"),
            new Account("Johnny Test", "Food", "fdfd", "Japan", "Tokyo"),
        ];
        public IEnumerable<SearchBarItem> Vocabulary { get; set; } = [new SearchBarItem(1, "dd"), new SearchBarItem(1, "a")];
        public void OnAutoCompleteChanged(SearchBarItem searchTerm)
        {

        }
    }
}