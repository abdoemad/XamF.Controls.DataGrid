using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamF.Controls.CustomDialogs.Core;

namespace XamF.Controls.CustomDialogs.Base
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public abstract partial class DialogBase : PopupPage
    {
        #region Variables
        protected Dictionary<DialogAction, Task> DialogActions;
        protected Action OnApearing;
        protected TaskCompletionSource<object> Proccess;
        //protected ILocator _locator;
        protected IPopupNavigation _popupNavigation;
        #endregion Variables
        #region Properties
        protected string _message;
        protected string Message { get => _message; set => _message = value; }
        #endregion Properties
        public DialogBase()
        {
            InitializeComponent();
            //_locator = App.Locator;
            _popupNavigation = PopupNavigation.Instance;
            this.CloseWhenBackgroundIsClicked = false;
        }
        #region Bindable Properties
        public static readonly BindableProperty ActionsPlaceHolderProperty
            = BindableProperty.Create(nameof(ActionsPlaceHolder), typeof(View), typeof(DialogBase));
        public View ActionsPlaceHolder
        {
            get { return (View)GetValue(ActionsPlaceHolderProperty); }
            set { SetValue(ActionsPlaceHolderProperty, value); }
        }
        #endregion Bindable Properties
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.lblMessage.Text = _message;
            OnApearing?.Invoke();
            Proccess = new TaskCompletionSource<object>();
        }
        public virtual Task<object> GetResult()
        {
            return Proccess.Task;
        }
    }
}