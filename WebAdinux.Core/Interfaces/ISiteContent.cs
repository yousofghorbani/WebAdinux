using WebAdinux.Core.ViewMoels;

namespace WebAdinux.Core.Interfaces
{
    public interface ISiteContent
    {
        Task<bool> Add(SiteContentViewModel viewModel);
        Task<long> Remove(long id);
        Task<List<GetSiteContentViewModel>> GetByHeaderId(long headerId);
        Task<bool> Update(long id ,SiteContentViewModel viewModel);
        Task<SiteContentViewModel?> GetById(long id);
    }
}