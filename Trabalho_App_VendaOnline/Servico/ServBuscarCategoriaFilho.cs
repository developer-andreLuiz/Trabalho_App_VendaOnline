using AppMercado.ModeloRetorno;
using AppMercado.VariaveisGlobais;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
namespace AppMercado.Servico
{
    class ServBuscarCategoriaFilho
    {
        private static string EnderecoUrl = "https://webapimercado.azurewebsites.net/api/CategoriaFilho/RetornoCompleto";
        public static void BuscarCategoriaFilhoResultado()
        {
            WebClient wc = new WebClient();
            string conteudo = wc.DownloadString(EnderecoUrl);

            Global.instancia.listaCategoriaFilhoGlobal = JsonConvert.DeserializeObject<List<CategoriaFilhoRetorno>>(conteudo);
        }
    }
}
