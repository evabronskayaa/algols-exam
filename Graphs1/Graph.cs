using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs1
{
    public class Edge
    {
        public readonly Node From;
        public readonly Node To;
        public readonly int Weight;

        public Edge(Node first, Node second, int weight = 0)
        {
            From = first;
            To = second;
            Weight = weight;
        }

        public bool IsIncident(Node node)
        {
            return From == node || To == node;
        }

        public Node OtherNode(Node node)
        {
            if (!IsIncident(node)) throw new ArgumentException();
            if (From == node) return To;
            return From;
        }
    }

    public class Node
    {
        private readonly List<Edge> _incidentEdges = new List<Edge>();
        public readonly int NodeNumber;

        public Node(int number)
        {
            NodeNumber = number;
        }
        
        // чтобы нормально в дебаге отображалось
        public override string ToString()
        {
            return NodeNumber.ToString();
        }

        //перечислить ноды кт инцидентны данной, но не позволит изменить
        public IEnumerable<Node> IncidentNodes
        {
            get { return _incidentEdges.Select(z => z.OtherNode(this)); }
            // get
            // {
            //     foreach (var edge in _incidentEdges) yield return edge.OtherNode(this);
            // }
        }
        
        // тоже самое, что и с узлами
        public IEnumerable<Edge> IncidentEdges
        {
            get
            {
                foreach (var edge in _incidentEdges) yield return edge;
            }
        }

        // создание связи в неориентированном графе
        public static Edge Connect(Node node1, Node node2, int weight = 0)
        {
            var edge = new Edge(node1, node2, weight);
            node1._incidentEdges.Add(edge);
            node2._incidentEdges.Add(edge);
            return edge;
        }
    }

    public class Graph
    {
        private readonly Node[] _nodes;
        
        // чтобы избежать повторения номеров вершин, узнаем их количество
        public Graph(int nodesCount)
        {
            _nodes = Enumerable.Range(0, nodesCount).Select(x => new Node(x)).ToArray();
        }

        public int Length => _nodes.Length;

        // для извлечения вершин, чтобы их соединить
        public Node this[int index] => _nodes[index];

        public IEnumerable<Node> Nodes
        {
            get
            {
                foreach (var node in _nodes) yield return node;
            }
        }

        public Edge Connect(int index1, int index2, int weight = 0)
        {
            return Node.Connect(_nodes[index1], _nodes[index2], weight);
        }

        public IEnumerable<Edge> Edges
        {
            get { return _nodes.SelectMany(z => z.IncidentEdges).Distinct(); }
        }

        public static Graph MakeGraph(params int[] incidentNodes)
        {
            var graph = new Graph(incidentNodes.Max() + 1);
            for (var i = 0; i < incidentNodes.Length - 1; i += 2)
                graph.Connect(incidentNodes[i], incidentNodes[i + 1]);
            return graph;
        }

        public bool IsCyclicUtil(Node current, bool[] visited, Node parent)
        {
            visited[current.NodeNumber] = true;

            foreach (var i in current.IncidentNodes)
            {
                if (!visited[i.NodeNumber])
                {
                    if (IsCyclicUtil(i, visited, current))
                        return true;
                }
                else if (i != parent)
                {
                    return true;
                }
                // могут быть ребра к тому же узлу
            }
            return false;

        }
    }
}