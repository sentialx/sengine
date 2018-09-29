using System.Collections.Generic;

namespace sengine.HTML {
    public partial class HTMLParser {
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
    }
}
