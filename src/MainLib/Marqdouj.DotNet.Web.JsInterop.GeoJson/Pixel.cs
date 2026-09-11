using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// <inheritdoc cref="Pixel"/>
    /// </summary>
    public interface IPixel : ICloneable
    {
        /// <summary>
        /// <inheritdoc cref="Pixel.X"/>
        /// </summary>
        double X { get; set; }

        /// <summary>
        /// <inheritdoc cref="Pixel.Y"/>
        /// </summary>
        double Y { get; set; }
    }

    /// <summary>
    /// Represent a pixel coordinate or offset. Extends an list of [x, y].
    /// </summary>
    public class Pixel : List<double>, IPixel
    {
        /// <summary>
        /// Json constructor for deserialization.
        /// </summary>
        [JsonConstructor]
        public Pixel()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"><see cref="X"/></param>
        /// <param name="y"><see cref="Y"/></param>
        public Pixel(double x, double y)
        {
            Add(x);
            Add(y);
        }

        /// <summary>
        /// X-axis position.
        /// </summary>
        [JsonIgnore]
        public double X
        {
            get { Verify(); return this[0]; }
            set { Verify(); this[0] = value; }
        }

        /// <summary>
        /// Y-axis position
        /// </summary>
        [JsonIgnore]
        public double Y
        {
            get { Verify(); return this[1]; }
            set { Verify(); this[1] = value; }
        }

        /// <summary>
        /// Checks if list has the minimum required elements (X, Y); if not adds them
        /// </summary>
        internal void Verify()
        {
            this.EnsureCount(2, 2);
        }

        /// <summary>
        /// <see cref="object.ToString()"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "[" + string.Join(", ", this) + "]";

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new Pixel(X, Y); ;
        }
    }
}
