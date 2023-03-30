using System;
using System;
using System.Collections.Generic;
using System.IO;

namespace bid_ask_spread
{
	public class FormataArquivo
	{
		public void Formata()
		{
            // Lê o arquivo de registros
            string[] linhas = File.ReadAllLines("/Users/matheus/Projects/bid-ask-spread/bid-ask-spread/ArquivosCSV/log_completo.txt");

            // Cria lista para armazenar os registros com o valor do SecId na tag 48
            List<string> registrosComTag48 = new List<string>();

            // Armazena os valores dos campos identificados
            List<string[]> valoresTags = new List<string[]>();

           
            // Percorre os registros
            foreach (string registro in linhas)
            {
                // Separa os campos do registro pelo caractere ';'
                string[] campos = registro.Split(';');

                // Armazena os valores dos campos identificados
                string tag52 = ""; //Horario
                string tag48 = ""; //SecrutyID
                string tag269 = ""; //0 = Bid / 1 = Offer
                string tag270 = ""; //Preço
                string tag271 = ""; //Quantidade

                // Percorre os campos identificados
                foreach (string campo in campos)
                {
                    // Separa o campo em tag e valor pelo caractere '='
                    string[] partes = campo.Split('=');


                    // Verifica se o campo é uma tag e se tem pelo menos duas partes
                    if (partes.Length >= 2 || partes[0].Length == 3)
                    {
                        string tag = partes[0];
                        string valor = partes[1];

                        // Verifica se a tag é a 48 e armazena o valor
                        if (tag == "48")
                        {
                            tag48 = valor;
                        }

                        // Verifica se a tag é a 52 e armazena o valor
                        if (tag == "52")
                        {
                            tag52 = valor;
                        }

                        // Verifica se a tag é a 269 e armazena o valor
                        if (tag == "269")
                        {
                            tag269 = valor;
                        }

                        // Verifica se a tag é a 270 e armazena o valor
                        if (tag == "270")
                        {
                            tag270 = valor;
                        }

                        // Verifica se a tag é a 271 e armazena o valor
                        if (tag == "271")
                        {
                            tag271 = valor;
                        }
                    }
                }

                //SecrutyIDs que serão trabalhadas
                List<string> tagsValidas = new List<string> { "3809779", "3803947", "3809688", "3809654", "3805660" };

                // Verifica se o valor da tag 48 tem o SecrutyID da variavel tagsValidas
                if (tagsValidas.Contains(tag48))
                {
                    registrosComTag48.Add(tag48 + ";" + tag52 + ";" + tag269 + ";" + tag270 + ";" + tag271);
                }
            }

            // Cria o arquivo CSV e escreve os valores das tags dos registros com tag 48 igual a 3809779
            using (StreamWriter sw = new StreamWriter("/Users/matheus/Projects/bid-ask-spread/bid-ask-spread/ArquivosCSV/registrosComTag48.csv"))
            {
                // Escreve o cabeçalho do arquivo CSV
                sw.WriteLine("SecrutyID;Horario;TipoOferta;Preço;Quantidade");

                // Escreve os valores das tags dos registros
                for (int i = 0; i < registrosComTag48.Count; i++)
                {
                    //string linha = string.Join(";", registrosComTag48[i]).Replace("|", ";");
                    string linha = string.Join(";", registrosComTag48[i]).Replace(";;",";0;0"); //Se preço e quantidade vazio, então = 0
                    sw.WriteLine(linha);
                }
            }
            //Console.WriteLine("Arquivo CSV gerado com sucesso [Por_SecID].");
            //Console.ReadLine();
        }
    }
	
}

