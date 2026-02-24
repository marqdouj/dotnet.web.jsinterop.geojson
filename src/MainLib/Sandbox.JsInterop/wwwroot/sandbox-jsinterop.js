function parseJsonString(jsonString) {
    const obj = JSON.parse(jsonString);
    return obj;
}

export function testGeoJSON(geoJSON) {
    console.debug("testGeoJSON", geoJSON);
    return JSON.stringify(geoJSON);
}

export function testGeoJSONString(geoJSON) {
    const obj = parseJsonString(geoJSON);
    console.debug("testGeoJSONString", geoJSON, obj);

    return JSON.stringify(obj);
}

