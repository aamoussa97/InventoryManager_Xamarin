using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using InventoryManager.Model;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace InventoryManager
{
    public partial class InvPageProducts : ContentPage
    {
        //private const string Url = "";
        private HttpClient _client = new HttpClient();
        private ObservableCollection<Product> _products;

        public InvPageProducts()
        {
            InitializeComponent();

            var authHeader = new AuthenticationHeaderValue("bearer", App._token);
            _client.DefaultRequestHeaders.Authorization = authHeader;

            GetProducts();
            addProductButton.Clicked += AddProductButton_Clicked;
        }

        async void AddProductButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InvInsertProduct());
        }

        protected async void GetProducts()
        {
            var productsContentRaw = await _client.GetStringAsync("http://dist.saluton.dk:7119/api/product/"); //http://dist.saluton.dk:7119/api/product/
            var productData = JsonConvert.DeserializeObject<List<Product>>(productsContentRaw);

            _products = new ObservableCollection<Product>(productData);
            productsListView.ItemsSource = _products;

            base.OnAppearing();
        }

        public void EditProduct(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void DeleteProduct(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            //var keyword = SearchBarProducts.Text;

            //var suggestions = _products.Where(productsListView. => c.Equals(keyword));
            //var s = from c in _products where c.Equals(keyword) select c;

            //productsListView.ItemsSource = suggestions;

            /*
            var _container = BindingContext as _products;
            productsListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                productsListView.ItemsSource = _container.MyListCollector;
            else
                productsListView.ItemsSource = _container.MyListCollector.Where(i => i.MyName.Contains(e.NewTextValue));

            productsListView.EndRefresh();*/
        }

    }
}
