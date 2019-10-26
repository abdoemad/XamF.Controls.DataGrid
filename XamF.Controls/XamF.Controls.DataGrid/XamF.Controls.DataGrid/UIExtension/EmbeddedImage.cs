using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamF.Controls.DataGrid.UIExtension
{
    /// <summary>
    /// Embedded image
    /// </summary>
    [ContentProperty("ResourceUri")]
    public class EmbeddedImage : IMarkupExtension
    {
        public string ResourceUri { get; set; }

        /// <summary>
        /// Passing service provider and return the target object
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(ResourceUri))
                return null;

            return ImageSource.FromResource(ResourceUri);
        }
    }
}
