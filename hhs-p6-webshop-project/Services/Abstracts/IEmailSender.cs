using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Services.Abstracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}