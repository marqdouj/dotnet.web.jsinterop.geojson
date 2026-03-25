using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using System.Text.Json;

namespace SandboxTests
{
    [TestClass]
    public sealed class PositionTests
    {
        private static readonly double longitude = 1.1;
        private static readonly double latitude = 2.2;
        private static readonly double? elevation2D = null;
        private static readonly double? elevation3D = 3.3;

        private static void ValidatePosition(Position position, double? elevation)
        {
            Assert.IsNotNull(position);
            Assert.AreEqual(longitude, position.Longitude);
            Assert.AreEqual(latitude, position.Latitude);
            Assert.AreEqual(elevation, position.Elevation);
        }

        #region Constructor - 2D/3D

        [TestMethod]
        public void Position_CreateWith_2D()
        {
            //Arrange
            //Act
            var position = new Position(longitude, latitude);

            //Assert
            Assert.IsTrue(position.IsValid);
            Assert.IsTrue(position.Is2D);
            Assert.IsFalse(position.Is3D);
            ValidatePosition(position, elevation2D);
        }

        [TestMethod]
        public void Position_CreateWith_3D()
        {
            //Arrange
            //Act
            var position = new Position(longitude, latitude, elevation3D);

            //Assert
            Assert.IsTrue(position.IsValid);
            Assert.IsFalse(position.Is2D);
            Assert.IsTrue(position.Is3D);
            ValidatePosition(position, elevation3D);
        }

        [TestMethod]
        public void Position_CreateWith_3D_Elevation_Null()
        {
            //Arrange
            //Act
            var position = new Position(longitude, latitude, elevation2D);

            //Assert
            Assert.IsTrue(position.IsValid);
            Assert.IsTrue(position.Is2D);
            Assert.IsFalse(position.Is3D);
            ValidatePosition(position, elevation2D);
        }

        #endregion

        #region Constuctor - Values

        [TestMethod]
        public void Position_CreateWithValues_2D()
        {
            //Arrange
            List<double> values = [longitude, latitude];

            //Act
            var position = new Position(values);

            //Assert
            Assert.IsTrue(position.IsValid);
            Assert.IsTrue(position.Is2D);
            Assert.IsFalse(position.Is3D);
            ValidatePosition(position, elevation2D);
        }

        [TestMethod]
        public void Position_CreateWithValues_3D()
        {
            //Arrange
            List<double> values = [longitude, latitude, elevation3D!.Value];

            //Act
            var position = new Position(values);

            //Assert
            Assert.IsTrue(position.IsValid);
            Assert.IsFalse(position.Is2D);
            Assert.IsTrue(position.Is3D);
            ValidatePosition(position, elevation3D);
        }

        [TestMethod]
        public void Position_CreateWithValues_Invalid_LessThan()
        {
            //Arrange
            List<double> values = [longitude];

            //Act,Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Position(values));
        }

        [TestMethod]
        public void Position_CreateWithValues_Invalid_GreaterThan()
        {
            //Arrange
            List<double> values = [longitude, latitude, elevation3D!.Value, 4.4];

            //Act,Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Position(values));
        }

