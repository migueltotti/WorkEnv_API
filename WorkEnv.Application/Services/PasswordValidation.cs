using System.Text.RegularExpressions;

namespace WorkEnv.Application.Extensions;

public partial class PasswordValidation
{
    public static bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        return MyRegex().IsMatch(password);
    }

    [GeneratedRegex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^A-Za-z0-9]).{8,30}$")]
    private static partial Regex MyRegex();
}