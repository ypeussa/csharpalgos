using System.Collections;
using System.Collections.Generic;

public class Graph<T> {
    public List<GraphNode<T>> nodes;
}

public class GraphNode<T> {
    public T data;
    public List<GraphNode<T>> edges;
    public List<float> weights;
}