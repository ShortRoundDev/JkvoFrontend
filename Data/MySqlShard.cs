using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;

namespace JkvoXyz.Data
{
    public class MySqlShard : IDbShard
    {
        private string _host { get; init; }
        private string _database { get; init; }
        private string _username { get; init; }
        private string _password { get; init; }

        public MySqlShard(string host, string database, string username, string password)
        {
            _host = host;
            _database = database;
            _username = username;
            _password = password;
        }
        public DbConnection GetShard(ulong shard)
        {
            return new MySqlConnection(
                $"Server=db{shard}.{_host}; " +
                $"Database={_database}; " +
                $"Uid={_username}; " +
                $"Pwd={_password};" +
                $"Port=3306;"
            );

        }
        public DbConnection GetShard(string? shortPath)
        {
            if(string.IsNullOrEmpty(shortPath))
            {
                return GetShard(0);
            }
            var shardCode = PathToULong(shortPath);
            var shardMod = (shardCode & 0xffu);
            var urlId = shardCode >> 8; // drop mod
            
            var shardNumber = urlId % shardMod;

            return GetShard(shardNumber);
        }

        private ulong PathToULong(string shortPath)
        {
            var bytes = Convert.FromHexString(shortPath);
            ulong hash = 0;
            for(int i = 0; i < bytes.Length && i < 5; i++) // should be 5 bytes. 4 = code, 5th = encoded mode
            {
                hash |= ((ulong)bytes[i]) << (8 * i);
            }
            return hash;
        }

        public async Task InsertShortCode(string shortCode, string url)
        {
            using (var shard = (MySqlConnection)GetShard(shortCode))
            {
                shard.Open();
                var command = shard.CreateCommand();
                command.CommandText = "INSERT INTO Paths VALUES(@Short, @Full)";
                command.Parameters.AddWithValue("Short", shortCode);
                command.Parameters.AddWithValue("Full", url);
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<string?> GetUrlFromShortCode(string shortCode)
        {
            using (var shard = (MySqlConnection)GetShard(shortCode))
            {
                shard.Open();
                var command = shard.CreateCommand();
                command.CommandText = "SELECT Full FROM Paths WHERE Short = @Short";
                command.Parameters.AddWithValue("Short", shortCode);
                var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    return reader.GetString(0);
                }
            }
            return null;
        }
    }
}
