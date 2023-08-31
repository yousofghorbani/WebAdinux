using System.Threading.Tasks;
using WebAdinux.Core.ViewMoels;

namespace WebAdinux.Core.Interfaces
{
    public interface IEmailMessage
    {
        Task<List<GetEmailMessageViewModel>> GetAll();
        Task<bool> Add(EmailMessageViewModel viewModel);
    }
}
