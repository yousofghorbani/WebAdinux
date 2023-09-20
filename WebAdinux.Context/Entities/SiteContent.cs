﻿namespace WebAdinux.Context.Entities
{
    public class SiteContent : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? FileLink { get; set; }
        public int Number { get; set; }
        public long HeaderId { get; set; }
        public short ContentType { get; set; }
        public string? Color { get; set; }

        public SiteHeader siteHeader { get; set; }
    }
}
