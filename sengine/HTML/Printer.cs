using System;
using System.Collections.Generic;

namespace sengine.HTML {
    public class Utils {
        private static ConsoleColor
            TagColor = ConsoleColor.Blue,
            TextColor = ConsoleColor.White,
            CommentColor = ConsoleColor.DarkGray,
            PropertyColor = ConsoleColor.Cyan,
            ValueColor = ConsoleColor.Red;

        public static void Print(List<DOMElement> tree, bool printClosing = true) {
            int lastLevel = 0;

            PrintChildren(tree, printClosing, ref lastLevel);
            Console.ResetColor();
        }

        private static void PrintChildren(List<DOMElement> tree, bool printClosing, ref int lastLevel, int level = 0) {
            lastLevel = level;

            foreach (DOMElement node in tree) {
                string gap = new string(' ', 2 * level);

                if (node.NodeType == NodeType.Text) {
                    PrintColored(gap + node.NodeValue, TextColor);
                } else if (node.NodeType == NodeType.Comment) {
                    PrintColored(gap + string.Format("<!--{0}-->", node.NodeValue), CommentColor);
                } else if (node.NodeType == NodeType.Element) {
                    PrintColored(string.Format("{0}<{1}", gap, node.TagName), TagColor, true);

                    if (node.Attributes.Count > 0) {
                        foreach (Attribute attr in node.Attributes) {
                            PrintColored(' ' + attr.Name, PropertyColor, true);

                            if (attr.Value != null) {
                                PrintColored("=\"", TagColor, true);
                                PrintColored(attr.Value, ValueColor, true);
                                PrintColored("\"", TagColor, true);
                            }
                        }
                    }

                    PrintColored(">\n", TagColor, true);

                    if (node.Children.Count > 0) {
                        PrintChildren(node.Children, printClosing, ref lastLevel, level + 1);
                    }

                    if (printClosing && (level < lastLevel || level == 0 || node.ParentNode.Children.Count - 1 == 0)) {
                        PrintColored(string.Format("{0}</{1}>", gap, node.TagName), TagColor);
                    }
                }
            }
        }

        private static void PrintColored(string text, ConsoleColor color, bool inline = false) {
            Console.ForegroundColor = color;
            if (!inline) Console.WriteLine(text);
            else Console.Write(text);
        }

        public static void PrintTokens(List<string> tokens) {
            foreach (string token in tokens) {
                Console.WriteLine(token);
            }
        }
    }
}
