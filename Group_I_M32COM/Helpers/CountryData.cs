using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.Helpers
{
    public class CountryData : ICountryDataService
    {
        public List<SelectListItem> LoadCountryList()
        {
            var CountryList = new List<SelectListItem>();
            CountryList.Add(new SelectListItem
            {
                Text = "Select Country",
                Value = ""
            });
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Any(x => x.Text == R.EnglishName)))
                {
                    CountryList.Add(new SelectListItem
                    {
                        Text = R.EnglishName,
                        Value = R.EnglishName.ToString(),
                    });
                    Console.WriteLine("Loaded countries: ", CountryList);
                }
            }
            return CountryList;
        }
    }

    public interface ICountryDataService
    {
        List<SelectListItem> LoadCountryList();
    }
}
