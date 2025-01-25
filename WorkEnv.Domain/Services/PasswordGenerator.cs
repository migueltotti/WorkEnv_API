using System.Text;

namespace WorkEnv.Domain.Services;

public class PasswordGenerator
{
    private const int PASSWORDLENGTH = 12;
    const string upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string lowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
    const string digits = "0123456789";
    const string symbols = "!@#$%^&*()_+-=[]{}|;:'\",.<>?/`~";
    
    public static string GeneratePassword()
    {
        string allCharacters = upperCaseLetters + lowerCaseLetters + digits + symbols;
        Random random = new Random();
        
        StringBuilder password = new StringBuilder(PASSWORDLENGTH);

        // Garante que pelo menos um caractere de cada conjunto seja incluído
        password.Append(upperCaseLetters[random.Next(upperCaseLetters.Length)]);
        password.Append(lowerCaseLetters[random.Next(lowerCaseLetters.Length)]);
        password.Append(digits[random.Next(digits.Length)]);
        password.Append(symbols[random.Next(symbols.Length)]);

        // Preenche o restante da senha com caracteres aleatórios
        for (int i = password.Length; i < PASSWORDLENGTH; i++)
        {
            password.Append(allCharacters[random.Next(allCharacters.Length)]);
        }

        // Embaralha a senha para torná-la imprevisível
        char[] passwordArray = password.ToString().ToCharArray();
        for (int i = passwordArray.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (passwordArray[i], passwordArray[j]) = (passwordArray[j], passwordArray[i]);
        }

        return new string(passwordArray);
    }
}