# DotNet.Web.JsInterop.GeoJson

## A .NET library for working with GeoJSON data in JavaScript interop scenarios.

### Features
- Provides a set of classes and methods for parsing, manipulating, and generating GeoJSON data.
- Supports various [GeoJSON](https://datatracker.ietf.org/doc/html/rfc7946#section-1.4) types,
  including Point, MultiPoint, LineString, MultiLineString, Polygon, MultiPolygon, GeometryCollection, Feature, and FeatureCollection.

### Serialization
- All GeoJSON types can be serialized to JSON format using the built-in `System.Text.Json` library, except `GeometryCollection` which requires special handling.
  - A default `ToJson` method is provided for each type.
  - `GeometryCollection`. This type requires special handling during serialization, as it contains a collection of geometries that can be of any GeoJSON type. 
	The default `ToJson` method implements the requirements to correctly serialize a GeometryCollection.

### Deserialization
  - All GeoJSON types can be serialized to JSON format using the built-in `System.Text.Json` library, except `GeometryCollection` which requires special handling. 
  - A default `FromJson` method is provided for each type.
  - `GeometryCollection`. This type requires special handling during deserialization, as it contains a collection of geometries that can be of any GeoJSON type. 
	The default `FromJson` method implements the requirements to correctly deserialize a GeometryCollection.

### Usage
- See the `Sandbox` project in the source code for examples of how to use the library in a Blazor application.

### Release Notes
- `v10.1.0`: 
  - `Position`. Longitude, Latitude, and Elevation have been changed to full properties.

- `v10.0.0`: Initial release with basic GeoJSON parsing and generation capabilities.
