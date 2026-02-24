using System.Text.Json;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// The base GeoJSON object containing the "type" discriminator and optional bounding box.
    /// All other geojson classes inherit from this.
    /// </summary>
    public abstract class GeoJsonObject(GeoJsonType type) : ICloneable
    {
        /// <summary>
        /// <see cref="GeoJsonType"/>
        /// </summary>
        [JsonPropertyOrder(-1)]
        [JsonInclude]
        public virtual GeoJsonType Type { get; internal set; } = type;

        /// <summary>
        /// <see cref="BoundingBox"/>
        /// </summary>
        public BoundingBox? Bbox { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract object Clone();

        /// <summary>
        /// Deserializes the GeoJsonObject to a JSON string.
        /// </summary>
        /// <returns></returns>
        public abstract string ToJson(JsonSerializerOptions? options = null);
    }
}
