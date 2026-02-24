using Marqdouj.DotNet.Web.JsInterop.GeoJson.JsonConverters;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// A GeoJSON BoundingBox. Supports exactly 4 (2D) or 6 (3D including elevation) doubles.
    /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-5"/>.
    /// </summary>
    /// <remarks>
    /// [west, south, east, north] or [west, south, elevation1, east, north, elevation2]
    /// </remarks>
    [JsonConverter(typeof(JsonBoundingBoxConverter))]
    public class BoundingBox : IReadOnlyList<double>, ICloneable
    {
        private readonly List<double> _data;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values">[west, south, east, north] or [west, south, elevation1, east, north, elevation2]</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public BoundingBox(IEnumerable<double> values)
        {
            _data = [.. values ?? throw new ArgumentNullException(nameof(values))];

            if (!IsValid)
                throw new ArgumentOutOfRangeException(nameof(values), "Values must contain exactly 4 (2D) or 6 (3D) numbers.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="west"></param>
        /// <param name="south"></param>
        /// <param name="east"></param>
        /// <param name="north"></param>
        public BoundingBox(double west, double south, double east, double north)
        {
            _data = [west, south, east, north];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="west"></param>
        /// <param name="south"></param>
        /// <param name="elevation1"></param>
        /// <param name="east"></param>
        /// <param name="north"></param>
        /// <param name="elevation2"></param>
        public BoundingBox(double west, double south, double elevation1, double east, double north, double elevation2)
        {
            _data = [west, south, elevation1, east, north, elevation2];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="southwest"></param>
        /// <param name="northeast"></param>
        /// <exception cref="ArgumentException"></exception>
        public BoundingBox(Position southwest, Position northeast)
        {
            _data = ConvertToList(southwest, northeast);
        }

        private static List<double> ConvertToList(Position southwest, Position northeast)
        {
            if (!southwest.IsValid)
                throw new ArgumentException("Southwest position is not valid.");
            if (!northeast.IsValid)
                throw new ArgumentException("Northeast position is not valid.");
            if (southwest.Count != northeast.Count)
                throw new ArgumentException("Southwest and Northeast counts do not match. Both positions must be either 2D or 3D.");

            var values = new List<double>();

            if (southwest.Is3D) //counts match so northwest is also 3D
            {
                values.Add(southwest.Longitude);
                values.Add(southwest.Latitude);
                values.Add(southwest.Elevation!.Value);
                values.Add(northeast.Longitude);
                values.Add(northeast.Latitude);
                values.Add(northeast.Elevation!.Value);
            }
            else
            {
                values.Add(southwest.Longitude);
                values.Add(southwest.Latitude);
                values.Add(northeast.Longitude);
                values.Add(northeast.Latitude);
            }

            return values;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [JsonIgnore] public double this[int index] => _data[index];

        /// <summary>
        /// <see cref="List{T}.Count"/>
        /// </summary>
        [JsonIgnore] public int Count => _data.Count;

        /// <summary>
        /// <see cref="IEnumerable.GetEnumerator"/>
        /// </summary>
        /// <returns></returns>
        public IEnumerator<double> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// <see cref="ICloneable"/>
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new BoundingBox(this);
        }

        #region Non-GeoJSON Helpers

        /// <summary>
        /// BoundingBox has [west, south, east, north].
        /// </summary>
        [JsonIgnore] public bool Is2D => Count == 4;

        /// <summary>
        /// BoundingBox has [west, south, elevation1, east, north, elevation2].
        /// </summary>
        [JsonIgnore] public bool Is3D => Count == 6;

        /// <summary>
        /// BoundingBox is either 2D or 3D. <see cref="Is2D"/> <see cref="Is3D"/>
        /// </summary>
        [JsonIgnore] public bool IsValid => Is2D || Is3D;

        /// <summary>
        /// Southwest position of BoundingBox.
        /// </summary>
        /// <returns>If IsValid then returns a Position object; otherwise null</returns>
        [JsonIgnore]
        public Position? Southwest
        {
            get
            {
                if (IsValid)
                    return Is2D ? new Position(this[0], this[1]) : new Position(this[0], this[1], this[2]);

                return null;
            }
        }

        /// <summary>
        /// Northeast position of BoundingBox.
        /// </summary>
        /// <returns>If IsValid then returns a Position object; otherwise null</returns>
        [JsonIgnore]
        public Position? Northeast
        {
            get
            {
                if (IsValid)
                    return Is2D ? new Position(this[2], this[3]) : new Position(this[3], this[4], this[5]);

                return null;
            }
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Serializes to Json.
        /// </summary>
        /// <returns></returns>
        public string ToJson(JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(this, options ?? GeoJsonDomain.JsonSerializerOptions);
        }

        /// <summary>
        /// Deserializes from Json.
        /// </summary>
        /// <returns></returns>
        public static BoundingBox? FromJson(string? json, JsonSerializerOptions? options = null)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;

            var result = JsonSerializer.Deserialize<List<double>>(json, options ?? GeoJsonDomain.JsonSerializerOptions);
            return result is null ? null : new BoundingBox(result);
        }

        #endregion

        /// <summary>
        /// <see cref="object.ToString()"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "[" + string.Join(", ", this) + "]";
        }
    }
}
