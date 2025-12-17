﻿using System;
using System.Collections.Generic;
using System.Linq;


// MELHORIA (2025): Bibliotecas desnecessárias comentadas.
// using System.Text;
// using System.Threading.Tasks;
// using System.IO;
// using System.Collections;

namespace Grafo_2022_2
{
    /*
    // CÓDIGO LEGADO (2022) - DEPRECIADO NA REFATORAÇÃO DE 2025.
    // Este arquivo (Cycle.cs, anteriormente NAIVE.cs) continha tentativas de implementação do algoritmo de Tarjan e detecção de ciclos
    // que não foram finalizadas ou integradas corretamente. Mantido apenas para histórico.

    class NAIVE : Grafo
    // /*
            ref: https://codeforces.com/blog/entry/71146?f0a28=2

            c++ code:

            int dfsAP(int u, int p) {
                int children = 0;
                low[u] = disc[u] = ++Time;
                for (int & v : adj[u]) {
                if (v == p) continue; // we don't want to go back through the same path.
                                      // if we go back is because we found another way back
                if (!disc[v]) { // if V has not been discovered before
                    children++;
                    dfsAP(v, u); // recursive DFS call
                    if (disc[u] <= low[v]) // condition #1
                        ap[u] = 1;
                    low[u] = min(low[u], low[v]); // low[v] might be an ancestor of u
                } else // if v was already discovered means that we found an ancestor
                    low[u] = min(low[u], disc[v]); // finds the ancestor with the least discovery time
            }
            return children;
        }

        void AP() {
            ap = low = disc = vector<int>(adj.size());
            Time = 0;
            for (int u = 0; u < adj.size(); u++)
                if (!disc[u])
                    ap[u] = dfsAP(u, u) > 1; // condition #2
        }
         * /
    //validar se o grafo é completo    
    //https://en.wikipedia.org/wiki/Tarjan%27s_strongly_connected_components_algorithm
    {
        protected List<List<vertice>> CompAltConect;
        protected Stack<vertice> Stackar;
        private int APeso;

        public List<List<vertice>> DetectaCiclo(List<vertice> nodes)
        {
            CompAltConect = new List<List<vertice>>();
            foreach (vertice vAux in nodes)
            {
                if (vAux.Id_Peso < 0 )
                {
                    VerCompAltConect(vAux);
                }
            }
            return CompAltConect;
        }
        private void VerCompAltConect(vertice v)
        {
            v.Id_Peso = APeso;
            v.rotulo = APeso;

            APeso++;
            Stackar.Push(v);

            foreach (vertice.aresta A_Aux in v.adjacencias)
            {
                A_Aux.Peso_Distancia = APeso;
            }
            APeso++;
            Stackar.Push(v);

            foreach (vertice.aresta vAux in v.adjacencias)
            {
                if (vAux.Peso_Distancia < 0)
                {
                    VerCompAltConect(v);
                    v.rotulo = Math.Min(v.Id_Peso, vAux.Peso_Distancia);
                }
                else if (Stackar.Contains(v))
                {
                    v.Id_Peso = Math.Min(v.Id_Peso, vAux.Peso_Distancia);
                }
            }

            if (v.Id_Peso == v.rotulo)
            {
                List<vertice> cycle = new List<vertice>();
                vertice w;

                do
                {
                    w = Stackar.Pop();
                    cycle.Add(w);
                } while (v != w);

                CompAltConect.Add(cycle);
            }
        }
    }
    */

    // MELHORIA (2025): Implementação funcional do Algoritmo de Tarjan para detecção de ciclos/SCCs.
    // (Anteriormente no arquivo NAIVE.cs)
    public class TarjanCycleDetection
    {
        // Variáveis auxiliares para o algoritmo de Tarjan
        private int _index;
        private Stack<Vertex> _stack;
        // Dicionário para armazenar a ordem de descoberta (index) de cada vértice
        private Dictionary<string, int> _indices;
        // Dicionário para armazenar o menor índice alcançável (Low-Link) a partir de um vértice
        private Dictionary<string, int> _lowLinks;
        // Conjunto para verificação rápida se o vértice está na pilha de processamento atual
        private HashSet<string> _onStack;
        // Lista que armazenará os Componentes Fortemente Conexos (SCCs) encontrados
        private List<List<Vertex>> _sccs;

