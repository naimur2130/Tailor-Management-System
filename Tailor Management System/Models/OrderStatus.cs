namespace Tailor_Management_System.Models
{
    public static class OrderStatus
    {
        public const string Pending = "Pending";              
        public const string AssignedToTailor = "Assigned";    
        public const string Rejected = "Rejected";            
        public const string ConfirmedByTailor = "Confirmed";  
        public const string Paid = "Paid";
        public const string ReadyForPayment = "ReadyForPayment";
        public const string Terminated = "Terminated";  
    }
}
