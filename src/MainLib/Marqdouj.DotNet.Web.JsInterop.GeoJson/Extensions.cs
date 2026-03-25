namespace Marqdouj.DotNet.Web.JsInterop.GeoJson
{
    internal static class Extensions
    {
        internal static void EnsureCount(this List<double> items, int min, int? max = null, double addDefault = 0)
        {
            while (items.Count < min)
            {
                items.Add(addDefault);
            }

            //Remove excess values
            if (max != null)
            {
                while (items.Count > max)
                {
                    items.RemoveAt(items.Count - 1);
                }
            }
        }
    }
}
