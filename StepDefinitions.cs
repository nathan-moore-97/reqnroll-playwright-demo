using FluentAssertions;
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

        TabStackService.Instance.Push(_newTab);

    }

    [Then("The following textx are visible")]
    public async Task ThenTheFollowingTextxAreVisible(DataTable dataTable)
    {
        var tableRows = dataTable.ToDictionary();
        foreach (var row in tableRows)
        {
            var textMatches = await _pageService.SupportPage.TextContainsGivenValueAsync(row.Value);
            textMatches.Should().BeTrue();
        }
    }

    [Then("a new tab should be opened to {string}")]
    public void ThenANewTabShouldBeOpenedTo(string url)
    {
        IPage newTab;
        TabStackService.Instance.TryPop(out newTab);
        newTab.Url.Should().Be(url);
    }
}
