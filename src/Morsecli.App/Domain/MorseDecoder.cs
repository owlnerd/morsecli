using System;
using System.Collections.Generic;
using System.Text;

namespace Morsecli.Domain;

internal class MorseDecoder
{
    private static readonly Dictionary<string, char> _decodingMap = new()
    {
        [".-"] = 'A',
        ["-..."] = 'B',
        ["-.-."] = 'C',
        ["-.."] = 'D',
        ["."] = 'E',
        ["..-."] = 'F',
        ["--."] = 'G',
        ["...."] = 'H',
        [".."] = 'I',
        [".---"] = 'J',
        ["-.-"] = 'K',
        [".-.."] = 'L',
        ["--"] = 'M',
        ["-."] = 'N',
        ["---"] = 'O',
        [".--."] = 'P',
        ["--.-"] = 'Q',
        [".-."] = 'R',
        ["..."] = 'S',
        ["-"] = 'T',
        ["..-"] = 'U',
        ["...-"] = 'V',
        [".--"] = 'W',
        ["-..-"] = 'X',
        ["-.--"] = 'Y',
        ["--.."] = 'Z',
        ["-----"] = '0',
        [".----"] = '1',
        ["..---"] = '2',
        ["...--"] = '3',
        ["....-"] = '4',
        ["....."] = '5',
        ["-...."] = '6',
        ["--..."] = '7',
        ["---.."] = '8',
        ["----."] = '9',
        [".-.-.-"] = '.',
        ["--..--"] = ',',
        ["..--.."] = '?',
        [".----."] = '\'',
        ["-.-.--"] = '!',
        ["-..-."] = '/',
        ["-.--."] = '(',
        ["-.--.-"] = ')',
        [".-..."] = '&',
        ["---..."] = ':',
        ["-.-.-."] = ';',
        ["-...-"] = '=',
        [".-.-."] = '+',
        ["-....-"] = '-',
        ["..--.-"] = '_',
        [".-..-."] = '"',
        ["...-..-"] = '$',
        [".--.-."] = '@'
    };

    private string _symbolSeparator = " ";
    private string _wordSeparator = " / ";

    public string SymbolSeparator
    {
        get => _symbolSeparator;
        init
        {
            if (value.Contains('.') || value.Contains('-') || string.IsNullOrEmpty(value) || _wordSeparator == value)
            {
                throw new ArgumentException("Symbol separator cannot contain . or -", nameof(value));
            }
            _symbolSeparator = value;
        }
    }

    public string WordSeparator
    {
        get => _wordSeparator;
        init
        {
            if (value.Contains('.') || value.Contains('-') || string.IsNullOrEmpty(value) || _symbolSeparator == value)
            {
                throw new ArgumentException("Word separator cannot contain . or -", nameof(value));
            }
            _wordSeparator = value;
        }
    }

    public bool DropUndecodable { get; init; }

    public string Decode(string input)
    {
        input = input.Trim();

        StringBuilder decoding = new StringBuilder();

        StringBuilder currentSymbol = new StringBuilder();

        int i = 0;
        while (i < input.Length)
        {
            if (IsSeparator(input, i, WordSeparator))
            {
                if (_decodingMap.TryGetValue(currentSymbol.ToString(), out char decodedSymbol))
                {
                    decoding.Append(decodedSymbol);
                    decoding.Append(' ');
                    i += WordSeparator.Length;
                }
                else
                {
                    HandleUndecodable(decoding, currentSymbol);
                    i += WordSeparator.Length;
                }
                currentSymbol.Clear();
            }
            else if (IsSeparator(input, i, SymbolSeparator))
            {
                if (_decodingMap.TryGetValue(currentSymbol.ToString(), out char decodedSymbola))
                {
                    decoding.Append(decodedSymbola);
                    i += SymbolSeparator.Length;
                }
                else
                {
                    HandleUndecodable(decoding, currentSymbol);
                    i += SymbolSeparator.Length;
                }
                currentSymbol.Clear();
            }
            else
            {
                currentSymbol.Append(input[i++]);
            }
        }

        if (_decodingMap.TryGetValue(currentSymbol.ToString(), out char decodedSymbole))
        {
            decoding.Append(decodedSymbole);
        }
        else
        {
            HandleUndecodable(decoding, currentSymbol);
        }

        return decoding.ToString();
    }

    private void HandleUndecodable(StringBuilder decoding, StringBuilder currentSymbol)
    {
        if (!DropUndecodable)
        {
            decoding.Append('[');
            decoding.Append(currentSymbol);
            decoding.Append(']');
        }
    }

    private bool IsSeparator(string input, int i, string separator)
    {
        return i + separator.Length <= input.Length
            && input.AsSpan(i).StartsWith(separator);
    }
}
