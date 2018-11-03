using System;
using System.Collections.Generic;
using System.Text;

namespace sengine.Interfaces {
    public abstract class Node<T> {
        public List<T> Children = new List<T>();
        public T ParentNode;
        public string NodeValue;
    }
}
