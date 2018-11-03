using System;

namespace sengine {
    class Program {
        static void Main(string[] args) {
            string html = "<html><!-- aha --><head><title>Test</title></head><body><a>test</a><b>Bold text</b></body></html>";
            string css = "a { background-color: red; margin: 1px; color: red; }";

            HTML.Document document = HTML.Parser.Parse(html);
            HTML.Utils.Print(document.Children);

            var stylesheet = CSS.Parser.Parse(css);
            var cssom = CSSOM.Builder.Build(stylesheet, document.Children);

            Console.ReadKey();
        }
    }
}
