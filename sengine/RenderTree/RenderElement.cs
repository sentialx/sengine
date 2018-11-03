using sengine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace sengine {
    public class RenderElement: Node<RenderElement> {
        public List<Declaration> Rules = new List<Declaration>();
        public double Width = double.NaN;
        public double Height = double.NaN;
        public double X = 0;
        public double Y = 0;
    }
}
