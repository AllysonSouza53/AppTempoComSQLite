using AppTempoComSQLite.Models;
using SQLite;

namespace AppTempoComSQLite.Helpers
{
    public class SQLiteDataBaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDataBaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Tempo>().Wait();
        }

        public Task<int> Inserir(Tempo p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<int> Deletar(int id)
        {
            return _conn.Table<Tempo>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Tempo>> PesquisarTudo()
        {
            return _conn.Table<Tempo>().ToListAsync();
        }

        public Task<List<Tempo>> Pesquisar(string q)
        {
            string sql = "SELECT * FROM Tempo " +
                         "WHERE Descricao LIKE '%" + q + "%'";

            return _conn.QueryAsync<Tempo>(sql);
        }
    }
}
