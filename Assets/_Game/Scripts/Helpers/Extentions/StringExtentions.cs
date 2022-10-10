using System;
using System.Linq;

namespace SBF.Extentions.Strings
{
    public static class StringExtentions
    {
        public static string ToCurrencySymbol(this string isoCode)
        {
            System.Globalization.RegionInfo regionInfo =
                (from culture in System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures)
                 where culture.Name.Length > 0 && !culture.IsNeutralCulture
                 let region = new System.Globalization.RegionInfo(culture.LCID)
                 where string.Equals(region.ISOCurrencySymbol, isoCode, StringComparison.InvariantCultureIgnoreCase)
                 select region)
                .FirstOrDefault();

            if (regionInfo == null)
            {
                return isoCode;
            }
            return regionInfo.CurrencySymbol;
        }
    }
}

