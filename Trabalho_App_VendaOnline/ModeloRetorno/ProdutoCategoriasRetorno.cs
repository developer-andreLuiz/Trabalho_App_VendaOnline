using System;
using System.Collections.Generic;
using System.Text;

namespace AppMercado.ModeloRetorno
{
    class ProdutoCategoriasRetorno
    {
        public int id { get; set; }
        public int codigo_produto { get; set; }
        public string id_pai { get; set; }
        public string id_filho { get; set; }
        public string id_neto { get; set; }
        public string id_bisneto { get; set; }

    }
}
