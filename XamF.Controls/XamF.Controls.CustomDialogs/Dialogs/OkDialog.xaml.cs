using Xamarin.Forms.Xaml;
using XamF.Controls.CustomDialogs.Base;

namespace XamF.Controls.CustomDialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OkDialog : DialogBase
    {
        public OkDialog(string message)
        {
            InitializeComponent();
            _message = message;
            this.btnOk.Clicked += (sender, arg) =>
            {
                Proccess.SetResult(true);
                _popupNavigation.PopAsync();
            };
        }
    }
}