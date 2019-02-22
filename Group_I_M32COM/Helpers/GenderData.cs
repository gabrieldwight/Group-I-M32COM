using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.Helpers
{
    public class GenderData : IGenderDataService
    {
        public enum GenderOptionsEnum
        {
            Male,
            Female
        }

        public List<SelectListItem> LoadGenderList()
        {
            var GenderList = new List<SelectListItem>();
            GenderList.Add(new SelectListItem
            {
                Text = "Select Gender",
                Value = ""
            });
            foreach (GenderOptionsEnum gender in Enum.GetValues(typeof(GenderOptionsEnum)))
            {
                GenderList.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(GenderOptionsEnum), gender),
                    Value = gender.ToString(),
                });
            }
            return GenderList;
        }
    }

    public interface IGenderDataService
    {
        List<SelectListItem> LoadGenderList();
    }
}
