using ReqnrollTestProject.Pages;

namespace ReqnrollTestProject.Services;

public interface IPageService
{
    HomePage HomePage { get; }
    SupportPage SupportPage { get; }
    AboutPage AboutPage { get; }
}

public class PagesService(HomePage homePage, SupportPage supportPage, AboutPage aboutPage) : IPageService
{
    public HomePage HomePage { get; } = homePage;

    public SupportPage SupportPage { get; } = supportPage;

    public AboutPage AboutPage { get; } = aboutPage;
}
