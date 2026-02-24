using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// 'geometry type' as described in RFC.
    /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-1.4"/>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GeometryType
    {
        /// <summary>
        /// <see cref="GeoJsonType.Point"/>
        /// </summary>
        Point,

        /// <summary>
        /// <see cref="GeoJsonType.MultiPoint"/>
        /// </summary>
        MultiPoint,

        /// <summary>
        /// <see cref="GeoJsonType.LineString"/>
        /// </summary>
        LineString,

        /// <summary>
        /// <see cref="GeoJsonType.MultiLineString"/>
        /// </summary>
        MultiLineString,

        /// <summary>
        /// <see cref="GeoJsonType.Polygon"/>
        /// </summary>
        Polygon,

        /// <summary>
        /// <see cref="GeoJsonType.MultiPolygon"/>
        /// </summary>
        MultiPolygon,

        /// <summary>
        /// <see cref="GeoJsonType.GeometryCollection"/>
        /// </summary>
        GeometryCollection,
    }

    /// <summary>
    /// 'GeoJSON types' as described in RFC.
    /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-1.4"/>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GeoJsonType
    {
        /// <summary>
        /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.2"/>
        /// </summary>
        Point,

        /// <summary>
        /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.3"/>
        /// </summary>
        MultiPoint,

        /// <summary>
        /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.4"/>
        /// </summary>
        LineString,

        /// <summary>
        /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.5"/>
        /// </summary>
        MultiLineString,

        /// <summary>
        /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.6"/>
        /// </summary>
        Polygon,

        /// <summary>
        /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.7"/>
        /// </summary>
        MultiPolygon,

        /// <summary>
        /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.8"/>
        /// </summary>
        GeometryCollection,

        /// <summary>
        /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-3.2"/>
        /// </summary>
        Feature,

        /// <summary>
        /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-3.3"/>
        /// </summary>
        FeatureCollection
    }

    /// <summary>
    /// 
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Checks if a <see cref="GeoJsonType"/> is the same value as a <see cref="GeometryType"/>
        /// </summary>
        /// <param name="enum1"></param>
        /// <param name="enum2"></param>
        /// <returns></returns>
        public static bool IsGeometryType(this GeoJsonType enum1, GeometryType enum2)
        {
            //var a = (int) enum1;
            //var b = (int) enum2;
            //return a == b;
            return enum1 == (GeoJsonType)enum2;
        }
    }
}
