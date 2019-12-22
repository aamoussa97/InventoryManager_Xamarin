using System;
namespace InventoryManager.Model
{
    public class ProductSku
    {
        public ProductSku(int mSkuid, long mSkuValue)
        {
            SKUID = mSkuid;
            SKUValue = mSkuValue;
        }

        public int SKUID { get; set; }

        public long SKUValue { get; set; }
    }
}
