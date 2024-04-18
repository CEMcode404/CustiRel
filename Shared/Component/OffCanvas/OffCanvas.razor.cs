using BlazorBootstrap;
using Microsoft.AspNetCore.Components;

namespace crm.Shared.Component.OffCanvas
{
    public partial class OffCanvas : ComponentBase
    {
        [Parameter]
        public OffCanvasSize Size { get; set; } = OffCanvasSize.Regular;
        [Parameter]
        public required OffCanvasControl Control { get; set; }
        [Parameter]
        public string Title { get; set; } = "OffCanvas";
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        private static readonly Dictionary<OffCanvasSize, OffcanvasSize> SizeMapping =
            new()
            {
            { OffCanvasSize.Large, OffcanvasSize.Large },
            { OffCanvasSize.Regular, OffcanvasSize.Regular },
            { OffCanvasSize.Small, OffcanvasSize.Small }
        };
    }

    public class OffCanvasControl
    {
        private Offcanvas? _offCanvasRef;

        public Offcanvas? OffCanvasRef
        {
            private get => _offCanvasRef;
            set => _offCanvasRef = value;
        }

        public void ShowOffCanvas()
        {
            OffCanvasRef?.ShowAsync();
        }

        public void HideOffCanvas()
        {
            OffCanvasRef?.HideAsync();
        }
    }

    public enum OffCanvasSize
    {
        Large = OffcanvasSize.Large,
        Regular = OffcanvasSize.Regular,
        Small = OffcanvasSize.Small
    }
}