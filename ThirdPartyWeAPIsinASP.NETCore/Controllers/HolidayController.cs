using Microsoft.AspNetCore.Mvc;
using ThirdPartyWeAPIsinASP.NETCore.IRepository;
using ThirdPartyWeAPIsinASP.NETCore.Model;

namespace ThirdPartyWeAPIsinASP.NETCore.Controllers
{
    public class HolidayController : Controller
    {
        private readonly IHolidaysApiService _holidaysApiService;

        public HolidayController(IHolidaysApiService holidaysApiService)
        {
            this._holidaysApiService = holidaysApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        [Route("GetHolidayData")]
        public async Task<IActionResult> Index(string countryCode, int Year)
        {
            List<PublicHoliday> publicHolidays = new List<PublicHoliday>();
            if (!string.IsNullOrEmpty(countryCode))
            {
                publicHolidays = await _holidaysApiService.GetPublicHolidays(countryCode, Year);
            }
            else if(publicHolidays!= null)
            {
                return View(publicHolidays);
            }

            return View(publicHolidays);
        }
    }
}
