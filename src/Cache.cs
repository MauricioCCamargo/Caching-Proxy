using LiteDB;

namespace src
{
    public class Cache
    {

        // public dto.CacheDto GetSetCache(string content, string url, string contentType)
        // {
        //     var cacheData = GetCache(url);
        //     if (cacheData == null)
        //     {
        //         SetCache(content, url, contentType);
        //         return new dto.CacheDto(content, contentType, "MISS");
        //     }
        //     return cacheData;
        // }

        public void ClearCache()
        {
            using (var db = new LiteDatabase(@"db/cache.db"))
            {
                var col = db.DropCollection("requests");
            }
        }

        public dto.CacheDto? GetCache(string url)
        {
            using (var db = new LiteDatabase(@"db/cache.db"))
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
            using (var db = new LiteDatabase(@"db/cache.db"))
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