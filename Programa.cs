using System;

// MELHORIA (2025): Bibliotecas desnecessárias foram removidas para um código mais limpo.
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using System.IO;
// using System.Collections;
// using System.Diagnostics;
// using System.Windows.Input;

namespace Grafo_2022_2
{
    /*
    // CÓDIGO LEGADO (2022):
    // O código original era funcional, mas misturava responsabilidades, usava `int.Parse` de forma insegura,
    // e chamava métodos e classes que foram refatorados por questões de performance e clareza.
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
                            Console.Clear();
                            // if (G.euleriano())
                            //     Console.Write("O grafo é euleriano.");
                            // else
                            //     Console.WriteLine("O grafo não é euleriano.");
                            // Console.ReadKey();
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
                                        G.ReiniciarGrafo();
                                        Console.WriteLine("Aperte Enter para sair");
                                        break;
                                    case 2:
                                        G.ReiniciarGrafo();
                                        Console.WriteLine("Aperte Enter para sair");
                                        break;
                                }
                            } while (Console.ReadKey().Key != ConsoleKey.Enter);
                            break;
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
                        case 14: //metodo para tentar usar NAIVE- TARJAN
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
    */

    // MELHORIA (2025): Classe 'Programa' refatorada para usar as novas classes de serviço,
    // seguir o princípio da responsabilidade única e aplicar boas práticas como tratamento de erros.
    class Programa
    {
        // Instância do Grafo Principal (Nova Classe Graph)
        static Graph grafo = new Graph();

        // Instâncias dos Algoritmos (Princípio da Responsabilidade Única)
        static DijkstraAlgorithm dijkstra = new DijkstraAlgorithm();
        static GraphColoring coloring = new GraphColoring();
        static TarjanCycleDetection tarjan = new TarjanCycleDetection();
        static NaiveCycleDetection naive = new NaiveCycleDetection();
        static SaveGraphAlgorithm saver = new SaveGraphAlgorithm();

