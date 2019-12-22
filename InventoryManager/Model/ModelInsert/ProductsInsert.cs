using System;
namespace InventoryManager.Model.ModelInsert
{
    public class ProductInsert
    {
        private long mProductSKU;
        private string mProductName;
        private string mProductBrand;
        private int mProductMaterialsOrderID;
        private int mProductPrice;
        private int mProductVariableCost;
        private int mProductStartFactor;
        private int mProductGrowthFactor;

        public ProductInsert(long mProductSKU, string mProductName, string mProductBrand, int mProductMaterialsOrderID, int mProductPrice, int mProductVariableCost, int mProductStartFactor, int mProductGrowthFactor)
        {
            this.mProductSKU = mProductSKU;
            this.mProductName = mProductName;
            this.mProductBrand = mProductBrand;
            this.mProductMaterialsOrderID = mProductMaterialsOrderID;
            this.mProductPrice = mProductPrice;
            this.mProductVariableCost = mProductVariableCost;
            this.mProductStartFactor = mProductStartFactor;
            this.mProductGrowthFactor = mProductGrowthFactor;
        }

        public long ProductSKU { get => mProductSKU; set => mProductSKU = value; }
        public string ProductName { get => mProductName; set => mProductName = value; }
        public string ProductBrand { get => mProductBrand; set => mProductBrand = value; }
        public int ProductMaterialsOrderID { get => mProductMaterialsOrderID; set => mProductMaterialsOrderID = value; }
        public int ProductPrice { get => mProductPrice; set => mProductPrice = value; }
        public int ProductVariableCost { get => mProductVariableCost; set => mProductVariableCost = value; }
        public int ProductStartFactor { get => mProductStartFactor; set => mProductStartFactor = value; }
        public int ProductGrowthFactor { get => mProductGrowthFactor; set => mProductGrowthFactor = value; }
    }
}
