using System.Text.RegularExpressions;

namespace WorkEnv.Application.Extensions;

public static partial class NameValidator
{
    public static bool IsValidName(this string name)
    {
        if (string.IsNullOrEmpty(name))
            return false;

        if (name[0].Equals(name[0].ToString().ToLower()))
            return false;
        
        return true;
    }
}