using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlFormat
{
    public static class HfConstants
    {
        private static List<string> mimeTypes = new List<string>();

        public static Dictionary<string, string> MimeTypesDic = new Dictionary<string, string>();

        static HfConstants()
        {
            var mimes = "text/apl,application/pgp,application/pgp-encrypted,application/pgp-keys,application/pgp-signature,text/x-ttcn-asn,text/x-asterisk,text/x-brainfuck,text/x-csrc,text/x-c,text/x-chdr,text/x-c++src,text/x-c++hdr,text/x-java,text/x-csharp,text/x-scala,text/x-kotlin,x-shader/x-vertex,x-shader/x-fragment,text/x-nesc,text/x-objectivec,text/x-objectivec++,text/x-squirrel,text/x-ceylon,text/x-clojure,text/x-clojurescript,application/edn,text/x-cmake,text/x-cobol,application/vnd.coffeescript,text/x-coffeescript,text/coffeescript,text/x-common-lisp,text/x-crystal,text/css,text/x-scss,text/x-less,text/x-gss,application/x-cypher-query,text/x-d,application/dart,text/x-diff,text/x-django,text/x-dockerfile,application/xml-dtd,text/x-dylan,text/x-ebnf,text/x-ecl,text/x-eiffel,text/x-elm,text/x-erlang,text/x-factor,text/x-fcl,text/x-forth,text/x-fortran,text/x-gfm,text/x-feature,text/x-go,text/x-groovy,text/x-haml,text/x-handlebars-template,text/x-haskell,text/x-literate-haskell,text/x-haxe,text/x-hxml,application/x-ejs,application/x-aspx,application/x-jsp,application/x-erb,text/html,message/http,text/x-idl,text/javascript,text/ecmascript,application/javascript,application/x-javascript,application/ecmascript,application/json,application/x-json,application/ld+json,text/typescript,application/typescript,text/jinja2,text/jsx,text/typescript-jsx,text/x-julia,text/x-livescript,text/x-lua,text/markdown,text/x-markdown,text/x-mathematica,application/mbox,text/mirc,text/x-ocaml,text/x-fsharp,text/x-sml,text/x-modelica,text/x-mscgen,text/x-xu,text/x-msgenny,text/x-mumps,text/x-nginx-conf,text/x-nsis,application/n-triples,application/n-quads,text/n-triples,text/x-octave,text/x-oz,text/x-pascal,text/x-perl,application/x-httpd-php,application/x-httpd-php-open,text/x-php,text/x-pig,application/x-powershell,text/x-properties,text/x-ini,text/x-protobuf,text/x-pug,text/x-jade,text/x-puppet,text/x-python,text/x-cython,text/x-q,text/x-rsrc,text/x-rpm-changes,text/x-rpm-spec,text/x-rst,text/x-ruby,text/x-rustsrc,text/rust,text/x-sas,text/x-sass,text/x-scheme,text/x-sh,application/x-sh,application/sieve,text/x-slim,application/x-slim,text/x-stsrc,text/x-smarty,text/x-solr,text/x-soy,application/sparql-query,text/x-spreadsheet,text/x-sql,text/x-mssql,text/x-mysql,text/x-mariadb,text/x-sqlite,text/x-cassandra,text/x-plsql,text/x-hive,text/x-pgsql,text/x-gql,text/x-gpsql,text/x-sparksql,text/x-esper,text/x-stex,text/x-latex,text/x-styl,text/x-swift,text/x-tcl,text/x-textile,text/x-tiddlywiki,text/tiki,text/x-toml,text/x-tornado,text/troff,text/x-troff,application/x-troff,text/x-ttcn,text/x-ttcn3,text/x-ttcnpp,text/x-ttcn-cfg,text/turtle,text/x-twig,text/x-vb,text/vbscript,text/velocity,text/x-verilog,text/x-systemverilog,text/x-tlv,text/x-vhdl,script/x-vue,text/x-vue,text/x-webidl,text/xml,application/xml,application/xquery,text/x-yacas,text/x-yaml,text/yaml,text/x-z80,text/x-ez80".Split(",").ToList();

            mimeTypes.AddRange(mimes);
            mimeTypes = mimeTypes.OrderBy(s => s).ToList();

            foreach (var mType in mimeTypes)
            {
                var mode = mType
                    .Replace("text/x-", string.Empty)
                    .Replace("application/x-", string.Empty)
                    .Replace("text/", string.Empty)
                    .Replace("application/", string.Empty);

                if (!MimeTypesDic.ContainsKey(mode))
                {
                    MimeTypesDic.Add(mode, mType);
                }
            }
        }
    }
}
