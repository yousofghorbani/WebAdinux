using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAdinux.Context.Context;
using WebAdinux.Context.Entities;
using WebAdinux.Context.Enums;
using WebAdinux.Core.Interfaces;
using WebAdinux.Core.ViewMoels;

namespace WebAdinux.Core.Services
{
    public class SiteContentService : ISiteContent
    {
        private readonly DataBaseContext _context;

        public SiteContentService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(SiteContentViewModel viewModel)
        {
            bool headerExists = await _context.siteHeaders.AnyAsync(x => x.Id == viewModel.HeaderId);
            if (!headerExists) return false;

            SiteContent content = new SiteContent()
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Icon = viewModel.Icon,
                FileLink = viewModel.FileLink,
                Number = viewModel.Number,
                HeaderId = viewModel.HeaderId,
                ContentType = (short)viewModel.ContentType,
                Color = viewModel.Color,
            };

            await _context.siteContent.AddAsync(content);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<GetSiteContentViewModel>> GetByHeaderId(long headerId) => await _context.siteContent.Where(x=> x.HeaderId == headerId).Select(x=> new GetSiteContentViewModel
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Icon = x.Icon,
            FileLink = x.FileLink,
            Number = x.Number,
            HeaderId = x.HeaderId,
            CreatedAt = x.CreatedAt,
            ModifiedAt = x.ModiFiedAt,
            ContentType = (ContentType)x.ContentType,
            Color = x.Color
        }).OrderBy(x=> x.Number).ToListAsync();

        public async Task<SiteContentViewModel?> GetById(long id) => await _context.siteContent.Where(x => x.Id == id).Select(x => new SiteContentViewModel
        {
            HeaderId = x.HeaderId,
            Description = x.Description,
            FileLink = x.FileLink,
            Number = x.Number,
            Icon = x.Icon,
            Title = x.Title,
            ContentType = (ContentType)x.ContentType,
            Color = x.Color
        }).FirstOrDefaultAsync();

        public async Task<long> Remove(long id)
        {
            var content = await _context.siteContent.FirstOrDefaultAsync(x=> x.Id == id);
            if(content == null) return 0;
            var headerId = content.HeaderId;
            _context.siteContent.Remove(content);
            await _context.SaveChangesAsync();
            return headerId;
        }

        public async Task<bool> Update(long id, SiteContentViewModel viewModel)
        {
            var content = await _context.siteContent.FirstOrDefaultAsync(x => x.Id == id);
            if (content == null) return false;
            
            content.Title = viewModel.Title;
            content.Description = viewModel.Description;
            content.Icon = viewModel.Icon;
            content.FileLink = viewModel.FileLink;
            content.Number = viewModel.Number;
            content.ContentType = (short)viewModel.ContentType;
            //content.HeaderId = viewModel.HeaderId;
            content.ModiFiedAt = DateTime.Now;
            content.Color = viewModel.Color;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}