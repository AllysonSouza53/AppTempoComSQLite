using MauiAppTempoSQLite.Models;
using SQLite;

namespace MauiAppTempoSQLite.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;//Cria a conexão assincrona e o _conn mantem ela aberta

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);//Cria a tabela Tempo
            _conn.CreateTableAsync<Tempo>().Wait();//Força a conexão assincrona, garantindo que a tabela esteja pronta antes de seguir
        }

        public Task<int> Inserir(Tempo p)//insere valores
        {
            return _conn.InsertAsync(p);
        }

        public Task<int> Deletar(int id)//deleta valores
        {
            return _conn.Table<Tempo>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Tempo>> PesquiserTudo()//busca todos os dados da tabela e os retorna
        {
            return _conn.Table<Tempo>().ToListAsync();
        }

        public Task<List<Tempo>> Pesquisar(string q)// pesquisa dados especificos da tabela e os retorna
        {
            string sql = "SELECT * FROM Tempo " +
                         "WHERE description LIKE '%" + q + "%'";

            return _conn.QueryAsync<Tempo>(sql);
        }
    }
}
