namespace Satisfactory.Calculator
{
    public class ItemQuantity
    {
        public ItemQuantity(string itemCode, int quantity)
        {
            ItemCode = itemCode;
            Quantity = quantity;
        }

        public string ItemCode { get; set; }

        public int Quantity { get; set; }
    }
}