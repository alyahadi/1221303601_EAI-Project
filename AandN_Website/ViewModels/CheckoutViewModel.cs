namespace AandN_Website.ViewModels
{
    public class CheckoutViewModel
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; } // optional for display
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        // Customer Info
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string PromotionCode { get; set; }
        public string PaymentMethod { get; set; }
    }
}
