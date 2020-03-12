using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlFormat.Models
{
    public class OutputViewModel
    {
        public string RawHtml { get; set; }

        public string FormattedHtml { get; set; }

        public string Guid { get; set; }
    }
}
