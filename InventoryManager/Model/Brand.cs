using System;
namespace InventoryManager.Model
{
    public class Brand
    {
        private int mBrandID;
        private string mBrandName;

        public Brand(int mBrandID, string mBrandName)
        {
            this.mBrandID = mBrandID;
            this.mBrandName = mBrandName;
        }

        public int BrandID { get => mBrandID; set => mBrandID = value; }
        public string BrandName { get => mBrandName; set => mBrandName = value; }
    }
}
