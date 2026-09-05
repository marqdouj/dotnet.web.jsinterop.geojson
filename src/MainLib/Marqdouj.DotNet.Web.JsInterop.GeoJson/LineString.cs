using System.Text.Json;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// Interface that identifies a <see cref="LineString"/>
    /// </summary>
    public interface ILineString : ICloneable
    {
        /// <summary>
        /// <inheritdoc cref="LineString.Coordinates"/>
        /// </summary>
        List<Position> Coordinates { get; set; }
    }

    /// <summary>
    /// <see cref="GeoJsonType.LineString"/>
    /// </summary>
    public class LineString() : Geometry(GeometryType.LineString), ILineString
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinates"></param>
        public LineString(List<Position> coordinates) : this() => Coordinates = coordinates;

        /// <summary>
        /// List of <inheritdoc cref="Position"/>
        /// </summary>
        public List<Position> Coordinates { get; set => field = value ?? []; } = [];

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (LineString)MemberwiseClone();
            clone.Coordinates = [.. Coordinates.Select(c => (Position)c.Clone())];
            clone.Bbox = (BoundingBox?)(Bbox?.Clone());
            return clone;
        }

        #region Serialization

        /// <summary>
        /// Serializes to Json.
        /// </summary>
        /// <returns></returns>
        public override string ToJson(JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(this, options ?? GeoJsonDomain.JsonSerializerOptions);
        }

        /// <summary>
        /// Deserializes from Json.
        /// </summary>
        /// <returns></returns>
        public static LineString? FromJson(string? json, JsonSerializerOptions? options = null)
        {
            return string.IsNullOrWhiteSpace(json) ? null : JsonSerializer.Deserialize<LineString>(json, options ?? GeoJsonDomain.JsonSerializerOptions);
        }

        #endregion
    }
}
