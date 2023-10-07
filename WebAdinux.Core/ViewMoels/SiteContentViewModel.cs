using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WebAdinux.Context.Enums;

namespace WebAdinux.Core.ViewMoels
{
    public class SiteContentViewModel
    {
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string? HtmlContent { get; set; }
        public string? Icon { get; set; }
        public string? FileLink { get; set; }
        public IFormFile? UploadFile { get; set; }
        public int Number { get; set; }
        public long HeaderId { get; set; }
        public ContentType ContentType { get; set; }
        public string? Color { get; set; }
    }
    public class GetSiteContentViewModel : SiteContentViewModel
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}