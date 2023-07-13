export function getCurrentPosition(dotNetHelper, callback) {

    const options = {
        enableHighAccuracy: true,
        timeout: 5000,
        maximumAge: 1
    };

    function success(pos) {

        const coord = {
            latitude: pos.coords.latitude,
            longitude: pos.coords.longitude,
            accuracy: pos.coords.accuracy
        };

        dotNetHelper.invokeMethodAsync(callback, coord);
    }

    function error(err) {
        console.warn(`ERROR(${err.code}): ${err.message}`);
    }

    navigator.geolocation.getCurrentPosition(success, error, options);
}

//export function initializeMaps(latLng) {
//    var latlng = new google.maps.LatLng(latLng[0], latLng[1]);
//    var options = {
//        zoom: 14, center: latLng,
//        mapTypeId: google.maps.MapTypeId.ROADMAP
//    };
//    var map = new google.maps.Map(document.getElementById("map"), options);
//}