using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using System.Text.Json;

namespace SandboxTests
{
    [TestClass]
    public sealed class BoundingBoxTests
    {
        private const double west = 1.1;
        private const double south = 2.2;
        private const double swElevation = 3.3;
        private const double east = 4.4;
        private const double north = 5.5;
        private const double neElevation = 6.6;
        private readonly Position southwest = new(west, south);
        private readonly Position northeast = new(east, north);
        private readonly Position southwestWithElevation = new(west, south, swElevation);
        private readonly Position northeastWithElevation = new(east, north, neElevation);

        private static void ValidateBoundingBox(BoundingBox bbox, Position southwest, Position northeast)
        {
            Assert.IsNotNull(bbox);
            Assert.IsTrue(bbox.IsValid);
            Assert.AreEqual(bbox.Southwest, southwest);
            Assert.AreEqual(bbox.Northeast, northeast);
        }

        #region Constructor - Positions

        [TestMethod]
        public void BoundingBox_CreateWithPositions_2D()
        {
            //Arrange
            //Act
            var bbox = new BoundingBox(southwest, northeast);

            //Assert
            Assert.IsTrue(bbox.Is2D);
            Assert.IsFalse(bbox.Is3D);
            ValidateBoundingBox(bbox, southwest, northeast);
        }

        [TestMethod]
        public void BoundingBox_CreateWithPositions_3D()
        {
            //Arrange
            //Act
            var bbox = new BoundingBox(southwestWithElevation, northeastWithElevation);

            //Assert
            Assert.IsFalse(bbox.Is2D);
            Assert.IsTrue(bbox.Is3D);
            ValidateBoundingBox(bbox, southwestWithElevation, northeastWithElevation);
        }

        #endregion

        #region Constuctor - Values

        [TestMethod]
        public void BoundingBox_CreateWithValues_2D()
        {
            //Arrange
            List<double> values = [west, south, east, north];

            //Act
            var bbox = new BoundingBox(values);

            //Assert
            Assert.IsTrue(bbox.IsValid);
            Assert.IsTrue(bbox.Is2D);
            Assert.IsFalse(bbox.Is3D);
        }

        [TestMethod]
        public void BoundingBox_CreateWithValues_3D()
        {
            //Arrange
            List<double> values = [west, south, swElevation, east, north, neElevation];

            //Act
            var bbox = new BoundingBox(values);

            //Assert
            Assert.IsTrue(bbox.IsValid);
            Assert.IsFalse(bbox.Is2D);
            Assert.IsTrue(bbox.Is3D);
        }

        [TestMethod]
        public void BoundingBox_CreateWithValues_Invalid_LessThan()
        {
            //Arrange
            List<double> values = [west];

            //Act,Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new BoundingBox(values));
        }

        [TestMethod]
        public void BoundingBox_CreateWithValues_Invalid_GreaterThan()
        {
            //Arrange
            List<double> values = [west, south, swElevation, east, north, neElevation, 7.7];

            //Act,Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new BoundingBox(values));
        }

        [TestMethod]
        public void BoundingBox_CreateWithValues_Invalid_Null()
        {
            //Arrange
            List<double>? values = null;

            //Act,Assert
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<ArgumentNullException>(() => new BoundingBox(values));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        #endregion

        #region Serialization

        private static readonly JsonSerializerOptions serializerOptions = new(JsonSerializerDefaults.Web);
        private static readonly string serialize2D = $"[{west},{south},{east},{north}]";
        private static readonly string serialize3D = $"[{west},{south},{swElevation},{east},{north},{neElevation}]";

        [TestMethod]
        public void BoundingBox_Serialize_ToJson_2D()
        {
            //Arrange
            var bbox = new BoundingBox(west, south, east, north);

            //Act
            var json = JsonSerializer.Serialize(bbox, serializerOptions);

            //Assert
            Assert.IsNotNull(json);
            Assert.AreEqual(serialize2D, json);
        }

        [TestMethod]
        public void BoundingBox_Serialize_ToJson_3D()
        {
            //Arrange
            var bbox = new BoundingBox(west, south, swElevation, east, north, neElevation);

            //Act
            var json = JsonSerializer.Serialize(bbox, serializerOptions);

            //Assert
            Assert.IsNotNull(json);
            Assert.AreEqual(serialize3D, json);
        }

        [TestMethod]
        public void BoundingBox_Serialize_FromJson_2D()
        {
            //Arrange
            //Act
            var bbox = BoundingBox.FromJson(serialize2D);

            //Assert
            Assert.IsNotNull(bbox);
            Assert.IsTrue(bbox.IsValid);
            Assert.IsTrue(bbox.Is2D);
            Assert.IsFalse(bbox.Is3D);
        }

        [TestMethod]
        public void BoundingBox_Serialize_FromJson_3D()
        {
            //Arrange
            //Act
            var bbox = BoundingBox.FromJson(serialize3D);

            //Assert
            Assert.IsNotNull(bbox);
            Assert.IsTrue(bbox.IsValid);
            Assert.IsFalse(bbox.Is2D);
            Assert.IsTrue(bbox.Is3D);
        }

        #endregion
    }
}
