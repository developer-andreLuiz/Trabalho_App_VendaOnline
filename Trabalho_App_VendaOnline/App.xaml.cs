using AppMercado.Servico;
using AppMercado.Telas;
using AppMercado.VariaveisGlobais;
using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMercado
{
    public partial class App : Application
    {
        public App()
        {
            Thread categoriaFilho = new Thread(BuscarCaterogiaFilho);
            Thread categoriaNeto = new Thread(BuscarCaterogiaNeto);
            Thread categoriaBisneto = new Thread(BuscarCaterogiaBisneto);
            Thread produto = new Thread(BuscarProdutos);
            Thread produtoCategorias = new Thread(BuscarProdutoCategorias);

            categoriaFilho.Start();
            categoriaNeto.Start();
            categoriaBisneto.Start();
            produto.Start();
            produtoCategorias.Start();

            Global.instancia.id_pai = string.Empty;
            Global.instancia.id_filho = string.Empty;
            Global.instancia.id_neto = string.Empty;
            Global.instancia.id_bisneto = string.Empty;

            BuscarCaterogiaPai();
            InitializeComponent();

            MainPage = new NavigationPage(new PageCategoriaPai());
        }

        void BuscarCaterogiaPai()
        {
            ServBuscarCategoriaPai.BuscarCategoriaPaiResultado();
        }
        static void BuscarCaterogiaFilho()
        {
            ServBuscarCategoriaFilho.BuscarCategoriaFilhoResultado();
        }
        static void BuscarCaterogiaNeto()
        {
            ServBuscarCategoriaNeto.BuscarCategoriaNetoResultado();
        }
        static void BuscarCaterogiaBisneto()
        {
            ServBuscarCategoriaBisneto.BuscarCategoriaBisnetoResultado();
        }
        static void BuscarProdutos()
        {
            ServBuscarProduto.BuscarProdutoResultado();
        }
        static void BuscarProdutoCategorias()
        {
            ServBuscarProdutoCategorias.BuscarProdutoCategoriasResultado();
        }





        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
    }
}
