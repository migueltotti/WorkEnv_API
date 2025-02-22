using System.Text.RegularExpressions;

namespace WorkEnv.Application.Extensions;

public static partial class NameValidator
{
    public static bool IsValidPassword(this string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (MyRegex().IsMatch(password))
            return false;
        
        return true;
    }

    [GeneratedRegex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,30}$")]
    private static partial Regex MyRegex();
}