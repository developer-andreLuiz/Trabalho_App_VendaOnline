using System;
using System.Collections.Generic;
using System.Text;

namespace AppMercado.ModeloRetorno
{
    class ProdutoRetorno
    {
        public int codigo { get; set; }
        public string codigo_barra { get; set; }
        public string nome { get; set; }
        public string nome_voz { get; set; }
        public float custo_unitario { get; set; }
        public float margem_venda { get; set; }
        public float valor_venda { get; set; }
        public float margem_promocao { get; set; }
        public float valor_promocao { get; set; }
        public string gramatura { get; set; }
        public string embalagem { get; set; }
        public float peso_produto { get; set; }
        public int iguala_produto { get; set; }
        public int iguala_fardo { get; set; }
        public string fardo { get; set; }
        public int quantidade_fardo { get; set; }
        public string img { get; set; }
      
    }
}
