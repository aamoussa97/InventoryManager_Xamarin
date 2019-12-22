using System;
namespace InventoryManager.Model
{
    public class User
    {
        private string mUsername;
        private string mPassword;

        public User(string mUsername, string mPassword)
        {
            this.mUsername = mUsername;
            this.mPassword = mPassword;
        }

        public string Username { get => mUsername; set => mUsername = value; }
        public string Password { get => mPassword; set => mPassword = value; }
    }
}
