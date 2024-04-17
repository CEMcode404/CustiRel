using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using System.Reflection;
using System.Reflection.Metadata;

namespace crm.Shared.Component.Grid.BaseGrid
{
    public partial class BaseGrid<TItem> : ComponentBase
    {
        [Parameter]
        public required IEnumerable<TItem> Data { get; set; } = [];

        [Parameter]
        public IEnumerable<IGridFeature> Features { get; set; } = [];
        protected override void OnInitialized()
        {
            base.OnInitialized();

            foreach (IGridFeature feature in Features)
                _featureRegistry.Add(feature);
        }


        private FeatureRegistry _featureRegistry = new();

        private async Task<GridDataProviderResult<TItem>> DataProvider(GridDataProviderRequest<TItem> request)
        {
            return await Task.FromResult(request.ApplyTo(Data));
        }

        private Task OnSelectedItemsChanged(HashSet<TItem> data)
        {
            if (_featureRegistry.Contains<GridSelection<TItem>>())
                _featureRegistry.Get<GridSelection<TItem>>()?.Action?.Invoke(data);

            return Task.CompletedTask;
        }

        private IEnumerable<(PropertyInfo prop, int index)> GetProperties<U>()
        {
            return typeof(U).GetProperties().Select((prop, index) => (prop, index));
        }

        private object GetPropertyValue(string propertyName, object srcObject)
        {
            var propertyValue = srcObject.GetType().GetProperty(propertyName)?.GetValue(srcObject);
            return propertyValue ?? new object();
        }
    }

    public class FeatureRegistry
    {
        private Dictionary<string, IGridFeature> _features = [];
        public void Add(IGridFeature feature)
        {
            if (feature.ValidateFeature())
                _features.Add(feature.GetType().Name, feature);
        }

        public T Get<T>() where T : IGridFeature, new()
        {
            string featureName = typeof(T).Name;
            return _features.TryGetValue(featureName, out IGridFeature? value) ? (T)value : new T();
        }

        public bool Contains<T>()
        {
            string featureName = typeof(T).Name;
            return _features.ContainsKey(featureName);
        }
    }

    public interface IGridFeature
    {
        internal bool ValidateFeature();
    }

    public class GridSort : IGridFeature
    {
        public IEnumerable<int> Columns { get; set; } = [];

        public bool IsSortColumn(int columnNumber)
        {
            return Columns.Contains(columnNumber);
        }

        bool IGridFeature.ValidateFeature()
        {
            return Columns.Any();
        }
    }

    public class GridPagination : IGridFeature
    {
        public int PageSize { get; set; } = 10;

        bool IGridFeature.ValidateFeature()
        {
            return true;
        }
    }

    public class GridRowActions : IGridFeature
    {
        public IEnumerable<GridAction> Actions { get; set; } = [];

        bool IGridFeature.ValidateFeature()
        {
            return Actions.Any();
        }
    }

    public class GridAction
    {
        public required string Name { get; set; }
        public required Action Action { get; set; }
    }

    public class GridSelection<TItem> : IGridFeature
    {
        public bool IsMultipleSelection { get; set; } = true;
        public Action<HashSet<TItem>>? Action { get; set; }

        bool IGridFeature.ValidateFeature()
        {
            return true;
        }

        public GridSelectionMode GetSelectionMode()
        {
            return IsMultipleSelection ? GridSelectionMode.Multiple : GridSelectionMode.Single;
        }
    }
}