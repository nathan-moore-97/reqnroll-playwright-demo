using Microsoft.Playwright;
using ReqnrollTestProject.Services;

namespace ReqnrollTestProject.Pages;

public class HomePage(IPageDependencyService pageDependencyService)
{
    private readonly IPageDependencyService _pageDependencyService = pageDependencyService;

    public ILocator HeaderNavigationOptions => _pageDependencyService.Page.Result.Locator("[class=' wp-block-navigation-item wp-block-navigation-link']");
    public ILocator PageButtonOptions => _pageDependencyService.Page.Result.Locator(".wp-block-button .wp-block-button__link");
    public ILocator NewsFeed => _pageDependencyService.Page.Result.Locator("li.category-news");

    public async Task GoToPageAsync()
    {
        await _pageDependencyService.Page.Result.GotoAsync(_pageDependencyService.AppSettings.Value.UiUrl);
    }

    public async Task ClickOnSupportButtonAsync()
    {
        var supportButton = await ReturnElementByContent(HeaderNavigationOptions, "support");

        await supportButton.ClickAsync();
    }

    public async Task ClickOnQuickstartButtonAsync()
    {
        var quickstartButton = await ReturnElementByContent(HeaderNavigationOptions, "quickstart");

        await quickstartButton.ClickAsync();
    }

    public async Task ClickOnDiscoverMoreButtonAsync()
    {
        var discoverMoreBtn = await ReturnElementByContent(PageButtonOptions, "Discover More");
        await discoverMoreBtn.ClickAsync();
    }

    public async Task<IPage> WaitForPopupAsync()
    {
        return await _pageDependencyService.Page.Result.WaitForPopupAsync();
    }

    private async Task<IElementHandle> ReturnElementByContent(ILocator locator, string content)
    {
        foreach (var element in await locator.ElementHandlesAsync())
        {
            var elementText = await element.TextContentAsync();

            if (elementText!.Equals(content, StringComparison.InvariantCultureIgnoreCase))
            {
                return element;
            }
        }

        return null;
    }

    public async Task<IReadOnlyList<IElementHandle>> ReturnListOfNewsItems()
    {
        return await NewsFeed.ElementHandlesAsync();
    }
}
