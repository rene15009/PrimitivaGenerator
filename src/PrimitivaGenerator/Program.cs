using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimitivaGenerator
{
    class Program
    {

        private static Dictionary<int, int> Numeros = new Dictionary<int, int>();
        private static Dictionary<int, List<int>> Combinaciones = new Dictionary<int, List<int>>();


        static void Main(string[] args)
        {
            //almacenará los posibles números para ver si salió en la combinacion o no. Como value se guardará el número de columna
            Numeros = Enumerable.Range(1, 49).ToDictionary(e => e, e => 0);
            //almacenará las combinaciones de cada columna
            Combinaciones = Enumerable.Range(1, 8).ToDictionary(c => c, c => new List<int>(6));

            char repetir = 'N';
            do
            {
                IniciaVectores();
                GenerarSecuencia();
                MostrarSecuencia();

                Console.WriteLine("Repetir (S\\N)?");
                repetir = Console.ReadKey().KeyChar;

            } while (repetir == 'S' || 's' == repetir);

        }

        /// <summary>
        /// resetea los vectores para poder generar otra combinación
        /// </summary>
        private static void IniciaVectores()
        {
            Enumerable.Range(1, 49).ForEach((k, _) => Numeros[k] = 0);
            Enumerable.Range(1, 8).ForEach((c, _) => Combinaciones[c].Clear());
        }

        private static void GenerarSecuencia()
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            int encontrados = 0;
            for (int i = 1; i < 9; i++) //columnas
            {

                while (encontrados < 6)
                {
                    var candidato = rnd.Next(1, 50);

                    if (Numeros[candidato] != 0) continue;

                    Numeros[candidato] = i; //columna
                    Combinaciones[i].Add(candidato);
                    encontrados++;
                }

                encontrados = 0;
            }
        }

        private static void MostrarSecuencia()
        {
            Console.Clear();

            Console.WriteLine(" Combinaciones Ganadoras ");
            Console.WriteLine("=========================");

            for (int i = 1; i < 9; i++) //columnas
            {
                Combinaciones[i].Sort();
                Console.WriteLine($"{i}) {(string.Join(" ", Combinaciones[i]))}");
            }

            Console.WriteLine();

            var noUsado = Numeros.Where(w => w.Value == 0).ToList();

            Console.WriteLine($"Numeros no utilizados {noUsado.Count()}");
            noUsado.ForEach((s, _) => Console.WriteLine(s.Key.ToString()));

            Console.WriteLine();
        }
    }
}
