using System.Text.Json;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// Interface that identifies a <see cref="MultiPoint"/>
    /// </summary>
    public interface IMultiPoint : ICloneable
    {
        /// <summary>
        /// <inheritdoc cref="MultiPoint.Coordinates"/>
        /// </summary>
        List<Position> Coordinates { get; set; }
    }

    /// <summary>
    /// <see cref="GeoJsonType.MultiPoint"/>
    /// </summary>
    public class MultiPoint() : Geometry(GeometryType.MultiPoint), IMultiPoint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinates"></param>
        public MultiPoint(List<Position> coordinates) : this() => Coordinates = coordinates;

        /// <summary>
        /// List of <see cref="Position"/>
        /// </summary>
        public List<Position> Coordinates { get; set => field = value ?? []; } = [];

        /// <summary>
        /// <see cref="ICloneable.Clone"/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (MultiPoint)MemberwiseClone();
            clone.Coordinates = [.. Coordinates.Select(p => (Position)p.Clone())];
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
        public static MultiPoint? FromJson(string? json, JsonSerializerOptions? options = null)
        {
            return string.IsNullOrWhiteSpace(json) ? null : JsonSerializer.Deserialize<MultiPoint>(json, options ?? GeoJsonDomain.JsonSerializerOptions);
        }

        #endregion
    }
}
