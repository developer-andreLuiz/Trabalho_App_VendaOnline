using AppMercado.ModeloRetorno;
using AppMercado.VariaveisGlobais;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AppMercado.Servico
{
    class ServBuscarCategoriaBisneto
    {
        private static string EnderecoUrl = "https://webapimercado.azurewebsites.net/api/CategoriaBisneto/RetornoCompleto";
        public static void BuscarCategoriaBisnetoResultado()
        {
            WebClient wc = new WebClient();
            string conteudo = wc.DownloadString(EnderecoUrl);

            Global.instancia.listaCategoriaBisnetoGlobal = JsonConvert.DeserializeObject<List<CategoriaBisnetoRetorno>>(conteudo);
        }
    }
}
