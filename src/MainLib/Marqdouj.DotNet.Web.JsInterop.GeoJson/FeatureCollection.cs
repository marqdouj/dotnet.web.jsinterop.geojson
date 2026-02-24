using System.Text.Json;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// <![CDATA[FeatureCollection<G, P>]]> — collection of features.
    /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-3.3"/>
    /// </summary>
    public class FeatureCollection<G, P>() : GeoJsonObject(GeoJsonType.FeatureCollection) where G : Geometry
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="features"><see cref="Feature{G, P}"/></param>
        /// <param name="bbox"><see cref="BoundingBox"/></param>
        /// <exception cref="ArgumentException"></exception>
        public FeatureCollection(IEnumerable<Feature<G, P>>? features = null, BoundingBox? bbox = null) :this()
        {
            Features = features?.ToList();
            Bbox = bbox;
        }

        /// <summary>
        /// List of features.
        /// </summary>
        public List<Feature<G, P>>? Features { get; set; }

        /// <summary>
        /// <see cref="ICloneable.Clone"/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (FeatureCollection<G, P>)MemberwiseClone();
            clone.Features = Features?.Select(f => (Feature<G, P>)f.Clone()).ToList();
            clone.Bbox = (BoundingBox?)Bbox?.Clone();
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
        /// Deserializes json to a strongly typed <see cref="FeatureCollection{G, P}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static FeatureCollection<T, GeoJsonProperties>? FromJson<T>(string? json, JsonSerializerOptions? options = null) where T : Geometry
        {
            return string.IsNullOrWhiteSpace(json) ? null : JsonSerializer.Deserialize<FeatureCollection<T, GeoJsonProperties>>(json, options ?? GeoJsonDomain.JsonSerializerOptions);
        }

        #endregion
    }
}
