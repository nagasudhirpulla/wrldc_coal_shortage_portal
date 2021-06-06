using System.Threading.Tasks;

namespace CoalShortagePortal.Core.Interfaces.Sms
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
