using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Grafo_2022_2
{
    class NAIVE : Grafo
    /*
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
         */
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
}    


