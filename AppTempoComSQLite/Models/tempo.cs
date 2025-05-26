using SQLite;

namespace MauiAppTempoSQLite.Models
{
    public class Tempo
        //declara os rotulos que serão registrado no banco
    {
        [PrimaryKey, AutoIncrement]//define o ID como chave primaria e Auto Increment
        public int Id { get; set; }
        public double? lon { get; set; }
        public double? lat { get; set; }
        public double? TempMin { get; set; }
        public double? TempMax { get; set; }
        public int? visibilidade { get; set; }
        public double? velocidade { get; set; }
        public string? main { get; set; }
        public string? Descricao { get; set; }
        public string? NascSol { get; set; }
        public string? PorSol { get; set; }
    }
}
