using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamF.Controls.CustomDialogs.Services
{
    public interface IDialogService
    {
        Task ShowDialogAsync(string title, string message, string buttonText);
        Task ShowInformationDialogAsync(string message);
        Task ShowOkDialogAsync(string message);
        Task<bool> ShowConfirmationDialogAsync(string message, string yesButtonText = "Yes", string noButtonText = "No");
        Task CloseOpendDialogsAsync();
    }
}
