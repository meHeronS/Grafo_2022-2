using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Grafo_2022_2
{
    class SalvaGrafo : Grafo
    {
        //Salvar todo o grafo gerado - ref: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/how-to-compute-column-values-in-a-csv-text-file-linq / https://www.youtube.com/watch?v=Jxf9Klwdefc

        //tentativa com o modelo HERO
        /*
         public class Hero
{
    public Hero(string name, string phone, DateTime birthDate)
    {
        Name = name;
        Phone = phone;
        BirthDate = birthDate;
    }

    public string Name { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    
    public static implicit operator string(Hero hero) 
        => $"{hero.Name},{hero.Phone},{hero.BirthDate.ToString("yyyy-MM-dd")}";

    public static implicit operator Hero(string line)
    {
        var data = line.Split(",");
        return new Hero(
            data[0], 
            data[1], 
            data[2].ToDateTime());
    }
}
         */
        public string IdVertice { get; set; }
        public int PesoVertice { get; set; }
        public int CorVertice { get; set; }
        public string AdjacenciaVertice { get; set; }
        public string VerticeDestino { get; set; }
        public int PesoDestino { get; set; }


        //um operador para transformar o vertice em uma string - deveria ao menos
        public static implicit operator string(SalvaGrafo salva)
            => $"{salva.IdVertice},{salva.PesoVertice},{salva.CorVertice},{salva.AdjacenciaVertice},{salva.VerticeDestino},{salva.PesoVertice}";


        public void SalvarGrafo(Grafo _grafo)
        {
            //criar uma pasta no c:
            string Pasta_Grafo = @"C:\Grafo_Heron_2022\dados/";

            if (!Directory.Exists(Pasta_Grafo))
                Directory.CreateDirectory(Pasta_Grafo);

            //roda a lista de vertices
            /* foreach (var V_aux in _grafo.vertices)
             {
                 Console.WriteLine(V_aux);
             }*/
            //transformar os vertices para string
            //gera a data para ser gravada em linhas
            //pega a informação (objeto) a ser tranformada
            //pega a lista de _grafo.vertice e transforma suas informações
            //após selecionar pega o objeto e tranforma para string
            //IEnumerable<Grafo> data = _grafo.vertices.Any(x => (string) x.ToString());

            // string data = strings.

            //File.WriteAllTextAsync(Path.Combine(Pasta_Grafo, "Grafo.csv", data.ToString());


            //criar uma pasta no c:
            //string Pasta_Grafo = @"C:\Grafo_Heron_2022\dados/";

            if (!Directory.Exists(Pasta_Grafo))
                Directory.CreateDirectory(Pasta_Grafo);
            //string[] teste = File.ReadAllLines(Path.Combine(Pasta_Grafo, "Grafo.csv"));
            //gerar o arquivo CSV na pasta grafo em C:
            using StreamWriter Salvar_Grafo_Gerado = new StreamWriter(Path.Combine(Pasta_Grafo, "Grafo.csv"));
            //using StreamWriter Salvar_Grafo_Gerado2 = new StreamWriter((@"C:\Grafo_Heron_2022dados//Grafo.txt"), true, Encoding.ASCII);

            //salvar as informações do objeto vertice
            foreach (vertice v in vertices)
            {
                Salvar_Grafo_Gerado.Write("Teste id vertice: ", v.Id_Vertice);
                Salvar_Grafo_Gerado.WriteLine("Teste peso vertice: ", v.Id_Peso);
                Salvar_Grafo_Gerado.WriteLine("Teste cor vertice: ", v.Id_Cor); //csv
                foreach (vertice.aresta A_Aux in v.adjacencias)
                {
                    Salvar_Grafo_Gerado.WriteLine("Teste id vertice destino: ", A_Aux.Id_Destino);
                    Salvar_Grafo_Gerado.WriteLine("Teste peso vertice destino: ", A_Aux.Peso_Distancia);
                }
                // Salvar_Grafo_Gerado2.Equals(v); //txt
                // Salvar_Grafo_Gerado.Close();
                //salva as informações de arestas
            }

            Console.WriteLine("Arquivo Salvo");
            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();



        }
    }
}
