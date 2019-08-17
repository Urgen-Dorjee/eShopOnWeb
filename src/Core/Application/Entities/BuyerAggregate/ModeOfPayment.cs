namespace eWebShop.Application.Entities.BuyerAggregate
{
    public class ModeOfPayment : BaseEntity
    {
        public string Alias { get; set; }
        public string CardId { get; set; } // actual card data must be stored in a PCI compliant system, like Stripe
        public string Last4 { get; set; }
    }
}
