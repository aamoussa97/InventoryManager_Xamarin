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

namespace InventoryManager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvTabbedPage : TabbedPage
    {
        //private const string Url = "";
        private HttpClient _client = new HttpClient();
        private ObservableCollection<Brand> _brands;
        private ObservableCollection<Product> _products;

        public InvTabbedPage()
        {
            InitializeComponent();

            GetProducts();
            GetBrands();

        }

        //getBrands()
        protected async void GetProducts() 
        {
            var productsContentRaw = await _client.GetStringAsync("http://dist.saluton.dk:7119/api/product/");
            var productData = JsonConvert.DeserializeObject<List<Product>>(productsContentRaw);

            _products = new ObservableCollection<Product>(productData);
            productsListView.ItemsSource = _products;

            base.OnAppearing();
        }

        protected async void GetBrands()
        {
            var brandsContentRaw = await _client.GetStringAsync("http://dist.saluton.dk:7119/api/product/brands");
            var brandData = JsonConvert.DeserializeObject<List<Brand>>(brandsContentRaw);

            _brands = new ObservableCollection<Brand>(brandData);
            brandsListView.ItemsSource = _brands;

            base.OnAppearing();
        }
    }
}
