using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamF.Controls.CustomDialogs.Services;
using XamF.Controls.CustomDialogs.Services.Imp;
using XamF.Controls.DataGrid.ObservableModels;

namespace XamF.Controls.DataGrid.ViewModels
{
    public class DataGridDemoViewModel : BaseViewModel
    {
        IDialogService _dialogService;
        private ObservableCollection<Product>  _productList;
        public ObservableCollection<Product> ProductList
        {
            get => _productList;
            set { SetProperty(ref _productList, value); }
        }

        public DataGridDemoViewModel()
        {
            _dialogService = new DialogService();
            PopulateProducts();
            OnPropertyChanged(nameof(ProductList));
        }

        private ICommand loadProductsCommand;
        public ICommand LoadProductsCommand => loadProductsCommand ?? (loadProductsCommand = new Command(() =>
        {
            PopulateProducts();
            OnPropertyChanged(nameof(ProductList));
        }));

        private IOrderedQueryable<Product> _productQuery;
        private ICommand _sortCommand;
        public ICommand SortCommand => _sortCommand ?? (_sortCommand = new Command((orderedQuery) =>
        {
            _productQuery = orderedQuery as IOrderedQueryable<Product>;
            ProductList = new ObservableCollection<Product>(_productQuery.Cast<Product>());
        }));

        private ICommand _productSelectedCommand;
        public ICommand SelectedProductCommand => _productSelectedCommand ?? (_productSelectedCommand = new Command<Product>(async (selectedProduct) =>
        {
           var confirmed = await _dialogService.ShowConfirmationDialogAsync($"Are you sure you want to delete Item: {selectedProduct.Name} ?");
           if (!confirmed)
               return;

           ProductList.Remove(selectedProduct);
        }));
        private List<Product> _cachedProducts;
        private ICommand _searchTextChangedCommand;
        public ICommand SearchTextChangedCommand => _searchTextChangedCommand ??
            (_searchTextChangedCommand = new Command<string>(async (searchText) =>
            {
                _productQuery = null;
                await Delay(async () =>
                {
                    await Task.Run(() =>
                    {
                        var products = _cachedProducts.Where(c => string.IsNullOrWhiteSpace(searchText)
                        || c.Name.ToLower().Contains(searchText.ToLower())).ToList();

                        _productList = new ObservableCollection<Product>(products);
                    });

                    OnPropertyChanged(nameof(ProductList));
                }).ConfigureAwait(false);
            }));
        void PopulateProducts()
        {
            _cachedProducts = new List<Product>() {
                new Product{
                    Name="Wireless Headphone",
                    Price=600,
                    Stock=50
                },
                new Product{
                    Name ="Samsung TV",
                    Price=300,
                    Stock = 20
                },
                new Product
                {
                    Name = "Dell Laptop",
                    Price = 1000,
                    Stock = 25
                },
                new Product{
                    Name ="Samsung Mobile",
                    Price=600,
                    Stock = 5
                },
                 new Product{
                    Name ="HTC Mobile",
                    Price=400,
                    Stock= 15
                 }};

            ProductList =  new ObservableCollection<Product>(_cachedProducts);
        }
        //================= Helper ==========

        //---- Delay
        private CancellationTokenSource throttleCts = new CancellationTokenSource();
        //TODO: move this method to helper class to be reusable
        private async Task Delay(Func<Task> taskDelegate)
        {
            try
            {
                Interlocked.Exchange(ref this.throttleCts, new CancellationTokenSource()).Cancel();
                await Task.Delay(TimeSpan.FromMilliseconds(700), this.throttleCts.Token)
                    .ContinueWith(async task =>
                    {
                        await taskDelegate();
                    }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                //Ignore any Threading errors
            }
        }
    }
}
