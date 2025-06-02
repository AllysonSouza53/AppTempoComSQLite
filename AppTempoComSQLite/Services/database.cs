using System.Diagnostics;
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

            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&appid={chave}";

            //Debug.WriteLine(url);

            using (HttpClient cliente = new HttpClient())
            {
                HttpResponseMessage resp = await cliente.GetAsync(url);

               // Debug.WriteLine(resp);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                   // Debug.WriteLine(json);

                    var rascunho = JObject.Parse(json);

                    //Debug.WriteLine(rascunho);

                    DateTime temp = new();
                    DateTime NascSol = temp.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime PorSol = temp.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    //Debug.WriteLine(PorSol.ToString());

                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        Descricao = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        TempMin = (double)rascunho["main"]["temp_min"],
                        TempMax = (double)rascunho["main"]["temp_max"],
                        velocidade = (double)rascunho["wind"]["speed"],
                        visibilidade = (int)rascunho["visibility"],
                        NascSol = NascSol.ToString(),
                        PorSol = PorSol.ToString(),
                    };
                }
            }

            return t;
        }
    }
}