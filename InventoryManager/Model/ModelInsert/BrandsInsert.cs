using System;
namespace InventoryManager.Model.ModelInsert
{
    public class BrandsInsert
    {
        private string mBrandName;

        public BrandsInsert(string mBrandName)
        {
            this.mBrandName = mBrandName;
        }

        public string BrandName { get => mBrandName; set => mBrandName = value; }
    }
}
