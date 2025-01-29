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
}
