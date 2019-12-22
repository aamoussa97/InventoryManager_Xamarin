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
    public partial class InvPageMaterials : ContentPage
    {
        //private const string Url = "";
        private HttpClient _client = new HttpClient();
        private ObservableCollection<Material> _materials;

        public InvPageMaterials()
        {
            InitializeComponent();

            var authHeader = new AuthenticationHeaderValue("bearer", App._token);
            _client.DefaultRequestHeaders.Authorization = authHeader;

            GetMaterials();
            addMaterialButton.Clicked += AddMaterialButton_Clicked;
        }

        async void AddMaterialButton_Clicked(object sender, EventArgs e)
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

        protected async void GetMaterials()
        {
            var materialsContentRaw = await _client.GetStringAsync("http://dist.saluton.dk:7119/api/material"); //http://dist.saluton.dk:7119/api/product/brands
            var materialData = JsonConvert.DeserializeObject<List<Material>>(materialsContentRaw);

            _materials = new ObservableCollection<Material>(materialData);
            materialsListView.ItemsSource = _materials;

            base.OnAppearing();
        }

        public void EditMaterial(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void DeleteMaterial(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }
    }
}
