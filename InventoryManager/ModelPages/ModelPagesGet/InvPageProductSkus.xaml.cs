using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using InventoryManager.Model;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using System.Net.Http.Headers;

namespace InventoryManager//.ModelPages.ModelPagesGet
{
    public partial class InvPageProductSkus : ContentPage
    {
        //private const string Url = "";
        private HttpClient _client = new HttpClient();
        private ObservableCollection<ProductSku> _skus;

        public InvPageProductSkus()
        {
            InitializeComponent();

            var authHeader = new AuthenticationHeaderValue("bearer", App._token);
            _client.DefaultRequestHeaders.Authorization = authHeader;

            GetSkus();
            addSkuButton.Clicked += AddSkuButton_Clicked;
        }

        async void AddSkuButton_Clicked(object sender, EventArgs e)
        {
            //var usernameInput = usernameTextBox.Text;
            //var passwordInput = passwordTextBox.Text;

            //Application.Current.MainPage.Navigation.PushAsync(InvInsertProduct);
            //App.Current.MainPage.Navigation.PushAsync(new InvInsertProduct);
            //await .PushAsync(new InvInsertProduct());
            //DisplayAlert("Alert", usernameTextBox.Text, "OK");

            if (Device.RuntimePlatform == Device.iOS)
                Application.Current.MainPage = new InvInsertProduct();
            else
                Application.Current.MainPage = new NavigationPage(new InvInsertProduct());
        }

        protected async void GetSkus()
        {
            var productsSkuContentRaw = await _client.GetStringAsync("http://dist.saluton.dk:7119/api/product/skus"); //http://dist.saluton.dk:7119/api/product/
            var productSkuData = JsonConvert.DeserializeObject<List<ProductSku>>(productsSkuContentRaw);

            _skus = new ObservableCollection<ProductSku>(productSkuData);
            skusListView.ItemsSource = _skus;

            base.OnAppearing();
        }

        public void EditSku(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void DeleteSku(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }

    }
}
