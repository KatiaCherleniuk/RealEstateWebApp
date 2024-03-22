(function (scope) {
    scope.map = scope.map || {};
    scope.map.initMap = _initMap;
    let _map = {};
    let _markers;
    let _marker = false; 
    let _ref;
    const mapURL = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
    const searchURL = 'https://nominatim.openstreetmap.org/reverse?format=jsonv2&lat={lat}&lon={lon}';
    function _initMap(ref, lat, long) {
        _ref = ref;
        _map = L.map('map').setView({ lon: long || 30, lat: lat || 50 }, 6);

        L.tileLayer(mapURL, {
            maxZoom: 19,
        }).addTo(_map);

        _markers = L.layerGroup([]);
        _map.addLayer(_markers);

        L.Control.geocoder({
            defaultMarkGeocode: false
        })
            .on('markgeocode', function (e) {
                if (e && e.geocode && e.geocode.center) {
                    let latlng = e.geocode.center;
                    let address = formatAddress(e.geocode.properties);
                    makeMarker(latlng, address);
                }
            })
            .addTo(_map);

        _map.on('click', onMapClick);
        
    }

    function onMapClick(e) {
        let lat = e.latlng.lat;
        let lng = e.latlng.lng;

        reverseGeocode(lat, lng);
    }

    function toggleMap(disable) {
        _map._handlers.forEach(function (handler) {
            if (disable) {
                handler.disable();
            } else {
                handler.enable();
            }
        });

        _map.getContainer().style.filter = disable ? "grayscale(100%)" : "none";

        if (disable) {
            _map.off('click');
            var element = document.getElementById('manualAddressInput');
            if (element) {
                element.focus();
                element.select();
            }
            _markers.removeLayer(_marker);

        } else {
            _map.on('click', onMapClick);
        }
    }

    scope.map.toggleMap = toggleMap;

    function makeMarker(latlng, address) {
        if (!_marker) {
            _marker = new L.Marker(latlng)
        }
        else
            _markers.removeLayer(_marker);

        //_map.setView(latlng, 12);
        let latitude = latlng.lat ?? latlng[0];
        let longitude = latlng.lng ?? latlng[1];

        _marker.setLatLng(latlng);
        _markers.addLayer(_marker);
        _marker.bindPopup(address).openPopup();
        _ref.invokeMethodAsync('OnAddressCoordinatesChanged', latitude, longitude, address);

        

    }
    function reverseGeocode(lat, lng) {

        fetch(searchURL.replace('{lat}', lat).replace('{lon}', lng))
            .then(response => response.json())
            .then(data => {
                let address = formatAddress(data);
                makeMarker([lat, lng], address)
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }
    function formatAddress(geocode) {
        let addressParts = [];

        if (geocode.address.state) {
            addressParts.push(geocode.address.state);
        }
        if (geocode.address.city) {
            addressParts.push(geocode.address.city);
        }
        if (geocode.address.town) {
            addressParts.push(geocode.address.town);
        }
        if (geocode.address.village) {
            addressParts.push(geocode.address.village);
        }
        if (geocode.address.road) {
            addressParts.push(geocode.address.road);
        }
        if (geocode.address.house_number) {
            addressParts.push(geocode.address.house_number);
        }
        return addressParts.join(', ');
    }
        
  
}) (window)
