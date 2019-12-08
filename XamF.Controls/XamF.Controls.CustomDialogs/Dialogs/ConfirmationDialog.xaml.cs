using Xamarin.Forms.Xaml;
using XamF.Controls.CustomDialogs.Base;

namespace XamF.Controls.CustomDialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmationDialog : DialogBase
    {
        public string YesButtonText { get; set; }
        public string NoButtonText { get; set; }
        public ConfirmationDialog(string message)
        {
            InitializeComponent();
            _message = message;
            OnApearing = () =>
            {
                this.btnNo.Text = NoButtonText;
                this.btnYes.Text = YesButtonText;
            };
            this.btnNo.Clicked += (sender, args) =>
            {
                Proccess.SetResult(false);
            };

            this.btnYes.Clicked += (sender, args) =>
            {
                Proccess.SetResult(true);
            };
        }
    }
}