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

namespace InventoryManager
{

    [DesignTimeVisible(true)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();

            loginButton.Clicked += LoginButton_Clicked;
        }

        void LoginButton_Clicked(object sender, EventArgs e)
        {
            var usernameInput = usernameTextBox.Text;
            var passwordInput = passwordTextBox.Text;

            LoginRest();
            //DisplayAlert("Alert", usernameTextBox.Text, "OK");
        }

        private async void LoginRest()
        {
            var usernameInput = usernameTextBox.Text;
            var passwordInput = passwordTextBox.Text;
            string url = "http://dist.saluton.dk:7119/api/session"; //"ttp://localhost:5000/api/session";//"https://localhost:5001/api/session"; //http://dist.saluton.dk:7119/api/session

            User user = new User(usernameInput.ToLower(), passwordInput);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                var json = JsonConvert.SerializeObject(user);
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

                            string responseStream = response.Content.ReadAsStringAsync().Result;
                            LoginToken loginToken = new LoginToken("");
                            loginToken = JsonConvert.DeserializeObject<LoginToken>(responseStream);
                            App._token = loginToken.Token;

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                if (Device.RuntimePlatform == Device.iOS)
                                    Application.Current.MainPage = new InvMainPage();
                                else
                                    Application.Current.MainPage = new NavigationPage(new InvMainPage());

                                //App.Current.MainPage = new NavigationPage(new InvMainPage()); //InvMenuPage
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
