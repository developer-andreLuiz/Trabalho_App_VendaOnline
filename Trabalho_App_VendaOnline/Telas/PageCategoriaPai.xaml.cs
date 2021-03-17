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
    public partial class PageCategoriaPai : ContentPage
    {
        public PageCategoriaPai()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            collectionViewTela.ItemsSource = VariaveisGlobais.Global.instancia.listaCategoriaPaiGlobal;
        }
        private void PushAsyncWithoutDuplicate(Page page)
        {
            if (Navigation.NavigationStack[Navigation.NavigationStack.Count - 1].GetType() != page.GetType())
            {
                Navigation.PushAsync(page);
            }
        }
        private async void imgBtnMenu_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IAudioService>().PlayAudioFile("button.mp3");
            await imgBtnMenu.ScaleTo(1.5, 50);
            await imgBtnMenu.ScaleTo(1, 50);

            PushAsyncWithoutDuplicate(new PageMenu());
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
                    foreach (var a in Global.instancia.listaCategoriaPaiGlobal)
                    {
                        if (a.nomePai.Equals(x.Text))
                        {
                            Global.instancia.id_pai = a.id_pai.ToString();
                            break;
                        }
                    }

                    bool filhoLocal = false;

                    foreach (var a in Global.instancia.listaCategoriaFilhoGlobal)
                    {
                        if (a.id_pai == int.Parse(Global.instancia.id_pai))
                        {
                            filhoLocal = true;
                            break;
                        }
                    }
                    if (filhoLocal)
                    {
                        PushAsyncWithoutDuplicate(new PageCategoriaFilho());
                    }
                    else
                    {
                        Global.instancia.id_filho = string.Empty;
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