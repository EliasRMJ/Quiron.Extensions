using System.Text.RegularExpressions;

namespace Quiron.Extensions
{
    public static partial class EmailExtension
    {
        public static bool IsValid(this string email)
        {
            var regex = MailRegex();
            var match = regex.Match(email);
            return match.Success;
        }

        [GeneratedRegex("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$")]
        private static partial Regex MailRegex();
    }
}