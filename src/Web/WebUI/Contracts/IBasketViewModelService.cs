using eWebShop.WebUI.Pages.Basket;
using System.Threading.Tasks;

namespace eWebShop.WebUI.Contracts
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    }
}
