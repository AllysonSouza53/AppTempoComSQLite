using AppTempoComSQLite.Models;
using Newtonsoft.Json.Linq;

namespace AppTempoComSQLite.Services
{
    public class DataService
    {
        public static async Task<Tempo?> PegarPrevisao(string cidade)
        {
 
            Tempo? t = null;

            string chave = "6135072afe7f6cec1537d5cb08a5a1a2";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&appid={chave}";

            using (HttpClient cliente = new HttpClient())
            {
                HttpResponseMessage resp = await cliente.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);

                    DateTime temp = new();
                    DateTime NascSol = temp.AddSeconds((double)rascunho["sys"]["NascSol"]).ToLocalTime();
                    DateTime PorSol = temp.AddSeconds((double)rascunho["sys"]["PorSol"]).ToLocalTime();

                    t = new()
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

            return t;
        }
    }
}