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
    public partial class TextCellTemplate : ContentView
    {
        private DataGridColumn _dataGridColumn;
        public DataGridColumn ColumnData => _dataGridColumn;
        public TextCellTemplate(DataGridColumn dataGridColumn)
        {
            InitializeComponent();

            _dataGridColumn = dataGridColumn;

            this.labelCell.SetBinding(Label.TextProperty, _dataGridColumn.PropertyName);
        }
    }
}