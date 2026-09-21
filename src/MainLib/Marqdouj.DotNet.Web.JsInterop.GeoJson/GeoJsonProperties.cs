namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// Typical GeoJSON properties: <![CDATA[IDictionary<string, object?>]]>
    /// </summary>
    public class GeoJsonProperties : Dictionary<string, object?>, ICloneable
    {
        /// <summary>
        /// Parmeterless constructor.
        /// </summary>
        public GeoJsonProperties() : base() { }

        /// <summary>
        /// Constructor with StringComparer.
        /// </summary>
        /// <param name="stringComparer"></param>
        public GeoJsonProperties(StringComparer stringComparer) : base(stringComparer) { }

        /// <summary>
        /// <see cref="object.MemberwiseClone"/>
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
