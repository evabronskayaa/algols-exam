using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs1
{
    public class DijkstraData
    {
        public Node Previous { get; set; }
        public double Price { get; set; }
    }

    public static class Algols
    {
        public static List<Node> Dijkstra(Graph graph, Dictionary<Edge, double> weights, Node start, Node end)
        {
            var notVisited = graph.Nodes.ToList();
            var track = new Dictionary<Node, DijkstraData>(); //оценки для каждой вершины
            track[start] = new DijkstraData {Price = 0, Previous = null};// информация о нулевой вершине

            while (true)
            {
                Node toOpen = null;
                var bestPrice = double.PositiveInfinity; // минимальная цена
                foreach (var e in notVisited)
                {
                    if (track.ContainsKey(e) && track[e].Price < bestPrice)
                    {
                        bestPrice = track[e].Price;
                        toOpen = e;
                    }
                }

                // случай, если в графе нет подходящих для раскрытия вершин
                if (toOpen == null) return null;
                if (toOpen == end) break;
                
                // иначе раскрываем
                // пробегаемся по вершинам, у которых она явл начальной
                foreach (var e in toOpen.IncidentEdges.Where(z => z.From == toOpen))
                {
                    try
                    {
                        var t = weights[e];
                    }
                    catch
                    {
                        continue;
                    }
                    var currentPrice = track[toOpen].Price + weights[e];
                    var nextNode = e.OtherNode(toOpen);
                    if (!track.ContainsKey(nextNode) || track[nextNode].Price > currentPrice)
                    {
                        track[nextNode] = new DijkstraData { Price = currentPrice, Previous = toOpen,};
                    }
                }

                // раскрытие вершины можно считать законченым
                notVisited.Remove(toOpen);
            }

            var result = new List<Node>();
            while (end != null)
            {
                result.Add(end);
                end = track[end].Previous;
            }

            result.Reverse();
            return result;
        }

        public static IEnumerable<Node> DepthSearch(this Node startNode)
        {
            var visited = new HashSet<Node>();
            var stack = new Stack<Node>();
            stack.Push(startNode);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                if (visited.Contains(node)) continue;
                visited.Add(node);
                yield return node;
                foreach (var incidentNode in node.IncidentNodes)
                    stack.Push(incidentNode);
            }
        }

        public static IEnumerable<Node> BreadthSearch(this Node startNode)
        {
            var visited = new HashSet<Node>();
            var queue = new Queue<Node>();
            queue.Enqueue(startNode);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                if (visited.Contains(node)) continue;
                visited.Add(node);
                yield return node;
                foreach (var incidentNode in node.IncidentNodes)
                    queue.Enqueue(incidentNode);
            }
        }
    }
}