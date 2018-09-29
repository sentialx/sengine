using System.Collections.Generic;

namespace sengine.CSS {
    public class CSSStyleRule {
        public string CSSText;
        public string SelectorText;
        public List<CSSRule> Rules = new List<CSSRule>();
    }
}
