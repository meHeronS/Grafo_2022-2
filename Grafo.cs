using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafo_2022_2
{
    public class Grafo
    {
        //Gerar uma lista com os vertices e seus pesos e preencher a matriz com os vertices.
        public List<vertice> vertices = new List<vertice>();
        //Gerar um contador de vertices no Grafo
        public int numVertices = 0;

        //Exibir o Grafo gerado
        public void exibirGrafo()
        {
            //Valida a quantidade de vertices, inciando em 0.
            Console.WriteLine("Grafo possui {0} vértices: ", numVertices);
            foreach (vertice v in vertices)
            {
                //Gerar informação do identificador do vertice, seu peso e sua cor
                Console.Write("Vértice {0}, valor: {1}, cor: {2} é adjacente a: ", v.ID_Vertice, v.id_Peso, v.idCor);

                foreach (vertice.aresta a in v.adjacencias)
                    Console.Write("{0} ", a.destino);

                Console.WriteLine();
            }
        }
        //verificar por nome do vertice
        public vertice existeVertice(string n)
        {
            vertice Aux = null;
            int i;

            i = 0;
            //Corre a lista e verifica nome a nome a existencia do vértice
            while ((i < vertices.Count) && (vertices.ElementAt(i).ID_Vertice != n))

                i++;

            if (i == vertices.Count) //se não tiver retorno, o metodo fica vazio
                return Aux=null;
            else //se houver algum retorno, ele volta o vertice instanciado.
                Aux = vertices.ElementAt(i); 
                return Aux;
        }

        public void incluirVertice(string _name, int n)
        {
            vertice v = new vertice();

            v.ID_Vertice = _name;
            v.id_Peso = n;

            //envia o vertice que está sendo inserido no sistema
            if (existeVertice(v.ID_Vertice) != null)
                Console.WriteLine("Vértice {0} já existe no Grafo.", v.ID_Vertice);
            else //após a validação se não existir ele é inserido no sistema
            {
                vertices.Add(v);
                numVertices++;
            }
        }


        public void incluirAresta(string v1, string v2, int v3, int v4)
        {
            vertice VerAux1, VerAux2; //dois vertices auxiliares para instanciar
            vertice.aresta Va; //instaciar a aresta do vertice
            int i;

            if (existeVertice(v1) == null)
                Console.WriteLine("O vértices ({0}) não existe no Grafo.",v1);               

            else if (existeVertice(v2) == null)
                Console.WriteLine("O vértice ({0}) não existe no Grafo.", v2);
            else
            {
                if (adjacentes(v1, v2))
                    Console.WriteLine("{0} e {1} já são adjacentes no Grafo.", v1, v2);
                else
                {
                    //setar as vertices auxiliares
                    VerAux1 = existeVertice(v1);
                    VerAux2 = existeVertice(v2);

                    //gerar a aresta e apontar seu destino
                    Va = new vertice.aresta();
                    Va.destino = VerAux2.id_Peso;
                    i = 0;

                    while ((i < vertices.Count) && (vertices.ElementAt(i).id_Peso != VerAux1.id_Peso))
                        i++;

                    vertices.ElementAt(i).adjacencias.Add(Va);

                    //gerar a aresta e apontar seu destino

                    Va = new vertice.aresta();
                    Va.destino = VerAux1.id_Peso;
                    i = 0;

                    while ((i < vertices.Count) && (vertices.ElementAt(i).id_Peso != VerAux2.id_Peso))
                        i++;

                    vertices.ElementAt(i).adjacencias.Add(Va);
                }
            }
        }
        //gerar adjacencia
        public bool adjacentes(string v1 ,string v2)
        {
            vertice v;
            int i;
            //gerar uma validação por nome
            if (existeVertice(v1).ID_Vertice!=null && existeVertice(v2).ID_Vertice!=null)
            {
                i = 0;

                while (vertices.ElementAt(i).ID_Vertice != v1)
                    i++;

                v = vertices.ElementAt(i);

                i = 0;

                while ((i < v.adjacencias.Count) && (v.adjacencias.ElementAt(i).destino != v2))
                    i++;

                if (i == v.adjacencias.Count)
                    return (false);
                else
                    return (true);
            }
            else
                return (false);
        }

        public int posicaoVertice(int n)
        {
            int i = 0;

            while ((i < vertices.Count) && (vertices.ElementAt(i).id_Peso != n))
                i++;

            if (i == vertices.Count)
                return (-1);
            else
                return (i);
        }

        public int posicaoAresta(List<vertice.aresta> adjacencias, int n)
        {
            int i = 0;

            while ((i < adjacencias.Count) && (adjacencias.ElementAt(i).destino != n))
                i++;

            if (i == adjacencias.Count)
                return (-1);
            else
                return (i);
        }

        public void removerVertice(vertice n)
        {
            int i;

            if (!existeVertice(n.))
                Console.WriteLine("O vértice {0} não existe no Grafo.", n);
            else
            {
                foreach (vertice v in vertices)
                {
                    i = posicaoAresta(v.adjacencias, n);

                    if (i != -1)
                        removerAresta(n, v.id_Peso);
                }

                vertices.RemoveAt(posicaoVertice(n));
                numVertices--;
            }
        }

        public void removerAresta(int vi, int vf)
        {
            int i, j;

            if (!adjacentes(vi, vf))
                Console.WriteLine("Os vértices {0} e {1} não são adjacentes.", vi, vf);
            else
            {
                i = posicaoVertice(vi);
                j = posicaoAresta(vertices.ElementAt(i).adjacencias, vf);
                vertices.ElementAt(i).adjacencias.RemoveAt(j);

                i = posicaoVertice(vf);
                j = posicaoAresta(vertices.ElementAt(i).adjacencias, vi);
                vertices.ElementAt(i).adjacencias.RemoveAt(j);
            }
        }

        public bool completo()
        {
            foreach (vertice v in vertices)
                if (v.adjacencias.Count < (numVertices - 1))
                    return (false);

            return (true);
        }

        public bool totalmenteDesconexo()
        {
            foreach (vertice v in vertices)
                if (v.adjacencias.Count != 0)
                    return (false);

            return (true);
        }

        public bool vazio()
        {
            return (numVertices == 0);
        }

        public void reiniciarGrafo()
        {
            vertices.Clear();
            numVertices = 0;
        }

        public Grafo gerarComplemento()
        {
            int i, j;
            Grafo GC = new Grafo();

            foreach (vertice v in vertices)
                GC.incluirVertice(v.id_Peso);

            for (i = 0; i < numVertices; i++)
                for (j = i + 1; j < numVertices; j++)
                {
                    if (!adjacentes(vertices.ElementAt(i).id_Peso, vertices.ElementAt(j).id_Peso))
                        GC.incluirAresta(vertices.ElementAt(i).id_Peso, vertices.ElementAt(j).id_Peso);
                }

            return (GC);
        }

        public void colorirGrafo()
        {
            int[] cores = new int[vertices.Count];
            List<vertice> adjacentes;
            int aux = 0;
            int opc;

            for (int i = 0; i < cores.Length; i++)
            {
                cores[i] = i + 1;
            }

            Console.WriteLine("Digite a opção de coloração: \n-pior solução = 1\n-greedy = 2");
            opc = int.Parse(Console.ReadLine());
            switch (opc)
            {
                case 1://colore todos os vertices de uma cor
                    foreach (vertice v in vertices)
                    {
                        v.idCor = cores[aux];
                        aux++;
                    }
                    break;
                case 2://algoritmo greedy, atribui em sequencia as cores verificando apenas se ela não está sendo utilizada por um adjacente
                    vertices[0].idCor = cores[0];
                    for (int j = 1; j < vertices.Count; j++)//para cada vertice
                    {
                        adjacentes = new List<vertice>();
                        foreach (vertice.aresta a in vertices[j].adjacencias)//para cada adjacente ao vertice j
                        {
                            for (int k = 0; k < vertices.Count; k++)//para cada vertice na lista de vertices
                            {
                                if (a.destino == vertices[k].id_Peso)//verifica se o vertice na lista é adj ao vertice j
                                {
                                    adjacentes.Add(vertices[k]); //adiciona a lista os vertices adj ao j
                                }
                            }
                        }

                        Console.WriteLine("vertice {0} tem {1} adjacentes", vertices[j].id_Peso, adjacentes.Count);

                        for (int i = 0; i < cores.Length; i++)//para cada cor
                        {
                            if (vertices[j].idCor == 0)
                            {
                                int contador = 0;
                                for (int k = 0; k < adjacentes.Count; k++)//para cada adjacente de j
                                {
                                    if (adjacentes[k].idCor != cores[i])//se a cor não esta sendo utilizada por um adjacente
                                    {
                                        contador++;
                                    }
                                }
                                if (contador == adjacentes.Count)//se a cor não está sendo utilizada por nenhum dos adjacentes
                                {
                                    Console.WriteLine("colorindo {0} de {1}", vertices[j].id_Peso, cores[i]);
                                    vertices[j].idCor = cores[i];//colore o vertice com aquela cor;
                                }
                            }
                        }
                    }
                    break;
            }

        }

        public void preencheGrafo()//função para realizar testes
        {
            vertice v;
            for (int i = 1; i < 4; i++)
            {
                v = new vertice();
                v.ID_Vertice = i.ToString();
                v.id_Peso = i;
                vertices.Add(v);
                numVertices++;
            }
        }
    }
    public class vertice
    {

        //gerar um objeto Vertice com os atributos
        public string ID_Vertice;
        public int id_Peso;
        public int idCor = 0;
        public List<aresta> adjacencias = new List<aresta>();
        //pega o atributo do valor da aresta
        public class aresta
        {
            public int destino;
        }
    }
    
}
