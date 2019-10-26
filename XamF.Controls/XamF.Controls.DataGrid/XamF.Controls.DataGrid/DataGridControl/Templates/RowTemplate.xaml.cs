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
    public partial class RowTemplate : ViewCell
    {
        private List<DataGridColumn> _columns;
        public RowTemplate()
        {
            InitializeComponent();
        }
        public RowTemplate(List<DataGridColumn> columns) : this()
        {
            _columns = columns;

            //----- add columns ----
            for (int i = 0; i < _columns.Count; i++)
            {
                var col = _columns[i];
                var colIndex = _columns.IndexOf(col);

                var columnDef = new ColumnDefinition { Width = col.Width };
                this.rowGridTemplate.ColumnDefinitions.Add(columnDef);

                View cellTemplate = null;
                if (col.CellTemplate == null)
                    cellTemplate = new TextCellTemplate(_columns[i]);
                else
                    cellTemplate = new ContentView() { Content = col.CellTemplate.CreateContent() as View };

                this.rowGridTemplate.Children.Add(cellTemplate);
                Grid.SetColumn(cellTemplate, colIndex + i);

                var seperatorColDef = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Absolute) };
                this.rowGridTemplate.ColumnDefinitions.Add(seperatorColDef);
                var sperator = new BoxView { Opacity = 0.3, BackgroundColor = Color.Black };
                this.rowGridTemplate.Children.Add(sperator);
                Grid.SetColumn(sperator, colIndex + i + 1);
            }
        }
    }
}