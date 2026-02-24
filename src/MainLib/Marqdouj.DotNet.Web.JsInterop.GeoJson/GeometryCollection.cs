using System.Text.Json;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// GeometryCollection: List of Geometry objects.
    /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.8"/>
    /// </summary>
    public class GeometryCollection() : Geometry(GeometryType.GeometryCollection)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="geometries"><see cref="Geometry"/></param>
        /// <param name="bbox"><see cref="BoundingBox"/></param>
        public GeometryCollection(IEnumerable<Geometry> geometries, BoundingBox? bbox = null) : this() 
        {
            Geometries = [.. geometries];
            Bbox = bbox;
        }

        /// <summary>
        /// Readonly List of geometries.
        /// </summary>
        public List<Geometry> Geometries { get; set => field = value ?? []; } = [];

        /// <summary>
        /// <see cref="ICloneable.Clone"/>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object Clone()
        {
            var clone = (GeometryCollection)MemberwiseClone();

            // Deep clone the geometries list
            clone.Geometries = [.. Geometries.Select(g => (Geometry)g.Clone())];
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
            var jsonObj = new GeometryCollectionJson(this);
            return JsonSerializer.Serialize(jsonObj, options ?? GeoJsonDomain.JsonSerializerOptions);
        }

        /// <summary>
        /// Deserializes from Json.
        /// </summary>
        /// <returns></returns>
        public static GeometryCollection? FromJson(string? json, JsonSerializerOptions? options = null)
        {
            var result = string.IsNullOrWhiteSpace(json) ? null : JsonSerializer.Deserialize<GeometryCollectionJson>(json, options ?? GeoJsonDomain.JsonSerializerOptions);
            if (result == null) return null;
            var geometries = new List<Geometry>();

            foreach (var geom in result.Geometries)
            {
                if (geom is JsonElement value)
                {
                    var type = value.GetProperty("type").GetString() ?? "";
                    var rawText = value.GetRawText();

                    if (Enum.IsDefined(typeof(GeometryType), type))
                    {
                        var gemometryType = Enum.Parse<GeometryType>(type);
                        Geometry? output = null;

                        switch (gemometryType)
                        {
                            case GeometryType.Point:
                                output = Point.FromJson(rawText, options);
                                break;
                            case GeometryType.MultiPoint:
                                output = MultiPoint.FromJson(rawText, options);
                                break;
                            case GeometryType.LineString:
                                output = LineString.FromJson(rawText, options);
                                break;
                            case GeometryType.MultiLineString:
                                output = MultiLineString.FromJson(rawText, options);
                                break;
                            case GeometryType.Polygon:
                                output = Polygon.FromJson(rawText, options);
                                break;
                            case GeometryType.MultiPolygon:
                                output = MultiPolygon.FromJson(rawText, options);
                                break;
                            case GeometryType.GeometryCollection:
                                // Recursive call to handle nested GeometryCollection
                                output = GeometryCollection.FromJson(rawText, options);
                                break;
                            default:
                                break;
                        }

                        if (output != null) geometries.Add(output);
                    }
                    else
                    {
                        throw new Exception($"Unable to parse GeometryCollection item: {rawText}");
                    }
                }
                else
                {
                    throw new Exception($"Unable to parse GeometryCollection item: {geom}");
                }
            }

            return new GeometryCollection(geometries, result.Bbox);
        }

        #endregion
    }

    /// <summary>
    /// Helper class used for Serialization only.
    /// The Geometries property is defined as <![CDATA[List<object>]]> to allow deserialization of heterogeneous geometry types.
    /// </summary>
    public class GeometryCollectionJson() : Geometry(GeometryType.GeometryCollection)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public GeometryCollectionJson(GeometryCollection source) : this()
        {
            Bbox = source.Bbox;
            Geometries = [.. source.Geometries];
        }

        /// <summary>
        /// 
        /// </summary>
        public List<object> Geometries { get; set => field = value ?? []; } = [];

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        override public object Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        override public string ToJson(JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(this, options ?? GeoJsonDomain.JsonSerializerOptions);
        }
    }
}
