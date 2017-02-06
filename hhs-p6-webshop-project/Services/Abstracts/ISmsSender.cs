using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}