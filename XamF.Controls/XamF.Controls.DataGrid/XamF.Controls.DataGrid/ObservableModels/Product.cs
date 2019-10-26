using System;
using System.Collections.Generic;
using System.Text;

namespace XamF.Controls.DataGrid.ObservableModels
{
    public class Product : ObserverBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
