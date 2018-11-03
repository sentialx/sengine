using System;

namespace sengine {
    class Program {
        static void Main(string[] args) {
            string html = "<html><!-- aha --><head><title>Test</title></head><body><a>test</a><b>Bold text</b></body></html>";
            string css = "a { background-color: red; margin: 1px; color: red; }";

            HTML.Document document = new HTML.Document(html);
            HTML.Utils.Print(document.Children);

            var cssom = new CSSOM(css, document);
            var renderTree = new RenderTree(cssom);


            Console.ReadKey();
        }
    }
}
