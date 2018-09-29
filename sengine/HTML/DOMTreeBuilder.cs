using System;
using System.Collections.Generic;
using System.Text;

namespace sengine.html {
    public partial class HTML {
        /// <summary>
        /// Gets corresponding opening tag to the closing tag name.
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="element"></param>
        /// <returns>The opening DOMElement</returns>
        private static DOMElement GetOpeningTag(string tagName, DOMElement element) {
            if (element != null) {
                if (element.TagName == tagName) {
                    return element;
                } else {
                    return GetOpeningTag(tagName, element.ParentNode);
                }
            }

            return null;
        }

        /// <summary>
        /// Builds DOM tree using tokens generated from tokenizer.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns>List of parent elements</returns>
        public static List<DOMElement> BuildTree(List<string> tokens) {
            List<DOMElement> elements = new List<DOMElement>();
            List<string> openedTags = new List<string>();

            DOMElement parent = null;

            for (int i = 0; i < tokens.Count; i++) {
                string token = tokens[i];

                string tagName = GetTagName(token).ToUpper();
                TagType tagType = GetTagType(token);

                if (tagType == TagType.SelfClosed || tagType == TagType.Opening || tagType == TagType.Text) {
                    DOMElement element = new DOMElement() {
                        NodeType = NodeType.Element,
                    };

                    if (parent != null) {
                        element.ParentNode = parent;
                        parent.Children.Add(element);
                    } else {
                        elements.Add(element);
                    }

                    if (tagType == TagType.Opening) {
                        element.TagName = tagName;
                        parent = element;
                        openedTags.Add(tagName);
                    } else if (tagType == TagType.Text) {
                        element.NodeType = NodeType.Text;
                        element.NodeValue = token;
                    }
                } else if (tagType == TagType.Closing) {
                    if (parent != null) {
                        var openedTagIndex = openedTags.LastIndexOf(tagName);

                        if (openedTagIndex != -1) {
                            if (parent.TagName == tagName) {
                                parent = parent.ParentNode;
                            } else {
                                var el = GetOpeningTag(tagName, parent);
                                if (el != null) {
                                    parent = el.ParentNode;
                                }
                            }

                            openedTags.RemoveAt(openedTagIndex);
                        }

                    }
                }
            }

            return elements;
        }
    }
}
