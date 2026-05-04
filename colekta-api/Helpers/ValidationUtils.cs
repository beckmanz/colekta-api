using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace colekta_api.Helpers;

public static class ValidationUtils
{
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
    }

    public static bool IsValidCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11) return false;

        if (new string(cpf[0], 11) == cpf) return false;

        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string dg = resto.ToString();
        tempCpf = tempCpf + dg;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        dg = dg + resto.ToString();

        return cpf.EndsWith(dg);
    }
    
    public static string GenerateUserName(string nomeCompleto)
    {
        if (string.IsNullOrWhiteSpace(nomeCompleto))
            return string.Empty;

        string firstName = nomeCompleto
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault() ?? "";

        string normalized = firstName.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var c in normalized)
        {
            var unicodeCategory = Char.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        string noAccents = sb.ToString().Normalize(NormalizationForm.FormC);

        string baseUserName = Regex.Replace(noAccents, @"[^a-zA-Z0-9]", "");
        string userName = baseUserName + Guid.NewGuid().ToString("N").Substring(0, 6);
        
        return userName;
    }

    public static string ToSlug(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        string normalizedString = text.Normalize(NormalizationForm.FormD);

        StringBuilder sb = new StringBuilder();

        foreach (char c in normalizedString)
        {
            UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
            if (category != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        string result = sb.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant();

        result = Regex.Replace(result, @"[^a-z0-9\s-]", "");

        result = Regex.Replace(result, @"[\s-]+", "-").Trim('-');

        return result;
    }
}