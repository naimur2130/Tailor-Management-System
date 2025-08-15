namespace Tailor_Management_System.ViewModel
{
    public class InvoiceViewModel
    {
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string DressType { get; set; }
        public string Measurements { get; set; }
        public string Address { get; set; }
        public decimal Amount { get; set; }
        public string OrderStatus { get; set; }
        public string TimeDuration { get; set; }
    }
}
