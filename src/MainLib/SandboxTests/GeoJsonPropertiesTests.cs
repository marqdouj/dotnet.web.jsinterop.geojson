using Marqdouj.DotNet.Web.JsInterop.GeoJson;

namespace SandboxTests
{
    [TestClass]
    public sealed class GeoJsonPropertiesTests
    {
        [TestMethod]
        public void Constructor_CaseInsensitive_GetValues()
        {
            //Arrange
            const string name = "MYPROP";
            const string first = "first";
            var p = new GeoJsonProperties(StringComparer.OrdinalIgnoreCase)
            {
                { name, first }
            };

            //Act
            var firstCheck = p[name] as string;
            var secondCheck = p[name.ToLower()] as string;

            //Assert
            Assert.AreEqual(firstCheck, secondCheck);
        }

        [TestMethod]
        public void Constructor_CaseSensitive_GetValues()
        {
            //Arrange
            const string name = "MYPROP";
            const string first = "first";
            const string second = "second";
            var p = new GeoJsonProperties()
            {
                { name, first },
                { name.ToLower(), second }
            };

            //Act
            var firstCheck = p[name] as string;
            var secondCheck = p[name.ToLower()] as string;

            //Assert
            Assert.HasCount(2, p);
            Assert.AreNotEqual(firstCheck, secondCheck);
        }
    }
}
