using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace FitQuest.API.Converters
{
    public partial class StringConverter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString()?.Trim(); // Se GetString retorna um valor nulo, a função Trim não é executado

            if(value == null)
                return null;

            return RemoveExtraWhiteEspaces().Replace(value, " "); // Se o Regex encontrar um amontado de espaço em branco, substituir por um único
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }

        [GeneratedRegex(@"\s+")]
        private static partial Regex RemoveExtraWhiteEspaces();
    }
}
