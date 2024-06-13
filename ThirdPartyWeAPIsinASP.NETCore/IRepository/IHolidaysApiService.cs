using ThirdPartyWeAPIsinASP.NETCore.Model;

namespace ThirdPartyWeAPIsinASP.NETCore.IRepository
{
    public interface IHolidaysApiService
    {
        Task<List<PublicHoliday>> GetPublicHolidays(string countryCode, int year);
    }
}
