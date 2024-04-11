using crm.Shared.Component.SearchBar;
using Microsoft.AspNetCore.Components;

//todo
// fix close or erase search term in searchbar error

namespace crm.Pages.Accounts
{
    public partial class Accounts : ComponentBase
    {
        public IEnumerable<SearchBarItem> Vocabulary { get; set; } = [new SearchBarItem(1, "dd"), new SearchBarItem(1, "a")];
        public void OnAutoCompleteChanged(SearchBarItem searchTerm)
        {
            Console.WriteLine(searchTerm.Name);
        }
    }
}