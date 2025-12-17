using System;
using System.Collections.Generic;
using System.Linq;

// MELHORIA (2025): As bibliotecas System.Text e System.IO foram comentadas pois a responsabilidade
// de manipulação de arquivos foi movida para a classe dedicada 'SaveGraphAlgorithm' (no arquivo Save.cs), seguindo o
// Princípio da Responsabilidade Única (SRP).
// using System.Text;
// using System.IO;

namespace Grafo_2022_2
{
    /*
    // CÓDIGO LEGADO (2022): Estrutura de Vértice e Aresta
    // As classes 'vertice' e 'aresta' (aninhada) não seguiam as convenções de nomenclatura do C# (PascalCase).
    // Os campos eram públicos, quebrando o encapsulamento. A aresta armazenava apenas o ID do destino (string),
    // o que exigia buscas constantes e ineficientes no grafo para obter o objeto do vértice vizinho.
    public class vertice
    {
        //gerar um objeto Vertice com os atributos
        public string Id_Vertice;
        public int Id_Peso = 0;
        public int Id_Cor = 99;

        //gerar uma lista de adjacencias do vertice
        public List<aresta> adjacencias = new List<aresta>();

        //Usar para Djikstra
        public int rotulo = 0; //rotular o vertice
        public bool permanente = false;

        //pega o atributo do valor da aresta e aponta para seu destino
        public class aresta
        {
            //Pega e atribui um valor na aresta
            public string Id_Destino; //identifica a vertice destino - não orientado
            public int Peso_Distancia; //gera um valor entre os vertices - da um peso para a aresta
        }
    }
    */

    // MELHORIA (2025): Classes 'Edge' (Aresta) e 'Vertex' (Vértice) refatoradas e traduzidas para Inglês.

    /// <summary>
    /// Represents an Edge (Aresta) in a graph, connecting a vertex to another with a weight.
    /// (Anteriormente: Aresta)
    /// </summary>
    public class Edge
    {
        // MELHORIA (Performance): Edge now stores the direct reference to the destination 'Vertex' object.
        // This avoids repeated and inefficient searches (O(n)) in the main vertex list.
        // (Anteriormente: Id_Destino [string])
        public Vertex Destination { get; private set; }

        // MELHORIA (Nomenclatura e Encapsulamento): Property 'Weight' with standard name and public setter.
        // (Anteriormente: Peso_Distancia)
        public int Weight { get; set; }

        public Edge(Vertex destination, int weight)
        {
            Destination = destination;
            Weight = weight;
        }
    }

    /// <summary>
    /// Represents a Vertex (Vértice) or Node in a graph.
    /// (Anteriormente: vertice)
    /// </summary>
    public class Vertex
    {
        // MELHORIA (Nomenclatura e Encapsulamento): Properties follow PascalCase standard.
        // The setter for 'Id' is private to ensure the identifier is not changed after creation.
        // (Anteriormente: Id_Vertice)
        public string Id { get; private set; }

        // (Anteriormente: Id_Peso)
        public int Weight { get; set; }

        // (Anteriormente: Id_Cor)
        public int Color { get; set; } = 0; // Initialized with 0 (no color).

        // MELHORIA (Estrutura): The adjacency list is now a list of 'Edge' objects,
        // containing both destination and weight, making graph navigation more efficient.
        // (Anteriormente: adjacencias)
        public List<Edge> Adjacencies { get; private set; } = new List<Edge>();

        // MELHORIA (Clareza): Attributes for Dijkstra's algorithm with clear names and default values.
        // Replaces the old 'rotulo' and 'permanente'.
        // (Anteriormente: rotulo)
        public int Distance { get; set; } = int.MaxValue;
        // (Anteriormente: permanente)
        public bool Visited { get; set; } = false;
        public Vertex Predecessor { get; set; } = null;

        public Vertex(string id, int weight)
        {
            Id = id;
            Weight = weight;
        }
    }

    /// <summary>
    /// Main class representing the Graph data structure.
    /// (Anteriormente: Grafo)
    /// </summary>
    public class Graph
    {
        // MELHORIA (2025): Replacement of 'List<vertice>' with 'Dictionary<string, Vertex>'.
        // Searching for a vertex in a list has O(n) complexity. In a dictionary (hash map),
        // the search has constant time complexity, O(1), which is drastically faster for large graphs.
        // (Anteriormente: vertices [List])
        private readonly Dictionary<string, Vertex> _vertices = new Dictionary<string, Vertex>();