        static void Main(string[] args)
        {
            int opcao;
            do
            {
                ExibirMenu();

                // MELHORIA: Uso de `int.TryParse` para evitar que o programa quebre
                // se o usuário digitar um texto em vez de um número.
                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    opcao = -1; // Força a entrada no 'default' do switch.
                }

                Console.WriteLine();

                switch (opcao)
                {
                    case 0:
                        grafo.GenerateTestGraph();
                        break;

                    case 1:
                        Console.Write("ID do Vértice: ");
                        string idV = Console.ReadLine();
                        Console.Write("Peso do Vértice: ");
                        if (int.TryParse(Console.ReadLine(), out int pesoV))
                            grafo.AddVertex(idV, pesoV);
                        else
                            Console.WriteLine("ERRO: Peso inválido!");
                        break;
                    case 2:
                        Console.Write("ID Origem: ");
                        string idO = Console.ReadLine();
                        Console.Write("ID Destino: ");
                        string idD = Console.ReadLine();
                        Console.Write("Peso da Aresta: ");
                        if (int.TryParse(Console.ReadLine(), out int pesoA))
                            grafo.AddEdge(idO, idD, pesoA);
                        else
                            Console.WriteLine("ERRO: Peso inválido!");
                        break;
                    case 3:
                        Console.Write("ID do Vértice a remover: ");
                        string idRem = Console.ReadLine();
                        grafo.RemoveVertex(idRem);
                        break;
                    case 4:
                        Console.Write("ID Origem da aresta a remover: ");
                        string idO_rem = Console.ReadLine();
                        Console.Write("ID Destino da aresta a remover: ");
                        string idD_rem = Console.ReadLine();
                        grafo.RemoveEdge(idO_rem, idD_rem);
                        break;
                    case 5:
                        grafo.DisplayGraph();
                        break;
                    case 6:
                        Console.Write("ID Origem: ");
                        string idO_adj = Console.ReadLine();
                        Console.Write("ID Destino: ");
                        string idD_adj = Console.ReadLine();
                        bool adj = grafo.AreAdjacent(idO_adj, idD_adj);
                        Console.WriteLine(adj ? "Os vértices são adjacentes." : "Os vértices NÃO são adjacentes.");
                        break;
                    case 7:
                        Console.WriteLine(grafo.IsComplete() ? "O grafo é COMPLETO." : "O grafo NÃO é completo.");
                        break;
                    case 8:
                        Console.WriteLine(grafo.IsTotallyDisconnected() ? "O grafo é TOTALMENTE DESCONEXO." : "O grafo possui arestas.");
                        break;
                    case 9:
                        bool isEulerian = grafo.IsEulerian();
                        Console.WriteLine(isEulerian ? "O grafo É EULERIANO (possui um ciclo euleriano)." : "O grafo NÃO É EULERIANO.");
                        break;
                    case 10:
                        Console.WriteLine("Você deseja salvar o grafo antes de reiniciar? \n1 - Sim\n2 - Não");
                        if (int.TryParse(Console.ReadLine(), out int saveOption) && saveOption == 1)
                        {
                            Console.Write("Nome do arquivo para salvar (ex: MeuGrafo.csv): ");
                            string resetSaveFilename = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(resetSaveFilename)) resetSaveFilename = "GrafoBackup.csv";
                            saver.SaveGraphToCsv(grafo, resetSaveFilename);
                        }
                        grafo.ResetGraph();
                        Console.WriteLine("Grafo reiniciado com sucesso.");
                        break;
                    case 11:
                        coloring.ColorGraph(grafo);
                        break;
                    case 12:
                        Console.Write("ID do Vértice de Origem: ");
                        string idStart = Console.ReadLine();
                        Console.Write("ID do Vértice de Destino: ");
                        string idEnd = Console.ReadLine();
                        dijkstra.Execute(grafo, idStart, idEnd);
                        break;
                    case 13:
                        Console.WriteLine("Escolha o método de detecção:");
                        Console.WriteLine("1 - Tarjan (Componentes Fortemente Conexos - Otimizado)");
                        Console.WriteLine("2 - Naive (Força Bruta/DFS Simples)");
                        
                        if (int.TryParse(Console.ReadLine(), out int cycleOp))
                        {
                            if (cycleOp == 1) tarjan.DetectCycles(grafo);
                            else if (cycleOp == 2) naive.Execute(grafo);
                            else Console.WriteLine("Opção inválida.");
                        }
                        else
                            Console.WriteLine("Opção inválida.");
                        break;
                    case 14:
                        Console.Write("Nome do arquivo para salvar (ex: MeuGrafo.csv): ");
                        string filename = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(filename)) filename = "GrafoExportado.csv";
                        saver.SaveGraphToCsv(grafo, filename);
                        break;
                    case 15:
                        Console.WriteLine("Saindo do programa...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;
                }

                if (opcao != 15)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcao != 15);
        }

        /// <summary>
        /// Exibe o menu de opções para o usuário.
        /// (Anteriormente: menu())
        /// </summary>
        static void ExibirMenu()
        {
            Console.Clear();
            Console.WriteLine("=== GERENCIADOR DE GRAFOS (REFATORADO 2025) ===");
            Console.WriteLine("0 - Gerar Grafo Aleatório (Teste)");
            Console.WriteLine("1 - Adicionar Vértice");
            Console.WriteLine("2 - Adicionar Aresta");
            Console.WriteLine("3 - Remover Vértice");
            Console.WriteLine("4 - Remover Aresta");
            Console.WriteLine("5 - Exibir Grafo");
            Console.WriteLine("6 - Verificar Adjacência");
            Console.WriteLine("7 - Verificar se é Completo");
            Console.WriteLine("8 - Verificar se é Totalmente Desconexo");
            Console.WriteLine("9 - Verificar se é Euleriano");
            Console.WriteLine("10 - Reiniciar Grafo");
            Console.WriteLine("11 - Colorir Grafo");
            Console.WriteLine("12 - Algoritmo de Dijkstra (Menor Caminho)");
            Console.WriteLine("13 - Detectar Ciclos (Tarjan)");
            Console.WriteLine("14 - Salvar Grafo (CSV)");
            Console.WriteLine("15 - Sair");
            Console.Write("\nOpção: ");
        }
    }
}
