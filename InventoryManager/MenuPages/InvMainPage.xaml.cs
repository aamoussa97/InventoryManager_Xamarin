using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace InventoryManager
{
    public partial class InvMainPage : MasterDetailPage
    {
        public InvMainPage()
        {
            InitializeComponent();

            invMasterPage.listView.ItemSelected += OnItemSelected;

            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                invMasterPage.listView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
