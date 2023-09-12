using WebAdinux.Core.ViewMoels;

namespace WebAdinux.Core.Interfaces
{
    public interface ISiteHeader
    {
        Task<bool> Add(SiteHeaderViewModel viewModel);
        Task<List<GetSiteHeaderViewModel>> Filter(bool? hasDropDown);
        Task<bool> Update(long id, SiteHeaderViewModel viewModel);
        Task<bool> Delete(long id);
        Task<SiteHeaderViewModel> GetById(long id);
    }
}