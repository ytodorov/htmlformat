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

namespace HtmlFormat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IMemoryCache memoryCache;

        public HomeController(ILogger<HomeController> logger,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CodeMirror(string guid)
        {
            var formattedHtml = string.Empty;
            if (!string.IsNullOrEmpty(guid))
            {
                formattedHtml = memoryCache.Get(guid)?.ToString();
            }
            return View((object)formattedHtml);
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