        /// <summary>
        /// Detects Strongly Connected Components (SCCs) which represent cycles in the graph.
        /// (Detecta Componentes Fortemente Conexos que representam ciclos no grafo.)
        /// </summary>
        public List<List<Vertex>> DetectCycles(Graph graph)
        {
            _index = 0;
            _stack = new Stack<Vertex>();
            _indices = new Dictionary<string, int>();
            _lowLinks = new Dictionary<string, int>();
            _onStack = new HashSet<string>();
            _sccs = new List<List<Vertex>>();

            // Inicializa e executa para todos os vértices (para cobrir grafos desconexos)
            foreach (var v in graph.Vertices)
            {
                if (!_indices.ContainsKey(v.Id))
                {
                    StrongConnect(v);
                }
            }

            // Exibir resultados no console
            Console.WriteLine($"\nDetectados {_sccs.Count} componentes fortemente conexos (ciclos/grupos).");
            int i = 1;
            foreach (var scc in _sccs)
            {
                var ids = string.Join(", ", scc.Select(v => v.Id));
                Console.WriteLine($"Grupo {i++}: [{ids}]");
            }

            return _sccs;
        }

        private void StrongConnect(Vertex v)
        {
            // Define o índice de profundidade (descoberta) para v
            _indices[v.Id] = _index;
            _lowLinks[v.Id] = _index;
            _index++;
            _stack.Push(v);
            _onStack.Add(v.Id);

            // Considera os sucessores (vizinhos) de v
            foreach (var edge in v.Adjacencies)
            {
                var w = edge.Destination;
                if (!_indices.ContainsKey(w.Id))
                {
                    // O sucessor w ainda não foi visitado; recursão nele
                    StrongConnect(w);
                    // Na volta da recursão, atualiza o lowLink de v se w encontrou um caminho para um ancestral
                    _lowLinks[v.Id] = Math.Min(_lowLinks[v.Id], _lowLinks[w.Id]);
                }
                else if (_onStack.Contains(w.Id))
                {
                    // O sucessor w está na pilha e, portanto, faz parte do componente atual (SCC).
                    // Isso indica uma aresta de retorno (back-edge) no grafo.
                    _lowLinks[v.Id] = Math.Min(_lowLinks[v.Id], _indices[w.Id]);
                }
            }

            // Se v é um nó raiz (lowLink == index), então encontramos um componente completo.
            // Desempilha todos os elementos até encontrar v para formar o SCC.
            if (_lowLinks[v.Id] == _indices[v.Id])
            {
                var scc = new List<Vertex>();
                Vertex w;
                do
                {
                    w = _stack.Pop();
                    _onStack.Remove(w.Id);
                    scc.Add(w);
                } while (w != v);
                
                _sccs.Add(scc);
            }
        }
    }

    // MELHORIA (2025): Implementação "Naive" (Ingênua/Força Bruta) para detecção de ciclos.
    // Utiliza uma busca em profundidade (DFS) simples para encontrar arestas de retorno (back-edges).
    public class NaiveCycleDetection
    {
        private HashSet<string> _visited;
        private bool _hasCycle;

        /// <summary>
        /// Executes a simple DFS to detect if there are any cycles in the graph.
        /// (Executa uma DFS simples para detectar se existem ciclos no grafo.)
        /// </summary>
        public void Execute(Graph graph)
        {
            _visited = new HashSet<string>();
            _hasCycle = false;

            Console.WriteLine("\n--- Detecção de Ciclos (Método Naive/Força Bruta) ---");

            foreach (var vertex in graph.Vertices)
            {
                if (!_visited.Contains(vertex.Id))
                {
                    if (HasCycleDFS(vertex, null))
                    {
                        _hasCycle = true;
                    }
                }
            }

            if (!_hasCycle) Console.WriteLine("Nenhum ciclo detectado.");
            Console.WriteLine("-----------------------------------------------------");
        }

        private bool HasCycleDFS(Vertex current, string parentId)
        {
            _visited.Add(current.Id);

            foreach (var edge in current.Adjacencies)
            {
                var neighbor = edge.Destination;

                // Se o vizinho é o pai (de onde viemos), ignoramos (grafo não direcionado)
                if (neighbor.Id == parentId) continue;

                // Se o vizinho já foi visitado e não é o pai, encontramos um ciclo (back-edge)
                if (_visited.Contains(neighbor.Id))
                {
                    Console.WriteLine($"Ciclo detectado! Aresta de retorno: {current.Id} -> {neighbor.Id}");
                    return true;
                }

                if (HasCycleDFS(neighbor, current.Id))
                {
                    return true;
                }
            }
            return false;
        }
    }
}    
