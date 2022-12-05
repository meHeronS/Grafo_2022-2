using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

//gerar o grafo em si e suas operações
namespace Grafo_2022_2
{
    //Gerar os vertices do Grafo - objeto
    public class vertice
    {
        //gerar um objeto Vertice com os atributos
        public string Id_Vertice;
        public int Id_Peso = 0;
        public int Id_Cor = 99;

        //gerar uma lista de adjacencias do vertice
        public List<aresta> adjacencias = new List<aresta>();

        //Usar para Djikstra
        public int rotulo = 0; //rotular o vertice
        public bool permanente = false;

        //pega o atributo do valor da aresta e aponta para seu destino
        public class aresta
        {
            //Pega e atribui um valor na aresta
            public string Id_Destino; //identifica a vertice destino - não orientado
            public int Peso_Distancia; //gera um valor entre os vertices - da um peso para a aresta

        }
    }
    public class Grafo
    {
        //Lista teste utilizado para o metodo de teste de funcionamento dos metodos gerados, não utilizada mais
        public List<vertice> Teste_vertices = new List<vertice>();
        //função para realizar testes, total convicção que funciona
        public void preencheGrafo()
        {
            vertice v;
            for (int i = 1; i < 4; i++)
            {
                v = new vertice();
                v.Id_Vertice = i.ToString(); //nomeia
                v.Id_Peso = i; //da peso pro vertice
                Teste_vertices.Add(v); //adiciona na lista
                numVertices++;
                if (i == 2)
                {
                    incluirAresta(1.ToString(), 2.ToString(), 100);
                    adjacentes(1.ToString(), 2.ToString());
                }
            }

        }
        //função para realizar testes
        public void PreencherVertices()//função para realizar testes
        {
            vertice v;
            for (int i = 0; i < 10; i++)
            {
                v = new vertice();
                v.Id_Vertice = i.ToString();
                v.Id_Peso = i;
                Teste_vertices.Add(v);
                vertices.Add(v);
                numVertices++;
            }
        }
        //função para realizar testes
        public void CriarGrafoTeste()
        {
            PreencherVertices();
        }
        //Gerar uma lista com os vertices e seus pesos e preencher a matriz com os vertices.
        public List<vertice> vertices = new List<vertice>();
        //Gerar um contador de vertices no Grafo
        public int numVertices = 0;
        //Exibir o Grafo gerado
        public void exibirGrafo()
        {
            //Valida a quantidade de vertices, inciando em 0.
            Console.WriteLine("Grafo possui {0} vértices. \n\n", numVertices);

            //informa todos os vertices e seus atributos
            foreach (vertice v in vertices)
            {
                //Gerar informação do identificador do vertice, seu peso e sua cor
                Console.Write("Vértice {0}, valor: {1}, cor: {2} é adjacente a: ", v.Id_Vertice, v.Id_Peso, v.Id_Cor);
                Console.WriteLine();
                //Gerar um relatório de quais os vertices que eles alcançam - ao menos deveria
                foreach (vertice.aresta V_aux in v.adjacencias)
                    Console.Write("Vertice adjacente: \n", V_aux.Id_Destino);
            }
        }
        //verificar por nome do vertice
        public vertice existeVertice(string n)
        {
            vertice Aux = null;
            int i;

            i = 0;
            //Corre a lista e verifica nome a nome a existencia do vértice
            while ((i < vertices.Count) && (vertices.ElementAt(i).Id_Vertice != n))

                i++;

            if (i == vertices.Count) //se não tiver retorno, o metodo fica vazio
                return Aux = null;
            else //se houver algum retorno, ele volta o vertice instanciado.
                Aux = vertices.ElementAt(i);
            return Aux;
        }
        //Adicionar vertices
        public void incluirVertice(string _Name, int _Peso)
        {
            //vertice auxiliar
            vertice v = new vertice();
            v.Id_Vertice = _Name;
            v.Id_Peso = _Peso;

            //envia o vertice que está sendo inserido no sistema
            if (existeVertice(v.Id_Vertice) != null)
                Console.WriteLine("Vértice {0} já existe no Grafo.", v.Id_Vertice);
            else //após a validação se não existir ele é inserido no sistema
            {
                //Adiciona o vertice na lista
                vertices.Add(v);
                numVertices++;
            }
        }
        //adicionar arestas não orientadas - ao menos deveria
        public void incluirAresta(string V_a1, string V_a2, int Peso_Distancia)
        {
            vertice VerAux1, VerAux2; //dois vertices auxiliares para instanciar
            vertice.aresta Va; //instaciar a aresta do vertice
            int i;
            //Valida se existe os vertices no grafo
            if (existeVertice(V_a1) != null || existeVertice(V_a2) != null)
                Console.WriteLine("Um dos dois vértices ({0} ou {1}) não existe no grafo.", V_a1, V_a2);
            else
            {
                if (adjacentes(V_a1, V_a2))
                    Console.WriteLine("{0} e {1} já são adjacentes no grafo.", V_a1, V_a2);
                else
                {
                    //setar as vertices auxiliares ponta a ponta na lista de arestas
                    //vertice inicial
                    VerAux1 = existeVertice(V_a1);
                    //vertice final
                    VerAux2 = existeVertice(V_a2);

                    //gerar a aresta e apontar seu Id_Destino
                    Va = new vertice.aresta();
                    Va.Id_Destino = VerAux2.Id_Vertice;
                    i = 0;
                    while ((i < vertices.Count) && (vertices.ElementAt(i).Id_Vertice != VerAux1.Id_Vertice))
                        i++;
                    //Gera aresta do vertice 1 -> 2 e adiciona como objeto
                    vertices.ElementAt(i).adjacencias.Add(Va);
                    //gerar a aresta e apontar seu Id_Destino
                    Va = new vertice.aresta();
                    Va.Id_Destino = VerAux1.Id_Vertice;
                    i = 0;

                    while ((i < vertices.Count) && (vertices.ElementAt(i).Id_Vertice != VerAux2.Id_Vertice))
                        i++;
                    //Gera aresta do vertice 2 -> 1 e adiciona como objeto
                    vertices.ElementAt(i).adjacencias.Add(Va);
                }
            }
        }
        //Valida se já existe a ajacencia entre os vertices
        public bool adjacentes(string V_a1, string V_a2)
        {
            vertice v;
            int i;

            //primeiro validar a existencia dos vertices
            if (existeVertice(V_a1) != null && existeVertice(V_a2) != null)
            {
                i = 0;
                //varrer a lista de vertices e encontrar o vertice inicial
                while (vertices.ElementAt(i).Id_Vertice != V_a1)
                    i++;

                v = vertices.ElementAt(i);

                i = 0;

                //varrer a lista de vertices e encontrar o vertice inicial e validar se encontra alguma adjacencia entre eles
                while ((i < v.adjacencias.Count) && (v.adjacencias.ElementAt(i).Id_Destino != V_a2))
                    i++;

                //se rodar toda a lista e não encotrar
                if (i == v.adjacencias.Count)
                    return (false);
                //se rodar toda a lista e encotrar
                else
                    return (true);
            }
            else
                //primeiro validar a existencia dos vertices e se não existir finaliza
                return (false);
        }
        public int posicaoVertice(string V_a1)
        {
            int i = 0;
            //validar a existencia do vertice e suas adjacencias

            //verifica a existencia de vertices e valida todas suas adjacencias
            while ((existeVertice(V_a1) != null) && (i < existeVertice(V_a1).adjacencias.Count) && (vertices.ElementAt(i).Id_Vertice != existeVertice(V_a1).Id_Vertice))
                i++;

            if (i == vertices.Count)
                return (-1);
            else
                return (i);

        }
        //pega a lista de adjacencia e valida se existe o vertice e gera a sequencia de posição ao vertice 1 -> 2
        public int posicaoAresta(List<vertice.aresta> adjacencias, string V_a1)
        {
            int i = 0;

            //validar a existencia do vertice e suas arestas
            while ((existeVertice(V_a1) != null) && (i < adjacencias.Count) && (adjacencias.ElementAt(i).Id_Destino != V_a1))
                i++;

            if (i == adjacencias.Count)
                return (-1);
            else
                return (i);

        }
        //remover o vertice
        public void removerVertice(string V_a1)
        {
            int i;
            //validar a existencia do vertice no Grafo
            if (existeVertice(V_a1) != null)
                Console.WriteLine("O vértice {0} não existe no Grafo.", V_a1);
            else
            {
                foreach (vertice V_a2 in vertices)
                {
                    //valida as adjacencias de vertices
                    i = posicaoAresta(V_a2.adjacencias, V_a1);
                    if (i != -1)
                        //valida as arestas e remove
                        removerAresta(V_a1, V_a2.Id_Vertice);
                }
                //remove vertices na lista corretamente
                vertices.RemoveAt(posicaoVertice(V_a1));
                numVertices--;
            }
        }
        //Remover aresta
        public void removerAresta(string V_a1, string V_a2)
        {
            //auxiliar os posicionamentos dos vertices e remover as arestas corretamente
            int i, j;

            if (!adjacentes(V_a1, V_a2))
                Console.WriteLine("Os vértices {0} e {1} não são adjacentes.", V_a1, V_a2);
            else
            {
                i = posicaoVertice(V_a1);
                j = posicaoAresta(vertices.ElementAt(i).adjacencias, V_a2);
                vertices.ElementAt(i).adjacencias.RemoveAt(j);

                i = posicaoVertice(V_a2);
                j = posicaoAresta(vertices.ElementAt(i).adjacencias, V_a1);
                vertices.ElementAt(i).adjacencias.RemoveAt(j);
            }
        }
        //Validar se o Grafo é completo
        public bool completo()
        {
            foreach (vertice v in vertices)
                if (v.adjacencias.Count < (numVertices - 1))
                    return (false);

            return (true);
        }
        //Validar se o Grafo é completamente desconexo
        public bool totalmenteDesconexo()
        {
            foreach (vertice v in vertices)
                if (v.adjacencias.Count != 0)
                    return (false);

            return (true);
        }
        //Validar se o Grafo existe
        public bool vazio()
        {
            return (numVertices == 0);
        }
        public void ReiniciarGrafo()
        {
            vertices.Clear();
            numVertices = 0;
        }
    }
}
