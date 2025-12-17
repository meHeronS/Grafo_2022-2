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
    // Mantido para histórico. A implementação original misturava interface com lógica e usava algoritmos menos eficientes.
    class Colorir_Grafo
    {
        // tentativa de colorir o grafo - referenciado código usando C++: https://acervolima.com/minimize-o-custo-para-colorir-todos-os-vertices-de-um-grafico-nao-direcionado-usando-determinada-operacao/ - ideia baseada naquela parada do slide Grafo Adjunto (ou Grafo de Linha) do slide (22-Coloração - Guloso - Welsh-Powell.pptx)
        //iria tentar fazer uma inteface no visual basic para tentar fazer alguma coisa com cores hexadecimais, mas não deu certo e ai so atribui numero
        //iria tentar usar: https://www.macoratti.net/18/10/c_hexacor1.htm - para validar de acordo com a numeração de hexadecimal
        public void colorirGrafo(Grafo G)
        {
            //vetor/lista de vertices
            int[] cores = new int[G.vertices.Count];
            List<vertice> adjacentes;
            int aux = 0;
            int opc;

            //vai "colorir" com números
            for (int i = 0; i < cores.Length; i++)
            {
                cores[i] = i + 1;
            }

            Console.WriteLine("Digite a opção de coloração: \n- Pior solução = 1\n- Guloso = 2");
            opc = int.Parse(Console.ReadLine());
            switch (opc)
            {
                //colore todos os vertices de uma cor
                case 1:
                    foreach (vertice v in G.vertices)
                    {
                        v.Id_Cor = cores[aux];
                        aux++;
                    }
                    break;
                //algoritmo guloso, atribui em sequencia as cores verificando apenas se ela não está sendo utilizada por um adjacente
                case 2:
                    G.vertices[0].Id_Cor = cores[0];
                    for (int j = 1; j < G.vertices.Count; j++)//para cada vertice
                    {
                        adjacentes = new List<vertice>();
                        foreach (vertice.aresta a in G.vertices[j].adjacencias)//para cada adjacente ao vertice j
                        {
                            for (int k = 0; k < G.vertices.Count; k++)//para cada vertice na lista de vertices
                            {
                                if (a.Id_Destino == G.vertices[k].Id_Vertice)//verifica se o vertice na lista é adj ao vertice j
                                {
                                    adjacentes.Add(G.vertices[k]); //adiciona a lista os vertices adj ao j
                                }
                            }
                        }

                        Console.WriteLine("vertice {0} tem {1} adjacentes", G.vertices[j].Id_Peso, adjacentes.Count);

                        for (int i = 0; i < cores.Length; i++)//para cada cor
                        {
                            if (G.vertices[j].Id_Cor == 0)
                            {
                                int contador = 0;
                                for (int k = 0; k < adjacentes.Count; k++)//para cada adjacente de j
                                {
                                    if (adjacentes[k].Id_Cor != cores[i])//se a cor não esta sendo utilizada por um adjacente
                                    {
                                        contador++;
                                    }
                                }
                                if (contador == adjacentes.Count)//se a cor não está sendo utilizada por nenhum dos adjacentes
                                {
                                    Console.WriteLine("colorindo {0} de {1}", G.vertices[j].Id_Peso, cores[i]);
                                    G.vertices[j].Id_Cor = cores[i];//colore o vertice com aquela cor;
                                }
                            }
                        }
                    }
                    break;
            }

        }
    }
    */

    // MELHORIA (2025): Algoritmo de Coloração refatorado.
    // (Anteriormente: Colorir_Grafo)
    public class GraphColoring
    {
        /// <summary>
        /// Colors the graph allowing the user to choose the method.
        /// (Colore o grafo permitindo ao usuário escolher o método.)
        /// </summary>
        public void ColorGraph(Graph graph)
        {
            if (graph.IsEmpty())
            {
                Console.WriteLine("O grafo está vazio. Nada para colorir.");
                return;
            }

            Console.WriteLine("Digite a opção de coloração: \n- Pior solução (1 cor por vértice) = 1\n- Guloso (Welsh-Powell) = 2");
            
            if (!int.TryParse(Console.ReadLine(), out int opc))
            {
                Console.WriteLine("Opção inválida.");
                return;
            }

            switch (opc)
            {
                case 1:
                    // Pior solução: Cada vértice tem uma cor única.
                    ApplyWorstCaseColoring(graph);
                    break;
                case 2:
                    // Guloso: Heurística de Welsh-Powell (Ordenação por grau).
                    ApplyGreedyColoring(graph);
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        private void ApplyWorstCaseColoring(Graph graph)
        {
            int color = 1;
            foreach (var vertex in graph.Vertices)
            {
                vertex.Color = color++;
                Console.WriteLine($"Vértice {vertex.Id} colorido com a cor {vertex.Color}");
            }
            Console.WriteLine($"\nColoração completa (Pior Caso). Total de cores usadas: {graph.VertexCount}");
        }

        private void ApplyGreedyColoring(Graph graph)
        {
            // 1. Reset all vertex colors to 0 (uncolored).
            foreach (var v in graph.Vertices) v.Color = 0;

            // 2. Welsh-Powell Heuristic: Sort vertices by degree (number of adjacencies) in descending order.
            var sortedVertices = graph.Vertices.OrderByDescending(v => v.Adjacencies.Count).ToList();

            int totalColorsUsed = 0;

            // 3. Assign colors
            foreach (var vertexToColor in sortedVertices)
            {
                // Find colors already used by neighbors.
                var neighborColors = new HashSet<int>();
                foreach (var edge in vertexToColor.Adjacencies)
                {
                    if (edge.Destination.Color != 0)
                    {
                        neighborColors.Add(edge.Destination.Color);
                    }
                }

                // Find the first available color.
                int newColor = 1;
                while (neighborColors.Contains(newColor))
                {
                    newColor++;
                }

                vertexToColor.Color = newColor;
                if (newColor > totalColorsUsed)
                {
                    totalColorsUsed = newColor;
                }
                
                Console.WriteLine($"Vértice {vertexToColor.Id} colorido com a cor {vertexToColor.Color}");
            }

            Console.WriteLine($"\nColoração completa (Guloso/Welsh-Powell). Total de cores usadas: {totalColorsUsed}");
        }
    }
}
