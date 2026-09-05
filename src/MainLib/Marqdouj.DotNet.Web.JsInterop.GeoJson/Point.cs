using System.Text.Json;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// Interface that identifies a <see cref="Point"/>
    /// </summary>
    public interface IPoint : ICloneable
    {
        /// <summary>
        /// <inheritdoc cref="Point.Coordinates"/>
        /// </summary>
        Position Coordinates { get; set; }
    }

    /// <summary>
    /// <see cref="GeoJsonType.Point"/>
    /// </summary>
    public class Point() : Geometry(GeometryType.Point), IPoint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinates"></param>
        public Point(Position coordinates) : this() => Coordinates = coordinates;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="elevation"></param>
        public Point(double longitude, double latitude, double? elevation = null) : this() => Coordinates = new(longitude, latitude, elevation);

        /// <summary>
        /// <see cref="Position"/>
        /// </summary>
        public Position Coordinates { get; set => field = value ?? new(0, 0); } = new(0, 0);

        /// <summary>
        /// <see cref="ICloneable.Clone"/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (Point)MemberwiseClone();
            clone.Bbox = (BoundingBox?)(Bbox?.Clone());
            clone.Coordinates = (Position)Coordinates.Clone();
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
        public static Point? FromJson(string? json, JsonSerializerOptions? options = null)
        {
            return string.IsNullOrWhiteSpace(json) ? null : JsonSerializer.Deserialize<Point>(json, options ?? GeoJsonDomain.JsonSerializerOptions);
        }

        #endregion
    }
}
