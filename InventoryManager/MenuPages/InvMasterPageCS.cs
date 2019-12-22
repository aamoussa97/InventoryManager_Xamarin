using System.Collections.Generic;
//using InventoryManager.ModelPages.ModelPagesGet;
using InventoryManager.Model;
using Xamarin.Forms;

namespace InventoryManager
{
    public class InvMasterPageCS : ContentPage
    {
        public ListView ListView { get { return listView; } }

        ListView listView;

        public InvMasterPageCS()
        {
            var masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Brands",
                TargetType = typeof(InvPageBrands)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Products",
                TargetType = typeof(InvPageProducts)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Materials",
                TargetType = typeof(InvPageMaterials)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Skus",
                TargetType = typeof(InvPageProductSkus)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Prices",
                TargetType = typeof(InvPageProductPrices)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Variablecosts",
                TargetType = typeof(InvPageProductVariableCosts)
            });

            listView = new ListView
            {
                ItemsSource = masterPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                    var image = new Image();
                    image.SetBinding(Image.SourceProperty, "IconSource");
                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Title");

                    grid.Children.Add(image);
                    grid.Children.Add(label, 1, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None
            };

            Icon = "hamburger.png";
            Title = "Personal Organiser";
            Padding = new Thickness(0, 40, 0, 0);
            Content = new StackLayout
            {
                Children = { listView }
            };
        }
    }
}
