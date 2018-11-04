using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace sengine.HTML {
    public partial class Parser {
        public static List<string> SelfClosingTags = new List<string>() {
            "AREA", "BASE", "BR", "COL", "COMMAND", "EMBED", "HR", "IMG", "INPUT",
            "KEYGEN", "LINK", "MENUITEM", "META", "PARAM", "SOURCE", "TRACK", "WBR"
        };

        /// <summary>
        /// Separates a HTML code to tokens.
        /// </summary>
        /// <param name="html"></param>
        /// <returns>List of tokens</returns>
        public static List<string> Tokenize(string html) {
            List<string> tokens = new List<string>();

            bool capturing = false;
            string capturedText = "";

            for (int i = 0; i < html.Length; i++) {
                char c = html[i];

                // < is a tag starting char
                if (c == '<') {
                    if (capturing) {
                        // Add to tokens the captured text before the '<' char
                        // and continue capturing the text.
                        tokens.Add(capturedText);
                    } else {
                        // Start capturing text if it wasn't captured before.
                        capturing = true;
                    }

                    capturedText = "";
                }
                // If the char is '>', it is the end of the tag.
                else if (c == '>' || i == html.Length - 1) {
                    // Stop capturing the text, and add the tag to tokens.
                    capturing = false;
                    capturedText += c;
                    tokens.Add(capturedText);
                }
                // If the text isn't captured, and it's not a tag, start capturing it.
                else if (!capturing) {
                    capturedText = "";
                    capturing = true;
                }

                // Capture the text.
                if (capturing) {
                    capturedText += c;
                }
            }

            return tokens;
        }

        /// <summary>
        /// Parses HTML code to a Document object.
        /// </summary>
        /// <param name="html"></param>
        /// <returns>Document</returns>
        public static List<DOMElement> Parse(string html) {
            var tokens = Tokenize(html);
            var elements = BuildTree(tokens);

            return elements;
        }

        /// <summary>
        /// Minifies html code.
        /// </summary>
        /// <param name="html"></param>
        /// <returns>Minified html code</returns>
        public static string Minify(string[] html) {
            string htmlMin = "";

            for (int i = 0; i < html.Length; i++) {
                htmlMin += html[i].Trim();
            }

            return htmlMin;
        }

        /// <summary>
        /// Gets comment text between <!-- and -->
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetCommentText(string token) {
            var regex = new Regex(@"<!--(.*?)-->");
            Match match = regex.Match(token);
            if (match.Success) {
                return match.Groups[1].Value;
            }
            return "";
        }

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
                NodeType nodeType = GetNodeType(token);

                if (tagType == TagType.Closing) {
                    if (parent != null) {
                        // Check if there's a similar tag previously opened with the same name.
                        var openedTagIndex = openedTags.LastIndexOf(tagName);

                        if (openedTagIndex != -1) {
                            if (parent.TagName == tagName) {
                                // When the current parent element has the same tag name, just go level up.
                                parent = parent.ParentNode;

                                // Example:
                                // <a>
                                //   <b>
                                //   </b>
                                //   parent = <a>
                                // </a>
                                // parent = null
                            } else {
                                // Now, we have to find the corresponding opening tag to the current closing tag.
                                var el = GetOpeningTag(tagName, parent);
                                if (el != null) {
                                    // Set current parent to parent of the found element.
                                    parent = el.ParentNode;
                                }

                                // Here's an example where it might occur:
                                // parent = null
                                // <a>
                                //   <b>
                                //     <c>
                                //   </b>
                                // parent = <a>
                            }

                            // Remove the opening tag from the collection.
                            openedTags.RemoveAt(openedTagIndex);
                        }
                    }
                } else {
                    DOMElement element = new DOMElement() {
                        NodeType = nodeType,
                    };

                    if (parent != null) {
                        element.ParentNode = parent;
                        parent.Children.Add(element);
                    } else {
                        elements.Add(element);
                    }

                    if (tagType == TagType.Opening && nodeType == NodeType.Element) {
                        element.TagName = tagName;

                        // Extracts all attributes from token.
                        element.Attributes = GetAttributes(token, tagName);

                        // Set current parent to currently processed element.
                        parent = element;

                        // Collect opened tags, to correctly close other tags.
                        openedTags.Add(tagName);
                    } else if (nodeType == NodeType.Text) {
                        element.NodeValue = token;
                    } else if (nodeType == NodeType.Comment) {
                        element.NodeValue = GetCommentText(token);
                    }
                }
            }

            return elements;
        }

        /// <summary>
        /// Gets tag name from tag code (e.g. <div> is div).
        /// </summary>
        /// <param name="source"></param>
        /// <returns>Tag name</returns>
        public static string GetTagName(string source) {
            return source.Replace("<", "").Replace("/", "").Replace(">", "").Split(" ")[0].Trim();
        }

        /// <summary>
        /// Determines given tag code type.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Tag type</returns>
        public static TagType GetTagType(string token) {
            if (token[0] == '<' && token[token.Length - 1] == '>') {
                string tagName = GetTagName(token);
                if (token[1] == '/') {
                    return TagType.Closing;
                } else if (SelfClosingTags.Contains(tagName)) {
                    return TagType.SelfClosing;
                } else {
                    return TagType.Opening;
                }
            }
            return TagType.None;
        }

        /// <summary>
        /// Determines node type of given tag's code.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static NodeType GetNodeType(string token) {
            if (token[0] == '<' && token[token.Length - 1] == '>') {
                string tagName = GetTagName(token);
                if (token.StartsWith("<!--")) {
                    return NodeType.Comment;
                } else if (token[1] == '!') {
                    return NodeType.DocumentType;
                } else {
                    return NodeType.Element;
                }
            } else {
                return NodeType.Text;
            }
        }

        /// <summary>
        /// Extracts attributes from given token.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static List<Attribute> GetAttributes(string source, string tagName) {
            List<Attribute> list = new List<Attribute>();
            Attribute attr = new Attribute();

            bool capturingValue = false;
            bool insideQuotes = false;

            for (int i = tagName.Length + 1; i < source.Length; i++) {
                if (source[i] == '=') {
                    capturingValue = true;
                } else if (source[i] == '"') {
                    insideQuotes = !insideQuotes;
                } else if (capturingValue) {
                    attr.Value += source[i];
                } else if (i != source.Length - 1 && source[i] != ' ') {
                    attr.Name += source[i];
                }

                if ((source[i] == '"' || source[i] == ' ' || source[i] == '>') && !insideQuotes) {
                    if (attr.Name != null && attr.Name.Length > 0) {
                        if (attr.Value != null) {
                            attr.Value = attr.Value.Trim();
                        }
                        list.Add(attr);
                    }

                    attr = new Attribute();
                    capturingValue = false;
                    insideQuotes = false;
                }
            }

            return list;
        }
    }
}