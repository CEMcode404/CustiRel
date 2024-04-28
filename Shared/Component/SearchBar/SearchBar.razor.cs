using crm.Shared.Utilities;
using Microsoft.AspNetCore.Components;

namespace crm.Shared.Component.SearchBar
{
    public partial class SearchBar : ComponentBase
    {
        [Parameter]
        public Func<string, Task<string[]>>? OnAutoCompleteChanged { get; set; }
        private string[] resultList = [];
        private bool isHidden = true;

        private readonly DebouncerDecorator Debouncer = new(1000);
        private async void HandleOnInput(ChangeEventArgs e)
        {
            if (OnAutoCompleteChanged != null && e.Value != null)
            {
                string inputString = ((string)e.Value).Trim();
                if (inputString == "")
                {
                    Debouncer.CancelDebounce();
                    HideDropdown();
                    await InvokeAsync(StateHasChanged);
                }
                else if (inputString.Length > 0)
                {
                    var debounceAction = Debouncer.Debounce(async () =>
                    {
                        resultList = await OnAutoCompleteChanged.Invoke((string)e.Value);
                        ShowDropdown();
                        await InvokeAsync(StateHasChanged);
                    });

                    await debounceAction();
                }
            }
        }

        private void ShowDropdown()
        {
            isHidden = false;
        }

        private void HideDropdown()
        {
            isHidden = true;
        }

        private async void HandleOnBlur()
        {
            //since e.relatedTarget is not available in c# 
            //i use a delay instead, to allow clicks to trigger after onblur inside
            // the dropdown
            await Task.Delay(100);
            HideDropdown();
            await InvokeAsync(StateHasChanged);
        }
    }

    public class SearchBarResultItem(string Name)
    {
        public string Name { get; set; } = Name;
        public Action? Action { get; set; }
    }
}