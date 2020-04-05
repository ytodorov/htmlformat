using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HtmlFormat.Models;
using AngleSharp.Html.Parser;
using System.IO;
using AngleSharp.Html;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;

namespace HtmlFormat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IMemoryCache memoryCache;

        private readonly IWebHostEnvironment env;


        public HomeController(
            ILogger<HomeController> logger,
            IMemoryCache memoryCache,
            IWebHostEnvironment env)
        {
            _logger = logger;
            this.memoryCache = memoryCache;
            this.env = env;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.Title = "HTML Format";
            homeViewModel.Description = "Format any HTML code here";

            homeViewModel.BaseUrlWithoutTrailingSlash = "https://www.htmlformat.org";
            if (env.EnvironmentName.Equals("Development"))
            {
                homeViewModel.BaseUrlWithoutTrailingSlash = "https://localhost:44392";
            }

            homeViewModel.CodeMirrorMode = "text/html";

            return View(homeViewModel);
        }

        [HttpGet("{*url}", Order = int.MaxValue)]
        public IActionResult CatchAll()
        {
            var modeFromUrl = HttpContext.Request.Path.ToString().Replace("/", string.Empty);

            modeFromUrl = modeFromUrl.Replace("-", "+");
            var mimeType = HfConstants.MimeTypesDic[modeFromUrl.ToLowerInvariant()];

            var mode = modeFromUrl.ToUpper();
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.Title = $"{mode} Format";
            homeViewModel.Description = $"Format any {mode} code here";

            homeViewModel.BaseUrlWithoutTrailingSlash = "https://www.htmlformat.org";
            if (env.EnvironmentName.Equals("Development"))
            {
                homeViewModel.BaseUrlWithoutTrailingSlash = "https://localhost:44392";
            }

            homeViewModel.CodeMirrorMode = mimeType;

            return View("Index", homeViewModel);
        }

        //[Route("json")]
        //public IActionResult Json()
        //{
        //    HomeViewModel homeViewModel = new HomeViewModel();
        //    homeViewModel.Title = "JSON Format";
        //    homeViewModel.Description = "Format any JSON code here";

        //    homeViewModel.BaseUrlWithoutTrailingSlash = "https://www.htmlformat.org";
        //    if (env.EnvironmentName.Equals("Development"))
        //    {
        //        homeViewModel.BaseUrlWithoutTrailingSlash = "https://localhost:44392";
        //    }

        //    homeViewModel.CodeMirrorMode = "application/json";

        //    return View(homeViewModel);
        //}

        //[Route("json-ld")]
        //public IActionResult JsonLD()
        //{
        //    HomeViewModel homeViewModel = new HomeViewModel();
        //    homeViewModel.Title = "JSON-LD Format";
        //    homeViewModel.Description = "Format any JSON-LD code here";

        //    homeViewModel.BaseUrlWithoutTrailingSlash = "https://www.htmlformat.org";
        //    if (env.EnvironmentName.Equals("Development"))
        //    {
        //        homeViewModel.BaseUrlWithoutTrailingSlash = "https://localhost:44392";
        //    }

        //    homeViewModel.CodeMirrorMode = "application/ld+json";

        //    return View("Index", homeViewModel);
        //}

        public IActionResult CodeMirror(string mode)
        {
            mode = mode.Replace(" ", "+");
            CodeMirrorViewModel codeMirrorViewModel = new CodeMirrorViewModel();
            codeMirrorViewModel.Mode = mode;
            if ("text/html".Equals(mode))
            {
                codeMirrorViewModel.Lint = false;
            }
            else if ("application/json".Equals(mode))
            {
            }
            else if ("application/ld+json".Equals(mode))
            {
                codeMirrorViewModel.DisplayGutterCustom = false;
            }
            else
            {
                // Generally disable lint
                codeMirrorViewModel.Lint = false;
            }

            return View(codeMirrorViewModel);
        }

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult Format(InputViewModel inputViewModel)
        {
            HtmlParserOptions htmlParserOptions = new HtmlParserOptions();
            var parser = new HtmlParser(htmlParserOptions);

            var document = parser.ParseDocument(inputViewModel.tbInput);

            var sw = new StringWriter();
            PrettyMarkupFormatter prettyMarkupFormatter = new PrettyMarkupFormatter();

            document.ToHtml(sw, prettyMarkupFormatter);

            var formattedHtml = sw.ToString();

            string guid = Guid.NewGuid().ToString();


            var result = new OutputViewModel()
            {
                RawHtml = inputViewModel.tbInput,
                FormattedHtml = formattedHtml,
                Guid = guid,
            };

            memoryCache.Set(guid, formattedHtml);

            return Json(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
