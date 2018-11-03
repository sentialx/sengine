using sengine.CSS;
using sengine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace sengine {
    public class CSSElement : Node<CSSElement> {
        public List<StyleDeclaration> Rules = new List<StyleDeclaration>();
    }
}
