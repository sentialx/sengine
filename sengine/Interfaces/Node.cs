using System.Collections.Generic;

namespace sengine.Interfaces {
    public abstract class Node<T> {
        public List<T> Children = new List<T>();
        public T ParentNode;
        public string NodeValue;
    }
}
