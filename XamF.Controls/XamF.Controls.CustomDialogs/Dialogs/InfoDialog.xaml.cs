using Xamarin.Forms.Xaml;
using XamF.Controls.CustomDialogs.Base;

namespace XamF.Controls.CustomDialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoDialog : DialogBase
    {
        public InfoDialog(string message) : base()
        {
            InitializeComponent();
            _message = message;
            this.CloseWhenBackgroundIsClicked = true;
        }
    }
}