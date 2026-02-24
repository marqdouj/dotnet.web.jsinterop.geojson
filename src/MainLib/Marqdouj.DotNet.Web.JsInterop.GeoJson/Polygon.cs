using System.Text.Json;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// <see cref="GeoJsonType.Polygon"/>
    /// </summary>
    public class Polygon() : Geometry(GeometryType.Polygon)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinates"></param>
        public Polygon(List<List<Position>> coordinates) : this() => Coordinates = coordinates;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinates"></param>
        public Polygon(List<Position> coordinates) : this() => Coordinates = [coordinates];

        /// <summary>
        /// 
        /// </summary>
        public List<List<Position>> Coordinates { get; set => field = value ?? []; } = [];

        /// <summary>
        /// <see cref="ICloneable.Clone"/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (Polygon)MemberwiseClone();
            clone.Bbox = (BoundingBox?)(Bbox?.Clone());

            var coordinates = new List<Position>();

            foreach (var coords in Coordinates)
            {
                var items = coords.Select(x => (Position)x.Clone()).ToList();
                coordinates.AddRange(items);
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
        public static Polygon? FromJson(string? json, JsonSerializerOptions? options = null)
        {
            return string.IsNullOrWhiteSpace(json) ? null : JsonSerializer.Deserialize<Polygon>(json, options ?? GeoJsonDomain.JsonSerializerOptions);
        }

        #endregion
    }
}
