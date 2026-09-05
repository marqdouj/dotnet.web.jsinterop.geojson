using System.Text.Json;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// Interface to identity any type of feature that implements <see cref="IFeature{G, P}"/>.
    /// </summary>
    public interface IFeature : ICloneable
    {
    }

    /// <summary>
    /// <inheritdoc cref="Feature{G, P}"/>
    /// </summary>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="P"></typeparam>
    public interface IFeature<G, P> : IFeature where G : Geometry
    {
        /// <summary>
        /// <inheritdoc cref="Feature{G, P}.Geometry"/>
        /// </summary>
        G? Geometry { get; set; }

        /// <summary>
        /// <inheritdoc cref="Feature{G, P}.Id"/>
        /// </summary>
        object? Id { get; set; }

        /// <summary>
        /// <inheritdoc cref="Feature{G, P}.Properties"/>
        /// </summary>
        P? Properties { get; set; }
    }

    /// <summary>
    /// <![CDATA[Feature<G, P>]]> — generic Feature object.
    /// G represents the geometry type (subclass of Geometry). Geometry MAY be null for a Feature.
    /// P represents properties (dictionary mapping string to any) and can be null.
    /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-3.2"/>
    /// </summary>
    public class Feature<G, P>() : GeoJsonObject(GeoJsonType.Feature), IFeature<G, P> where G : Geometry
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="geometry"><see cref="GeoJson.Geometry"/></param>
        /// <param name="properties">Typical use: <see cref="GeoJsonProperties"/></param>
        /// <param name="id"><see cref="Id"/></param>
        /// <param name="bbox"><see cref="BoundingBox"/></param>
        public Feature(G? geometry = null, P? properties = default, object? id = null, BoundingBox? bbox = null) : this()
        {
            Geometry = geometry;
            Properties = properties;
            Id = id;
            Bbox = bbox;
        }

        /// <summary>
        /// The geometry may be null (e.g., Feature with null geometry).
        /// </summary>
        public G? Geometry { get; set; }

        /// <summary>
        /// Id can be string or number in original spec; we model as object? (string, int, long, double, or null).
        /// </summary>
        public object? Id { get; set; }

        /// <summary>
        /// Properties: often a dictionary, but generic type P allows custom property containers.
        /// The original type allowed { [name: string]: any } | null. 
        /// Typical use: <see cref="GeoJsonProperties"/>
        /// </summary>
        public P? Properties { get; set; }

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
        public static Feature<G, P>? FromJson(string? json, JsonSerializerOptions? options = null)
        {
            return string.IsNullOrWhiteSpace(json) ? null : JsonSerializer.Deserialize<Feature<G, P>>(json, options ?? GeoJsonDomain.JsonSerializerOptions);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (Feature<G, P>)MemberwiseClone();

            clone.Geometry = (G?)(Geometry?.Clone());
            clone.Properties = (P?)((Properties as ICloneable)?.Clone());
            clone.Bbox = (BoundingBox?)(Bbox?.Clone());

            return clone;
        }

        #endregion
    }
}
