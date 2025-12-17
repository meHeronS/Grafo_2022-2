﻿using System;
using System.Collections.Generic;
using System.Linq;

// MELHORIA (2025): Bibliotecas desnecessárias comentadas.
// using System.Text;
// using System.Threading.Tasks;
// using System.IO;

namespace Grafo_2022_2
{
    /*
    // CÓDIGO LEGADO (2022):
    // A implementação original continha erros conceituais graves sobre o algoritmo de Dijkstra.
    // 1. Lógica Incorreta: Não acumulava o custo do caminho (distância da origem + peso da aresta).
    //    O código fazia 'if (peso < rotulo)', o que apenas verificava a aresta localmente, comportando-se
    //    como um algoritmo guloso simples (Prim incorreto) e não como Dijkstra.
    // 2. Performance: Iterava sobre todos os vértices repetidamente dentro de loops aninhados (Complexidade O(V^3) ou pior).
    // 3. Nomenclatura: Nomes como 'Algoritmo_1' e 'A_dijkstra' não seguiam boas práticas.
    public class Algoritmo_1
    {
        //tentativa de fazer uma validação de Dijkstra
        //ref: https://www.revista-programar.info/artigos/algoritmo-de-dijkstra/
        public void A_dijkstra(Grafo G, string Id_Name1, string Id_Name2)
        {            
            List<vertice> caminhoAtual = new List<vertice>(), caminho = new List<vertice>();
            // vertice verticedestino = new vertice();
            // vertice verticeatual = new vertice();
            /*
            vertice atual;
            //setando os vertices de acordo com seu tipo
            foreach (vertice v in G.vertices)
            {
                if (v.Id_Vertice == G.existeVertice(Id_Name1).Id_Vertice)
                {
                    v.permanente = true;
                    v.rotulo = 0;
                    atual = v;
                    caminhoAtual.Add(v);
                }
                else
                {
                    v.permanente = false;
                    v.rotulo = int.MaxValue;
                }
            }

            foreach (vertice v in G.vertices)
            {
                rotular(v,G);
            }

            foreach (vertice v in G.vertices)
            {
                if (v.Id_Vertice == G.existeVertice(Id_Name2).Id_Vertice)
                {
                    Console.WriteLine("id da origem: {0}\nrotulo da origem: {1}", v.Id_Vertice, v.rotulo);
                }
                else if (v.Id_Vertice == G.existeVertice(Id_Name2).Id_Vertice)
                {
                    Console.WriteLine("d do destino: {0}\nrotulo do destino: {1}", v.Id_Vertice, v.rotulo);
                }
            }
        }
        //atribuir o menor caminho entre vertices
        public void rotular(vertice v, Grafo G)
        {
            foreach (vertice comparador in G.vertices)
            {
                foreach (vertice.aresta a in v.adjacencias)
                {
                    if (G.adjacentes(a.Id_Destino, v.Id_Vertice))
                    {
                        if (a.Peso_Distancia < v.rotulo)
                        {
                            v.rotulo = a.Peso_Distancia;
                        }
                    }
                }
            }
            v.permanente = true;
        }
    }
    */

    // MELHORIA (2025): Implementação correta e otimizada do Algoritmo de Dijkstra.
    public class DijkstraAlgorithm
    {
        /// <summary>
        /// Executes Dijkstra's algorithm to find the shortest path between two vertices.
        /// (Executa o algoritmo de Dijkstra para encontrar o menor caminho entre dois vértices.)
        /// </summary>
        /// <param name="graph">The graph instance.</param>
        /// <param name="startId">ID of the starting vertex.</param>
        /// <param name="endId">ID of the destination vertex.</param>
        public void Execute(Graph graph, string startId, string endId)
        {
            // MELHORIA: Busca O(1) usando o novo método do grafo (Dictionary).
            // Anteriormente, isso era feito percorrendo listas (O(n)).
            var startNode = graph.GetVertexById(startId);
            var endNode = graph.GetVertexById(endId);

            if (startNode == null || endNode == null)
            {
                Console.WriteLine("ERRO: Um ou ambos os vértices não foram encontrados.");
                return;
            }

            // 1. Initialization (Inicialização)
            // Resetar distâncias e estados para garantir que o algoritmo rode limpo,
            // caso o grafo já tenha sido usado anteriormente.
            foreach (var v in graph.Vertices)
            {
                v.Distance = int.MaxValue; // Infinito
                v.Visited = false;
                v.Predecessor = null;
            }

            startNode.Distance = 0;
            
            // Lista de vértices a serem processados.
            // Em cenários de alta performance (.NET 6+), usaríamos PriorityQueue.
            // Para este escopo acadêmico/portfólio, uma lista ordenada funciona adequadamente.
            var unvisited = graph.Vertices.ToList();

            // 2. Main Loop (Loop Principal)
            while (unvisited.Count > 0)
            {
                // Encontrar o vértice não visitado com a menor distância.
                // Simula a extração do mínimo de uma fila de prioridade.
                var current = unvisited.OrderBy(v => v.Distance).First();

                if (current.Distance == int.MaxValue) 
                {
                    // Todos os vértices restantes são inalcançáveis (grafo desconexo).
                    break; 
                }
                
                // Remove da lista e marca como visitado.
                unvisited.Remove(current);
                current.Visited = true;

                // Otimização: Se chegamos ao destino, podemos parar antecipadamente.
                if (current == endNode) break;

                // 3. Relaxation (Relaxamento das arestas)
                // CORREÇÃO CRÍTICA: A lógica correta de Dijkstra é: Distância acumulada até aqui + peso da aresta.
                // O código legado fazia apenas "if (peso < rotulo)", ignorando o caminho percorrido.
                foreach (var edge in current.Adjacencies)
                {
                    var neighbor = edge.Destination;
                    
                    // Se o vizinho já foi visitado (fechado), pulamos.
                    if (neighbor.Visited) continue;

                    int newDist = current.Distance + edge.Weight;

                    // Se encontramos um caminho mais curto até o vizinho, atualizamos.
                    if (newDist < neighbor.Distance)
                    {
                        neighbor.Distance = newDist;
                        neighbor.Predecessor = current;
                    }
                }
            }

            // Exibir Resultado
            DisplayPath(startNode, endNode);
        }

        private void DisplayPath(Vertex start, Vertex end)
        {
            if (end.Distance == int.MaxValue)
            {
                Console.WriteLine($"Não há caminho entre {start.Id} e {end.Id}.");
            }
            else
            {
                Console.WriteLine($"Menor caminho de {start.Id} para {end.Id}: Custo {end.Distance}");
                
                // Reconstruir o caminho usando os predecessores (Backtracking)
                var path = new List<string>();
                var curr = end;
                while (curr != null)
                {
                    path.Add(curr.Id);
                    curr = curr.Predecessor;
                }
                path.Reverse(); // Inverter para mostrar da origem ao destino

                Console.WriteLine("Caminho: " + string.Join(" -> ", path));
            }
        }
    }
}
