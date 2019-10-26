using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamF.Controls.DataGrid.DataGridControl.Core.Enums;

namespace XamF.Controls.DataGrid.DataGridControl.Core
{
    public class DataGridColumn : BindableObject
    {
        //public event EventHandler<DataGridColumn> OrderChanged;
        public DataGridColumn()
        {
            _width = new GridLength(1, GridUnitType.Star);
        }

        #region Properties
        private string _title;
        public string Title
        {
            get => _title;
            set => _title = value;
        }

        private GridLength _width;
        public GridLength Width
        {
            get => _width;
            set => _width = value;
        }

        private string _propertyName;
        public string PropertyName
        {
            get => _propertyName;
            set => _propertyName = value;
        }

        //public static readonly BindableProperty PropertyBindingExpressionProperty = BindableProperty.Create(
        //    propertyChanged: (o, e, r) =>
        //    {
        //    },
        //    propertyName: nameof(PropertyBindingExpression),
        //    returnType: typeof(string),
        //    declaringType: typeof(DataGridColumn));

        //public string PropertyBindingExpression
        //{
        //    get { return (string)GetValue(PropertyBindingExpressionProperty); }
        //    set { SetValue(PropertyBindingExpressionProperty, value); }
        //}
        public DataTemplate CellTemplate { get; set; }
        public bool SortingEnabled { get; set; }

        private OrderType _sortOrderType;
        public OrderType SortOrderType
        {
            get => _sortOrderType;
            set
            {
                _sortOrderType = value;
                OnPropertyChanged(nameof(SortOrderType));
            }
        }
        #endregion Properties

        public void ToggleSorting()
        {
            if (SortOrderType == OrderType.None)
                SortOrderType = OrderType.Ascending;
            else if (SortOrderType == OrderType.Ascending)
                SortOrderType = OrderType.Decscending;
            else
                SortOrderType = OrderType.Ascending;
        }

        public void ResetOrderType()
        {
            SortOrderType = OrderType.None;
        }
    }
}
