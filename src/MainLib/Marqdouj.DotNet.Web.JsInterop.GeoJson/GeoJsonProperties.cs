namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    /// <summary>
    /// Typical GeoJSON properties: <![CDATA[IDictionary<string, object?>]]>
    /// </summary>
    public class GeoJsonProperties : Dictionary<string, object?>, ICloneable
    {
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
