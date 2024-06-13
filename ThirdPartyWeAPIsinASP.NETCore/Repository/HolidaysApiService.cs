using System.Net;
using System.Text.Json;
using ThirdPartyWeAPIsinASP.NETCore.IRepository;
using ThirdPartyWeAPIsinASP.NETCore.Model;

namespace ThirdPartyWeAPIsinASP.NETCore.Repository
{
    public class HolidaysApiService : IHolidaysApiService
    {
        private readonly HttpClient client;
        private readonly string baseAddress = "https://date.nager.at";

        public HolidaysApiService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("PublicHolidaysApi");
        }

        public async Task<List<PublicHoliday>> GetPublicHolidays(string countryCode, int year)
        {
            var url = $"{baseAddress}/api/v2/PublicHolidays/{year}/{countryCode}";
            var result = new List<PublicHoliday>();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<List<PublicHoliday>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new HttpRequestException("Country code not found");
                return null;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }

    }
}
