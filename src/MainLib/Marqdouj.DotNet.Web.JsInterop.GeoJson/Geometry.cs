using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// Interface for all Geometry types <see cref="GeometryType"/>.
    /// </summary>
    public interface IGeometry : IGeoJSON, ICloneable
    {
        /// <summary>
        /// <see cref="GeometryType"/>
        /// </summary>
        GeometryType Type { get; }
    }

    /// <summary>
    /// Base class for all Geometry types <see cref="GeometryType"/>.
    /// </summary>
    public abstract class Geometry(GeometryType type) : GeoJsonObject((GeoJsonType)type), IGeometry
    {
        /// <summary>
        /// <see cref="GeometryType"/>
        /// </summary>
        [JsonPropertyOrder(-1)]
        [JsonInclude]
        public new GeometryType Type { get => (GeometryType)base.Type; internal set => base.Type = (GeoJsonType)value; }
    }
}