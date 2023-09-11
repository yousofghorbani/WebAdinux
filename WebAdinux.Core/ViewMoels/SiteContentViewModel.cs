namespace WebAdinux.Core.ViewMoels
{
    public class SiteContentViewModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? FileLink { get; set; }
        public int Number { get; set; }
        public long HeaderId { get; set; }
    }
    public class GetSiteContentViewModel : SiteContentViewModel
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}