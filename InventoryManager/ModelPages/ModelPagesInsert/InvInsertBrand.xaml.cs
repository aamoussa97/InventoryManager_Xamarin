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
using InventoryManager.Model.ModelInsert;

using Xamarin.Forms;
using System.Net.Http.Headers;

namespace InventoryManager.ModelPages.ModelPagesInsert
{
    public partial class InvInsertBrand : ContentPage
    {
        public InvInsertBrand()
        {
            InitializeComponent();
            insertButton.Clicked += InsertButton_Clicked;
        }

        async void InsertButton_Clicked(object sender, EventArgs e)
        {
            InsertBrand();
        }

        private async void InsertBrand()
        {
            var brandNameInput = nameTextBox.Text;
            string url = "http://dist.saluton.dk:7119/api/product/brands"; //"ttp://localhost:5000/api/session";//"https://localhost:5001/api/session"; //http://dist.saluton.dk:7119/api/session

            BrandsInsert brandsInsert = new BrandsInsert(brandNameInput);

            using (var client = new HttpClient())

            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                var json = JsonConvert.SerializeObject(brandsInsert);
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    var authHeader = new AuthenticationHeaderValue("bearer", App._token);
                    client.DefaultRequestHeaders.Authorization = authHeader;

                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        try
                        {
                            response.EnsureSuccessStatusCode();
                            Console.WriteLine("OK, brands inserted.");

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                DisplayAlert("POST status", "Brand inserted succesful", "OK");
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
