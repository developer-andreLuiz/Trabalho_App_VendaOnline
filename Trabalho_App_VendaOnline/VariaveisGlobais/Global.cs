using AppMercado.ModeloRetorno;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppMercado.VariaveisGlobais
{
    class Global
    {
        public static Global instancia = new Global();

        public List<CategoriaPaiRetorno> listaCategoriaPaiGlobal = new List<CategoriaPaiRetorno>();

        public List<CategoriaFilhoRetorno> listaCategoriaFilhoGlobal = new List<CategoriaFilhoRetorno>();

        public List<CategoriaNetoRetorno> listaCategoriaNetoGlobal = new List<CategoriaNetoRetorno>();

        public List<CategoriaBisnetoRetorno> listaCategoriaBisnetoGlobal = new List<CategoriaBisnetoRetorno>();

        public List<ProdutoRetorno> listaProdutoGlobal = new List<ProdutoRetorno>();

        public List<ProdutoCategoriasRetorno> listaProdutoCategoriasGlobal = new List<ProdutoCategoriasRetorno>();

        public string id_pai { get; set; }
        public string id_filho { get; set; }
        public string id_neto { get; set; }
        public string id_bisneto { get; set; }

    }
}
