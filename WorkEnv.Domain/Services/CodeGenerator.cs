using System.Text;

namespace WorkEnv.Domain.Services;

public class CodeGenerator
{
    private const int PASSWORDLENGTH = 6;
    private const string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string LowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
    private const string Digits = "0123456789";
    
    /// <summary>
    /// Generate a 6 digits string code with at least 1 uppercase character, 1 lowercase character and 1 digit.
    /// </summary>
    /// <returns>string</returns>
    /// <example>
    /// var inviteCode = CodeGenerator.GenerateCode();
    /// Console.WriteLine(inviteCode) // 1a8BCd
    /// </example>
    /// <returns></returns>
    public static string GenerateCode()
    {
        var allCharacters = UpperCaseLetters + LowerCaseLetters + Digits;
        var random = new Random();
        
        var code = new StringBuilder(PASSWORDLENGTH);

        // Garante que pelo menos um caractere de cada conjunto seja incluído
        code.Append(UpperCaseLetters[random.Next(UpperCaseLetters.Length)]);
        code.Append(LowerCaseLetters[random.Next(LowerCaseLetters.Length)]);
        code.Append(Digits[random.Next(Digits.Length)]);

        // Preenche o restante da senha com caracteres aleatórios
        for (var i = code.Length; i < PASSWORDLENGTH; i++)
        {
            code.Append(allCharacters[random.Next(allCharacters.Length)]);
        }

        // Embaralha a senha para torná-la imprevisível
        char[] codeArray = code.ToString().ToCharArray();
        for (int i = codeArray.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (codeArray[i], codeArray[j]) = (codeArray[j], codeArray[i]);
        }

        return new string(codeArray);
    }
}