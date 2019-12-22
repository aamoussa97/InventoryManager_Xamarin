using System;
namespace InventoryManager.Model
{
    public struct Product
    {
        public int ProductID { get; set; }
        public long ProductSKU { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public int ProductPrice { get; set; }
        public int ProductVariableCost { get; set; }
        public int ProductStartFactor { get; set; }
        public int ProductGrowthFactor { get; set; }
        
        public int ProductQuantity { get; set; }
        
        public Product(int id,
                       long sku,
                       string name,
                       string brandID,
                       int price,
                       int variablecost,
                       int startFactor,
                       int growthFactor,
                       int quantity
                       )
        {
            this.ProductID = id;
            this.ProductSKU = sku;
            this.ProductName = name;
            this.ProductBrand = brandID;
            this.ProductPrice = price;
            this.ProductVariableCost = variablecost;
            this.ProductStartFactor = startFactor;
            this.ProductGrowthFactor = growthFactor;
            this.ProductQuantity = quantity;
        }
    }
}