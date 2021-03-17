using AppMercado.ModeloRetorno;
using AppMercado.VariaveisGlobais;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AppMercado.Servico
{
    class ServBuscarProdutoCategorias
    {
        private static string EnderecoUrl = "https://webapimercado.azurewebsites.net/api/ProdutoCategorias/RetornoCompleto";
        public static void BuscarProdutoCategoriasResultado()
        {
            WebClient wc = new WebClient();
            string conteudo = wc.DownloadString(EnderecoUrl);

            Global.instancia.listaProdutoCategoriasGlobal = JsonConvert.DeserializeObject<List<ProdutoCategoriasRetorno>>(conteudo);
        }
    }
}
