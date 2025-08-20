using System.Text.RegularExpressions;

namespace WorkEnv.Application.Services;

public class CpfCnpjValidation
{
    public static bool IsCpfOrCnpjValid(string? cpfOrCnpj)
    {
        return Regex.IsMatch(cpfOrCnpj,
            @"^([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})$");
    }
}