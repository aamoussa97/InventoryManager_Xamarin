using System;
namespace InventoryManager.Model
{
    public class LoginToken
    {
        private string token;

        public LoginToken(string token)
        {
            this.token = token;
        }

        public string Token { get => token; set => token = value; }
    }
}
