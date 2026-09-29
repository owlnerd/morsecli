using Morsecli.Domain;

//MorseEncoder encoder = new MorseEncoder((" ", " / ", true);

var encoder = new MorseEncoder()
{
    WordSeparator = " / ",
    ReduceWordSeparators = true,
    DropUnencodable = true,
};

string input = "Jebeno sranje je nepodnosljivo.";
string output = encoder.Encode(input);

Console.WriteLine($"Input: {input}");
Console.WriteLine($"Putput: {output}");

var decoder = new MorseDecoder()
{
    WordSeparator = " / ",
    SymbolSeparator = " ",
    DropUndecodable = false
};

string secondInput = ".-..kek--- . -... . -. --- / .ddfd.. .-. .- -. .--- . / .--- . / -. . .--. --- -.. -. --- ... .-.. .--- .. ...- --- .---.---.----";
string secondeInput = ".... .... /";
string secondOutput = decoder.Decode(secondeInput);
Console.WriteLine($"Putput: |{secondOutput}|");