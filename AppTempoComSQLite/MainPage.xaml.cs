using System.Collections.ObjectModel;
using System.Diagnostics;
using AppTempoComSQLite.Models;
using AppTempoComSQLite.Services;

namespace AppTempoComSQLite
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Tempo> lista = new ObservableCollection<Tempo>();

        public MainPage()
        {
            InitializeComponent();

            lst_previsoes_tempo.ItemsSource = lista;
        }

        protected async override void OnAppearing()
        {
            try
            {
                lista.Clear();
                App.Db.PesquisarTudo().Result.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.PegarPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        t.Cidade = txt_cidade.Text; 
                        t.DataConsulta = DateTime.Now; 

                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.NascSol} \n" +
                                         $"Por do Sol: {t.PorSol} \n" +
                                         $"Temp Máx: {t.TempMax} \n" +
                                         $"Temp Min: {t.TempMin} \n";


                        await App.Db.Inserir(t);


                        lista.Clear();
                        App.Db.PesquisarTudo().Result.ForEach(i => lista.Add(i));
                    }
                    else
                    {
                        lbl_res.Text = "Sem dados de Previsão";
                    }

                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                } 

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private void lst_previsoes_tempo_Refreshing(object sender, EventArgs e)
        {

        }

        private void lst_previsoes_tempo_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string q = e.NewTextValue;

                lista.Clear();

                List<Tempo> tmp = await App.Db.Pesquisar(q);

                tmp.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecinado = sender as MenuItem;
                Tempo t = selecinado.BindingContext as Tempo;

                bool confirm = await DisplayAlert(
                    "Tem Certeza?", $"Remover previsão para {t.Cidade}?", "Sim", "Não");

                if (confirm)
                {
                    await App.Db.Deletar(t.Id);
                    lista.Remove(t);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }

}
