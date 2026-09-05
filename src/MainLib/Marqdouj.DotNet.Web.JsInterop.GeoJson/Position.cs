using Marqdouj.DotNet.Web.JsInterop.GeoJson.JsonConverters;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// A GeoJSON Position - a geographical location specifying
    /// longitude and latitude in decimal degrees, and optionally an elevation in meters.
    /// <see href="https://tools.ietf.org/html/rfc7946#section-3.1.1"/>
    /// </summary>
    /// <remarks>
    /// [longitude, latitude] or [longitude, latitude, elevation]
    /// </remarks>
    [JsonConverter(typeof(JsonPositionConverter))]
    public class Position : IReadOnlyList<double>, IGeoJSON, ICloneable, IEquatable<Position>
    {
        private readonly List<double> _data;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values">[longitude, latitude] or [longitude, latitude, elevation]</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Position(IEnumerable<double> values)
        {
            _data = [.. values ?? throw new ArgumentNullException(nameof(values))];

            if (!IsValid)
                throw new ArgumentOutOfRangeException(nameof(values), "Values must be 2D (lon, lat) or 3D (lon, lat, elev).");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        public Position(double longitude, double latitude)
        {
            _data = [longitude, latitude];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="elevation"></param>
        public Position(double longitude, double latitude, double? elevation) : this(longitude, latitude)
        {
            if (elevation != null)
                _data.Add(elevation.Value);
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
            return new Position(this);
        }

        #region Non-GeoJSON Helpers
        //Not part of the GeoJSON specification, but useful for convenience.

        /// <summary>
        /// Position has longitude and latitude.
        /// </summary>
        [JsonIgnore] public bool Is2D => Count == 2;

        /// <summary>
        /// Position has longitude, latitude and elevation.
        /// </summary>
        [JsonIgnore] public bool Is3D => Count == 3;

        /// <summary>
        /// Position is either 2D or 3D. <see cref="Is2D"/> <see cref="Is3D"/>
        /// </summary>
        [JsonIgnore] public bool IsValid => Is2D || Is3D;

        /// <summary>
        /// Longitude in decimal degrees.
        /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-4"/>
        /// </summary>
        [JsonIgnore]
        public double Longitude { get => _data[0]; set => _data[0] = value; }

        /// <summary>
        /// Latitude in decimal degrees.
        /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-4"/>
        /// </summary>
        [JsonIgnore]
        public double Latitude { get => _data[1]; set => _data[1] = value; }

        /// <summary>
        /// Elevation in meters.
        /// <see href="https://datatracker.ietf.org/doc/html/rfc7946#section-4"/>
        /// </summary>
        [JsonIgnore]
        public double? Elevation 
        { 
            get => Is3D ? _data[2] : null; 
            set 
            {
                if (value != null)
                {
                    _data.EnsureCount(3, 3);
                    _data[2] = value.Value;
                }
                else
                {
                    _data.EnsureCount(2, 2);
                }
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
        public static Position? FromJson(string? json, JsonSerializerOptions? options = null)
        {
            if (string.IsNullOrWhiteSpace(json)) 
                return null;

            var result = JsonSerializer.Deserialize<List<double>>(json, options ?? GeoJsonDomain.JsonSerializerOptions);
            return result is null ? null : new Position(result);
        }

        #endregion


        /// <summary>
        /// <see cref="Object.ToString"/> using a format specifier.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
                return ToString();

            return Elevation.HasValue
                ? $"[{Longitude.ToString(format)}, {Latitude.ToString(format)}, {Elevation.Value.ToString(format)}]"
                : $"[{Longitude.ToString(format)}, {Latitude.ToString(format)}]";
        }

        /// <summary>
        /// <see cref="object.ToString()"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Elevation.HasValue
                ? $"[{Longitude}, {Latitude}, {Elevation.Value}]"
                : $"[{Longitude}, {Latitude}]";
        }

        #region IEquatable

        /// <summary>
        /// <see cref="IEquatable{T}"/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Equals(Position? other)
        {
            return other is not null && _data.SequenceEqual(other._data);
        }

        /// <summary>
        /// <see cref="IEquatable{T}"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Position);
        }

        /// <summary>
        /// <see cref="IEquatable{T}"/>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override int GetHashCode()
        {
            return this.GetListHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position1"></param>
        /// <param name="position2"></param>
        /// <returns></returns>
        public static bool operator ==(Position position1, Position position2)
        {
            if (position1 is null)
            {
                return position2 is null;
            }

            return position1.Equals(position2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position1"></param>
        /// <param name="position2"></param>
        /// <returns></returns>
        public static bool operator !=(Position position1, Position position2)
        {
            if (position1 is null)
            {
                return position2 is not null;
            }

            return !position1.Equals(position2);
        }

        #endregion
    }
}
