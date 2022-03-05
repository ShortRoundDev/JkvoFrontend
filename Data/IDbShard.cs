using System.Data.Common;

namespace JkvoXyz.Data
{
    public interface IDbShard
    {
        public DbConnection GetShard(ulong shard);
        public DbConnection GetShard(string shortPath);

        public Task InsertShortCode(string shortCode, string url);
        public Task<string?> GetUrlFromShortCode(string shortCode);
    }
}
