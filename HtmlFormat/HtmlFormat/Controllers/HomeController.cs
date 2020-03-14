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
            homeViewModel.Description = "Your on-line tool to format any HTML code.";

            homeViewModel.BaseUrl = "https://www.htmlformat.org/";
            if (env.EnvironmentName.Equals("Development"))
            {
                homeViewModel.BaseUrl = "https://localhost:44392/";
            }

            return View(homeViewModel);
        }

        public IActionResult CodeMirror(string guid)
        {
            var formattedHtml = string.Empty;
            if (!string.IsNullOrEmpty(guid))
            {
                formattedHtml = memoryCache.Get(guid)?.ToString();
                formattedHtml = Regex.Replace(formattedHtml, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
                
            }

            return View((object)formattedHtml);
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
