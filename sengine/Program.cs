using sengine.HTML;
using sengine.CSS;
using System;

namespace sengine {
    class Program {
        static void Main(string[] args) {
            string html = "<html><!-- aha --><head><title>Test</title></head><body><div class='test'>test</div><b>Bold text</b></body></html>";
            string css = ".aha { background-color: red; margin: 1px; }";

            Document document = HTMLParser.Parse(html);
            HTMLUtils.Print(document.Children);

            var cssom = CSSParser.Parse(css);

            Console.ReadKey();
        }
    }
}
