using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlFormat.Models
{
    public class HomeViewModel
    {
        public string BaseUrlWithoutTrailingSlash { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CodeMirrorMode { get; set; }
    }
}
