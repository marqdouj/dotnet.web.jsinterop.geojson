using Marqdouj.DotNet.Web.JsInterop.GeoJson;

namespace SandboxTests
{
    [TestClass]
    public sealed class EnumTests
    {
        [TestMethod]
        public void GeoJsonType_IsGeometryType()
        {
            var geometryTypes = Enum.GetValues<GeometryType>().ToList();
            var geoJsonTypes = Enum.GetValues<GeoJsonType>().ToList();

            for (int i = 0; i < geometryTypes.Count; i++)
            {
                Assert.IsTrue((geoJsonTypes[i]).IsGeometryType(geometryTypes[i]));
            }
        }

        [TestMethod]
        public void GeoJsonType_IsGeometryType_Invalid()
        {
            var geometryTypes = Enum.GetValues<GeometryType>().ToList();
            List<GeoJsonType> geoJsonTypes = [GeoJsonType.Feature, GeoJsonType.FeatureCollection];

            for (int i = 0; i < geometryTypes.Count; i++)
            {
                var geometryType = geometryTypes[i];

                foreach (var geoJsonType in geoJsonTypes)
                {
                    Assert.IsFalse(geoJsonType.IsGeometryType(geometryType));
                }
            }
        }
    }
}
