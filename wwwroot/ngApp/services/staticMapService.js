class StaticMapService {
    constructor($resource) {
    }

    setMap(lat, lng) {
        return lat + "," + lng
    }
}