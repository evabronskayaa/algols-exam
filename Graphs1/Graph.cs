using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        readonly List<Edge> edges = new List<Edge>();
        public readonly int NodeNumber;

        public Node(int number)
        {
            NodeNumber = number;
        }

        public override string ToString()
        {
            return NodeNumber.ToString();
        }

        //перечислить ноды кт инцидентны данной, но не позволит изменить
        public IEnumerable<Node> IncidentNodes
        {
            get { return edges.Select(z => z.OtherNode(this)); }
        }

        public IEnumerable<Edge> IncidentEdges
        {
            get
            {
                foreach (var e in edges) yield return e;
            }
        }

        public static Edge Connect(Node node1, Node node2)
        {
            var edge = new Edge(node1, node2);
            node1.edges.Add(edge);
            node2.edges.Add(edge);
            return edge;
        }

        public static void Disconnect(Edge edge)
        {
            edge.From.edges.Remove(edge);
            edge.To.edges.Remove(edge);
        }
    }

    public class Graph
    {
        private Node[] nodes;

        public Graph(int nodesCount)
        {
            nodes = Enumerable.Range(0, nodesCount).Select(z => new Node(z)).ToArray();
        }

        public int Length
        {
            get { return nodes.Length; }
        }

        public Node this[int index]
        {
            get { return nodes[index]; }
        }

        public IEnumerable<Node> Nodes
        {
            get
            {
                foreach (var node in nodes) yield return node;
            }
        }

        public Edge Connect(int index1, int index2)
        {
            return Node.Connect(nodes[index1], nodes[index2]);
        }

        public void Delete(Edge edge)
        {
            Node.Disconnect(edge);
        }

        public IEnumerable<Edge> Edges
        {
            get { return nodes.SelectMany(z => z.IncidentEdges).Distinct(); }
        }

        public static Graph MakeGraph(params int[] incidentNodes)
        {
            var graph = new Graph(incidentNodes.Max() + 1);
            for (int i = 0; i < incidentNodes.Length - 1; i += 2)
                graph.Connect(incidentNodes[i], incidentNodes[i + 1]);
            return graph;
        }
    }
}