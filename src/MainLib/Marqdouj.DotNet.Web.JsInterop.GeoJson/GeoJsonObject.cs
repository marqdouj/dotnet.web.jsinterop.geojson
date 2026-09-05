using System.Text.Json;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// Interface used to identify objects that derive from <see cref="GeoJsonObject"/>.
    /// Helps with type checking and casting of types, especially when working with c# `Generics`.
    /// </summary>
    public interface IGeoJsonObject : IGeoJSON
    {
        /// <summary>
        /// <see cref="BoundingBox"/>
        /// </summary>
        BoundingBox? Bbox { get; set; }

        /// <summary>
        /// <see cref="GeoJsonType"/>
        /// </summary>
        GeoJsonType Type { get; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        object Clone();

        /// <summary>
        /// Deserializes the GeoJsonObject to a JSON string.
        /// </summary>
        /// <returns></returns>
        string ToJson(JsonSerializerOptions? options = null);
    }

    /// <summary>
    /// The base GeoJSON object containing the "type" discriminator and optional bounding box.
    /// All other geojson classes inherit from this.
    /// </summary>
    public abstract class GeoJsonObject(GeoJsonType type) : ICloneable, IGeoJsonObject
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
        /// <inheritdoc/>
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
