using System.Security.Principal;

namespace eWebShop.Application.Contracts
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
