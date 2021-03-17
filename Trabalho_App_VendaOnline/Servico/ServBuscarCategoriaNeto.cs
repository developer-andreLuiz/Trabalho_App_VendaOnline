using AppMercado.ModeloRetorno;
using AppMercado.VariaveisGlobais;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AppMercado.Servico
{
    class ServBuscarCategoriaNeto
    {
        private static string EnderecoUrl = "https://webapimercado.azurewebsites.net/api/CategoriaNeto/RetornoCompleto";
        public static void BuscarCategoriaNetoResultado()
        {
            WebClient wc = new WebClient();
            string conteudo = wc.DownloadString(EnderecoUrl);

            Global.instancia.listaCategoriaNetoGlobal = JsonConvert.DeserializeObject<List<CategoriaNetoRetorno>>(conteudo);
        }
    }
}