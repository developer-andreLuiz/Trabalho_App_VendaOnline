using System;
using System.Collections.Generic;
using System.Text;

namespace AppMercado.ModeloRetorno
{
    class CategoriaNetoRetorno
    {
        public int id_pai { get; set; }//chave primaria
        public int id_filho { get; set; }//chave primaria
        public string nomeNeto { get; set; }//chave primaria
        public string img { get; set; }//not null
        public int ordem { get; set; }//not null
        public int id_neto { get; set; }//chave primaria auto numerico 
    }
}
