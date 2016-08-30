using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ProcessorUtilities
{
    public class Constant
    {
        public static string SqlConnectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=CrazyCustoms;Data Source=.";

        public static DateTime ConvertToDateTime(string input)
        {
            return new DateTime(int.Parse(input.Substring(0, 4)), int.Parse(input.Substring(4, 2)), int.Parse(input.Substring(6, 2)), int.Parse(input.Substring(8, 2)), int.Parse(input.Substring(10, 2)), 0);
        }

        public static int ConvertToInt(string input)
        {
            return 0;
        }

        public static QueryTypes GetQueryType(string input)
        {
            RegexOptions options = RegexOptions.Singleline | RegexOptions.IgnoreCase;
            Regex regexDeclarationNumber = new Regex("^\\d{18,18}$", options);
            Regex regexContainerNumber = new Regex("^[a-z]{4,4}\\d{7,7}$", options);
            if (regexDeclarationNumber.IsMatch(input, 0))
                return QueryTypes.DeclarationNumber;
            if (regexContainerNumber.IsMatch(input, 0))
            {
                return QueryTypes.ContainerNumber;
            }
            return QueryTypes.BillNumber;
        }
    }
}
