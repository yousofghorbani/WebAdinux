using System.Threading.Tasks;
using WebAdinux.Core.ViewMoels;

namespace WebAdinux.Core.Interfaces
{
    public interface IEmailMessage
    {
        Task<List<GetEmailMessageViewModel>> GetAll(bool isArchived);
        Task<GetEmailMessageViewModel?> GetById(long id);
        Task<bool> Add(EmailMessageViewModel viewModel);
        Task<bool> Archive(long id);
        Task<bool> UnArchive(long id);
    }
}
