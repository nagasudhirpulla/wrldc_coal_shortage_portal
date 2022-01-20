using System.Text.RegularExpressions;

namespace CoalShortagePortal.WebApp.Utils
{
    public class TextUtils
    {
        public static string SanitizeText(string inpTxt)
        {
            if (inpTxt == null)
            {
                return inpTxt;
            }
            Regex rgx = new("[^a-zA-Z0-9,._)( -]");
            string sanTxt = rgx.Replace(inpTxt, "");
            return sanTxt;
        }
    }
}
