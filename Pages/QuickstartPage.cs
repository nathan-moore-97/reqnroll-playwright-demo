using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using ReqnrollTestProject.Services;

namespace ReqnrollTestProject.Pages
{
    public class QuickstartPage(IPageDependencyService pageDependencyService)
    {
        private readonly IPageDependencyService _pageDependencyService = pageDependencyService;

        public ILocator H1Texts => _pageDependencyService.Page.Result.Locator("h1");

        public async Task<bool> H1TextContainsGivenValueAsync(string value)
        {
            foreach (var text in await H1Texts.ElementHandlesAsync())
            {
                var textValue = await text.TextContentAsync();
                if (textValue.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }

    }
}
