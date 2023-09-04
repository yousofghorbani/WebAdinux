namespace WebAdinux.Context.Entities
{
    public class SiteHeader : BaseEntity
    {
        public string Title { get; set; }
        public string? Link { get; set; }
        public bool HasDropDown { get; set; }
        public long? ParentId { get; set; }


        public SiteHeader siteHeader { get; set; }
        public ICollection<SiteHeader> siteHeaders { get; set; }
        public ICollection<SiteContent> siteContents { get; set; }
    }
}