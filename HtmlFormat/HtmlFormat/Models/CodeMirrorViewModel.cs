using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlFormat.Models
{
    public class CodeMirrorViewModel
    {
        public string DefaultValue { get; set; }

        public string Mode { get; set; }

        public bool Line { get; set; } = true;

        public bool MatchBrackets { get; set; } = true;

        public bool LineNumbers { get; set; } = true;

        public bool LineWrapping { get; set; } = true;

        public bool StyleActiveLine { get; set; } = true;

        public bool AutoCloseBrackets { get; set; } = true;

        public bool FoldGutter { get; set; } = true;

        public bool DisplayGutterCustom { get; set; } = true;

        public bool Lint { get; set; } = true;
    }
}
