using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamF.Controls.CustomDialogs.Services.Imp
{
    public class DialogService : IDialogService
    {
        private readonly IPopupNavigation _popupNavigation;
        public DialogService()
        {
            _popupNavigation = PopupNavigation.Instance;
        }
        protected Page CurrentMainPage => Application.Current.MainPage;
        public async Task ShowDialogAsync(string title, string message, string buttonText)
        {
            await CurrentMainPage.DisplayAlert(title, message, buttonText);
        }
        #region Custom Popups
        public async Task<bool> ShowConfirmationDialogAsync(string message, string yesButtonText = "Yes", string noButtonText = "No")
        {
            var confirmationDialog = new ConfirmationDialog(message)
            {
                YesButtonText = yesButtonText,
                NoButtonText = noButtonText
            };
            await _popupNavigation.PushAsync(confirmationDialog);
            var result = await confirmationDialog.GetResult();
            await _popupNavigation.PopAllAsync();
            return (bool)result;
        }
        public async Task ShowInformationDialogAsync(string message)
        {
            var informatinoDialog = new InfoDialog(message);
            await _popupNavigation.PushAsync(informatinoDialog);
        }
        public async Task CloseOpendDialogsAsync()
        {
            await _popupNavigation.PopAllAsync();
        }
        public async Task ShowOkDialogAsync(string message)
        {
            var okDialog = new OkDialog(message);
            await _popupNavigation.PushAsync(okDialog);
        }
        #endregion Custom Popups
    }
}
