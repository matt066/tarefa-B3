using System;
using System.Globalization;

namespace bid_ask_spread
{
    public class PrecoBidAsk 
    {
        public string secrutyID = ""; //SecrutyID
        public string horario = ""; //Horario
        public string tipoOferta = ""; //0 = Bid / 1 = Offer
        public string preco = ""; //Preço
        public string quantidade = ""; //Quantidade

        public List<PrecoBidAsk> ListaRegistrosPorSecID(string secId)
        {
            string caminhoCSV = "/Users/matheus/Projects/bid-ask-spread/bid-ask-spread/ArquivosCSV/registrosComTag48.csv";
            List<PrecoBidAsk> registros = new List<PrecoBidAsk>();

            using (StreamReader sr = new StreamReader(caminhoCSV))
            {
                // ignorando a primeira linha, que contém os cabeçalhos das colunas
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string linha = sr.ReadLine();
                    string[] campos = linha.Split(';');

                    PrecoBidAsk registro = new PrecoBidAsk
                    {
                        secrutyID = campos[0],
                        horario = campos[1],
                        tipoOferta = campos[2],
                        preco = campos[3],
                        quantidade = campos[4],
                    };

                    //Considera somente registro com o secID informado
                    if (registro.secrutyID.StartsWith(secId))
                    {
                        registros.Add(registro);
                    }
                }

                // Cria o arquivo CSV e escreve os valores de acordo com SecId
                using (StreamWriter sw = new StreamWriter("/Users/matheus/Projects/bid-ask-spread/bid-ask-spread/ArquivosCSV/registrosPorSecIDSelecionado.csv"))
                {
                    // Escreve o cabeçalho do arquivo CSV
                    sw.WriteLine("SecrutyID;Horario;TipoOferta;Preço;Quantidade");

                    // Escreve os valores das tags dos registros
                    for (int i = 0; i < registros.Count; i++)
                    {
                        string linha = string.Join(";", registros[i].secrutyID, registros[i].horario, registros[i].tipoOferta, registros[i].preco, registros[i].quantidade);
                        sw.WriteLine(linha);
                    }
                }
            }
            //Console.WriteLine(registros);
            //Console.ReadLine();
            return registros;
        }

        public string MaiorBid(List<PrecoBidAsk> registros)
        {
            List<PrecoBidAsk> lista = new List<PrecoBidAsk>();
            lista = registros;

            List<PrecoBidAsk> registrosFiltrados = new List<PrecoBidAsk>();

            for (int i = 1; i < lista.Count; i++) // começa em 1 para ignorar o cabeçalho
            {
                string tipoOferta = lista[i].tipoOferta;

                //coluna na posição 2 é o tipo de oferta, neste caso "0" == bid
                if (tipoOferta == "0")
                {
                    PrecoBidAsk registroFiltrado = new PrecoBidAsk();
                    registrosFiltrados.Add(lista[i]);
                }
            }

            //Retorna o maior valor de Bid

            //Consulta maior preço na lista enviada no arquivo registrosPorSecIdSelecionado.csv
            PrecoBidAsk r = registrosFiltrados.OrderByDescending(p => p.preco).FirstOrDefault();

            //Guarda maior preço para a consulta seguinte
            string a = r.preco;

            //Consulta menor data e hora considerando o maior preço
            PrecoBidAsk r2 = registrosFiltrados.Where(p => p.preco == a).OrderBy(h => h.horario).FirstOrDefault();

            string maxBid = string.Join(";", r.secrutyID, r.horario, r.tipoOferta, r.preco, r.quantidade);

            return maxBid;
        }

        public string MenorAsk(List<PrecoBidAsk> registros)
        {
            List<PrecoBidAsk> lista = new List<PrecoBidAsk>();
            lista = registros;

            List<PrecoBidAsk> registrosFiltrados = new List<PrecoBidAsk>();

            for (int i = 1; i < lista.Count; i++) // começa em 1 para ignorar o cabeçalho
            {
                string tipoOferta = lista[i].tipoOferta;
                string valor = lista[i].preco;

                //coluna na posição 2 é o tipo de oferta, neste caso "1" == ask e desconsidera ofertas com preço vazio (0)
                if (tipoOferta == "1" && valor != "0")
                {
                    PrecoBidAsk registroFiltrado = new PrecoBidAsk();
                    registrosFiltrados.Add(lista[i]);
                }
            }

            //Retorna o menor valor de Ask

            //Consulta maior preço na lista enviada no arquivo registrosPorSecIdSelecionado.csv
            PrecoBidAsk r = registrosFiltrados.OrderBy(p => p.preco).FirstOrDefault();

            //Guarda menor preço para a consulta seguinte
            string a = r.preco;

            //Consulta menor data e hora considerando o maior preço
            PrecoBidAsk r2 = registrosFiltrados.Where(p => p.preco == a).OrderBy(h => h.horario).FirstOrDefault();

            string minAsk = string.Join(";", r.secrutyID, r.horario, r.tipoOferta, r.preco, r.quantidade);

            return minAsk;
        }

    }
}

