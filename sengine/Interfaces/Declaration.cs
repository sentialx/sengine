using System;
using System.Collections.Generic;
using System.Text;

namespace sengine.Interfaces {
    public class Declaration {
        public string Property;
    }

    public class Declaration<T>: Declaration {
        public T Value;
    }
}
