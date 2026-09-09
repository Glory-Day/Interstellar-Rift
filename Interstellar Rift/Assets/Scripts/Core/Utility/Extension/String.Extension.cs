using System.Text.RegularExpressions;

namespace Core.Utility.Extension
{
    public static class String_Extension
    {
        private static readonly Regex PascalCaseRegex = new(@"(?<=[a-z0-9])(?=[A-Z])|(?<=[A-Z])(?=[A-Z][a-z])", RegexOptions.Compiled);

        public static string ToNicifyPascalCase(this string input)
        {
            return PascalCaseRegex.Replace(input, " ");
        }

        public static string ToBoldStyle(this string input)
        {
            return $"<b>{input}</b>";
        }
    }
}
