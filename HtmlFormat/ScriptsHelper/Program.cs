using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptsHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.AllDirectories);

            var jsFiles = files.Where(f => f.EndsWith(".js", StringComparison.InvariantCultureIgnoreCase)).ToList();

            var cssFiles = files.Where(f => f.EndsWith(".css", StringComparison.InvariantCultureIgnoreCase)).ToList();


            var jsAddonFiles = jsFiles.Where(f => f.Contains($"{Path.DirectorySeparatorChar}addon{Path.DirectorySeparatorChar}",
                StringComparison.InvariantCultureIgnoreCase)).ToList();

            var jsModeFiles = jsFiles.Where(f => f.Contains($"{Path.DirectorySeparatorChar}mode{Path.DirectorySeparatorChar}",
                StringComparison.InvariantCultureIgnoreCase)).ToList();
            
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var css in cssFiles)
            {
                var f = css.Replace($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}package{Path.DirectorySeparatorChar}", string.Empty);
                f = f.Replace(Path.DirectorySeparatorChar.ToString(), "/");
                f = f.Replace(".css", ".min.css");
                //stringBuilder.AppendLine($@"<link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/codemirror@5.52.2/lib/codemirror.min.css"">");
                stringBuilder.AppendLine($@"<link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/codemirror@5.52.2/{f}"">");

            }

            foreach (var js in jsAddonFiles)
            {
                var f = js.Replace($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}package{Path.DirectorySeparatorChar}", string.Empty);
                f = f.Replace(Path.DirectorySeparatorChar.ToString(), "/");
                f = f.Replace(".js", ".min.js");
                //stringBuilder.AppendLine($@"<link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/codemirror@5.52.2/lib/codemirror.min.css"">");
                stringBuilder.AppendLine($@"<script src=""https://cdn.jsdelivr.net/npm/codemirror@5.52.2/{f}""></script>");

            }

            foreach (var js in jsModeFiles)
            {
                var f = js.Replace($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}package{Path.DirectorySeparatorChar}", string.Empty);
                f = f.Replace(Path.DirectorySeparatorChar.ToString(), "/");
                f = f.Replace(".js", ".min.js");
                //stringBuilder.AppendLine($@"<link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/codemirror@5.52.2/lib/codemirror.min.css"">");
                stringBuilder.AppendLine($@"<script src=""https://cdn.jsdelivr.net/npm/codemirror@5.52.2/{f}""></script>");

            }
            var result = stringBuilder.ToString();
            Console.WriteLine("Hello World!");
        }
    }
}
