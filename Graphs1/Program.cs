using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание графа на основе инцидентных вершин
            // var graph = Graph.MakeGraph(
            //     0, 1,
            //     0, 2,
            //     1, 3,
            //     1, 4,
            //     2, 3,
            //     3, 4);
            
            // foreach (var e in graph[0].BreadthSearch())
            // {
            //     Console.WriteLine(e.NodeNumber);
            // }
            
            // foreach (var e in graph[0].DepthSearch())
            // {
            //     Console.WriteLine(e.NodeNumber);
            // }

            var graph = new Graph(4);
            var weights = new Dictionary<Edge, double>
            {
                [graph.Connect(0, 1)] = 1,
                [graph.Connect(0, 2)] = 2,
                [graph.Connect(0, 3)] = 6,
                [graph.Connect(1, 3)] = 4,
                [graph.Connect(2, 3)] = 2
            };

            
            graph.Connect(0, 1, 1);
            graph.Connect(0, 2, 2);
            graph.Connect(0, 3, 6);
            graph.Connect(1, 3, 4);
            graph.Connect(2, 3, 2);
            
            var path = Algols.Dijkstra(graph, weights, graph[0], graph[3]).Select(n => n.NodeNumber);
            
            foreach (var e in Kruskal(graph))
            {
                Console.WriteLine(e.Weight);
            }
            
            // var t = Prim(graph);
            //
            // foreach (var m in t)
            // {
            //     Console.WriteLine(m.To.NodeNumber + " " + m.From.NodeNumber + " " +
            //                       m.Weight);
            // }
        }

        public static IEnumerable<Edge> Kruskal(Graph graph)
        {
            var tree = new List<Edge>();
            foreach (var edge in graph.Edges.Where(t => t.Weight > 0).OrderBy(x => x.Weight))
            {
                tree.Add(edge);
                var temp = MakeGraph(tree);
                if (HasCycle(temp))
                    tree.Remove(edge);
            }

            return tree;
        }
        
        private static bool HasCycle(Graph graph)
        {
            int length = graph.Nodes.Count();
            bool[] visited = new bool[length];

            for (int i = 0; i < length; i++) visited[i] = false;

            for (int u = 0; u < length; u++)
                if (!visited[u])
                    if (graph.IsCyclicUtil((Node) graph.Nodes.Where(t => t.NodeNumber == u)
                             .First(), visited, null))
                        return true;
            return false;
        }

        public static IEnumerable<Edge> Prim(Graph graph)
        {
            var tree = new List<Edge>();
            var possibleEdges = new List<Edge>();
            var nodes = new List<Node>();
            Random rnd = new Random();
            int num = rnd.Next(0, graph.Length);
            Node node = graph.Nodes.Where(t => t.NodeNumber == num).First();
            nodes.Add(node);

            while (tree.Count < graph.Length - 1)
            {
                foreach (var t in node.IncidentEdges)
                    if (!possibleEdges.Contains(t) && t.Weight > 0 &&
                        tree.Find(b => b.To == t.From && b.From == t.To) == null
                        && !tree.Contains(t))
                        possibleEdges.Add(t);

                possibleEdges = possibleEdges.OrderBy(b => b.Weight).ToList();

                foreach (var t in possibleEdges)
                {
                    tree.Add(t);
                    if ((nodes.Contains(t.To) && nodes.Contains(t.From)) || HasCycle(MakeGraph(tree)))
                    {
                        tree.Remove(t);
                        continue;
                    }

                    node = t.To == node ? t.From : t.To;
                    possibleEdges.Remove(t);
                    break;
                }
            }

            return tree;
        }

        private static Graph MakeGraph(List<Edge> tree)
        {
            List<Node> nodes = new List<Node>();
            foreach (var edge in tree)
            {
                if (!nodes.Contains(edge.From))
                    nodes.Add(edge.From);
                if (!nodes.Contains(edge.To))
                    nodes.Add(edge.To);
            }

            Graph graph = new Graph(nodes.Count);
            nodes.OrderBy(t => t.NodeNumber);
            foreach (var n in tree)
            {
                if (!graph.Edges.Contains(n))
                    graph.Connect(graph.Nodes.Where(t => t.NodeNumber == nodes.IndexOf(n.To))
                            .First().NodeNumber,
                        graph.Nodes.Where(t => t.NodeNumber == nodes.IndexOf(n.From))
                            .First().NodeNumber, n.Weight);
            }

            return graph;
        }

        
    }
}