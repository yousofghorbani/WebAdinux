using Microsoft.AspNetCore.Mvc;
using WebAdinux.Core.Interfaces;

namespace WebAdinux.ViewComponents.HeaderComponent
{
    public class HeaderComponent: ViewComponent
    {
        private readonly ISiteHeader _header;

        public HeaderComponent(ISiteHeader header)
        {
            _header = header;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var res = await _header.Filter(null);
            return await Task.FromResult((IViewComponentResult)View("MyHeaderView", res));
        }
    }
}
