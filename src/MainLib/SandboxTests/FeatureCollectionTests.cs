using Marqdouj.DotNet.Web.JsInterop.GeoJson;

namespace SandboxTests
{
    [TestClass]
    public sealed class FeatureCollectionTests
    {
        #region Serialization

        const string jsonOutput = @"{""type"":""FeatureCollection"",""features"":[{""type"":""Feature"",""geometry"":{""type"":""LineString"",""coordinates"":[[-122.18822,47.63208],[-122.18204,47.63196]],""bbox"":[1,2.1,-3,-4.4]},""id"":""myLine"",""properties"":{""Prop1"":""Value1"",""Prop2"":""Value2""},""bbox"":[5,6,7,8]}],""bbox"":[9,10,11,12]}";

        [TestMethod]
        public void FeatureCollection_ToJson()
        {
            //Arrange
            var bbox = new BoundingBox(9, 10, 11, 12);
            var features = new List<Feature<LineString, GeoJsonProperties>>() { DataService.GetLineStringFeature() };
            var collection = new FeatureCollection<LineString, GeoJsonProperties>(features, bbox);

            //Act
            var json = collection.ToJson();
            //Console.WriteLine(json);

            //Assert
            Assert.AreEqual(jsonOutput, json);
        }

        [TestMethod]
        public void FeatureCollection_FromJson()
        {
            //Arrange
            //Act
            var collection = FeatureCollection<LineString, GeoJsonProperties>.FromJson<LineString>(jsonOutput);
            var featureEmpty = FeatureCollection<LineString, GeoJsonProperties>.FromJson<LineString>("");
            var featureNull = FeatureCollection<LineString, GeoJsonProperties>.FromJson<LineString>(null);

            //Assert
            Assert.AreEqual(jsonOutput, collection?.ToJson());
            Assert.IsNull(featureEmpty);
            Assert.IsNull(featureNull);
        }

        #endregion
    }
}
