using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamF.Controls.DataGrid.DataGridControl.Core;
using XamF.Controls.DataGrid.DataGridControl.Templates;
using XamF.Controls.DataGrid.DataGridControl.Utilities;

namespace XamF.Controls.DataGrid.DataGridControl
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataGrid : ContentView
    {
        private DataGridColumn _currentSortingColumn;
        private bool _sameList = false;
        public DataGrid()
        {
            InitializeComponent();
            this.listView.ItemTapped += ListView_ItemTapped;
            this.listView.Refreshing += ListView_Refreshing;
        }

        private void ListView_Refreshing(object sender, EventArgs e)
        {
            RefreshCommand.Execute(e);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            Init();
        }

        private void Init()
        {
            if (Columns == null)
                return;

            var dataTemplate = new DataTemplate(() => new RowTemplate(Columns));
            this.listView.ItemTemplate = dataTemplate;

            int index = 0;
            foreach (var col in Columns)
            {
                var columnDef = new ColumnDefinition { Width = col.Width };
                headerContainer.ColumnDefinitions.Add(columnDef);

                var headerCell = new HeaderCellTemplate(col);
                headerCell.OnHeaderTapped += HeaderCell_OnHeaderTapped;
                headerContainer.Children.Add(headerCell);
                Grid.SetColumn(headerCell, Columns.IndexOf(col));
                index++;
            }
        }
        //============================== Commands ===============
        //---------- Refresh Command ---------
        public static readonly BindableProperty RefreshCommandProperty = BindableProperty.Create(
        propertyName: nameof(RefreshCommand),
        returnType: typeof(ICommand),
        declaringType: typeof(DataGrid));

        public ICommand RefreshCommand
        {
            get { return (ICommand)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }
        //-------- Sort Command -----------

        public static readonly BindableProperty SortCommandProperty =
            BindableProperty.Create(
                propertyName: nameof(SortCommand),
                returnType: typeof(ICommand),
                declaringType: typeof(DataGrid));

        public ICommand SortCommand
        {
            get { return (ICommand)GetValue(SortCommandProperty); }
            set { SetValue(SortCommandProperty, value); }
        }

        //-------- Header Tapped Command
        public static readonly BindableProperty HeaderTappedCommandProperty =
            BindableProperty.Create(
                propertyName: nameof(HeaderTappedCommand),
                returnType: typeof(ICommand),
                declaringType: typeof(DataGrid));

        public ICommand HeaderTappedCommand
        {
            get { return (ICommand)GetValue(HeaderTappedCommandProperty); }
            set { SetValue(HeaderTappedCommandProperty, value); }
        }

        //-------- Item Tapped Command
        public static readonly BindableProperty ItemTappedCommandProperty =
            BindableProperty.Create(
                propertyName: nameof(ItemTappedCommand),
                returnType: typeof(ICommand),
                declaringType: typeof(DataGrid));

        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set { SetValue(ItemTappedCommandProperty, value); }
        }
        //============================== Event Handling ===========
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ItemTappedCommand?.Execute(e.Item);
            listView.SelectedItem = null;
        }

        private void HeaderCell_OnHeaderTapped(object sender, DataGridColumn dataGridColumn)
        {
            if (_currentSortingColumn != dataGridColumn && dataGridColumn.SortingEnabled)
            {
                _currentSortingColumn?.ResetOrderType();
                _currentSortingColumn = dataGridColumn;
            }
            
            if (_autoSort && _currentSortingColumn != null && _currentSortingColumn.SortingEnabled)
            {
                var orderQuery = ItemsSource.AsQueryable().SortBy(_currentSortingColumn.PropertyName, _currentSortingColumn.SortOrderType);

                SortCommand?.Execute(orderQuery);
            }
            HeaderTappedCommand?.Execute(dataGridColumn);
        }
        //--------- ItemsSource -----
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            propertyName: nameof(ItemsSource),
            returnType: typeof(IEnumerable),
            declaringType: typeof(DataGrid), 
            defaultBindingMode: BindingMode.TwoWay, 
            propertyChanged: async (bindableObj, oldValue, newValue) =>
            {
                var control = bindableObj as DataGrid;
                var itemsCount = (newValue as IEnumerable<object>)?.Count();
                var requiredHeight = itemsCount * control.listView.RowHeight;
                control.listView.HeightRequest = requiredHeight ?? 0;

                //--- Assume that the GetHashCode is value-based so it is identifier
                await control.ResetOrderIfNotSameList(oldValue as IEnumerable<object>, newValue as IEnumerable<object>);
            });

        private Task ResetOrderIfNotSameList(IEnumerable<object> oldValue, IEnumerable<object> newValue)
        {
            return Task.Run(() =>
            {
                unchecked
                {
                    int oldListId = int.MinValue;
                    if (oldValue != null)
                    {
                        var oldList = oldValue;
                        oldListId = oldList.Aggregate(1, (acumulative, next) => { return acumulative ^ next.GetHashCode(); });
                    }

                    var newList = newValue;
                    int newListId = newList.Aggregate(1, (acumulative, next) => { return acumulative ^ next.GetHashCode(); });
                    if (oldListId != newListId)
                    {
                        this._sameList = false;
                        this._currentSortingColumn?.ResetOrderType();
                    }
                    else
                        this._sameList = true;
                }
            });
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        //---------------------------- Columns
        public List<DataGridColumn> Columns
        {
            get { return (List<DataGridColumn>)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public static readonly BindableProperty ColumnsProperty = BindableProperty.Create(
                nameof(Columns),
                typeof(List<DataGridColumn>),
                typeof(DataGrid),
                defaultValueCreator: b => { return new List<DataGridColumn>(); });
        //---------------- Footer Place Holder ----------
        public static readonly BindableProperty FooterHolderProperty = BindableProperty.Create(nameof(FooterPlaceHolder), typeof(View), typeof(DataGrid));

        public View FooterPlaceHolder
        {
            get { return (View)GetValue(FooterHolderProperty); }
            set { SetValue(FooterHolderProperty, value); }
        }
        //--------- Header Background Color -----------
        public static readonly BindableProperty HeaderBackgroundColorProperty = BindableProperty.Create(nameof(HeaderBackgroundColor), typeof(Color), typeof(DataGrid));

        public Color HeaderBackgroundColor
        {
            get { return (Color)GetValue(HeaderBackgroundColorProperty); }
            set { SetValue(HeaderBackgroundColorProperty, value); }
        }

        private bool _autoSort = true;
        public bool AutoSort { get => _autoSort; set => _autoSort = value; }
    }
}