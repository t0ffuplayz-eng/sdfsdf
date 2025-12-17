namespace Com.SebiBin.Validtors;

using System.Text.RegularExpressions;

/// <summary>
/// Klasa walidatora cyfry kontrolnej numerów: dowodu osobistego, mDowodu, karty pobytu i paszportu
/// </summary>
public static class DocumentNumberValidator
{
    public static Regex validNumbersRegex = new Regex(
        "(^([A-Z]{4}[0-9]{5})$)|(^([A-Z]{3}[0-9]{6})$)|(^([A-Z]{2}[0-9]{7})$)",
        RegexOptions.Multiline
        | RegexOptions.Singleline
        | RegexOptions.IgnorePatternWhitespace
        | RegexOptions.Compiled
    );

    public static Regex seriaNumberRegex = new Regex(
        "^(?<seria>[A-Z]{2,4})(?<checkDigit>[0-9])(?<number>[0-9]{4,6})$",
        RegexOptions.Multiline
        | RegexOptions.Singleline
        | RegexOptions.IgnorePatternWhitespace
        | RegexOptions.Compiled
    );
    
    private const string LetterValues = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static readonly int[] Weights = new[] { 7, 3, 1, 7, 3, 1, 7, 3, 1 };
    
    /// <summary>
    /// Sprawdza poprawność cyfry kontrolnej numerów: dowodu osobistego, mDowodu, karty pobytu i paszportu
    /// </summary>
    /// <param name="docNumber"></param>
    /// <returns></returns>
    public static bool IsDocumentNumberCorrect(this string? docNumber)
    {
        if (docNumber == null)
        {
            return false;
        }

        var numberToCheck = docNumber.Replace(" ", "").ToUpper();
        if (!validNumbersRegex.IsMatch(numberToCheck))
        {
            return false; // Niepoprawny
        }
        
        var match = seriaNumberRegex.Match(numberToCheck);

        var withoutControlDigit = $"{match.Groups["seria"].Value}{match.Groups["number"].Value}"; // pomijamy cyfrę kontrolną
        if (!int.TryParse(match.Groups["checkDigit"].Value, out var currentCheckControl))
        {
            return false;
        }
        var checkSum = 0;
        for (var counter = 0; counter < withoutControlDigit.Length; counter += 1)
        {
            checkSum += LetterValues.IndexOf(withoutControlDigit[counter]) * Weights[counter];
        }

        return currentCheckControl == (checkSum % 10);
    }
}
