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
using InventoryManager.ModelPages.ModelPagesInsert;
using InventoryManager.Model.ModelDelete;

using Xamarin.Forms;
using System.Net.Http.Headers;

namespace InventoryManager
{
    public partial class InvPageBrands : ContentPage
    {
        //private const string Url = "";
        private HttpClient _client = new HttpClient();
        private ObservableCollection<Brand> _brands;

        public InvPageBrands()
        {
            InitializeComponent();

            var authHeader = new AuthenticationHeaderValue("bearer", App._token);
            _client.DefaultRequestHeaders.Authorization = authHeader;

            GetBrands();
            addBrandButton.Clicked += AddBrandButton_Clicked;
        }

        async void AddBrandButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InvInsertBrand());

            /*
            if (Device.RuntimePlatform == Device.iOS)
                Application.Current.MainPage = new InvInsertBrand();
            else
                Application.Current.MainPage = new NavigationPage(new InvInsertBrand());
                Navigation.PushAsync(new InvInsertBrand());
                */               
        }

        protected async void GetBrands()
        {
            var brandsContentRaw = await _client.GetStringAsync("http://dist.saluton.dk:7119/api/product/brands");
            var brandData = JsonConvert.DeserializeObject<List<Brand>>(brandsContentRaw);

            _brands = new ObservableCollection<Brand>(brandData);
            brandsListView.ItemsSource = _brands;

            base.OnAppearing();
        }

        public void EditBrand(object sender, EventArgs e)
        {
            var menu = sender as MenuItem;
            var item = menu.CommandParameter as Brand; //or your type

            var mi = ((MenuItem)sender);
            DisplayAlert(item.BrandName, mi.CommandParameter + " more context action", "OK");
        }

        public async void DeleteBrand(object sender, EventArgs e)
        {
            var menu = sender as MenuItem;
            var item = menu.CommandParameter as Brand; //or your type

            BrandsDelete brandsDelete = new BrandsDelete(item.BrandID);
            DeleteBrandCall(brandsDelete);
        }

        private async void UpdateBrandCall(Brand brand)
        {
            string url = "http://dist.saluton.dk:7119/api/product/brands"; //"ttp://localhost:5000/api/session";//"https://localhost:5001/api/session"; //http://dist.saluton.dk:7119/api/session

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Put, url))
            {
                var json = JsonConvert.SerializeObject(brand);           
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        try
                        {
                            response.EnsureSuccessStatusCode();
                            Console.WriteLine("OK, brands updated.");

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                DisplayAlert("PUT status", "Brand updated succesfull", "OK");
                            });
                        }

                        catch (HttpRequestException e)
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                DisplayAlert("Login status", "Wrong credentials", "OK");
                            });
                        }
                    }
                }
            }
        }

        private async void DeleteBrandCall(BrandsDelete brandsDelete)
        {
            string url = "http://dist.saluton.dk:7119/api/product/brands"; //"ttp://localhost:5000/api/session";//"https://localhost:5001/api/session"; //http://dist.saluton.dk:7119/api/session

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Delete, url))
            {
                var json = JsonConvert.SerializeObject(brandsDelete);
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        try
                        {
                            response.EnsureSuccessStatusCode();
                            Console.WriteLine("OK, brands deleted.");

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                DisplayAlert("DELETE status", "Brand deleted succesfull", "OK");
                            });
                        }

                        catch (HttpRequestException e)
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                DisplayAlert("Login status", "Wrong credentials", "OK");
                            });
                        }
                    }
                }
            }
        }
    }
}
