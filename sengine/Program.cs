using sengine.html;
using System;

namespace sengine {
    class Program {
        static void Main(string[] args) {
            string html = "<html><head><title>Test</title></head><body><div class='test'>test</div></body></html>";

            Document document = HTML.Parse(html);

            HTML.Print(document.Children);

            Console.ReadKey();
        }
    }
}
