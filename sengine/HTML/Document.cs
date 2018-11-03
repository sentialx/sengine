using System.Collections.Generic;

namespace sengine.HTML {
    public class Document {
        public DOMElement DocumentElement;
        public DOMElement Body;
        public DOMElement Head;
        public List<DOMElement> Children = new List<DOMElement>();

        public Document(string html) {
            var elements = Parser.Parse(html);
            this.Children = elements;
        }
    }
}
