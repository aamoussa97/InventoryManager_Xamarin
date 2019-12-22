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
    public partial class InvPageProductVariableCosts : ContentPage
    {

        //private const string Url = "";
        private HttpClient _client = new HttpClient();
        private ObservableCollection<ProductVariableCost> _variableCosts;

        public InvPageProductVariableCosts()
        {
            InitializeComponent();

            var authHeader = new AuthenticationHeaderValue("bearer", App._token);
            _client.DefaultRequestHeaders.Authorization = authHeader;

            GetVariableCosts();
            addVariableCostButton.Clicked += AddVariableCostButton_Clicked;
        }

        async void AddVariableCostButton_Clicked(object sender, EventArgs e)
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

        protected async void GetVariableCosts()
        {
            var productsVariableCostsContentRaw = await _client.GetStringAsync("http://dist.saluton.dk:7119/api/product/variablecosts"); //http://dist.saluton.dk:7119/api/product/
            var productVariableCostData = JsonConvert.DeserializeObject<List<ProductVariableCost>>(productsVariableCostsContentRaw);

            _variableCosts = new ObservableCollection<ProductVariableCost>(productVariableCostData);
            variableCostListView.ItemsSource = _variableCosts;

            base.OnAppearing();
        }

        public void EditProductVariableCost(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void DeleteProductVariableCost(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }
    }
}
