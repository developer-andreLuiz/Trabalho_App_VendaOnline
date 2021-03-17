using System;
using System.Collections.Generic;
using System.Text;

namespace AppMercado.ModeloRetorno
{
    class CategoriaPaiRetorno
    {
        public string nomePai { get; set; }//chave primaria
        public string img { get; set; }//not null
        public int ordem { get; set; }//not null
        public int id_pai { get; set; }//chave primaria auto numerico 
    }
}
