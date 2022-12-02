using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grafo_2022_2
{
    public class Algoritmo_1
    {
        //tentativa de fazer uma validação de Dijkstra
        //ref: https://www.revista-programar.info/artigos/algoritmo-de-dijkstra/
        public void A_dijkstra(Grafo G, string Id_Name1, string Id_Name2)
        {            
            List<vertice> caminhoAtual = new List<vertice>(), caminho = new List<vertice>();
            /*vertice verticedestino = new vertice();
            vertice verticeatual = new vertice();*/
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
}
