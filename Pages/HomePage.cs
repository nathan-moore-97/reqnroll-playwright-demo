﻿using Microsoft.Playwright;
using ReqnrollTestProject.Services;

namespace ReqnrollTestProject.Pages;

public class HomePage(IPageDependencyService pageDependencyService)
{
    private readonly IPageDependencyService _pageDependencyService = pageDependencyService;

    public ILocator HeaderNavigationOptions => _pageDependencyService.Page.Result.Locator("[class=' wp-block-navigation-item wp-block-navigation-link']");
    public ILocator PageButtonOptions => _pageDependencyService.Page.Result.Locator("[class=' wp-block-button__link wp-element-button']");

    public async Task GoToPageAsync()
    {
        await _pageDependencyService.Page.Result.GotoAsync(_pageDependencyService.AppSettings.Value.UiUrl);
    }

    public async Task ClickOnSupportButtonAsync()
    {
        var supportButton = await ReturnButtonFromHeaderOptionsAsync("support");

        await supportButton.ClickAsync();
    }

    public async Task ClickOnQuickstartButtonAsync()
    {
        var quickstartButton = await ReturnButtonFromHeaderOptionsAsync("quickstart");

        await quickstartButton.ClickAsync();
    }

    public async Task ClickOnDiscoverMoreButtonAsync()
    {
        var discoverMoreBtn = await ReturnAHrefFromBodyAsync("Discover More");
        await discoverMoreBtn.ClickAsync();
    }

    public async Task<IPage> WaitForPopupAsync()
    {
        return await _pageDependencyService.Page.Result.WaitForPopupAsync();
    }

    private async Task<IElementHandle> ReturnButtonFromHeaderOptionsAsync(string btnOption)
    {
        foreach (var option in await HeaderNavigationOptions.ElementHandlesAsync())
        {
            var spanElement = await option.QuerySelectorAsync("span");
            var optionText = await spanElement?.TextContentAsync();

            if (optionText.Equals(btnOption, StringComparison.InvariantCultureIgnoreCase))
            {
                return option;
            }
        }
       
        return null;
    }

    // Redundant, parameterize me
    private async Task<IElementHandle> ReturnAHrefFromBodyAsync(string content)
    {
        foreach (var option in await PageButtonOptions.ElementHandlesAsync())
        {
            var element = await option.QuerySelectorAsync("a");
            var optionText = await element?.TextContentAsync();

            if (optionText.Equals(content, StringComparison.InvariantCultureIgnoreCase))
            {
                return option;
            }
        }

        return null;
    }
}
