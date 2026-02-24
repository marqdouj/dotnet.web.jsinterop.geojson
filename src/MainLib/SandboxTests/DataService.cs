using Marqdouj.DotNet.Web.JsInterop.GeoJson;

namespace SandboxTests
{
    internal static class DataService
    {
        public static readonly string JsonFeaturePoint = @"{""type"":""Feature"",""geometry"":{""type"":""Point"",""coordinates"":[1,2],""bbox"":null},""id"":""point-1"",""properties"":{""name"":""Test Point"",""value"":42},""bbox"":[1.1,2,-3.1,-4]}";

        internal static Feature<LineString, GeoJsonProperties> GetLineStringFeature()
        {
            var bboxA = new BoundingBox(1.0, 2.1, -3, -4.4);
            var bboxB = new BoundingBox(5, 6, 7, 8);
            var props = new GeoJsonProperties() { { "Prop1", "Value1" }, { "Prop2", "Value2" } };
            var id = "myLine";
            var line = new LineString(GetLineStringCoordinates()) { Bbox = bboxA };
            var feature = new Feature<LineString, GeoJsonProperties>(line, props, id, bboxB);
            return feature;
        }

        internal static List<Position> GetLineStringCoordinates()
        {
            List<Position> coordinates = [
                 new Position(-122.18822, 47.63208),
                 new Position(-122.18204, 47.63196),
            ];

            return coordinates;
        }

        internal static Feature<Point, GeoJsonProperties> GetPointFeature()
        {
            return new()
            {
                Id = "point-1",
                Geometry = new Point(new Position(1.0, 2.0)),
                Properties = new GeoJsonProperties
            {
                { "name", "Test Point" },
                { "value", 42 }
            },
                Bbox = new BoundingBox(1.1, 2.0, -3.1, -4),
            };
        }
    }
}
