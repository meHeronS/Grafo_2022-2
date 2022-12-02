using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grafo_2022_2
{
    class Colorir_Grafo
    {
        /*tentativa de colorir o grafo - referenciado código usando C++: https://acervolima.com/minimize-o-custo-para-colorir-todos-os-vertices-de-um-grafico-nao-direcionado-usando-determinada-operacao/ - ideia baseada naquela parada do slide Grafo Adjunto (ou Grafo de Linha) do slide (22-Coloração - Guloso - Welsh-Powell.pptx)*/
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
}

