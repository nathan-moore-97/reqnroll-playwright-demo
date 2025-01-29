using FluentAssertions;
using FluentAssertions.Collections;
using Microsoft.Playwright;
using Reqnroll;
using Reqnroll.CommonModels;
using ReqnrollTestProject.Extensions;
using ReqnrollTestProject.Services;

namespace ReqnrollTestProject;

[Binding]
public class StepDefinitions(IPageService pageService)
{
    private readonly IPageService _pageService = pageService;

    [Given("The Reqnroll page is loaded")]
    public async Task GivenTheReqnrollPageIsLoadedAsync()
    {
        await _pageService.HomePage.GoToPageAsync();
    }

    [When("The support button is clicked")]
    public async Task WhenTheSupportButtonIsClicked()
    {
        await _pageService.HomePage.ClickOnSupportButtonAsync();
    }

    [When("The quickstart button is clicked")]
    public async Task WhenTheQuickstartButtonIsClicked()
    {
        var newTabTask = _pageService.HomePage.WaitForPopupAsync();

        await _pageService.HomePage.ClickOnQuickstartButtonAsync();

        var _newTab = await newTabTask;

        await _newTab.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        // This might be an anti pattern
        TabManager.Instance.Push(_newTab);

    }

    [When("The Discover More button is clicked")]
    public async Task WhenTheDiscoverMoreButtonIsClicked()
    {
        await _pageService.HomePage.ClickOnDiscoverMoreButtonAsync();
    }


    [Then("The following Support Page texts are visible")]
    public async Task ThenTheFollowingSupportPageTextsAreVisible(DataTable dataTable)
    {
        var tableRows = dataTable.ToDictionary();
        foreach (var row in tableRows)
        {
            var textMatches = await _pageService.SupportPage.TextContainsGivenValueAsync(row.Value);
            textMatches.Should().BeTrue();
        }
    }

    [Then("The following about page texts are visible")]
    public async Task ThenTheFollowingAboutPageTextsAreVisible(DataTable dataTable)
    {
        var tableRows = dataTable.ToDictionary();
        foreach (var row in tableRows)
        {
            var textMatches = await _pageService.AboutPage.H2ContainsGivenValue(row.Value);
            textMatches.Should().BeTrue();
        }
    }


    [Then("a new tab should be opened to {string}")]
    public void ThenANewTabShouldBeOpenedTo(string url)
    {
        IPage newTab;
        TabManager.Instance.TryPop(out newTab);
        newTab.Url.Should().Be(url);
    }

    [Then("the news feed has items")]
    public async Task ThenTheNewsFeedHasItems()
    {
        IReadOnlyList<IElementHandle> feed = await _pageService.HomePage.ReturnListOfNewsItems();
        feed.Should().NotBeEmpty();

    }
}
