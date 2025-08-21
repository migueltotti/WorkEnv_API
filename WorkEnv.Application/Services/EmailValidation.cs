using System.Text.RegularExpressions;

namespace WorkEnv.Application.Extensions;

public static partial class EmailValidation
{
    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        if (MyRegex().IsMatch(email))
            return false;

        return true;
    }

    [GeneratedRegex("/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/")]
    private static partial Regex MyRegex();
}