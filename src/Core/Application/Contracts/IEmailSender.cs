using System.Threading.Tasks;

namespace eWebShop.Application.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
