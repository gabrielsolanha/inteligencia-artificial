using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste
{

    class Program
    {


        static async Task Main(string[] args)
        {
            FiltrarCanhoto();
        }

        public static void FiltrarCanhoto()
        {
            int[] estado = { 3, 3, 0, 0, 0 };
            List<int[]> estadosAnteriores = new List<int[]>() { new int[] { 3, 3, 0, 0, 0 } };
            int[] estadoFinal = { 0, 0, 3, 3 };
            double[] ditanciaAcumulada = { 0 };
            int passos = 0;
            bool indo = false;
            while (EuclideanDistance4D(estado, estadoFinal) != 0)
            {
                passos++;
                indo = !indo;
                // missisonarios Lado01 = estado[0];
                // canibais Lado01 = estado[1];
                // missionarios Lado02 = estado[2];
                // canibais Lado02 = estado[3];
                Console.WriteLine("Procurando passosvalidos para o passo: " + passos + "\n");
                Console.WriteLine("missisonarios Lado01 = " + estado[0] + "\n");
                Console.WriteLine("canibais Lado01 = " + estado[1] + "\n");
                Console.WriteLine("missionarios Lado02 = " + estado[2] + "\n");
                Console.WriteLine("canibais Lado02 = " + estado[3] + "\n");
                List<int[]> passosValidos = ProcuraPassosValidos(estado, indo);
                estado = BuscaProximoEstado(passosValidos, estado, estadosAnteriores, estadoFinal, ditanciaAcumulada);
                estadosAnteriores.Add(estado);


                if (passos == 60) break;

            }

            Console.WriteLine("O numero de passos foi: " + passos + "\n");




        }
        public int Heuristic(int c1, int m1, int c2, int m2)
        {
            // Calcula o número mínimo de viagens necessárias para transportar todas as pessoas para o outro lado do rio
            int trips = (int)Math.Ceiling((c1 + m1) / 2.0) + (int)Math.Ceiling((c2 + m2) / 2.0);

            return trips;
        }

        public static double EuclideanDistance4D(int[] estado, int[] estadoSeundario)
        {
            double deltaX = estado[0] - estadoSeundario[0];
            double deltaY = estado[1] - estadoSeundario[1];
            double deltaZ = estado[2] - estadoSeundario[2];
            double deltaH = estado[3] - estadoSeundario[3];
            return Math.Sqrt(deltaX * deltaX) + Math.Sqrt(deltaY * deltaY) + Math.Sqrt(deltaZ * deltaZ) + Math.Sqrt(deltaH * deltaH);
        }
        public static int[] BuscaProximoEstado(List<int[]> passosValidos, int[] estado, List<int[]> estadosAnteriores, int[] estadoFinal, double[] ditanciaAcumulada)
        {
            int[] estTemp = new int[estado.Length];
            Array.Copy(estado, estTemp, estado.Length);
            passosValidos = passosValidos.Where(x =>
                                                    !estadosAnteriores.Any(c => c.SequenceEqual(x))
                                                    ).ToList();
            double distancia = double.MaxValue;
            foreach (var passo in passosValidos)
            {

                double andar = ditanciaAcumulada[0] + EuclideanDistance4D(estado, passo);
                double distanciaFim = EuclideanDistance4D(passo, estadoFinal) + andar;
                if (distanciaFim < distancia)
                {
                    distancia = distanciaFim;
                    estTemp = passo;
                }
            }
            ditanciaAcumulada[0] = distancia;
            return estTemp;
        }
        public static List<int[]> ProcuraPassosValidos(int[] estado, bool indo)
        {
            List<int[]> retorno = new List<int[]>();
            if (indo)
            {
        //passo sai 2 missionários
        if (estado[0] >= 2)
                {
                    int[] estTemp = new int[estado.Length];
                    Array.Copy(estado, estTemp, estado.Length);
                    estTemp[0] = estTemp[0] - 2;
                    estTemp[2] = estTemp[2] + 2;
                    estTemp[4] = estTemp[4] == 0 ? 1 : 0;
                    if (IsEstadoValid(estTemp)) retorno.Add(estTemp);
                }
        //passo sai 2 canibais
        if (estado[1] >= 2)
                {
                    int[] estTemp = new int[estado.Length];
                    Array.Copy(estado, estTemp, estado.Length);
                    estTemp[1] = estTemp[1] - 2;
                    estTemp[3] = estTemp[3] + 2;
                    estTemp[4] = estTemp[4] == 0 ? 1 : 0;
                    if (IsEstadoValid(estTemp)) retorno.Add(estTemp);
                }
        //passo sai 1 missionário
        if (estado[0] >= 1)
                {
                    int[] estTemp = new int[estado.Length];
                    Array.Copy(estado, estTemp, estado.Length);
                    estTemp[0] = estTemp[0] - 1;
                    estTemp[2] = estTemp[2] + 1;
                    estTemp[4] = estTemp[4] == 0 ? 1 : 0;
                    if (IsEstadoValid(estTemp)) retorno.Add(estTemp);
                }
        //passo sai 1 canibal
        if (estado[1] >= 1)
                {
                    int[] estTemp = new int[estado.Length];
                    Array.Copy(estado, estTemp, estado.Length);
                    estTemp[1] = estTemp[1] - 1;
                    estTemp[3] = estTemp[3] + 1;
                    estTemp[4] = estTemp[4] == 0 ? 1 : 0;
                    if (IsEstadoValid(estTemp)) retorno.Add(estTemp);
                }
        //passo sai 1 de cada
        if (estado[0] >= 1 && estado[1] >= 1)
                {
                    int[] estTemp = new int[estado.Length];
                    Array.Copy(estado, estTemp, estado.Length);
                    estTemp[0] = estTemp[0] - 1;
                    estTemp[1] = estTemp[1] - 1;
                    estTemp[2] = estTemp[2] + 1;
                    estTemp[3] = estTemp[3] + 1;
                    estTemp[4] = estTemp[4] == 0 ? 1 : 0;
                    if (IsEstadoValid(estTemp)) retorno.Add(estTemp);
                }
            }
            else
            {
        //passo sai 2 missionários
        if (estado[2] >= 2)
                {
                    int[] estTemp = new int[estado.Length];
                    Array.Copy(estado, estTemp, estado.Length);
                    estTemp[2] = estTemp[2] - 2;
                    estTemp[0] = estTemp[0] + 2;
                    estTemp[4] = estTemp[4] == 0 ? 1 : 0;
                    if (IsEstadoValid(estTemp)) retorno.Add(estTemp);
                }
        //passo sai 2 canibais
        if (estado[3] >= 2)
                {
                    int[] estTemp = new int[estado.Length];
                    Array.Copy(estado, estTemp, estado.Length);
                    estTemp[3] = estTemp[3] - 2;
                    estTemp[1] = estTemp[1] + 2;
                    estTemp[4] = estTemp[4] == 0 ? 1 : 0;
                    if (IsEstadoValid(estTemp)) retorno.Add(estTemp);
                }
        //passo sai 1 missionário
        if (estado[2] >= 1)
                {
                    int[] estTemp = new int[estado.Length];
                    Array.Copy(estado, estTemp, estado.Length);
                    estTemp[2] = estTemp[2] - 1;
                    estTemp[0] = estTemp[0] + 1;
                    estTemp[4] = estTemp[4] == 0 ? 1 : 0;
                    if (IsEstadoValid(estTemp)) retorno.Add(estTemp);
                }
        //passo sai 1 canibal
        if (estado[3] >= 1)
                {
                    int[] estTemp = new int[estado.Length];
                    Array.Copy(estado, estTemp, estado.Length);
                    estTemp[3] = estTemp[3] - 1;
                    estTemp[1] = estTemp[1] + 1;
                    estTemp[4] = estTemp[4] == 0 ? 1 : 0;
                    if (IsEstadoValid(estTemp)) retorno.Add(estTemp);
                }
        //passo sai 1 de cada
        if (estado[2] >= 1 && estado[3] >= 1)
                {
                    int[] estTemp = new int[estado.Length];
                    Array.Copy(estado, estTemp, estado.Length);
                    estTemp[3] = estTemp[3] - 1;
                    estTemp[2] = estTemp[2] - 1;
                    estTemp[1] = estTemp[1] + 1;
                    estTemp[0] = estTemp[0] + 1;
                    estTemp[4] = estTemp[4] == 0 ? 1 : 0;
                    if (IsEstadoValid(estTemp)) retorno.Add(estTemp);
                }
            }
            return retorno;
        }

        public static bool IsEstadoValid(int[] estado)
        {
            return estado[0] >= 0 && estado[0] <= 3
                && estado[1] >= 0 && estado[1] <= 3
                && estado[2] >= 0 && estado[2] <= 3
                && estado[3] >= 0 && estado[3] <= 3
                && (estado[0] != 0 ? estado[0] >= estado[1] : estado[0] == 0)
                && (estado[2] != 0 ? estado[2] >= estado[3] : estado[2] == 0);
        }
    }
}