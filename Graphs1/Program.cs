using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs1
{
    class Program
    {
        static void Main(string[] args)
        {
            // var graph = Graph.MakeGraph(
            //     0, 1,
            //     0, 2,
            //     1, 3,
            //     1, 4,
            //     2, 3,
            //     3, 4);
            //
            // foreach (var e in graph[0].BreadthSearch())
            // {
            //     Console.WriteLine(e.NodeNumber);
            // }
            //
            // foreach (var e in graph[0].DepthSearch())
            // {
            //     Console.WriteLine(e.NodeNumber);
            // }
            
            var graph = new Graphs1.Graph(4);
            var weights = new Dictionary<Edge, double>
            {
                [graph.Connect(0, 1)] = 1,
                [graph.Connect(0, 2)] = 2,
                [graph.Connect(0, 3)] = 6,
                [graph.Connect(1, 3)] = 4,
                [graph.Connect(2, 3)] = 2
            };

            var path = Algols.Dijkstra(graph, weights, graph[0], graph[3]).Select(n => n.NodeNumber);

            foreach (var e in path)
            {
                Console.WriteLine(e);
            }
        }
        
        public static IEnumerable<Edge> Kruskal(IEnumerable<Edge> edges)
        {
            var tree = new List<Edge>();
            foreach (var edge in edges.OrderBy(x => x.Weight))
            {
                tree.Add(edge);
                if (!HasCycle(tree))
                    tree.Remove(edge);
            }
            return tree;
        }

        private static bool HasCycle(List<Edge> edges)
        {
            
            return true;
        }
    }
}