namespace src
{
    public class Request
    {
        public dto.ResponseDto GetRequest(string url)
        {
            var client = new HttpClient();

            var response = client.GetAsync(url).Result;
            return new dto.ResponseDto(response.Content.ReadAsStringAsync().Result, response.Content.Headers.ContentType?.ToString() ?? "application/json");
        }
    }
}