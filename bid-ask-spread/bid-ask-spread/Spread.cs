using System;
using System.Linq;
using System.Text;

namespace bid_ask_spread
{
	public class Spread
	{
		public Spread()
		{
		}

		public string CalculaSpread(string bid, string ask)
		{
			string[] b = bid.Split(";");
			string[] a = ask.Split(";");
            StringBuilder resultado = new StringBuilder();

			float bidPreco = float.Parse(b[3]);
            float askPreco = float.Parse(a[3]);
			float calculoSpread = bidPreco - askPreco;

            resultado.Append("Minute: " + b[1].Substring(9,5) + "\n"); //Extrai somente as horas e minutos
			resultado.Append("SecId: " + b[0] + "\n");
			resultado.Append("Best Bid: " + b[3] + " - " + b[4] + " units \n");
			resultado.Append("Best Ask: " + a[3] + " - " + a[4] + " units \n");
			resultado.Append("Spread: " + calculoSpread);

            return resultado.ToString();
		}
	}
}

