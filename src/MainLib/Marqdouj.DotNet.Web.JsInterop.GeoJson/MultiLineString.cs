using System.Text.Json;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// <see cref="GeoJsonType.MultiLineString"/>
    /// </summary>
    public class MultiLineString() : Geometry(GeometryType.MultiLineString)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinates"></param>
        public MultiLineString(List<List<Position>> coordinates) : this() => Coordinates = coordinates;

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
            var clone = (MultiLineString)MemberwiseClone();
            clone.Bbox = (BoundingBox?)(Bbox?.Clone());

            var coordinates = new List<List<Position>>();
            foreach (var line in Coordinates)
            {
                var items = line.Select(p => (Position)p.Clone()).ToList();
                coordinates.Add(items); 
            }
            clone.Coordinates = coordinates;

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
        public static MultiLineString? FromJson(string? json, JsonSerializerOptions? options = null)
        {
            return string.IsNullOrWhiteSpace(json) ? null : JsonSerializer.Deserialize<MultiLineString>(json, options ?? GeoJsonDomain.JsonSerializerOptions);
        }

        #endregion
    }
}
