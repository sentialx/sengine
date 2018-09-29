using System;
using System.Collections.Generic;

namespace sengine.HTML {
    public partial class HTMLParser {
        public static void Print(List<DOMElement> tree, bool printClosing = true) {
            int lastLevel = 0;
            PrintChildren(tree, printClosing, ref lastLevel);
        }

        private static void PrintChildren(List<DOMElement> tree, bool printClosing, ref int lastLevel, int level = 0) {
            lastLevel = level;

            foreach (DOMElement node in tree) {
                string gap = new string(' ', 2 * level);

                if (node.NodeType == NodeType.Text) {
                    Console.WriteLine(gap + node.NodeValue);
                } else if (node.NodeType == NodeType.Element) {
                    Console.WriteLine(string.Format("{0}<{1}>", gap, node.TagName));

                    if (node.Children.Count > 0) {
                        PrintChildren(node.Children, printClosing, ref lastLevel, level + 1);
                    }
                }

                if (printClosing && (level < lastLevel || level == 0) && node.TagName != null && node.TagName.Trim().Length > 0) {
                    Console.WriteLine(string.Format("{0}</{1}>", gap, node.TagName));
                }

            }
        }

        public static void PrintTokens(List<string> tokens) {
            foreach (string token in tokens) {
                Console.WriteLine(token);
            }
        }
    }
}
