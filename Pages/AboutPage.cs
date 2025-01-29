using Microsoft.Playwright;
using ReqnrollTestProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollTestProject.Pages;

public class AboutPage(IPageDependencyService pageDependencyService)
{
    private readonly IPageDependencyService _pageDependencyService = pageDependencyService;

    public ILocator H2Texts => _pageDependencyService.Page.Result.Locator("h2");

    public async Task<bool> H2ContainsGivenValue(string value)
    {
        foreach (var text in await H2Texts.ElementHandlesAsync())
        {
            var textValue = await text.TextContentAsync();
            if (textValue.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                return true;
        }

        return false;
    }
}
