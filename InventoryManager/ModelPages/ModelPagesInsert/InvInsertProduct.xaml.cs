using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using InventoryManager.Model;
using InventoryManager.Model.ModelInsert;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace InventoryManager
{
    public partial class InvInsertProduct : ContentPage
    {
        private HttpClient _client = new HttpClient();
        private ObservableCollection<Brand> _brands;

        public InvInsertProduct()
        {
            InitializeComponent();

            var authHeader = new AuthenticationHeaderValue("bearer", App._token);
            _client.DefaultRequestHeaders.Authorization = authHeader;

            PopulateBrandPicker();
            insertButton.Clicked += InsertButton_Clicked;
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem; // This is the model selected in the picker
            InsertProduct("APPLE");//selectedItem.ToString);
        }

        async void InsertButton_Clicked(object sender, EventArgs e)
        {
            InsertProduct("");
        }

        private async void PopulateBrandPicker()
        {
            List<String> brandsList = new List<String>();

            var brandsContentRaw = await _client.GetStringAsync("http://dist.saluton.dk:7119/api/product/brands"); //http://dist.saluton.dk:7119/api/product/brands
            var brandData = JsonConvert.DeserializeObject<List<Brand>>(brandsContentRaw);
            _brands = new ObservableCollection<Brand>(brandData);

            foreach (var brands in _brands)
            {
                brandsList.Add(brands.BrandName);
            }

            //brandPicker.ItemsSource = _brands;
            brandPicker.ItemsSource = brandsList;
            
            base.OnAppearing();
        }

        private async void InsertProduct(String brandPickerValue)
        {
            var productNameInput = nameTextBox.Text;
            var productSKUInput = skuTextBox.Text;
            long productSKUInputLong = long.Parse(productSKUInput);
            var productBrandPicker = brandPickerValue;//brandPicker;
            int productMaterialsInput = int.Parse(materialsTextBox.Text);
            int productPriceInput = int.Parse(priceTextBox.Text);
            int productVariableCostInput = int.Parse(variablecostTextBox.Text);
            int productStartFactorInput = int.Parse(startfactorTextBox.Text);
            int productGrowthFactorInput = int.Parse(growthfactorTextBox.Text);

            string url = "http://dist.saluton.dk:7119/api/product"; //"ttp://localhost:5000/api/session";//"https://localhost:5001/api/session"; //http://dist.saluton.dk:7119/api/session

            ProductInsert productInsert = new ProductInsert(productSKUInputLong, productNameInput, brandPickerValue, productMaterialsInput, productPriceInput, productVariableCostInput, productStartFactorInput, productGrowthFactorInput); 
    
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                var json = JsonConvert.SerializeObject(productInsert);
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
                            Console.WriteLine("OK, products inserted.");

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                DisplayAlert("POST status", "Product inserted succesful", "OK");
                                Navigation.PopAsync();
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
