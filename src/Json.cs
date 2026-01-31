namespace src
{
    public class Json
    {
        public object? Deserialize(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        }
    }
}