        // MELHORIA (2025): The vertex counter was replaced by a public property
        // getting the value directly from the dictionary.
        // (Anteriormente: numVertices)
        public int VertexCount => _vertices.Count;

        // MELHORIA (2025): Exposes the vertex collection safely (read-only)
        // via an interface (IEnumerable).
        public IEnumerable<Vertex> Vertices => _vertices.Values;

        /// <summary>
        /// Displays the graph's adjacency list in the console.
        /// (Anteriormente: exibirGrafo)
        /// </summary>
        public void DisplayGraph()
        {
            /*
            // CÓDIGO LEGADO (2022):
            public void exibirGrafo()
            {
                Console.WriteLine("Grafo possui {0} vértices. \n\n", numVertices);
                foreach (vertice v in vertices)
                {
                    Console.Write("Vértice {0}, valor: {1}, cor: {2} é adjacente a: ", v.Id_Vertice, v.Id_Peso, v.Id_Cor);
                    Console.WriteLine();
                    foreach (vertice.aresta V_aux in v.adjacencias)
                        Console.Write("Vertice adjacente: \n", V_aux.Id_Destino);
                }
            }
            */

            // MELHORIA (2025):
            // Uses string interpolation and 'string.Join' to create a clean representation.
            Console.WriteLine($"\n--- Lista de Adjacências do Grafo ({VertexCount} vértices) ---");
            foreach (var vertex in Vertices)
            {
                var adjacencies = string.Join(", ", vertex.Adjacencies.Select(a => $"{a.Destination.Id}({a.Weight})"));
                Console.WriteLine($"Vértice {vertex.Id} -> [ {adjacencies} ]");
            }
            Console.WriteLine("-----------------------------------------------------\n");
        }

        /// <summary>
        /// Adds a new vertex to the graph.
        /// (Anteriormente: incluirVertice)
        /// </summary>
        /// <param name="id">The vertex identifier (Anteriormente: _Name)</param>
        /// <param name="weight">The vertex weight (Anteriormente: _Peso)</param>
        public void AddVertex(string id, int weight)
        {
            /*
            // CÓDIGO LEGADO (2022):
            public void incluirVertice(string _Name, int _Peso)
            {
                vertice v = new vertice();
                v.Id_Vertice = _Name;
                v.Id_Peso = _Peso;
                if (existeVertice(v.Id_Vertice) != null)
                    Console.WriteLine("Vértice {0} já existe no Grafo.", v.Id_Vertice);
                else {
                    vertices.Add(v);
                    numVertices++;
                }
            }
            */

            // MELHORIA (2025):
            // A busca 'ContainsKey' em um Dictionary é O(1), muito mais rápida.
            // O novo vértice só é criado se o ID não existir, evitando alocação desnecessária.
            if (!_vertices.ContainsKey(id))
            {
                var newVertex = new Vertex(id, weight);
                _vertices.Add(id, newVertex);
            }
            else
            {
                Console.WriteLine($"AVISO: Vértice com ID '{id}' já existe no grafo.");
            }
        }

        /// <summary>
        /// Adds an undirected edge between two vertices.
        /// (Anteriormente: incluirAresta)
        /// </summary>
        /// <param name="sourceId">Source vertex ID (Anteriormente: V_a1)</param>
        /// <param name="destinationId">Destination vertex ID (Anteriormente: V_a2)</param>
        /// <param name="weight">Edge weight (Anteriormente: Peso_Distancia)</param>
        public void AddEdge(string sourceId, string destinationId, int weight)
        {
            /*
            // CÓDIGO LEGADO (2022):
            public void incluirAresta(string V_a1, string V_a2, int Peso_Distancia)
            {
                // ... (código complexo com vários loops e chamadas a existeVertice) ...
            }
            */

            // MELHORIA (2025):
            // A busca por ambos os vértices é O(1) usando 'TryGetValue'.
            // A verificação de adjacência é mais limpa e a aresta armazena a referência direta ao objeto.
            if (_vertices.TryGetValue(sourceId, out var source) && _vertices.TryGetValue(destinationId, out var destination))
            {
                // Verifica se a aresta já existe para evitar duplicatas.
                if (source.Adjacencies.Any(a => a.Destination == destination))
                {
                    Console.WriteLine($"AVISO: Aresta entre '{sourceId}' e '{destinationId}' já existe.");
                    return;
                }

                // Adiciona a aresta nos dois sentidos para um grafo não direcionado.
                source.Adjacencies.Add(new Edge(destination, weight));
                destination.Adjacencies.Add(new Edge(source, weight));
            }
            else
            {
                Console.WriteLine($"ERRO: Um dos vértices ('{sourceId}' ou '{destinationId}') não foi encontrado.");
            }
        }

