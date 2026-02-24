using System.Text.Json;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson.JsonConverters
{
    internal static class IReadOnlyListDoubleConverter
    {
#pragma warning disable IDE0060 // Remove unused parameter
        public static IReadOnlyList<double> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            // Handle null JSON
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null!;
            }

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException($"Expected StartArray token, but got {reader.TokenType}.");
            }

            var list = new List<double>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    return list.AsReadOnly();
                }

                if (reader.TokenType != JsonTokenType.Number)
                {
                    throw new JsonException($"Expected Number token in array, but got {reader.TokenType}.");
                }

                if (!reader.TryGetDouble(out double value))
                {
                    throw new JsonException("Invalid number format for double.");
                }

                list.Add(value);
            }

            throw new JsonException("Unexpected end of JSON while reading IReadOnlyList<double>.");
        }

        public static void Write(Utf8JsonWriter writer, IReadOnlyList<double> value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartArray();
            foreach (var number in value)
            {
                writer.WriteNumberValue(number);
            }
            writer.WriteEndArray();
        }
    }
}
