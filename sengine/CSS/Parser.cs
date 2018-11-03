namespace sengine.CSS {
    public partial class Parser {
        public static StyleSheet Parse(string code) {
            var styleSheet = new StyleSheet();

            string capturedText = "";
            string capturedCode = "";

            StyleRule styleRule = new StyleRule();
            StyleDeclaration rule = new StyleDeclaration();

            for (int i = 0; i < code.Length; i++) {
                capturedCode += code[i];

                if (code[i] == '{') {
                    styleRule = new StyleRule() {
                        SelectorText = capturedText.Trim()
                    };
                    capturedText = "";
                } else if (code[i] == ':') {
                    rule = new StyleDeclaration() {
                        Property = capturedText.Trim()
                    };
                    capturedText = "";
                } else if (code[i] == ';') {
                    rule.Value = capturedText.Trim();
                    styleRule.Rules.Add(rule);
                    capturedText = "";
                } else if (code[i] == '}') {
                    styleRule.CssText = capturedCode.Trim();
                    styleSheet.Rules.Add(styleRule);
                    capturedCode = "";
                } else {
                    capturedText += code[i];
                }
            }

            return styleSheet;
        }
    }
}