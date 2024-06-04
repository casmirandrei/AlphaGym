namespace AlphaGym.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string CardNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }
        public bool IsPaid { get; set; }
        public List<PurchasedItem> PurchasedItems { get; set; }
    }

    public class PurchasedItem
    {
        public int PurchasedItemID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public int PaymentID { get; set; }
        public Payment Payment { get; set; }
    }
}
