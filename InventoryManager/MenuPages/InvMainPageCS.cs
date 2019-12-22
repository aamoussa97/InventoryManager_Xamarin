using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace InventoryManager
{
    public class InvMainPageCS : MasterDetailPage
    {
        InvMasterPageCS invMasterPage;

        public InvMainPageCS()
        {
            invMasterPage = new InvMasterPageCS();
            Master = invMasterPage;
            Detail = new NavigationPage(new InvPageBrands());

            invMasterPage.ListView.ItemSelected += OnItemSelected;

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
                invMasterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}