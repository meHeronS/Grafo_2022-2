﻿using System;
using System.IO;
using System.Text;

namespace Grafo_2022_2
{
    /*
    // CÓDIGO LEGADO (2022) - DEPRECIADO
    // Esta classe era uma tentativa inicial de salvar o grafo, mas continha caminhos hardcoded
    // e misturava conceitos. Mantida para histórico.
    public class TESTEsSalvar
    {
        //criar uma pasta no c:
        // string Pasta_Grafo = @"C:\Grafo_Heron_2022\dados/";

        // if (!Directory.Exists(Pasta_Grafo))
        //    Directory.CreateDirectory(Pasta_Grafo);
        //string[] teste = File.ReadAllLines(Path.Combine(Pasta_Grafo, "Grafo.csv"));
        //gerar o arquivo CSV na pasta grafo em C:
        // using StreamWriter Salvar_Grafo_Gerado = new StreamWriter(Path.Combine(Pasta_Grafo, "Grafo.csv"));
        //using StreamWriter Salvar_Grafo_Gerado2 = new StreamWriter((@"C:\Grafo_Heron_2022dados//Grafo.txt"), true, Encoding.ASCII);

        //salvar as informações do objeto vertice
        // foreach (vertice v in vertices)
        // {
        //    Salvar_Grafo_Gerado.Write("Teste id vertice: ", v.Id_Vertice);
        //    // ...
        // }
        
        class Hero
        {
            public string IdVertice { get; set; }
            public string PesoVertice { get; set; }
            public string BirthDate { get; set; }
        }
    }
    */

    // MELHORIA (2025): Classe profissional para persistência de dados do grafo.
    // Substitui a antiga TESTEsSalvar/Hero. Renomeada para SaveGraphAlgorithm.
    public class SaveGraphAlgorithm
    {
        /// <summary>
        /// Saves the graph to a CSV file in the user's Documents/Grafos folder.
        /// (Salva o grafo em um arquivo CSV na pasta Documentos/Grafos do usuário.)
        /// </summary>
        public void SaveGraphToCsv(Graph graph, string fileName = "GrafoExportado.csv")
        {
            try
            {
                // Caminho dinâmico: Meus Documentos / Grafos
                string docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string folderPath = Path.Combine(docsPath, "Grafos");

                // Cria o diretório se não existir
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, fileName);

                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    // Cabeçalho do CSV
                    writer.WriteLine("IdOrigem;PesoOrigem;CorOrigem;IdDestino;PesoAresta");

                    foreach (var vertex in graph.Vertices)
                    {
                        // Se o vértice não tiver arestas, salvamos apenas ele
                        if (vertex.Adjacencies.Count == 0)
                        {
                            writer.WriteLine($"{vertex.Id};{vertex.Weight};{vertex.Color};;");
                        }
                        else
                        {
                            foreach (var edge in vertex.Adjacencies)
                            {
                                writer.WriteLine($"{vertex.Id};{vertex.Weight};{vertex.Color};{edge.Destination.Id};{edge.Weight}");
                            }
                        }
                    }
                }

                Console.WriteLine($"\nSUCESSO: Grafo salvo em: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nERRO ao salvar o grafo: {ex.Message}");
            }
        }
    }
}
