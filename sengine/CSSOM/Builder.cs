using sengine.CSS;
using sengine.HTML;
using System;
using System.Collections.Generic;
using System.Text;

namespace sengine.CSSOM {
    public class Builder {
        public static List<string> InheritableProperties = new List<string>() {
            "font-size", "color", "font-weight", "font-style"
        };

        public static List<CSSElement> Build(StyleSheet styleSheet, List<DOMElement> elements, List<StyleDeclaration> toInherit = null) {
            if (toInherit == null) {
                toInherit = new List<StyleDeclaration>();
            }

            List<CSSElement> cssElements = new List<CSSElement>();

            foreach (var element in elements) {
                CSSElement cssElement = new CSSElement();

                foreach (var rule in styleSheet.Rules) {
                    if (element.TagName != null && element.TagName == rule.SelectorText.ToUpper() || rule.SelectorText == "*" || element.NodeType == NodeType.Text) {
                        cssElement.Rules = rule.Rules;
                        cssElement.NodeValue = element.NodeValue;

                        if (element.NodeType != NodeType.Text) {
                            toInherit.AddRange(GetStylesToInherit(rule.Rules));
                        }
                    }
                }

                if (element.Children.Count > 0) {
                    var children = Build(styleSheet, element.Children, toInherit);

                    if (cssElement.Rules.Count > 0) {
                        cssElement.Children = children;
                    } else {
                        cssElements.AddRange(children);
                    }
                }

                if (cssElement.Rules.Count > 0) {
                    cssElements.Add(cssElement);
                }
                
            }

            return cssElements;
        }

        public static List<StyleDeclaration> GetStylesToInherit(List<StyleDeclaration> styles) {
            List<StyleDeclaration> stylesToInherit = new List<StyleDeclaration>();

            foreach (var style in styles) {
                if (InheritableProperties.Contains(style.Property)) {
                    stylesToInherit.Add(style);
                }
            }

            return stylesToInherit;
        }
    }
}
