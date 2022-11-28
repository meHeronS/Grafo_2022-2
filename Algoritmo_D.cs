using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafo_2022_2
{
    public class Algoritmo_1
    {
        public void A_dijkstra(Grafo G)
        {
            List<vertice> caminhoAtual = new List<vertice>(), caminho = new List<vertice>();
            /*vertice verticedestino = new vertice();
            vertice verticeatual = new vertice();*/
            int atual = -1;
            foreach (vertice v in vertices)//setando os vertices de acordo com seu tipo
            {
                if (v.id == origem)
                {
                    v.permanente = true;
                    v.rotulo = 0;
                    atual = v.id;
                    caminhoAtual.Add(v);
                }
                else
                {
                    v.permanente = false;
                    v.rotulo = int.MaxValue;
                }
            }


            foreach (vertice v in vertices)
            {
                rotular(v);
            }

            foreach (vertice v in vertices)
            {
                if (v.id == origem)
                {
                    Console.WriteLine("id da origem: {0}\nrotulo da origem: {1}", v.id, v.rotulo);
                }
                else if (v.id == destino)
                {
                    Console.WriteLine("d do destino: {0}\nrotulo do destino: {1}", v.id, v.rotulo);
                }
            }
        }
    }
}
