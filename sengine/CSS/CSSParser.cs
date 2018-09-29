namespace sengine.CSS {
    public partial class CSSParser {
        public static CSSStyleSheet Parse(string code) {
            var styleSheet = new CSSStyleSheet();

            string capturedText = "";
            string capturedCode = "";

            CSSStyleRule styleRule = new CSSStyleRule();
            CSSRule rule = new CSSRule();

            for (int i = 0; i < code.Length; i++) {
                capturedCode += code[i];

                if (code[i] == '{') {
                    styleRule = new CSSStyleRule() {
                        SelectorText = capturedText.Trim()
                    };
                    capturedText = "";
                } else if (code[i] == ':') {
                    rule = new CSSRule() {
                        Property = capturedText.Trim()
                    };
                    capturedText = "";
                } else if (code[i] == ';') {
                    rule.Value = capturedText.Trim();
                    styleRule.Rules.Add(rule);
                    capturedText = "";
                } else if (code[i] == '}') {
                    styleRule.CSSText = capturedCode.Trim();
                    styleSheet.CSSRules.Add(styleRule);
                    capturedCode = "";
                } else {
                    capturedText += code[i];
                }
            }

            return styleSheet;
        }
    }
}
