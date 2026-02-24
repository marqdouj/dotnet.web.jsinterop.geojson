using System.Text.Json;

namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    internal static class GeoJsonDomain
    {
        internal static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);

        #region List HashCode

        /// <summary>
        /// Computes a deterministic hash code for a list of doubles, respecting order.
        /// </summary>
        internal static int GetListHashCode(this IEnumerable<double> list)
        {
            ArgumentNullException.ThrowIfNull(list);

            unchecked // Allow arithmetic overflow without exceptions
            {
                int hash = 17;
                foreach (var item in list)
                {
                    // Use BitConverter to ensure double's bit pattern is hashed
                    hash = hash * 31 + BitConverter.DoubleToInt64Bits(item).GetHashCode();
                }
                return hash;
            }
        }

        /// <summary>
        /// Compares two lists by their computed hash codes.
        /// </summary>
        internal static bool HasSameHash(this IEnumerable<double> list1, IEnumerable<double> list2)
        {
            return GetListHashCode(list1) == GetListHashCode(list2);
        }

        #endregion
    }
}
