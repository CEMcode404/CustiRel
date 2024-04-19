using BlazorBootstrap;
using Microsoft.AspNetCore.Components;

namespace crm.Shared.Component.BinaryDialog
{
    public partial class BinaryDialog
    {
        [Parameter]
        public required string Title { get; set; } = "";
        [Parameter]
        public required string Message { get; set; } = "";
        [Parameter]
        public required BinaryDialogControl Control { get; set; }
        [Parameter]
        public Action? OnYes { get; set; }
        [Parameter]
        public Action? OnNo { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Control.Title = Title;
            Control.Message = Message;

            if (OnYes != null) Control.OnYes = OnYes;
            if (OnNo != null) Control.OnNo = OnNo;
        }
    }


    public class BinaryDialogControl
    {
        public ConfirmDialog? BinaryDialogRef { private get; set; }
        public string Title { private get; set; } = "";
        public string Message { private get; set; } = "";
        public Action? OnYes { private get; set; }
        public Action? OnNo { private get; set; }

        private ConfirmDialogOptions _confirmationDialogOptions = new()
        {
            IsScrollable = true,
            IsVerticallyCentered = true
        };

        public async void ShowBinaryDialog()
        {
            if (BinaryDialogRef != null)
            {
                var confirmation = await BinaryDialogRef.ShowAsync(Title, Message, _confirmationDialogOptions);
                if (confirmation) OnYes?.Invoke();
                else OnNo?.Invoke();
            }
        }
    }
}