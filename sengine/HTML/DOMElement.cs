using System.Collections.Generic;

namespace sengine.HTML {
    public class DOMElement {
        public DOMElement ParentNode;
        public List<DOMElement> Children = new List<DOMElement>();
        public NodeType NodeType = NodeType.Element;
        public string InnerHTML;
        public string OuterHTML;
        public string NodeValue;
        public string TagName;
    }
}
