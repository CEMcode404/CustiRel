using BlazorBootstrap;
using Microsoft.AspNetCore.Components;

namespace crm.Shared.Component.SearchBar
{
    public partial class SearchBar : ComponentBase
    {
        public SearchBar()
        {
            Vocabulary = [];
        }

        [Parameter]
        public Action<SearchBarItem>? OnAutoCompleteChanged { get; set; }
        [Parameter]
        public IEnumerable<SearchBarItem>? Vocabulary { get; set; }

        private string? _searchTerm;

        public async Task<AutoCompleteDataProviderResult<SearchBarItem>> SearchTermResultDataProvider(AutoCompleteDataProviderRequest<SearchBarItem> request)
        {
            Vocabulary ??= [];

            return await Task.FromResult(request.ApplyTo(Vocabulary.OrderBy(searchTermItem => searchTermItem.Name)));
        }

        private void _OnAutoCompleteChanged(SearchBarItem result)
        {
            OnAutoCompleteChanged?.Invoke(result);
        }
    }

    public class SearchBarItem(int Id, string Name)
    {

        private string _name = Name;
        private int _id = Id;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}