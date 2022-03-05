using Force.Crc32;
using System.Text;

namespace JkvoXyz.Util
{
    public static class UrlHasher
    {
        public static string MakeShortCode(string url, byte totalShards)
        {
            var urlBytes = Encoding.UTF8.GetBytes(url);
            var hash = Crc32Algorithm.Compute(urlBytes);

            var shortPath = Convert.ToHexString(BitConverter.GetBytes(hash));
            var top = Convert.ToHexString(new byte[] { totalShards });

            shortPath = top + shortPath;
            return shortPath;
        }
    }
}
