namespace Movies.API.Services
{
    public class DownloadExt
    {
        public static async Task<byte[]?> GetUrlContentImg(string url)
        {
            using (var client = new HttpClient())
            using (var result = await client.GetAsync(url))
                return result.IsSuccessStatusCode ? await result.Content.ReadAsByteArrayAsync() : null;
        }

        public static async Task<byte[]?> GetUrlContentPdfOrExcel(string url)
        {
            using (var client = new HttpClient())
            using (var result = await client.GetAsync(url))
                return result.IsSuccessStatusCode ? await result.Content.ReadAsByteArrayAsync() : null;
        }


    }
}
