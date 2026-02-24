using System.Text.Json;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// <see cref="GeoJsonType.MultiPolygon"/>
    /// </summary>
    public class MultiPolygon() : Geometry(GeometryType.MultiPolygon)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinates"></param>
        public MultiPolygon(List<List<List<Position>>> coordinates) : this() => Coordinates = coordinates;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygons"></param>
        public MultiPolygon(List<Polygon> polygons) : this() => Coordinates = [.. polygons.Select(p => p.Coordinates)];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinates"></param>
        public MultiPolygon(List<List<Position>> coordinates) : this() => Coordinates = [coordinates];

        /// <summary>
        /// 
        /// </summary>
        public List<List<List<Position>>> Coordinates { get; set => field = value ?? []; } = [];

        /// <summary>
        /// <see cref="ICloneable.Clone"/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (MultiPolygon)MemberwiseClone();
            clone.Bbox = (BoundingBox?)(Bbox?.Clone());

            var coordinates = new List<List<Position>>();
            foreach (var polygons in Coordinates)
            {
                foreach (var polygon in polygons)
                {
                    var items = polygon.Select(p => (Position)p.Clone()).ToList();
                    coordinates.Add(items);
                }
            }

            clone.Coordinates = [coordinates];

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
        public static MultiPolygon? FromJson(string? json, JsonSerializerOptions? options = null)
        {
            return string.IsNullOrWhiteSpace(json) ? null : JsonSerializer.Deserialize<MultiPolygon>(json, options ?? GeoJsonDomain.JsonSerializerOptions);
        }

        #endregion
    }
}