        /// <summary>
        /// Removes a vertex and all connected edges.
        /// (Anteriormente: removerVertice)
        /// </summary>
        /// <param name="id">Vertex ID (Anteriormente: V_a1)</param>
        public void RemoveVertex(string id)
        {
            /*
            // CÓDIGO LEGADO (2022):
            public void removerVertice(string V_a1)
            {
                // ... (código complexo com loops aninhados e chamadas a posicaoAresta/removerAresta) ...
            }
            */

            // MELHORIA (2025):
            // A remoção do vértice principal do dicionário é O(1).
            // A remoção das arestas de referência é feita de forma mais limpa e declarativa com LINQ.
            if (_vertices.Remove(id))
            {
                // Itera sobre os vértices restantes para remover as arestas que apontavam para o vértice removido.
                foreach (var vertex in Vertices)
                {
                    vertex.Adjacencies.RemoveAll(edge => edge.Destination.Id == id);
                }
            }
            else
            {
                Console.WriteLine($"ERRO: Vértice '{id}' não encontrado para remoção.");
            }
        }

        /// <summary>
        /// Removes an edge between two vertices.
        /// (Anteriormente: removerAresta)
        /// </summary>
        /// <param name="sourceId">Source vertex ID (Anteriormente: V_a1)</param>
        /// <param name="destinationId">Destination vertex ID (Anteriormente: V_a2)</param>
        public void RemoveEdge(string sourceId, string destinationId)
        {
            /*
            // CÓDIGO LEGADO (2022):
            public void removerAresta(string V_a1, string V_a2)
            {
                // ... (código com chamadas a posicaoVertice e posicaoAresta) ...
            }
            */

            // MELHORIA (2025):
            if (_vertices.TryGetValue(sourceId, out var source) && _vertices.TryGetValue(destinationId, out var destination))
            {
                // Usa o método 'RemoveAll' do LINQ, que é mais eficiente e legível
                // do que encontrar o índice e depois remover pelo índice.
                int removedSource = source.Adjacencies.RemoveAll(a => a.Destination == destination);
                int removedDest = destination.Adjacencies.RemoveAll(a => a.Destination == source);

                if (removedSource == 0 || removedDest == 0)
                {
                    Console.WriteLine($"AVISO: Aresta entre '{sourceId}' e '{destinationId}' não existia.");
                }
            }
            else
            {
                Console.WriteLine($"ERRO: Um dos vértices ('{sourceId}' ou '{destinationId}') não foi encontrado.");
            }
        }

        /// <summary>
        /// Checks if two vertices are adjacent.
        /// (Anteriormente: adjacentes)
        /// </summary>
        /// <param name="sourceId">Source vertex ID (Anteriormente: V_a1)</param>
        /// <param name="destinationId">Destination vertex ID (Anteriormente: V_a2)</param>
        public bool AreAdjacent(string sourceId, string destinationId)
        {
            /*
            // CÓDIGO LEGADO (2022):
            public bool adjacentes(string V_a1, string V_a2)
            {
                // ... (loops) ...
            }
            */

            // MELHORIA (2025):
            if (_vertices.TryGetValue(sourceId, out var source))
            {
                // Usa LINQ (.Any) para uma verificação mais limpa e declarativa.
                // A lógica é mais direta: "Existe alguma aresta na lista de adjacências da origem
                // cujo destino é o vértice destino?"
                return source.Adjacencies.Any(a => a.Destination.Id == destinationId);
            }
            return false;
        }

        /// <summary>
        /// Checks if the graph is complete (all vertices connected to all others).
        /// (Anteriormente: completo)
        /// </summary>
        public bool IsComplete()
        {
            /*
            // CÓDIGO LEGADO (2022):
            public bool completo()
            {
                foreach (vertice v in vertices)
                    if (v.adjacencias.Count < (numVertices - 1))
                        return (false);
                return (true);
            }
            */

            // MELHORIA (2025): A lógica é a mesma, mas usa as novas propriedades e LINQ para maior clareza.
            // Um grafo é completo se cada um de seus 'n' vértices se conecta a todos os outros (n-1) vértices.
            int n = VertexCount;
            if (n <= 1) return true;

            return Vertices.All(vertex => vertex.Adjacencies.Count == n - 1);
        }

