using AppMercado.Modelo;
using AppMercado.ModeloRetorno;
using AppMercado.SQLite;
using AppMercado.SQLite.ModeloDB;
using AppMercado.VariaveisGlobais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMercado.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListarProdutos : ContentPage
    {
        CarrinhoDeCompraSQLite carrinhoDeCompraSQLite = new CarrinhoDeCompraSQLite();
        public PageListarProdutos()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            collectionViewTela.ItemsSource = produtoListas();
            ExibirValorTotal();
        }
        List<ProdutoLista> produtoListas()
        {
            List<ProdutoCategoriasRetorno> listCategoria = new List<ProdutoCategoriasRetorno>();
            List<ProdutoLista> list = new List<ProdutoLista>();

            //Alimenta listCategoria
            foreach (var b in Global.instancia.listaProdutoCategoriasGlobal)
           {
                if (b.id_pai.Equals(Global.instancia.id_pai) && b.id_filho.Equals(Global.instancia.id_filho) && b.id_neto.Equals(Global.instancia.id_neto) && b.id_bisneto.Equals(Global.instancia.id_bisneto))
                {
                    listCategoria.Add(b);
                }
           }


            //Alimenta list
            foreach (var c in listCategoria)
            {
                ProdutoRetorno produtoRetorno = Global.instancia.listaProdutoGlobal[Global.instancia.listaProdutoGlobal.FindIndex(a=>a.codigo==c.codigo_produto)];

                ProdutoLista pLista = new ProdutoLista();

                pLista.codigo = produtoRetorno.codigo;
                pLista.nome = produtoRetorno.nome;
                pLista.nome_voz = produtoRetorno.nome_voz;
                pLista.gramatura = produtoRetorno.gramatura;
                pLista.img = produtoRetorno.img;

                if (produtoRetorno.valor_promocao > 0)
                {
                    pLista.valor_atual = "R$" + Math.Round(produtoRetorno.valor_promocao, 2);
                    pLista.valor_antigo = "R$" + Math.Round(produtoRetorno.valor_venda, 2);
                }
                else
                {
                    pLista.valor_atual = "R$" + Math.Round(produtoRetorno.valor_venda, 2);
                    pLista.valor_antigo = string.Empty;
                }
                pLista.quantidade = string.Empty;
                pLista.caminho_img_quantidade = string.Empty;
                pLista.caminho_img_menos = string.Empty;

                list.Add(pLista);
            }
            
           
            List<CarrinhoDeCompra> listaCarrinho = new List<CarrinhoDeCompra>();
            CarrinhoDeCompraSQLite carrinhoDeCompraSQL = new CarrinhoDeCompraSQLite();
            listaCarrinho = carrinhoDeCompraSQL.GetAllProduto();
            if (listaCarrinho.Count > 0)
            {
                foreach (CarrinhoDeCompra b in listaCarrinho)
                {
                    int index = -1;
                    foreach (ProdutoLista item in list)
                    {
                        if (item.codigo == b.codigo)
                        {
                            index = list.IndexOf(item);
                            break;
                        }
                    }
                    try
                    {
                        list[index].quantidade = b.quantidade.ToString();
                        list[index].caminho_img_quantidade = "fundocontador.png";
                        list[index].caminho_img_menos = "menos.png";
                    }
                    catch { }
                }
            }
            return list;
        }

        private void PushAsyncWithoutDuplicate(Page page)
        {
            if (Navigation.NavigationStack[Navigation.NavigationStack.Count - 1].GetType() != page.GetType())
            {
                Navigation.PushAsync(page);
            }
        }
        private void imgBtnCarrinho_Clicked(object sender, EventArgs e)
        {
            PushAsyncWithoutDuplicate(new PageCarrinhoDeCompra());
        }
        private async void imgBtnHome_Clicked(object sender, EventArgs e)
        {
            await imgBtnHome.ScaleTo(1.5, 50);
            await imgBtnHome.ScaleTo(1, 50);

            PushAsyncWithoutDuplicate(new PageCategoriaPai());
        }

        private void imgInterrogacao_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("", "Duvida", "ok");
        }

        private void imgMenos_Tapped(object sender, EventArgs e)
        {
            Image menos = (Image)sender;
            Label lblQuantidadeTemp = menos.FindByName<Label>("lblQuantidade");
            Image imgQuantidadeTemp = menos.FindByName<Image>("imgQuantidade");
            TapGestureRecognizer tapGest = (TapGestureRecognizer)menos.GestureRecognizers[0];
            ProdutoLista produtoLista = tapGest.CommandParameter as ProdutoLista;
            CarrinhoDeCompra carrinhoDeCompra = new CarrinhoDeCompra();
            carrinhoDeCompra = carrinhoDeCompraSQLite.GetProduto(produtoLista.codigo);
            int quantidade;
            try
            {
                quantidade = int.Parse(lblQuantidadeTemp.Text);
                if (quantidade > 1)
                {

                    carrinhoDeCompra.quantidade--;
                    lblQuantidadeTemp.Text = carrinhoDeCompra.quantidade.ToString();
                    carrinhoDeCompraSQLite.AtualizarProduto(carrinhoDeCompra);
                }
                else
                {
                    menos.IsVisible = false;
                    imgQuantidadeTemp.IsVisible = false;
                    lblQuantidadeTemp.Text = string.Empty;

                    carrinhoDeCompraSQLite.DeletarProduto(carrinhoDeCompra);
                }
            }
            catch { }

            try
            {
                lblValorTotal.Text = "R$" + (float.Parse(lblValorTotal.Text.Replace("R$", "")) - carrinhoDeCompra.valor_venda);
            }
            catch { }




        }

        private async void imgProduto_Tapped(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            TapGestureRecognizer tapGest = (TapGestureRecognizer)img.GestureRecognizers[0];
            ProdutoLista produtoLista = tapGest.CommandParameter as ProdutoLista;

            Frame frame = img.FindByName<Frame>("frmPai");
            double inicialX = frame.X;
            double inicialY = frame.Y;
            await frame.ScaleTo(0.3, 50);
            await frame.TranslateTo(200, -100, 200);
            frame.IsVisible = false;
            await frame.TranslateTo(inicialX, inicialY, 0);
            await frame.ScaleTo(1, 0);
            frame.IsVisible = true;
            Label lblQuantidadeTemp = img.FindByName<Label>("lblQuantidade");
            Image imgQuantidadeTemp = img.FindByName<Image>("imgQuantidade");
            Image imgMenosTemp = img.FindByName<Image>("imgMenos");
            CarrinhoDeCompra carrinhoDeCompra = new CarrinhoDeCompra();
            CarrinhoDeCompra carrinhoDeCompraVerifiracao = new CarrinhoDeCompra();
            carrinhoDeCompra.codigo = produtoLista.codigo;
            carrinhoDeCompra.nome = produtoLista.nome;
            carrinhoDeCompra.img = produtoLista.img;
            carrinhoDeCompra.valor_venda = (float)Math.Round(float.Parse(produtoLista.valor_atual.Replace("R$", "")), 2);

            carrinhoDeCompraVerifiracao = carrinhoDeCompraSQLite.GetProduto(produtoLista.codigo);

            if (carrinhoDeCompraVerifiracao == null)
            {
                lblQuantidadeTemp.Text = "1";
                imgQuantidadeTemp.Source = "fundocontador.png";
                imgQuantidadeTemp.IsVisible = true;
                imgMenosTemp.Source = "menos.png";
                imgMenosTemp.IsVisible = true;




                carrinhoDeCompra.quantidade = 1;
                carrinhoDeCompra.valor_total = carrinhoDeCompra.valor_venda;
                carrinhoDeCompraSQLite.InserirProduto(carrinhoDeCompra);
            }
            else
            {


                carrinhoDeCompra.id = carrinhoDeCompraVerifiracao.id;
                carrinhoDeCompra.quantidade = carrinhoDeCompraVerifiracao.quantidade + 1;
                lblQuantidadeTemp.Text = carrinhoDeCompra.quantidade.ToString();
                carrinhoDeCompra.valor_total += carrinhoDeCompra.quantidade * carrinhoDeCompra.valor_venda;
                carrinhoDeCompra.valor_total = (float)Math.Round(carrinhoDeCompra.valor_total, 2);

                carrinhoDeCompraSQLite.AtualizarProduto(carrinhoDeCompra);
            }
            await imgBtnCarrinho.ScaleTo(1.5, 250);
            await imgBtnCarrinho.ScaleTo(1, 250);
            try
            {
                lblValorTotal.Text = "R$" + (float.Parse(lblValorTotal.Text.Replace("R$", "")) + carrinhoDeCompra.valor_venda);
            }
            catch { }
            await lblValorTotal.ScaleTo(1.5, 250);
            await lblValorTotal.ScaleTo(1, 250);

        }
        float AtualizarValor()
        {
            float valor = 0;
            List<CarrinhoDeCompra> listCarrinhoCompra = new List<CarrinhoDeCompra>();
            listCarrinhoCompra = carrinhoDeCompraSQLite.GetAllProduto();

            foreach (var a in listCarrinhoCompra)
            {
                try
                {
                    valor += a.valor_total;
                }
                catch
                {

                }

            }

            return (float)Math.Round(valor, 2);

        }
        void ExibirValorTotal()
        {
            lblValorTotal.Text = "R$" + AtualizarValor().ToString();
        }
    }
}