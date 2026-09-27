using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Morsecli.App.Domain;

internal class MorseEncoder
{
    private static readonly ReadOnlyDictionary<char, string> _encodingMap = new(new Dictionary<char, string>()
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
        ['@'] = ".--.-.",
        [' '] = "/"
    });

    private string SymbolSeparator { get; }
    private string WordSeparator { get; }

    public MorseEncoder(string symbolSeparator, string wordSeparator)
    {
        SymbolSeparator = symbolSeparator;
        WordSeparator = wordSeparator;
    }

    public string Encode(string input)
    {
        StringBuilder result = new StringBuilder();

        input = input.Trim();

        string encodedSymbol;

        if (_encodingMap.TryGetValue(input[0], out encodedSymbol))
        {

        }

        for (int i = 0; i < input.Length; i++)
        {
            if (_encodingMap.TryGetValue(input[i], out encodedSymbol))
            {
                result.Append(SymbolSeparator);
                result.Append(encodedSymbol);
            }
            else if (!char.IsWhiteSpace(input[i]))
            {
                result.Append('[');
                result.Append(input[i]);
                result.Append(']');
                result.Append(SymbolSeparator);
            }
            else
            {
                result.Append(WordSeparator);
            }
        }

        return result.ToString();
    }
}
