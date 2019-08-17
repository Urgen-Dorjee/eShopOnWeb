using Ardalis.GuardClauses;
using eWebShop.Application.Contracts;
using System.Collections.Generic;

namespace eWebShop.Application.Entities.BuyerAggregate
{
    public class Buyer : BaseEntity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }
        private List<ModeOfPayment> _paymentMethods = new List<ModeOfPayment>();
        public IEnumerable<ModeOfPayment> PaymentMethods => _paymentMethods.AsReadOnly();
        private Buyer()
        {}
        public Buyer(string identity) : this()
        {
            Guard.Against.NullOrEmpty(identity, nameof(identity));
            IdentityGuid = identity;
        }
    }
}
