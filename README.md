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
- `v10.5.2`
  - `Interfaces`. Added interfaces to all IGeoJSON objects to identify their specific types.
	- Helps with type checking and casting of types, especially when working with c# `Generics`.
	- The new interfaces are: 
	  - `IFeature`, `IFeature{G, P}: IFeature where G : Geometry`.
	  - `IFeatureCollection`, `IFeatureCollection{G, P} : IFeatureCollection where G : Geometry`.
	  - `IGeometryCollection`.
	  - `ILineString`.
	  - `IMultiLineString`.
	  - `IMultiPoint`.
	  - `IMultiPolygon`.
	  - `IPoint`.
	  - `IPolygon`.
	  - `IPosition`.

- `v10.5.1`
  - `IGeoJsonObject`. Interface added to identify objects that derive from `GeoJsonObject`.
	- Helps with type checking and casting of types, especially when working with c# `Generics`.
  - `IGeoJSON`. Interface added to identify objects used in GeoJSON operations. Includes `Position`, `IGeoJsonObject` and `IGeometry`.
	- Helps with type checking and casting of types, especially when working with c# `Generics`.

- `v10.5.0`
  - `IGeometry` interface added to the `Geometry` base class. Helps with type checking and casting of geometry types, especially when working with c# `Generics`.

- `v10.4.0`
  - Update NuGet packages.

- `v10.3.1`
  - `Position`. Added `ToString(string format)` method.

- `v10.3.0`
  - Changed method signature `FeatureCollection<T, GeoJsonProperties>? FromJson<T>` to `FeatureCollection<T, P>? FromJson<T>`.

- `v10.2.1`
  - Update NuGet packages.

- `v10.2.0`
  - Added interface `IBoundingBoxEdit` for editing `BoundingBox` instances.

- `v10.1.1`
  - Update NuGet packages.

- `v10.1.0`
  - `Position`. Longitude, Latitude, and Elevation have been changed to full properties.

- `v10.0.0`. Initial release with basic GeoJSON parsing and generation capabilities.
