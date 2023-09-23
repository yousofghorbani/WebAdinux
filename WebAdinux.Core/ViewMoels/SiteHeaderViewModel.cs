using WebAdinux.Context.Enums;

namespace WebAdinux.Core.ViewMoels
{
    public class SiteHeaderViewModel
    {
        public string Title { get; set; }
        public string? Link { get; set; }
        public bool? HasDropDown { get; set; }
        public long? ParentId { get; set; }
        public bool? Visible { get; set; }
    }
    public class SiteSubHeaderViewModel
    {
        public string Title { get; set; }
        public string? Link { get; set; }
        public bool? HasDropDown { get; set; }
        public long? ParentId { get; set; }
        public bool? Visible { get; set; }
        public Visible VisibleType { get; set; }
    }
    public class GetSiteHeaderViewModel : SiteHeaderViewModel
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}