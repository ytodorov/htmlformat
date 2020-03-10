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

namespace HtmlFormat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;  
        }

        public IActionResult Index()
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

            var indentedHtml = sw.ToString();

            var result = new OutputViewModel()
            {
                RawHtml = inputViewModel.tbInput,
                FormattedHtml = indentedHtml,
            };

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
