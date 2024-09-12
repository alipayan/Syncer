namespace Syncer.APIs.Models.Domain;

public class Emoji
{
    public required string Code { get; set; }

    public required string ShortName { get; set; }


    public static Emoji Create(string code, string shortName)
    {
        var unidoce = char.ConvertToUtf32(code, 0);

        var hexadecimal = $"U+{unidoce:x4}";
        return new Emoji { Code = code, ShortName = shortName };
    }

}