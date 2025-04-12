using Microsoft.VisualBasic;

namespace RadioCabs.Models.ViewModel
{
    public class SubscriptionVM
    {
        public bool IsSubscribed { get; set; }
        public string CurrentPlan { get; set; }
        public string? LastPaymentDate { get; set; }
    }
}
