using MauiAppTempoSQLite.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoSQLite.Services
{
    public class DataService
    {
        public static async Task<Tempo?> PegarPrevisao(string cidade)//recebe cidade para achar previsão
        {
            //acesso a API
            Tempo? t = null;

            string chave = "6135072afe7f6cec1537d5cb08a5a1a2";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&appid={chave}";

            using (HttpClient cliente = new HttpClient())//cria instancia HTTP
            {
                HttpResponseMessage resp = await cliente.GetAsync(url);

                if (resp.IsSuccessStatusCode)//verifica se foi bem sucedido
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);

                    DateTime temp = new();
                    DateTime NascSol = temp.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime PorSol = temp.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    t = new()//cria o objeto tempo
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        Descricao = (string)rascunho["weather"][0]["Descricao"],
                        main = (string)rascunho["weather"][0]["main"],
                        TempMin = (double)rascunho["main"]["TempMin"],
                        TempMax = (double)rascunho["main"]["TempMax"],
                        velocidade = (double)rascunho["wind"]["Velocidade"],
                        visibilidade = (int)rascunho["Visibilidade"],
                        NascSol = NascSol.ToString(),
                        PorSol = PorSol.ToString(),
                    };
                }
            }

            return t;//retorna objeto
        }
    }
}