        /// <summary>
        /// Checks if the graph is totally disconnected (no edges).
        /// (Anteriormente: totalmenteDesconexo)
        /// </summary>
        public bool IsTotallyDisconnected()
        {
            /*
            // CÓDIGO LEGADO (2022):
            public bool totalmenteDesconexo()
            {
                foreach (vertice v in vertices)
                    if (v.adjacencias.Count != 0)
                        return (false);
                return (true);
            }
            */

            // MELHORIA (2025): Simplificado com LINQ. Verifica se 'todos' os vértices
            // têm uma contagem de adjacências igual a zero.
            return Vertices.All(v => v.Adjacencies.Count == 0);
        }

        /// <summary>
        /// Checks if the graph is empty (no vertices).
        /// (Anteriormente: vazio)
        /// </summary>
        public bool IsEmpty()
        {
            return VertexCount == 0;
        }

        /// <summary>
        /// Clears the graph (removes all vertices and edges).
        /// (Anteriormente: ReiniciarGrafo)
        /// </summary>
        public void ResetGraph()
        {
            /*
            // CÓDIGO LEGADO (2022):
            public void ReiniciarGrafo()
            {
                vertices.Clear();
                numVertices = 0;
            }
            */

            // MELHORIA (2025): A lógica é encapsulada. Em vez de limpar a lista e zerar o contador
            // externamente, o próprio grafo se encarrega de limpar seu dicionário de vértices.
            _vertices.Clear();
        }

        /// <summary>
        /// Helper method to get a vertex by ID.
        /// (Anteriormente: existeVertice)
        /// </summary>
        /// <param name="id">Vertex ID</param>
        public Vertex GetVertexById(string id)
        {
            _vertices.TryGetValue(id, out var vertex);
            return vertex; // Retorna o vértice se encontrado, ou null caso contrário.
        }

        /// <summary>
        /// Generates a random test graph with vertices and edges.
        /// (Gera um grafo de teste aleatório com vértices e arestas para facilitar a validação.)
        /// (Anteriormente: CriarGrafoTeste / preencheGrafo)
        /// </summary>
        public void GenerateTestGraph(int vertexCount = 10, int edgeCount = 15)
        {
            Random rnd = new Random();
            
            // 1. Criar Vértices
            for (int i = 0; i < vertexCount; i++)
            {
                string id = i.ToString();
                if (!_vertices.ContainsKey(id))
                {
                    AddVertex(id, rnd.Next(1, 100)); // Peso aleatório entre 1 e 100
                }
            }

            // 2. Criar Arestas Aleatórias
            var keys = _vertices.Keys.ToList();
            for (int i = 0; i < edgeCount; i++)
            {
                string source = keys[rnd.Next(keys.Count)];
                string dest = keys[rnd.Next(keys.Count)];
                
                // Evitar laços (origem == destino) e arestas duplicadas
                if (source != dest && !AreAdjacent(source, dest))
                {
                    AddEdge(source, dest, rnd.Next(1, 50)); // Peso aleatório entre 1 e 50
                }
            }
            
            Console.WriteLine($"\nSUCESSO: Grafo de teste gerado com {vertexCount} vértices e arestas aleatórias.");
        }

        /// <summary>
        /// Checks if the graph is Eulerian (contains an Eulerian Cycle).
        /// A graph is Eulerian if it is connected (ignoring isolated vertices) and every vertex has an even degree.
        /// (Verifica se o grafo é Euleriano: Conexo e todos os vértices com grau par.)
        /// (Anteriormente: euleriano)
        /// </summary>
        public bool IsEulerian()
        {
            // 1. Check connectivity (ignoring isolated vertices)
            if (!IsConnected())
                return false;

            // 2. Check if every vertex has an even degree
            foreach (var vertex in Vertices)
            {
                if (vertex.Adjacencies.Count % 2 != 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Helper method to check if all non-zero degree vertices are connected.
        /// </summary>
        private bool IsConnected()
        {
            var verticesWithEdges = Vertices.Where(v => v.Adjacencies.Count > 0).ToList();
            
            if (verticesWithEdges.Count == 0) return true; // Trivial case

            var startVertex = verticesWithEdges.First();
            var visited = new HashSet<string>();
            var queue = new Queue<Vertex>();
            
            queue.Enqueue(startVertex);
            visited.Add(startVertex.Id);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var edge in current.Adjacencies)
                {
                    if (!visited.Contains(edge.Destination.Id))
                    {
                        visited.Add(edge.Destination.Id);
                        queue.Enqueue(edge.Destination);
                    }
                }
            }

            return verticesWithEdges.All(v => visited.Contains(v.Id));
        }
    }
}
