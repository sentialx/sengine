using System;
using System.Collections.Generic;
using System.Text;

namespace sengine.Structs
{
    public struct Size
    {
        public double Width;
        public double Height;

        public Size(double width, double height) {
            this.Width = width;
            this.Height = height;
        }

        public Size(double size) {
            this.Width = size;
            this.Height = size;
        }
    }
}
