using System.Text;

namespace Morsecli.Domain;

internal class MorseEncoder
{
    private static readonly Dictionary<char, string> _encodingMap = new()
    {
        ['A'] = ".-",
        ['B'] = "-...",
        ['C'] = "-.-.",
        ['D'] = "-..",
        ['E'] = ".",
        ['F'] = "..-.",
        ['G'] = "--.",
        ['H'] = "....",
        ['I'] = "..",
        ['J'] = ".---",
        ['K'] = "-.-",
        ['L'] = ".-..",
        ['M'] = "--",
        ['N'] = "-.",
        ['O'] = "---",
        ['P'] = ".--.",
        ['Q'] = "--.-",
        ['R'] = ".-.",
        ['S'] = "...",
        ['T'] = "-",
        ['U'] = "..-",
        ['V'] = "...-",
        ['W'] = ".--",
        ['X'] = "-..-",
        ['Y'] = "-.--",
        ['Z'] = "--..",
        ['0'] = "-----",
        ['1'] = ".----",
        ['2'] = "..---",
        ['3'] = "...--",
        ['4'] = "....-",
        ['5'] = ".....",
        ['6'] = "-....",
        ['7'] = "--...",
        ['8'] = "---..",
        ['9'] = "----.",
        ['.'] = ".-.-.-",
        [','] = "--..--",
        ['?'] = "..--..",
        ['\''] = ".----.",
        ['!'] = "-.-.--",
        ['/'] = "-..-.",
        ['('] = "-.--.",
        [')'] = "-.--.-",
        ['&'] = ".-...",
        [':'] = "---...",
        [';'] = "-.-.-.",
        ['='] = "-...-",
        ['+'] = ".-.-.",
        ['-'] = "-....-",
        ['_'] = "..--.-",
        ['"'] = ".-..-.",
        ['$'] = "...-..-",
        ['@'] = ".--.-."
    };

    public string SymbolSeparator { get; init; } = " ";
    public string WordSeparator { get; init; } = " / ";
    public bool ReduceWordSeparators { get; init; }
    public bool DropUnencodable { get; init; }

    public string Encode(string input)
    {
        input = input.Trim();

        StringBuilder encoding = new StringBuilder();

        bool lastSymbolIsWordSeparator = true;

        foreach (char currentSymbol in input)
        {
            if (_encodingMap.TryGetValue(char.ToUpperInvariant(currentSymbol), out string? encodedSymbol))
            {
                HandleEncodable(encoding, encodedSymbol, ref lastSymbolIsWordSeparator);
            }
            else if (char.IsWhiteSpace(currentSymbol))
            {
                HandleWhiteSpace(encoding, ref lastSymbolIsWordSeparator);
            }
            else
            {
                HandleUnencodable(encoding, currentSymbol, ref lastSymbolIsWordSeparator);
            }
        }

        string finalencoding = encoding.ToString();

        return finalencoding.EndsWith(WordSeparator)
            ? finalencoding.Substring(0, finalencoding.Length - WordSeparator.Length)
            : finalencoding;
    }

    private void HandleEncodable(StringBuilder encoding, string encodedSymbol, ref bool lastSymbolIsWordSeparator)
    {
        if (!lastSymbolIsWordSeparator)
        {
            encoding.Append(SymbolSeparator);
        }
        encoding.Append(encodedSymbol);
        lastSymbolIsWordSeparator = false;
    }

    private void HandleUnencodable(StringBuilder encoding, char input, ref bool lastSymbolIsWordSeparator)
    {
        if (!DropUnencodable)
        {
            if (!lastSymbolIsWordSeparator)
            {
                encoding.Append(SymbolSeparator);
            }
            encoding.Append('[');
            encoding.Append(input);
            encoding.Append(']');
            lastSymbolIsWordSeparator = false;
        } else
        {
            lastSymbolIsWordSeparator = true;
        }
    }

    private void HandleWhiteSpace(StringBuilder encoding, ref bool lastSymbolIsWordSeparator)
    {
        if (ReduceWordSeparators)
        {
            if (!lastSymbolIsWordSeparator)
            {
                encoding.Append(WordSeparator);
            }
        }
        else
        {
            encoding.Append(WordSeparator);
        }
        lastSymbolIsWordSeparator = true;
    }
}
