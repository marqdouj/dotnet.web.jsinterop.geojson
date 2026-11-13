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

        [TestMethod]
        public void GeometryType_ToGeometryType_True()
        {
            var target = GeometryType.GeometryCollection;
            var value = target.ToString();

            var result = value.ToGeometryType();

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GeoJsonType_ToGeoJsonType_True()
        {
            var target = GeoJsonType.GeometryCollection;
            var value = target.ToString();

            var result = value.ToGeoJsonType();

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GeometryType_ToGeometryType_False()
        {
            var target = GeometryType.GeometryCollection;
            var value = "badvalue";

            var result = value.ToGeometryType();

            Assert.AreNotEqual(target, result);
        }

        [TestMethod]
        public void GeoJsonType_ToGeoJsonType_False()
        {
            var target = GeoJsonType.GeometryCollection;
            var value = "badvalue";

            var result = value.ToGeoJsonType();

            Assert.AreNotEqual(target, result);
        }
    }
}
