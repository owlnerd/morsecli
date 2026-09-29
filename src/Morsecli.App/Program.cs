using Morsecli.Domain;

//MorseEncoder encoder = new MorseEncoder((" ", " / ", true);

var encoder = new MorseEncoder()
{
    ReduceWordSeparators = true,
    DropUnencodable = true,
};

string input = "Zavisi sa koje strane pristupapmo resavanju tog problema.";

Console.WriteLine($"Input: {input}");
Console.WriteLine($"Putput: {encoder.Encode(input)}");

