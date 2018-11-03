﻿using sengine.Interfaces;
using System.Collections.Generic;

namespace sengine.HTML {
    public class DOMElement: Node<DOMElement> {
        public NodeType NodeType = NodeType.Element;
        public string InnerHTML;
        public string OuterHTML;
        public string TagName;
    }
}
