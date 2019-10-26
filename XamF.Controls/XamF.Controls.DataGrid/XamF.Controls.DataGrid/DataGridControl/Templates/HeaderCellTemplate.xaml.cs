using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamF.Controls.DataGrid.DataGridControl.Core;

namespace XamF.Controls.DataGrid.DataGridControl.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderCellTemplate : ContentView
    {
        private DataGridColumn _dataGridColumn;
        public DataGridColumn ColumnData => _dataGridColumn;

        public event EventHandler<DataGridColumn> OnHeaderTapped;
        public HeaderCellTemplate(DataGridColumn dataGridColumn)
        {
            InitializeComponent();

            _dataGridColumn = dataGridColumn;
            this.BindingContext = this;

            this.headerLabel.Text = dataGridColumn.Title;
        }
        // TODO: subscribe from xaml
        private void HeaderTapped(object sender, EventArgs e)
        {
            if (_dataGridColumn.SortingEnabled)
                _dataGridColumn.ToggleSorting();

            OnHeaderTapped?.Invoke(sender, _dataGridColumn);
        }
    }
}