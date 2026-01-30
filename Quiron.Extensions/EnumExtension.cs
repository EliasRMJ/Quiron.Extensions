using System.Globalization;

namespace Quiron.Extensions
{
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionEnumAttribute(string defaultValue, string pt = "", string es = "")
        : Attribute
    {
        public string Description { get; set; } = defaultValue;
        public string Pt { get; } = pt;
        public string Es { get; } = es;
    }

    public static class EnumExtension
    {
        public static string? GetDescription(this Enum value, CultureInfo? culture = null)
        {
            try
            {
                var type = value.GetType();
                var info = type.GetField(value.ToString());

                if (info != null)
                {
                    var atributos = info.GetCustomAttributes(typeof(DescriptionEnumAttribute), false) as DescriptionEnumAttribute[];
                    var name = atributos!.Length > 0 ? atributos[0].Description : null;

                    if (culture != null && !string.IsNullOrWhiteSpace(culture.Name))
                    {
                        if (culture.Name == "pt" || culture.Name == "pt-BR")
                            name = atributos[0].Pt;
                        else if (culture.Name == "es" || culture.Name == "es-ES")
                            name = atributos[0].Es;
                    }

                    return name;
                }
                else { return string.Empty; }
            }
            catch
            {
                return $"Unknown ({value})";
            }
        }
    }
}