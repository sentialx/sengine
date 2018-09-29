using System;
using System.Collections.Generic;
using System.Text;

namespace sengine.html {
    public partial class HTML {
        public static List<string> selfClosingTags = new List<string>() {
            "AREA", "BASE", "BR", "COL", "COMMAND", "EMBED", "HR", "IMG", "INPUT",
            "KEYGEN", "LINK", "MENUITEM", "META", "PARAM", "SOURCE", "TRACK", "WBR"
        };

        /// <summary>
        /// Parses HTML code to a Document object.
        /// </summary>
        /// <param name="html"></param>
        /// <returns>Document</returns>
        public static Document Parse(string html) {
            var tokens = Tokenize(html);
            var elements = BuildTree(tokens);

            Document document = new Document();

            document.Children = elements;

            return document;
        }

        /// <summary>
        /// Minifies html code.
        /// </summary>
        /// <param name="html"></param>
        /// <returns>Minified html code</returns>
        public static string Minify(string[] html) {
            string htmlMin = "";

            for (int i = 0; i < html.Length; i++) {
                htmlMin += html[i].Trim();
            }

            return htmlMin;
        }

    }
}
