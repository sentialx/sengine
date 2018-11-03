using sengine.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace sengine {
    public class RenderTree {
        public List<RenderElement> Children;

        public RenderTree(CSSOM cssom) {
            this.Children = this.Build(cssom.Children);
        }

        private List<RenderElement> Build(List<CSSElement> elements, RenderElement parent = null) {
            List<RenderElement> result = new List<RenderElement>();

            foreach (var element in elements) {
                var renderElement = new RenderElement();

                foreach (var rule in element.Rules) {
                    if (rule.Property == "display" && rule.Value == "none") {
                        goto skip;
                    }
                }

                if (element.NodeValue != null) {
                    var textSize = CalculateTextSize(element.NodeValue);

                    renderElement.Width = textSize.Width;
                    renderElement.Height = textSize.Height;
                    renderElement.NodeValue = element.NodeValue;
                    renderElement.ParentNode = parent;

                    this.ResizeParentNodes(renderElement);
                }

                result.Add(renderElement);

                if (element.Children.Count > 0) {
                    renderElement.Children = Build(element.Children, renderElement);
                }

                skip:;
            }

            return result;
        }

        public void ResizeParentNodes(RenderElement element) {
            if (element.ParentNode != null) {
                if (double.IsNaN(element.ParentNode.Width)) element.ParentNode.Width = 0;
                if (double.IsNaN(element.ParentNode.Height)) element.ParentNode.Height = 0;

                element.ParentNode.Width += element.Width;
                element.ParentNode.Height += element.Height;

                this.ResizeParentNodes(element.ParentNode);
            }
        }

        public static Size CalculateTextSize(string text) {
            return new Size(text.Length * 16, 16);
        }
    }
}
