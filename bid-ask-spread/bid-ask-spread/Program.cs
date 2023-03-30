using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using bid_ask_spread;

class Program
{
    public static void Main(string[] args)
    {

        string t = "10:26:15.161: 656307 - 70 - 1128 = 935 = X34 = 656307";

        string[] t2 = t.Split('\u0001');

        //Formata o arquivo somente com dados necessarios
        FormataArquivo arquivo = new FormataArquivo();
        arquivo.Formata();


        /* SecId - Referencia
         * 3809779
         * 3803947
         * 3809688
         * 3809654
         * 3805660
         */

        //Selecionar o ativos no terminal
        Console.WriteLine("Digite a opção para selecionar o SecId:\n" +
                           "1 - 3809779\n" +
                           "2 - 3803947\n" +
                           "3 - 3809688\n" +
                           "4 - 3809654\n" +
                           "5 - 3805660\n");

        string inputSecId = Console.ReadLine();
        string secId = ""; 

        switch (inputSecId)
        {
            case "1":
                secId = "3809779";
                break;
            case "2":
                secId = "3803947";
                break;
            case "3":
                secId = "3809688";
                break;
            case "4":
                secId = "3809654";
                break;
            case "5":
                secId = "3805660";
                break;
            default:
                Console.WriteLine("Opção Invalida");
                break;
        }

        //Pegar maior preço de comprar pelo SecId
        PrecoBidAsk registrosPorSecID = new PrecoBidAsk();

        List<PrecoBidAsk> registros = new List<PrecoBidAsk>();
        registros = registrosPorSecID.ListaRegistrosPorSecID(secId);

        //Chama metódo que captura registro com maior Bid e menor horario de negociação
        string maxBid = "";
        PrecoBidAsk bid = new PrecoBidAsk();
        maxBid = bid.MaiorBid(registros);

        //Chama metódo que captura registro com menor Ask e menor horario de negociação
        string minAsk = "";
        PrecoBidAsk ask = new PrecoBidAsk();
        minAsk = ask.MenorAsk(registros);

        //Calcula Spread
        string spread;
        Spread valorSpread = new Spread();
        spread = valorSpread.CalculaSpread(maxBid, minAsk);

        Console.WriteLine("Maior Bid: " + maxBid + "\n" + "Menor Ask: " + minAsk + "\n");
        Console.WriteLine(spread);
        Console.ReadLine();
    }

}
