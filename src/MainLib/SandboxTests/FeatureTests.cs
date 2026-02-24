using Marqdouj.DotNet.Web.JsInterop.GeoJson;

namespace SandboxTests
{
    [TestClass]
    public sealed class FeatureTests
    {
        #region Serialization

        #region Point


        [TestMethod]
        public void Feature_ToJson_Point()
        {
            //Arrange
            //Act
            var json = DataService.GetPointFeature().ToJson();
            //Console.WriteLine(json);

            //Assert
            Assert.AreEqual(DataService.JsonFeaturePoint, json);
        }

        [TestMethod]
        public void Feature_FromJson_NullOrWhiteSpace()
        {
            //Arrange
            //Act
            var featureEmpty = Feature<Point, GeoJsonProperties>.FromJson("");
            var featureNull = Feature<Point, GeoJsonProperties>.FromJson(null);
            var featureWhiteSpace = Feature<Point, GeoJsonProperties>.FromJson(null);

            //Assert
            Assert.IsNull(featureEmpty);
            Assert.IsNull(featureNull);
            Assert.IsNull(featureWhiteSpace);
        }

        [TestMethod]
        public void Feature_FromJson_Point()
        {
            //Arrange
            //Act
            var feature = Feature<Point, GeoJsonProperties>.FromJson(DataService.JsonFeaturePoint);

            //Assert
            Assert.AreEqual(DataService.JsonFeaturePoint, feature?.ToJson());
        }

        #endregion

        #endregion
    }
}
