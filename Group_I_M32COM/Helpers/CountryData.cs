using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.Helpers
{
    public class CountryData : ICountryDataService
    {
        public List<string> LoadCountryList()
        {
            List<string> CountryList = new List<string>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.EnglishName)))
                {
                    CountryList.Add(R.EnglishName);
                    Console.WriteLine("Loaded countries: ", CountryList);
                }
            }

            CountryList.Sort();
            return CountryList;
            //ViewData["CountryList"] = CountryList;
        }
    }

    public interface ICountryDataService
    {
        List<string> LoadCountryList();
    }
}
