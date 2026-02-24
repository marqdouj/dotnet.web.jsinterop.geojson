using System.Text.Json;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson.JsonConverters
{
    /// <summary>
    /// <see cref="JsonConverter{T}"/>
    /// </summary>
    public class JsonBoundingBoxConverter : JsonConverter<BoundingBox>
    {
        /// <summary>
        /// <see cref="JsonConverter{T}.ReadAsPropertyName(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override BoundingBox? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = IReadOnlyListDoubleConverter.Read(ref reader, typeToConvert, options);
            return new BoundingBox(list);
        }

        /// <summary>
        /// <see cref="JsonConverter{T}.Write(Utf8JsonWriter, T, JsonSerializerOptions)"/>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, BoundingBox value, JsonSerializerOptions options)
        {
            IReadOnlyListDoubleConverter.Write(writer, value, options);
        }
    }
}
