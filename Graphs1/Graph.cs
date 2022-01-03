using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graphs1
{
    public class Graph
    {
        public List<Vertex> Vertexes { get; set; }
        public List<EdgeData> Edges { get; set; }


        public static Graph FromJSON(string json)
        {
            var graph = JsonSerializer.Deserialize<Graph>(json, JsonOptions);
            graph.RestoreAll();
            graph.CheckOriented();
            graph.CheckWeighted();
            graph.CheckTransport();
            return graph;
        }
        public static Graph FromCSV(string[] csv, char separator = ';')
        {
            var graph = new Graph();

            string[][] textMatrix = csv.Select(r => r.Split(separator)).ToArray();

            int Vertexes = textMatrix.Length;

            int temp = 0;
            graph.Vertexes = new Vertex[Vertexes].Select(p => new Vertex() { Number = temp++ }).ToList();

            int?[][] matrix = new int?[Vertexes][].Select(r => new int?[Vertexes]).ToArray();

            for (int y = 0; y < Vertexes; y++)
            {
                for (int x = 0; x < Vertexes; x++)
                {
                    int result;
                    if (int.TryParse(textMatrix[y][x], out result)) matrix[y][x] = result;
                }
            }

            List<EdgeData> edges = new List<EdgeData>();

            for (int y = 0; y < Vertexes; y++)
            {
                for (int x = 0; x < Vertexes; x++)
                {
                    if (matrix[y][x].HasValue) edges.Add(new EdgeData(y, matrix[y][x].Value, x));
                }
            }

            graph.Edges = edges.ToList();
            graph.RestoreAll();
            graph.CheckOriented();
            graph.CheckWeighted();
            graph.CheckTransport();

            return graph;
        }


        public void SaveJSON(string path) => File.WriteAllText(path, JsonSerializer.Serialize(this, JsonOptions));
        public void SaveCSV(string path, char separator = ';')
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < Vertexes.Count; y++)
            {
                for (int x = 0; x < Vertexes.Count; x++)
                {
                    EdgeData edge = Edges.Find(e => e.From == y && e.To == x);
                    if (edge != null) sb.Append(edge.Weight);
                    sb.Append(separator);
                }
                sb.Append('\n');
            }
            File.WriteAllText(path, sb.ToString());
        }


        private void Restore(EdgeData edge)
        {
            var from = Vertexes.Find(v => v.Number == edge.From);
            var to = Vertexes.Find(v => v.Number == edge.To);

            if (from == null || to == null) throw new Exception("Одна или обе вершины не существуют!");

            edge.FromVertex = from;
            edge.ToVertex = to;

            from.Edges.Add(new Edge()
            {
                Weight = edge.Weight,
                Destination = edge.To,
                DestinationVertex = to,
                EdgeDataObject = edge,
                Capacity = edge.Capacity
            });
        }
        private void RestoreAll()
        {
            foreach (var ed in Edges)
                Restore(ed);
        }


        public void AddVertex()
        {
            Vertexes.Add(new Vertex()
            {
                Number = Vertexes.Any() ? Vertexes.Select(v => v.Number).Max() + 1 : 0
            });
        }
        public void RemoveVertex(int number)
        {
            var vertex = Vertexes.Find(v => v.Number == number);

            if (vertex == null) throw new Exception("Нет такой вершины!");

            foreach (var v in Vertexes)
            {
                if (v.Number > number) v.Number--;
            }

            var connectedVertexes = Vertexes
                .Where(v => v.Edges.Find(e => e.Destination == number) != null);

            foreach (var v in connectedVertexes)
            {
                v.Edges.Remove(v.Edges.Find(e => e.Destination == number));
            }

            Edges.RemoveAll(e => e.From == number || e.To == number);

            Vertexes.Remove(vertex);

            CheckOriented();
            CheckWeighted();
            CheckTransport();
        }


        public void AddEdge(int from, int to, int weight = 1, int capacity = 1)
        {
            if (Edges.Find(e => e.From == from && e.To == to) != null) throw new Exception($"Ребро из {from} в {to} уже существует!") ;
            
            var fromVertex = Vertexes.Find(v => v.Number == from);
            var toVertex = Vertexes.Find(v => v.Number == to);

            if (fromVertex == null || toVertex == null) throw new Exception("Одна или обе вершины не существуют!");

            var newEdge = new EdgeData(from, weight, to, capacity);

            Edges.Add(newEdge);
            Restore(newEdge);
            CheckOriented();
            CheckWeighted();
            CheckTransport();
        }
        public void RemoveEdge(int from, int to)
        {
            var edge = Edges.Find(e => e.From == from && e.To == to);

            if (edge == null) throw new Exception($"Ребро из {from} в {to} не существует!");

            var vertex = Vertexes.Find(v => v.Number == from);
            var vertexEdge = vertex.Edges.Find(e => e.Destination == to);

            vertex.Edges.Remove(vertexEdge);
            Edges.Remove(edge);
            CheckOriented();
            CheckWeighted();
            CheckTransport();
        }

        /// <summary>
        /// True, if not all of the edges are both way.
        /// </summary>
        public bool IsOriented
        {
            get => isOriented;
        }
        private bool isOriented;
        private void CheckOriented()
        {
            if (Edges.Any()) 
                isOriented = !Edges.All(
                    e1 => Edges.Find(
                        e2 => e1.To == e2.From && e1.From == e2.To && e1.Weight == e2.Weight) != null);
            else isOriented = false;
        }


        /// <summary>
        /// True, if not all edges are the same weight.
        /// </summary>
        public bool IsWeighted
        {
            get => isWeighted;
        }
        private bool isWeighted;
        private void CheckWeighted()
        {
            if (Edges.Any()) isWeighted = !Edges.All(e => e.Weight == Edges[0].Weight);
            else isWeighted = false;
        }


        /// <summary>
        /// True, if all edges's capacity above 0.
        /// </summary>
        public bool IsTransport
        {
            get => isTransport;
        }
        private bool isTransport;
        private void CheckTransport()
        {
            if (Edges.Any()) isTransport = Edges.All(e => e.Capacity > 0);
            else isTransport = false;
        }

        private readonly static JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
    }

    public class Vertex
    {
        public int Number { get; set; }
        [JsonIgnore]
        public List<Edge> Edges { get; set; } = new List<Edge>();
    }


    public class Edge
    {
        public int Destination { get; set; }
        public int Weight { get; set; }   
        
        public int Capacity { get; set; }

        [JsonIgnore()]
        public EdgeData EdgeDataObject { get; set; }

        [JsonIgnore()]
        public Vertex DestinationVertex { get; set; }
    }

    /// <summary>
    /// Represents graph's edge containing both of the points.
    /// </summary>
    public class EdgeData
    {
        /// <summary>
        /// Represents the first position vertex as a Vertex object. This is not JSON-serializable.
        /// </summary>
        [JsonIgnore]
        public Vertex FromVertex { get; set; }

        /// <summary>
        /// Represents the first position vertex as Int32.
        /// </summary>
        public int From { get; set; }

        /// <summary>
        /// Represents a weight of this Edge. 
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Represents the maximum capacity of this edge in Transport Networks
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Represents the second position vertex as a Vertex object. This is not JSON-serializable.
        /// </summary>
        [JsonIgnore]
        public Vertex ToVertex { get; set; }

        /// <summary>
        /// Represents the second position vertex as Int32.
        /// </summary>
        public int To { get; set; }

        public EdgeData(int from, int weight, int to, int capacity = 0)
        {
            From = from;
            Weight = weight;
            To = to;
            Capacity = capacity;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EdgeData)) return false;
            var edge = obj as EdgeData;
            return
                From == edge.From &&
                Weight == edge.Weight &&
                To == edge.To &&
                Capacity == edge.Capacity;
        }

        public EdgeData() { }
    }

    public class GraphVisualizationData
    {
        public readonly Graph RawGraph;
        public IEnumerable<Vertex> HighlightedVertexes { get; set; }
        public IEnumerable<EdgeData> HighlightedEdges { get; set; }
      
        public GraphVisualizationData(Graph rawGraph)
        {
            RawGraph = rawGraph;
            HighlightedVertexes = new Vertex[0];
            HighlightedEdges = new EdgeData[0];
        }

        /// <summary>
        /// Highlights specified vertexes.
        /// </summary>
        /// <param name="vertexes"></param>
        /// <returns></returns>
        public GraphVisualizationData WithHighlightedVertexes(params Vertex[] vertexes)
        {
            HighlightedVertexes = vertexes ?? HighlightedVertexes;
            return this;
        }

        /// <summary>
        /// Highlights specified vertexes.
        /// </summary>
        /// <param name="vertexes"></param>
        /// <returns></returns>
        public GraphVisualizationData WithHighlightedVertexes(IEnumerable<Vertex> vertexes)
        {
            HighlightedVertexes = vertexes ?? HighlightedVertexes;
            return this;
        }


        /// <summary>
        /// Highlights specified edges.
        /// </summary>
        /// <param name="edges"></param>
        /// <returns></returns>
        public GraphVisualizationData WithHighlightedEdges(params EdgeData[] edges)
        {
            HighlightedEdges = edges ?? HighlightedEdges;
            return this;
        }

        /// <summary>
        /// Highlights specified edges.
        /// </summary>
        /// <param name="edges"></param>
        /// <returns></returns>
        public GraphVisualizationData WithHighlightedEdges(IEnumerable<EdgeData> edges)
        {
            HighlightedEdges = edges ?? HighlightedEdges;
            return this;
        }

        //Do it like:
        // 
        //  new GraphVisualizationData(graph)
        //      .WithHighlightedVertexes(graph.Vertexes[0], graph.Vertexes[^1])
        //      .WithHighlightedEdges(null);
        //
        //  It will work and not throw a null ref, I promise.
    }
}