using AppMercado.ModeloRetorno;
using AppMercado.VariaveisGlobais;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
namespace AppMercado.Servico
{
    class ServBuscarCategoriaPai
    {
        private static string EnderecoUrl = "https://webapimercado.azurewebsites.net/api/CategoriaPai/RetornoCompleto";
        public static void BuscarCategoriaPaiResultado()
        {
            WebClient wc = new WebClient();
            string conteudo = wc.DownloadString(EnderecoUrl);

            Global.instancia.listaCategoriaPaiGlobal = JsonConvert.DeserializeObject<List<CategoriaPaiRetorno>>(conteudo);
        }
    }
}
