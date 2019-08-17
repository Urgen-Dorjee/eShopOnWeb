using System;
using System.Collections.Generic;
using System.Linq;

namespace eWebShop.WebUI.Pages.Basket
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>();
        public string BuyerId { get; set; }
        public decimal Total()
        {
            return Math.Round(Items.Sum(c => c.UnitPrice * c.Quantity), 2);
        }
    }
}
