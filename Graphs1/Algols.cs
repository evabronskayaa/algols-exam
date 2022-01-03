using System;
using System.Collections.Generic;
using Graphs1;

namespace Graphs1
{
    public class Algols
    {
        public static void DepthTraversal(Graph graph)
        {
            List<Vertex> vertex = new List<Vertex>();
            List<EdgeData> bridges = new List<EdgeData>();
            Stack<Vertex> stack = new Stack<Vertex>();
            stack.Push(graph.Vertexes[0]);
            //?
            GraphVisualizationData data = new GraphVisualizationData(graph);

            Visualizer.CreateBitmaps(data);
            Visualizer.AddLog("Кладём в стек первую же вершину");

            while (stack.Count != 0)
            {
                var point = stack.Pop();
                Visualizer.AddLog("Достаём из стека вершину " + (point.Number + 1).ToString());

                if (!vertex.Contains(point))
                {
                    Visualizer.AddLog("Эту вершину ещё не посещали");
                    vertex.Add(point);
                }

                foreach (var edge in point.Edges)
                {
                    if (!vertex.Contains(edge.DestinationVertex))
                    {
                        Visualizer.AddLog("Находим вершину-соседа " + (edge.DestinationVertex.Number + 1).ToString()
                                                                    + " и кладём в стек");
                        stack.Push(edge.DestinationVertex);
                        bridges.Add(edge.EdgeDataObject);
                    }
                }
            }
        }

        public static void BreadthTraversal(Graph graph)
        {
            List<Vertex> vertex = new List<Vertex>();
            var queue = new Queue<Vertex>();
            queue.Enqueue(graph.Vertexes[0]);
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (!vertex.Contains(point))
                {
                    vertex.Add(point);
                }

                foreach (var edge in point.Edges)
                {
                    if (!vertex.Contains(edge.DestinationVertex))
                    {
                        queue.Enqueue(edge.DestinationVertex);
                    }
                }
            }
        }

        public static bool BFS(Graph graph, Vertex source, Vertex sink,
            ref Vertex[] parents)
        {
            List<Vertex> vertex = new List<Vertex>();
            var queue = new Queue<Vertex>();
            queue.Enqueue(graph.Vertexes[0]);
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (!vertex.Contains(point))
                {
                    vertex.Add(point);

                    foreach (var edge in point.Edges)
                    {
                        if (!vertex.Contains(edge.DestinationVertex))
                        {
                            if (edge.DestinationVertex == sink)
                            {
                                parents[point.Number] = sink;
                                return true;
                            }

                            queue.Enqueue(edge.DestinationVertex);
                            parents[point.Number] = edge.DestinationVertex;
                        }
                    }
                }
            }

            return false;
        }

        //public static void FordFulkerson(GraphVisualizationData graph)
        //{
        //    Vertex source = graph.RawGraph.Vertexes[0];
        //    Vertex sink = graph.RawGraph.Vertexes[graph.RawGraph.Vertexes.Count-1];

        public static int FordFulkerson(GraphVisualizationData graph, Vertex source, Vertex sink)
        {
            // Create a residual graph and fill
            // the residual graph with given
            // capacities in the original graph as
            // residual capacities in residual graph

            // Residual graph where rGraph[i,j]
            // indicates residual capacity of
            // edge from i to j (if there is an
            // edge. If rGraph[i,j] is 0, then
            // there is not)

            // This array is filled by BFS and to store path
            Vertex[] parents = new Vertex[graph.RawGraph.Vertexes.Count];
            //foreach (EdgeData t in graph.RawGraph.Edges)
            //{
            //    t.Capacity = t.Weight;
            //}
            int max_flow = 0; // There is no flow initially

            // Augment the flow while there is path from source
            // to sink
            while (BFS(graph.RawGraph, source, sink, ref parents))
            {
                // Find minimum residual capacity of the edhes
                // along the path filled by BFS. Or we can say
                // find the maximum flow through the path found.
                int path_flow = int.MaxValue;
                for (Vertex v = source; v != sink; v = parents[v.Number])
                {
                    Vertex temp = parents[v.Number];

                    if (graph.RawGraph.Edges.Find(t =>
                        t.FromVertex == parents[v.Number - 1] &&
                        t.ToVertex == temp) != null)
                        path_flow
                            = Math.Min(path_flow,
                                graph.RawGraph.Edges.Find(t =>
                                    t.FromVertex == parents[v.Number - 1] &&
                                    t.ToVertex == temp).Weight - graph.RawGraph.Edges.Find(t =>
                                    t.FromVertex == parents[v.Number - 1] &&
                                    t.ToVertex == temp).Capacity);
                }

                // update residual capacities of the edges and
                // reverse edges along the path
                for (Vertex v = source; v != sink; v = parents[v.Number])
                {
                    Vertex temp = parents[v.Number - 1];
                    temp.Edges.Find(t => t.DestinationVertex == v).Capacity += path_flow;
                }

                // Add path flow to overall flow
                max_flow += path_flow;
            }

            // Return the overall flow
            return max_flow;
        }
    }
}
