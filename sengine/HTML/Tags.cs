namespace sengine.html {
    public partial class HTML {
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
            if (token[0] == '<') {
                string tagName = GetTagName(token);
                if (token[1] == '/') {
                    return TagType.Closing;
                } else if (selfClosingTags.Contains(tagName)) {
                    return TagType.SelfClosed;
                } else {
                    return TagType.Opening;
                }
            } else {
                return TagType.Text;
            }
        }

    }
}
