using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace Grafo_2022_2
{
    class Programa
    {
        //Menu para chamar os metodos a serem executados pelo programa
        static int menu()
        {
            Console.Clear();
            Console.WriteLine("\n0 - preencher grafo aleatoriamente para testes"); //funcionava
            Console.WriteLine("1 - Exibir grafo");
            Console.WriteLine("2 - Incluir vértice");
            Console.WriteLine("3 - Incluir aresta");
            Console.WriteLine("4 - Remover vértice");
            Console.WriteLine("5 - Remover aresta");
            Console.WriteLine("6 - Testar adjacência");
            Console.WriteLine("7 - Verificar completo");
            Console.WriteLine("8 - Verificar totalmente desconexo");
            Console.WriteLine("9 - Verificar euleriano");
            Console.WriteLine("10 - Reiniciar grafo");
            Console.WriteLine("11 - Colorir grafo");
            Console.WriteLine("12 - dijkstra");
            Console.WriteLine("13 - Salvar o Grafo Gerado");
            Console.WriteLine("14 - Sair");
            Console.Write("\nOpção Desejada: ");
            
            return (int.Parse(Console.ReadLine()));            
        }
        //Inicio programa Main
        static void Main(string[] args)
        {
            //Principal para o programa
            {
                //string de id dos vertices.
                string Id_Name1, Id_Name2;
                //ints para chamar setar peso para as arestas e vertices 
                int Menu, V1;
                Grafo G = new Grafo();
                Algoritmo_1 A1 = new Algoritmo_1();
                Colorir_Grafo C = new Colorir_Grafo();
                NAIVE nAive = new NAIVE();
                SalvaGrafo salvaGrafo = new SalvaGrafo();
                do
                {
                    Menu = menu();

                    switch (Menu)
                    { 
                        //tentativa de automatizar um grafo para teste
                        case 0:
                            Console.Clear();
                            G.CriarGrafoTeste();
                            Console.WriteLine("10 Vertices para teste criados");
                            Console.WriteLine("Aperte qualquer tecla para continuar");
                            Console.ReadKey();
                            break;

                        case 1: // exibir grafo gerado
                            Console.Clear();
                            G.exibirGrafo();
                            Console.ReadKey();
                            break;

                        case 2: // incluir vertice - nome e peso
                            Console.Clear();
                            Console.Write("Informe o identificador do vértice: ");
                            Id_Name1 = Console.ReadLine();
                            Console.WriteLine("Informe seu peso");
                            V1 = int.Parse(Console.ReadLine());
                            G.incluirVertice(Id_Name1, V1);
                            Console.ReadKey();
                            //G.preencheGrafo();
                            break;

                        case 3: // incluir aresta a partir do vertice informado
                            Console.Clear();
                            Console.Write("Informe vértice de origem: ");
                            Id_Name1 = Console.ReadLine();
                            Console.Write("Informe vértice de destino: ");
                            Id_Name2 = Console.ReadLine();
                            Console.WriteLine("Informe o peso da aresta a ser inserida");
                            V1 = int.Parse(Console.ReadLine());
                            //Sempre validar se existe adjacencia já entre os vertices
                            if (G.adjacentes(Id_Name1, Id_Name2))
                                Console.WriteLine("Os vértices {0} e {1} já são adjacentes.", Id_Name1, Id_Name2);
                            else
                                //se não houver inclui as arestas
                                G.incluirAresta(Id_Name1, Id_Name2,V1);
                            Console.ReadKey();
                            break;

                        case 4: // remover vertice
                            Console.Clear();
                            Console.Write("Informe o identificador do vértice: ");
                            Id_Name1 = Console.ReadLine();
                            G.removerVertice(Id_Name1);
                            Console.ReadKey();
                            break;

                        case 5: // remover aresta
                            Console.Clear();
                            Console.Write("Informe vértice de origem: ");
                            Id_Name1 = Console.ReadLine();
                            Console.Write("Informe vértice de destino: ");
                            Id_Name2 = Console.ReadLine();
                            G.removerAresta(Id_Name1, Id_Name2);
                            Console.ReadKey();
                            break;

                        case 6: // testar adjacência
                            Console.Clear();
                            Console.Write("Informe vértice de origem: ");
                            Id_Name1 = Console.ReadLine();
                            Console.Write("Informe vértice de destino: ");
                            Id_Name2 = Console.ReadLine();
                            if (G.adjacentes(Id_Name1, Id_Name2))
                                Console.WriteLine("Os vértices {0} e {1} são adjacentes.", Id_Name1, Id_Name2);
                            else
                                Console.WriteLine("Os vértices {0} e {1} não são adjacentes.", Id_Name1, Id_Name2);
                            Console.ReadKey();
                            break;

                        case 7: // verificar grafo completo
                            Console.Clear();
                            if (G.vazio())
                                Console.WriteLine("Grafo vazio.");
                            else if (G.completo())
                                Console.Write("O grafo é completo.");
                            else
                                Console.WriteLine("O grafo não é completo.");
                            Console.ReadKey();
                            break;

                        case 8: // verificar grafo desconexo
                            Console.Clear();
                            if (G.vazio())
                                Console.WriteLine("Grafo vazio.");
                            else if (G.totalmenteDesconexo())
                                Console.Write("O grafo é totalmente desconexo.");
                            else
                                Console.WriteLine("O grafo não é totalmente desconexo.");
                            Console.ReadKey();
                            break;

                        case 9: // verificar euleriano tentar
                            Console.Clear();/*
                            if (G.euleriano())
                                Console.Write("O grafo é euleriano.");
                            else
                                Console.WriteLine("O grafo não é euleriano.");
                            Console.ReadKey();*/
                            break;

                        case 10: // reiniciar grafo
                            Console.WriteLine("Você deseja salvar o grafo? \n1 - Sim\n2 - Não");
                            int Opcao = int.Parse(Console.ReadLine());
                            do
                            {
                                switch (Opcao)
                                {
                                    case 1:
                                        salvaGrafo.SalvarGrafo(G);
                                        break;
                                    case 2:
                                        G.ReiniciarGrafo();
                                        break;
                                }
                            } while (true);

                        case 11: //colorir os vertices
                            Console.Clear();
                            C.colorirGrafo(G);
                            Console.WriteLine("Grafo colorido");
                            Console.ReadKey();
                            break;

                        case 12: // algoritmo de Dijkstra - tentativa - ref: https://eximia.co/o-algoritmo-de-dijkstra-em-c/
                            Console.Clear();
                            Console.WriteLine("Algoritmo de Djkistra");

                            Console.Write("Informe vértice de origem: ");
                            Id_Name1 = Console.ReadLine();
                            Console.Write("Informe vértice de destino: ");
                            Id_Name2 = Console.ReadLine();
                            //passar o grafo e fazer um caminho de Djikstra entre os vertices
                            A1.A_dijkstra(G, Id_Name1, Id_Name2);
                            Console.ReadKey();
                            break;

                        case 13: //metodo de salvar arquivo com todos os dados do grafo. sobrescreve o arquivo já gerado. não consegui gerar versões: (1)(2)
                            Console.Clear();
                            salvaGrafo.SalvarGrafo(G);
                            //SalvarGrafo();
                            Console.ReadKey();
                            break;
                        case 14: //metodo para tentar usar NAIVA- TARJAN
// tests simple model presented on https://en.wikipedia.org/wiki/Tarjan%27s_strongly_connected_components_algorithm
                            Console.Clear();
                            Console.WriteLine("Algoritmo de Tarjan");
                            //recebe a lista de vertices para testar, entendi que é assim                            
                            var lista_ciclo = nAive.DetectaCiclo(G.vertices);                            
                            Console.ReadKey();
                            break;
                    }
                } while (Menu != 15);
            }
        }
        
    }
}