        [TestMethod]
        public void Position_CreateWithValues_Invalid_Null()
        {
            //Arrange
            List<double>? values = null;

            //Act,Assert
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<ArgumentNullException>(() => new Position(values));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        #endregion

        #region Serialization

        private static readonly JsonSerializerOptions serializerOptions = new(JsonSerializerDefaults.Web);
        private static readonly string serialize2D = $"[{longitude},{latitude}]";
        private static readonly string serialize3D = $"[{longitude},{latitude},{elevation3D}]";

        [TestMethod]
        public void Position_Serialize_ToJson_2D()
        {
            //Arrange
            double? elevation = null;
            var position = new Position(longitude, latitude, elevation);

            //Act
            var json = JsonSerializer.Serialize(position, serializerOptions);
            
            //Assert
            Assert.IsNotNull(json);
            Assert.AreEqual(serialize2D, json);
        }

        [TestMethod]
        public void Position_Serialize_ToJson_3D()
        {
            //Arrange
            double? elevation = 3.3;
            var position = new Position(longitude, latitude, elevation);

            //Act
            var json = JsonSerializer.Serialize(position, serializerOptions);

            //Assert
            Assert.IsNotNull(json);
            Assert.AreEqual(serialize3D, json);
        }

        [TestMethod]
        public void Position_FromJson_NullOrWhiteSpace()
        {
            //Arrange
            //Act
            var featureEmpty = Position.FromJson("");
            var featureNull = Position.FromJson(null);
            var featureWhiteSpace = Position.FromJson(null);

            //Assert
            Assert.IsNull(featureEmpty);
            Assert.IsNull(featureNull);
            Assert.IsNull(featureWhiteSpace);
        }

        [TestMethod]
        public void Position_FromJson_2D()
        {
            //Arrange
            double? elevation = null;

            //Act
            var position = Position.FromJson(serialize2D);

            //Assert
            Assert.IsNotNull(position);
            Assert.IsTrue(position.IsValid);
            Assert.IsTrue(position.Is2D);
            Assert.IsFalse(position.Is3D);
            ValidatePosition(position, elevation);
        }

        [TestMethod]
        public void Position_FromJson_3D()
        {
            //Arrange
            double? elevation = 3.3;

            //Act
            var position = Position.FromJson(serialize3D);

            //Assert
            Assert.IsNotNull(position);
            Assert.IsTrue(position.IsValid);
            Assert.IsFalse(position.Is2D);
            Assert.IsTrue(position.Is3D);
            ValidatePosition(position, elevation);
        }

        #endregion

        #region Latitude

        [TestMethod]
        public void Position_Latitude_Get()
        {
            //Arrange
            var position = new Position(longitude, latitude);

            //Act
            var value = position.Latitude;

            //Assert
            Assert.AreEqual(latitude, value);
            Assert.IsTrue(position.IsValid);
            Assert.IsTrue(position.Is2D);
            Assert.IsFalse(position.Is3D);
        }

        [TestMethod]
        public void Position_Latitude_Set()
        {
            //Arrange
            var position = new Position([1, 2]);

            //Act
            var value = position.Latitude;
            position.Latitude = latitude;

            //Assert
            Assert.AreEqual(2, value);
            Assert.AreEqual(latitude, position.Latitude);
            Assert.IsTrue(position.IsValid);
            Assert.IsTrue(position.Is2D);
            Assert.IsFalse(position.Is3D);
        }

        #endregion

        #region Longitude

        [TestMethod]
        public void Position_Longitude_Get()
        {
            //Arrange
            var position = new Position(longitude, latitude);

            //Act
            var value = position.Longitude;

            //Assert
            Assert.AreEqual(longitude, value);
            Assert.IsTrue(position.IsValid);
            Assert.IsTrue(position.Is2D);
            Assert.IsFalse(position.Is3D);
        }

        [TestMethod]
        public void Position_Longitude_Set()
        {
            //Arrange
            var position = new Position([1, 2]);

            //Act
            var value = position.Longitude;
            position.Longitude = longitude;

            //Assert
            Assert.AreEqual(1, value);
            Assert.AreEqual(longitude, position.Longitude);
            Assert.IsTrue(position.IsValid);
            Assert.IsTrue(position.Is2D);
            Assert.IsFalse(position.Is3D);
        }

        #endregion

        #region Elevation

        [TestMethod]
        public void Position_Elevation_Get()
        {
            //Arrange
            var position = new Position(longitude, latitude, elevation3D);

            //Act
            var value = position.Elevation;

            //Assert
            Assert.AreEqual(elevation3D, value);
            Assert.IsTrue(position.IsValid);
            Assert.IsFalse(position.Is2D);
            Assert.IsTrue(position.Is3D);
        }

        [TestMethod]
        public void Position_Elevation_Set()
        {
            //Arrange
            var position = new Position([1, 2, 3]);

            //Act
            var value = position.Elevation;
            position.Elevation = elevation3D;

            //Assert
            Assert.AreEqual(3, value);
            Assert.AreEqual(elevation3D, position.Elevation);
            Assert.IsTrue(position.IsValid);
            Assert.IsFalse(position.Is2D);
            Assert.IsTrue(position.Is3D);
        }

        [TestMethod]
        public void Position_Elevation_Set_Null()
        {
            //Arrange
            var position = new Position([1, 2, 3]);

            //Act
            var value = position.Elevation;
            position.Elevation = elevation2D;

            //Assert
            Assert.AreEqual(3, value);
            Assert.AreEqual(elevation2D, position.Elevation);
            Assert.IsTrue(position.IsValid);
            Assert.IsTrue(position.Is2D);
            Assert.IsFalse(position.Is3D);
        }

        #endregion
    }
}
