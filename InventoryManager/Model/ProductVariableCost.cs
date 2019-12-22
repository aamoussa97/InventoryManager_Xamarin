using System;
namespace InventoryManager.Model
{
    public class ProductVariableCost
    {
        public ProductVariableCost(int mVariableCostID, int mVariableCostValue)
        {
            VariableCostID = mVariableCostID;
            VariableCostValue = mVariableCostValue;
        }

        public int VariableCostID { get; set; }

        public int VariableCostValue { get; set; }
    }
}
