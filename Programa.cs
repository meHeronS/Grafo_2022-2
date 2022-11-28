using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafo_2022_2
{
    class Programa
    {
        //Menu para chamar os metodos a serem executados pelo programa
        static int menu()
        {
            Console.Clear();
            Console.WriteLine("1 - Exibir grafo");
            Console.WriteLine("2 - Incluir vértice");
            Console.WriteLine("3 - Incluir aresta");
            Console.WriteLine("4 - Remover vértice");
            Console.WriteLine("5 - Remover aresta");
            Console.WriteLine("6 - Testar adjacência");
            Console.WriteLine("7 - Verificar completo");
            Console.WriteLine("8 - Verificar totalmente desconexo");
            Console.WriteLine("9 - Verificar euleriano");
            Console.WriteLine("10 - Gerar complemento");
            Console.WriteLine("11 - Reiniciar grafo");
            Console.WriteLine("12 - Mostrar graus");
            Console.WriteLine("13 - Contar vértices isolados");
            Console.WriteLine("14 - Colorir grafo");
            Console.WriteLine("15 - Aloritmo de Dijkstra");
            Console.WriteLine("16 - Salvar o Grafo Gerado");
            Console.WriteLine("17 - Sair");
            Console.Write("Opção: ");

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
                int Menu, V1, V2;
                Grafo G = new Grafo(), GC;
                Algoritmo_1 A1 = new Algoritmo_1();

                do
                {
                    Menu = menu();

                    switch (Menu)
                    {
                        case 1: // exibir grafo
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
                            Console.WriteLine("Informe o seu peso");
                            V1 = int.Parse(Console.ReadLine());
                            Console.Write("Informe vértice de destino: ");
                            Id_Name2 = Console.ReadLine();
                            Console.WriteLine("Informe o seu peso");
                            V2 = int.Parse(Console.ReadLine());
                            if (G.adjacentes(Id_Name1, Id_Name2, V1, V2))
                                Console.WriteLine("Os vértices {0} e {1} já são adjacentes.", Id_Name1, Id_Name2);
                            else
                                G.incluirAresta(Id_Name1, Id_Name2);
                            Console.ReadKey();
                            break;

                        case 4: // remover vertice
                            Console.Clear();
                            Console.Write("Informe o identificador do vértice: ");
                            V1 = Console.ReadLine();
                            G.removerVertice(V1);
                            Console.ReadKey();
                            break;

                        case 5: // remover aresta
                            Console.Clear();
                            Console.Write("Informe vértice de origem: ");
                            V1 = int.Parse(Console.ReadLine());
                            Console.Write("Informe vértice de destino: ");
                            V2 = int.Parse(Console.ReadLine());
                            G.removerAresta(V1, V2);
                            Console.ReadKey();
                            break;

                        case 6: // testar adjacência
                            Console.Clear();
                            Console.Write("Informe vértice de origem: ");
                            V1 = int.Parse(Console.ReadLine());
                            Console.Write("Informe vértice de destino: ");
                            V2 = int.Parse(Console.ReadLine());
                            if (G.adjacentes(V1, V2))
                                Console.WriteLine("Os vértices {0} e {1} são adjacentes.", V1, V2);
                            else
                                Console.WriteLine("Os vértices {0} e {1} não são adjacentes.", V1, V2);
                            Console.ReadKey();
                            break;

                        case 7: // verificar completo
                            Console.Clear();
                            if (G.vazio())
                                Console.WriteLine("Grafo vazio.");
                            else if (G.completo())
                                Console.Write("O grafo é completo.");
                            else
                                Console.WriteLine("O grafo não é completo.");
                            Console.ReadKey();
                            break;

                        case 8: // verificar totalmente desconexo
                            Console.Clear();
                            if (G.vazio())
                                Console.WriteLine("Grafo vazio.");
                            else if (G.totalmenteDesconexo())
                                Console.Write("O grafo é totalmente desconexo.");
                            else
                                Console.WriteLine("O grafo não é totalmente desconexo.");
                            Console.ReadKey();
                            break;

                        case 9: // verificar euleriano
                            Console.Clear();
                            /*if (G.euleriano())
                                Console.Write("O grafo é euleriano.");
                            else
                                Console.WriteLine("O grafo não é euleriano.");*/
                            Console.ReadKey();
                            break;

                        case 10: // gerar complemento
                            Console.Clear();
                            GC = G.gerarComplemento();
                            GC.exibirGrafo();
                            Console.ReadKey();
                            break;

                        case 11: // reiniciar grafo
                            Console.Clear();
                            G.reiniciarGrafo();
                            Console.ReadKey();
                            break;

                        case 12: // mostrar graus
                            Console.Clear();
                            // mostrarGraus
                            Console.ReadKey();
                            break;

                        case 13: // contar vértices isolados
                            Console.Clear();
                            // contar vertices isolados
                            Console.ReadKey();
                            break;
                        case 14: //colorir os vertices
                            Console.Clear();
                            G.colorirGrafo();
                            Console.WriteLine("Grafo colorido");
                            Console.ReadKey();
                            break;
                        case 15: // algoritmo de Dijkstra - puxar em outra 
                            Console.Clear();
                            A1.A_dijkstra(G);
                            Console.WriteLine("Algoritmo de Djkistra");
                            Console.ReadKey();
                            break;
                        case 16: //metodo de salvar arquivo
                            Console.Clear();
                            //SalvarGrafo();
                            Console.ReadKey();
                            break;
                    }
                } while (Menu != 15);
            }
        }
    }
}
