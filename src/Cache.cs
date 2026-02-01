using LiteDB;

namespace src
{
    public class Cache
    {

        private string _dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"/cache-proxy/db/";
        private string _dbName = "cache.db";

        public Cache()
        {
            if (!Directory.Exists(_dbPath))
            {
                Directory.CreateDirectory(_dbPath);
            }
        }

        public void ClearCache()
        {
            using (var db = new LiteDatabase(_dbPath + _dbName))
            {
                var col = db.DropCollection("requests");
            }
        }

        public dto.CacheDto? GetCache(string url)
        {
            using (var db = new LiteDatabase(_dbPath + _dbName))
            {
                var col = db.GetCollection<src.Model.ResponseModel>("requests");

                var cacheEntry = col.FindOne(x => x.Url == url);

                if (cacheEntry != null)
                {
                    return new dto.CacheDto(cacheEntry.Data, cacheEntry.ContentType, "HIT");
                }

                return null;
            }
        }

        public dto.CacheDto SetCache(string content, string url, string contentType)
        {
            using (var db = new LiteDatabase(_dbPath + _dbName))
            {
                var col = db.GetCollection<src.Model.ResponseModel>("requests");

                var request = new src.Model.ResponseModel
                {
                    Url = url,
                    Data = content,
                    ContentType = contentType
                };

                col.Insert(request);

                return new dto.CacheDto(content, contentType, "MISS");
            }
        }
    }
}