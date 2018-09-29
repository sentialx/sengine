using System.Collections.Generic;

namespace sengine.CSS {
    public class StyleRule {
        public string CssText;
        public string SelectorText;
        public List<StyleDeclaration> Rules = new List<StyleDeclaration>();
    }
}
