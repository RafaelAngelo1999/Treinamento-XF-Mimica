using System;
using System.Collections.Generic;
using System.Text;
using Mimica.Model;

namespace Mimica.Armazenamento
{
    public class Armazenamento
    {
        public static Jogo Jogo { get; set; }
        public static short RodadaAtual { get; set; }

        public static string[][] Palavras =
        {
            //Fac 1
            new string [] {"Olho","Lingua","Chinelo","Penalti","Bola","Ping-Pong",},
            //Med 3
            new string [] {"Carpinteiro","Amarelo","Teclado","Notebook","Cartão"},
            //Dif 5
            new string [] {"Elefante","Camelo","Lampada","Monitor","Lanterna","Bolo",},
        };
    }
}
