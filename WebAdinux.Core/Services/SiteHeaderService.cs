using Microsoft.EntityFrameworkCore;
using WebAdinux.Context.Context;
using WebAdinux.Context.Entities;
using WebAdinux.Core.Interfaces;
using WebAdinux.Core.ViewMoels;

namespace WebAdinux.Core.Services
{
    public class SiteHeaderService : ISiteHeader
    {
        private readonly DataBaseContext _context;

        public SiteHeaderService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(SiteHeaderViewModel viewModel)
        {
            if (viewModel.ParentId != null)
            {
                var exists = await _context.siteHeaders.AnyAsync(x => x.Id == viewModel.ParentId && x.HasDropDown == true);
                if (!exists) return false;
            }
            if (viewModel.Link != null)
            {
                var exists = await _context.siteHeaders.AnyAsync(x => x.Link == viewModel.Link);
                if (exists) return false;
            }
            SiteHeader siteHeader = new SiteHeader()
            {
                HasDropDown = (bool)viewModel.HasDropDown,
                Link = viewModel.Link,
                ParentId = viewModel.ParentId,
                Title = viewModel.Title,
                Visible = viewModel.Visible,
            };
            await _context.siteHeaders.AddAsync(siteHeader);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(long id)
        {
            var siteHeader = await _context.siteHeaders.FirstOrDefaultAsync(x => x.Id == id);
            if (siteHeader == null) return false;
            _context.siteHeaders.Remove(siteHeader);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<GetSiteHeaderViewModel>> Filter(bool? hasDropDown) => await _context.siteHeaders.Where(x => hasDropDown == null ? true : x.HasDropDown == hasDropDown).Select(x => new GetSiteHeaderViewModel
        {
            Id = x.Id,
            Title = x.Title,
            CreatedAt = x.CreatedAt,
            HasDropDown = x.HasDropDown,
            Link = x.Link,
            ModifiedAt = x.ModiFiedAt,
            ParentId = x.ParentId,
            Visible= x.Visible,
        }).ToListAsync();

        public async Task<SiteHeaderViewModel> GetById(long id) => await _context.siteHeaders.Where(x => x.Id == id).Select(x => new SiteHeaderViewModel { HasDropDown = x.HasDropDown, Link = x.Link, ParentId = x.ParentId, Title = x.Title, Visible =  x.Visible }).FirstOrDefaultAsync();

        public async Task<bool> Update(long id, SiteHeaderViewModel viewModel)
        {
            var siteHeader = await _context.siteHeaders.FirstOrDefaultAsync(x => x.Id == id);

            if (siteHeader == null) return false;
            if (viewModel.ParentId != null)
            {
                var exists = await _context.siteHeaders.AnyAsync(x => x.ParentId == viewModel.ParentId);
                if (!exists) return false;
            }
            if (viewModel.Link != null)
            {
                var exists = await _context.siteHeaders.AnyAsync(x => x.Link == viewModel.Link && x.Id != id);
                if (exists) return false;
            }

            siteHeader.Title = viewModel.Title;
            siteHeader.ModiFiedAt = DateTime.Now;
            siteHeader.Link = viewModel.Link;
            siteHeader.Visible = viewModel.Visible;
            //siteHeader.ParentId = viewModel.ParentId;
            //siteHeader.HasDropDown = (bool)viewModel.HasDropDown;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}