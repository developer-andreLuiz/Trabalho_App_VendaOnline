using AppMercado.ModeloRetorno;
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
    public partial class PageCategoriaFilho : ContentPage
    {
        public PageCategoriaFilho()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            collectionViewTela.ItemsSource = retornoCategoriaFilho();
        }
        List<CategoriaFilhoRetorno> retornoCategoriaFilho()
        {
            List<CategoriaFilhoRetorno> list = new List<CategoriaFilhoRetorno>();

            foreach (var a in Global.instancia.listaCategoriaFilhoGlobal)
            {
                if (a.id_pai == int.Parse(Global.instancia.id_pai))
                {
                    list.Add(a);
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
        private async void imgBtnHome_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IAudioService>().PlayAudioFile("button.mp3");
            await imgBtnHome.ScaleTo(1.5, 50);
            await imgBtnHome.ScaleTo(1, 50);

            PushAsyncWithoutDuplicate(new PageCategoriaPai());
        }
        private async void imgBtnCarrinho_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IAudioService>().PlayAudioFile("button.mp3");
            await imgBtnCarrinho.ScaleTo(1.5, 50);
            await imgBtnCarrinho.ScaleTo(1, 50);

            PushAsyncWithoutDuplicate(new PageCarrinhoDeCompra());
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                Frame frame = (Frame)sender;
                Label x = frame.FindByName<Label>("lblNome");

                if (x.Text != null)
                {
                    foreach (var a in Global.instancia.listaCategoriaFilhoGlobal)
                    {
                        if (a.nomeFilho.Equals(x.Text) && a.id_pai == int.Parse(Global.instancia.id_pai))
                        {
                            Global.instancia.id_filho = a.id_filho.ToString();
                            break;
                        }
                    }
                    bool netoLocal = false;
                   
                    foreach (var a in Global.instancia.listaCategoriaNetoGlobal)
                    {
                        if (a.id_pai == int.Parse(Global.instancia.id_pai) && a.id_filho == int.Parse(Global.instancia.id_filho))
                        {
                            netoLocal = true;
                            break;
                        }
                    }


                    if (netoLocal)
                    {
                        PushAsyncWithoutDuplicate(new PageCategoriaNeto());
                    }
                    else
                    {
                        
                        Global.instancia.id_neto = string.Empty;
                        Global.instancia.id_bisneto = string.Empty;
                        PushAsyncWithoutDuplicate(new PageListarProdutos());
                    }
                    
                }
            }
            catch { }
        }
    }
}