class DynamicMapController {
    constructor($scope, $state, $window, $dragonService, $accountService, $staticMapService, $profileService) {
        this.staticMapService = $staticMapService;
        this.window = $window;
        this.state = $state;
        this.start = "";
        this.end = "";
        this.markers = [];
        let zoomLevel = 11;
        this.zoomLevel = zoomLevel;
        

        



        this.log = {
            location: this.getLog()
        };
        this.profileId = $accountService.getClaim("profileId");
        this.dragonService = $dragonService;
        this.profile = $profileService.getProfile(this.profileId);
        



        this.frack = $dragonService.getDragon(this.profileId).$promise.then((dragon) => {
            this.origin = { lat: dragon.latitude, lng: dragon.longitude };
            this.radius = dragon.kmRadius;
            this.name = dragon.name;

            this.dragons = $dragonService.getDragons().$promise.then((dragons) => {
                this.otherDragonMarker(dragons);
            });

            this.zoomLevel = this.setZoom(this.radius);  // Set map zoom level based on dragon.kmRadius
            console.log(this.zoomLevel);

            let mapElement = document.getElementById('map');
            this.map = new google.maps.Map(mapElement, {
                zoom: this.zoomLevel,
                center: this.origin,
                mapTypeId: 'hybrid',
                minZoom: 3
            });
            console.log("radius is" + this.radius);

            this.canada = new google.maps.Rectangle({
                strokeColor: '#FF0000',
                strokeOpacity: 0,
                strokeWeight: 0,
                fillColor: '#FF0000',
                fillOpacity: 0,
                map: this.map,
                bounds: {
                    north: 70.3520,
                    south: 48.6402,
                    east: -53.7862,
                    west: -168.3894
                },
                clickable: false
            });

            this.europe = new google.maps.Rectangle({
                strokeColor: '#FF0000',
                strokeOpacity: 0,
                strokeWeight: 1,
                fillColor: '#FF0000',
                fillOpacity: 0,
                map: this.map,
                bounds: {
                    north: 71.1629,
                    south: 37.3789,
                    east: 49.2188,
                    west: -23.9062
                },
                clickable: false
            });

            this.hawaii = new google.maps.Rectangle({
                strokeColor: '#FF0000',
                strokeOpacity: 0,
                strokeWeight: 1,
                fillColor: '#FF0000',
                fillOpacity: 0,
                map: this.map,
                bounds: {
                    north: 27.3376,
                    south: 13.3736,
                    east: -148.7109,
                    west: -165.5859
                },
                clickable: false
            });

            this.oceania = new google.maps.Rectangle({
                strokeColor: '#FF0000',
                strokeOpacity: 0,
                strokeWeight: 1,
                fillColor: '#FF0000',
                fillOpacity: 0,
                map: this.map,
                bounds: {
                    north: -13.3896,
                    south: -41.6662,
                    east: 155.0391,
                    west: 112.8515
                },
                clickable: false
            });

            this.russia = new google.maps.Rectangle({
                strokeColor: '#FF0000',
                strokeOpacity: 0,        // 0
                strokeWeight: 1,
                fillColor: '#FF0000',
                fillOpacity: 0,        // 0
                map: this.map,
                bounds: {
                    north: 77.2181,
                    south: 51.1311,
                    east: -170.1562,
                    west: 60.1171
                },
                clickable: false
            });

            this.sAfrica = new google.maps.Rectangle({
                strokeColor: '#FF0000',
                strokeOpacity: 0,
                strokeWeight: 1,
                fillColor: '#FF0000',
                fillOpacity: 0,
                map: this.map,
                bounds: {
                    north: 7.6348,
                    south: -36.6596,
                    east: 41.1328,
                    west: 10.8984
                },
                clickable: false
            });

            this.sAmerica = new google.maps.Rectangle({
                strokeColor: '#FF0000',
                strokeOpacity: 0,
                strokeWeight: 1,
                fillColor: '#FF0000',
                fillOpacity: 0,
                map: this.map,
                bounds: {
                    north: 15.2099,
                    south: -55.6218,
                    east: -35.5078,
                    west: -80.1563
                },
                clickable: false
            });

            this.seAsia = new google.maps.Rectangle({
                strokeColor: '#FF0000',
                strokeOpacity: 0,
                strokeWeight: 1,
                fillColor: '#FF0000',
                fillOpacity: 0,
                map: this.map,
                bounds: {
                    north: 50.3,
                    south: 8.8471,
                    east: 151.17,
                    west: 78.2227
                },
                clickable: false
            });

            this.swAmericaMexico = new google.maps.Rectangle({
                strokeColor: '#FF0000',
                strokeOpacity: 0,
                strokeWeight: 1,
                fillColor: '#FF0000',
                fillOpacity: 0,
                map: this.map,
                bounds: {
                    north: 41.6914,
                    south: 15.0774,
                    east: -86.9805,
                    west: -124.7168
                },
                clickable: false
            });

            this.cityCircle = new google.maps.Circle({
                strokeColor: '#54ff93',
                strokeOpacity: 1,
                strokeWeight: 1,
                fillColor: '#54ff93',
                fillOpacity: .10,
                map: this.map,
                center: this.origin,
                radius: this.radius * 1000, //this is originally in meters, we're converting to Km
                clickable: false
            });

            google.maps.event.addListener(this.map, 'click', (event) => {
                this.addMarker({
                    coord: event.latLng,
                    content: "Travel here?",
                }, this.map);

                if (this.end == "") {
                    this.end = event.latLng;
                    $scope.$apply();
                };

            });

            let markerDetailList = this.getMarkerDetailList();
            markerDetailList.forEach((markerDetail) => {
                this.addMarker(markerDetail, this.map);
            });
        });



        this.messages = [];
        this.message = "";
        this.names = []
        // this.username = "";
        this.connection = new signalR.HubConnection('/chat');
        this.connection.start();
        this.connection.on('send', (name, data) => {
            this.messages.push(name);
            // this.names.push(name);
            this.messages.push(data);
            $scope.$apply();
        });

        let score = 0;
        this.score = this.getScore();
        console.log("Score is: " + this.score);

        this.profileId = $accountService.getClaim("profileId");
        this.dragon = $dragonService.getDragon(this.profileId);
        this.updateDragon = {
            xp: this.score,
            kmRadius: this.score * .5,
            kmTravelled: this.getKm(),
            challengesWon: 1,
            tripsTaken: 1
        };

        this.updateItemLog = {
            alexandrite: this.window.sessionStorage.getItem("alexandriteScore"),
            amethyst: this.window.sessionStorage.getItem("amethystScore"),
            animalmask: this.window.sessionStorage.getItem("animalMaskScore"),
            aquamarine: this.window.sessionStorage.getItem("aquamarineScore"),
            beadednecklace: this.window.sessionStorage.getItem("beadedNecklaceScore"),
            bloodstone: this.window.sessionStorage.getItem("bloodstoneScore"),
            boomarang: this.window.sessionStorage.getItem("boomarangScore"),
            cherryblossom: this.window.sessionStorage.getItem("cherryBlossomScore"),
            cholla: this.window.sessionStorage.getItem("chollaScore"),
            croissant: this.window.sessionStorage.getItem("croissantScore"),
            garnet: this.window.sessionStorage.getItem("garnetScore"),
            hamburger: this.window.sessionStorage.getItem("hamburgerScore"),
            hockeypuck: this.window.sessionStorage.getItem("hockeyPuckScore"),
            jade: this.window.sessionStorage.getItem("jadeScore"),
            koaladoll: this.window.sessionStorage.getItem("koalaDollScore"),
            mapleleaf: this.window.sessionStorage.getItem("mapleLeafScore"),
            matryoshkadoll: this.window.sessionStorage.getItem("matryoshkaDollScore"),
            moonstone: this.window.sessionStorage.getItem("moonstoneScore"),
            noodlebowl: this.window.sessionStorage.getItem("noodleBowlScore"),
            opal: this.window.sessionStorage.getItem("opalScore"),
            parrotfeather: this.window.sessionStorage.getItem("parrotFeatherScore"),
            pearl: this.window.sessionStorage.getItem("pearlScore"),
            pierogi: this.window.sessionStorage.getItem("pierogiScore"),
            pinacolada: this.window.sessionStorage.getItem("pinaColadaScore"),
            pineapple: this.window.sessionStorage.getItem("pineappleScore"),
            plumeria: this.window.sessionStorage.getItem("plumeriaScore"),
            sapphire: this.window.sessionStorage.getItem("sapphireScore"),
            teabag: this.window.sessionStorage.getItem("teaBagScore"),
            tigerseye: this.window.sessionStorage.getItem("tigersEyeScore"),
            topaz: this.window.sessionStorage.getItem("topazScore"),
            turquoise: this.window.sessionStorage.getItem("turquoiseScore")
        };

        this.trigger = this.window.sessionStorage.getItem("trigger");
        console.log("initial:" + this.trigger);
        this.addTravel();
        this.updateStats();
    }

    // check(e){
    //     if(e.keyCode == 13){
    //         this.sendMessage();
    //     }
    // }

    setZoom(radius) {
        if (radius < 30) return 11;
        else if (radius < 50) return 10;
        else if (radius < 100) return 9;
        else if (radius < 200) return 8;
        else if (radius < 350) return 7;
        else if (radius < 700) return 6;
        else if (radius < 1400) return 5;
        else if (radius < 2700) return 4;
        else return 3;
    }

    getScore() {
        return this.window.sessionStorage.getItem("gameScore");
    }
    getGridScore(){
        return this.window.sessionStorage.getItem("gridScore");
    }

    updateStats() {
        console.log("before updateStats:" + this.trigger);
        if (this.trigger == "true") {
            this.dragonService.updateDragon(this.profileId, this.updateDragon);
            this.dragonService.updateItemLog(this.profileId, this.updateItemLog);
            this.window.sessionStorage.setItem("gameScore", 0);
            this.window.sessionStorage.setItem("alexandriteScore", 0);
            this.window.sessionStorage.setItem("amethystScore", 0);
            this.window.sessionStorage.setItem("animalMaskScore", 0);
            this.window.sessionStorage.setItem("aquamarineScore", 0);
            this.window.sessionStorage.setItem("beadedNecklaceScore", 0);
            this.window.sessionStorage.setItem("bloodstoneScore", 0);
            this.window.sessionStorage.setItem("boomarangScore", 0);
            this.window.sessionStorage.setItem("cherryBlossomScore", 0);
            this.window.sessionStorage.setItem("chollaScore", 0);
            this.window.sessionStorage.setItem("croissantScore", 0);
            this.window.sessionStorage.setItem("garnetScore", 0);
            this.window.sessionStorage.setItem("hamburgerScore", 0);
            this.window.sessionStorage.setItem("hockeyPuckScore", 0);
            this.window.sessionStorage.setItem("jadeScore", 0);
            this.window.sessionStorage.setItem("koalaDollScore", 0);
            this.window.sessionStorage.setItem("mapleLeafScore", 0);
            this.window.sessionStorage.setItem("matryoshkaDollScore", 0);
            this.window.sessionStorage.setItem("moonstoneScore", 0);
            this.window.sessionStorage.setItem("noodleBowlScore", 0);
            this.window.sessionStorage.setItem("opalScore", 0);
            this.window.sessionStorage.setItem("parrotFeatherScore", 0);
            this.window.sessionStorage.setItem("pearlScore", 0);
            this.window.sessionStorage.setItem("pierogiScore", 0);
            this.window.sessionStorage.setItem("pinaColadaScore", 0);
            this.window.sessionStorage.setItem("pineappleScore", 0);
            this.window.sessionStorage.setItem("plumeriaScore", 0);
            this.window.sessionStorage.setItem("sapphireScore", 0);
            this.window.sessionStorage.setItem("teaBagScore", 0);
            this.window.sessionStorage.setItem("tigersEyeScore", 0);
            this.window.sessionStorage.setItem("topazScore", 0);
            this.window.sessionStorage.setItem("turquoiseScore", 0);

            this.score = this.getScore();
            this.window.sessionStorage.removeItem("trigger");
            this.window.sessionStorage.setItem("trigger", false);
            this.trigger = this.window.sessionStorage.getItem("trigger");
            console.log("after updateStats:" + this.trigger);
        }
        else { };
    }
    addTravel() {
        this.dragonService.addLog(this.log);
    }
    travelCheck(destination) {
        if (document.getElementById("destinationcoord").value = "") {
            this.window.document.addEventListener('keyup', e => {
                if (e.keyCode == 13) {
                    this.sendMessage();
                }
            })

            this.window.alert("You gotta pick a place first! Click where you wanna go on the map.")
        }
        else if (confirm("Is this where we're going today?") == true) {
            var x = destination.toString().replace(/[{()}]/g, '').split(',').map(Number);
            var lat1 = this.origin.lat;
            var lon1 = this.origin.lng;
            var lat2 = x[0];
            var lon2 = x[1];
            this.lat = lat2;
            this.lon = lon2;
            var p = 0.017453292519943295;    // Math.PI / 180
            var c = Math.cos;
            var a = 0.5 - c((lat2 - lat1) * p) / 2 +
                c(lat1 * p) * c(lat2 * p) *
                (1 - c((lon2 - lon1) * p)) / 2;

            var y = 12742 * Math.asin(Math.sqrt(a)); // 2 * R; R = 6371 km
            if (y > this.radius) {
                this.window.alert("That's a bit too far, isn't it? :(");
                this.state.reload();
            }
            else {
                // random event generator
                var random = Math.floor((Math.random() * 10) + 1);
                console.log(random);
                if (random == 5) {

                    console.log("success");
                    this.state.go("randomEvent");
                }
                else {
                    this.window.sessionStorage.setItem("trigger", true)
                    this.window.sessionStorage.setItem("gameScore", 0);
                    this.window.sessionStorage.setItem("alexandriteScore", 0);
                    this.window.sessionStorage.setItem("amethystScore", 0);
                    this.window.sessionStorage.setItem("animalMaskScore", 0);
                    this.window.sessionStorage.setItem("aquamarineScore", 0);
                    this.window.sessionStorage.setItem("beadedNecklaceScore", 0);
                    this.window.sessionStorage.setItem("bloodstoneScore", 0);
                    this.window.sessionStorage.setItem("boomarangScore", 0);
                    this.window.sessionStorage.setItem("cherryBlossomScore", 0);
                    this.window.sessionStorage.setItem("chollaScore", 0);
                    this.window.sessionStorage.setItem("croissantScore", 0);
                    this.window.sessionStorage.setItem("garnetScore", 0);
                    this.window.sessionStorage.setItem("hamburgerScore", 0);
                    this.window.sessionStorage.setItem("hockeyPuckScore", 0);
                    this.window.sessionStorage.setItem("jadeScore", 0);
                    this.window.sessionStorage.setItem("koalaDollScore", 0);
                    this.window.sessionStorage.setItem("mapleLeafScore", 0);
                    this.window.sessionStorage.setItem("matryoshkaDollScore", 0);
                    this.window.sessionStorage.setItem("moonstoneScore", 0);
                    this.window.sessionStorage.setItem("noodleBowlScore", 0);
                    this.window.sessionStorage.setItem("opalScore", 0);
                    this.window.sessionStorage.setItem("parrotFeatherScore", 0);
                    this.window.sessionStorage.setItem("pearlScore", 0);
                    this.window.sessionStorage.setItem("pierogiScore", 0);
                    this.window.sessionStorage.setItem("pinaColadaScore", 0);
                    this.window.sessionStorage.setItem("pineappleScore", 0);
                    this.window.sessionStorage.setItem("plumeriaScore", 0);
                    this.window.sessionStorage.setItem("sapphireScore", 0);
                    this.window.sessionStorage.setItem("teaBagScore", 0);
                    this.window.sessionStorage.setItem("tigersEyeScore", 0);
                    this.window.sessionStorage.setItem("topazScore", 0);
                    this.window.sessionStorage.setItem("turquoiseScore", 0);
                    var geocoder = new google.maps.Geocoder();
                    geocoder.geocode({ "location": { lat: parseFloat(this.lat), lng: parseFloat(this.lon) } }, (results, status) => {
                        if (status == google.maps.GeocoderStatus.OK && results.length > 0) {
                            this.location = results[3].formatted_address;
                            this.window.sessionStorage.setItem('log', this.location);
                        }
                    });
                    if (this.hawaii.getBounds().contains(destination) == true) {
                        this.window.sessionStorage.setItem("KmTravelled", y);
                        this.staticMapService.setMap(lat2, lon2);

                        this.state.go("region1", { latitude: lat2, longitude: lon2 });
                    }

                    else if (this.swAmericaMexico.getBounds().contains(destination) == true) {
                        this.window.sessionStorage.setItem("KmTravelled", y);
                        this.staticMapService.setMap(lat2, lon2);

                        this.state.go("region2", { latitude: lat2, longitude: lon2 });
                    }

                    else if (this.seAsia.getBounds().contains(destination) == true) {
                        this.window.sessionStorage.setItem("KmTravelled", y);
                        this.staticMapService.setMap(lat2, lon2);

                        this.state.go("region3", { latitude: lat2, longitude: lon2 });
                    }

                    else if (this.europe.getBounds().contains(destination) == true) {
                        this.window.sessionStorage.setItem("KmTravelled", y);
                        this.staticMapService.setMap(lat2, lon2);

                        this.state.go("region4", { latitude: lat2, longitude: lon2 });
                    }

                    else if (this.canada.getBounds().contains(destination) == true) {
                        this.window.sessionStorage.setItem("KmTravelled", y);
                        this.staticMapService.setMap(lat2, lon2);

                        this.state.go("region5", { latitude: lat2, longitude: lon2 });
                    }

                    else if (this.sAmerica.getBounds().contains(destination) == true) {
                        this.window.sessionStorage.setItem("KmTravelled", y);
                        this.staticMapService.setMap(lat2, lon2);

                        this.state.go("region6", { latitude: lat2, longitude: lon2 });
                    }

                    else if (this.sAfrica.getBounds().contains(destination) == true) {
                        this.window.sessionStorage.setItem("KmTravelled", y);
                        this.staticMapService.setMap(lat2, lon2);

                        this.state.go("region7", { latitude: lat2, longitude: lon2 });
                    }

                    else if (this.russia.getBounds().contains(destination) == true) {
                        this.window.sessionStorage.setItem("KmTravelled", y);
                        this.staticMapService.setMap(lat2, lon2);

                        this.state.go("region8", { latitude: lat2, longitude: lon2 });
                    }

                    else if (this.oceania.getBounds().contains(destination) == true) {
                        this.window.sessionStorage.setItem("KmTravelled", y);
                        this.staticMapService.setMap(lat2, lon2);

                        this.state.go("region9", { latitude: lat2, longitude: lon2 });
                    }

                    else {
                        this.window.sessionStorage.setItem("KmTravelled", y);
                        this.staticMapService.setMap(lat2, lon2);

                        // JLT - old code
                        // this.state.go("steve");

                        // JLT - new code
                        this.state.go("steve", { latitude: lat2, longitude: lon2 });
                    }
                }
            }
        }
        else {
            this.state.reload();
        }
    }
    // newLog(){
    //         var geocoder = new google.maps.Geocoder();
    //         geocoder.geocode({ "location": {lat: parseFloat(this.lat), lng: parseFloat(this.lon)} }, (results, status) => {
    //             if (status == google.maps.GeocoderStatus.OK && results.length > 0) {
    //                 this.location = results[0].formatted_address;  
    //                 this.window.sessionStorage.setItem('log', this.location);                      
    //             }
    //         });
    // }
    getLog() {
        return this.window.sessionStorage.getItem('log');
    }

    addMarker(markerDetail, map) {
        if (document.getElementById("destinationcoord").value == "") {
            var marker = new google.maps.Marker({
                optimized: false,
                position: markerDetail.coord,
                map: map,
                // icon:props.iconImage
            });
        }

        if (markerDetail.iconImage) {
            marker.setIcon(markerDetail.iconImage);
        }

        if (markerDetail.content) {
            var infoWindow = new google.maps.InfoWindow({
                content: markerDetail.content,
            });

            marker.addListener('click', function () {
                infoWindow.open(map, marker);
            });
        }
    }

    otherDragonMarker(d) {
        this.marker = [];
        this.positions = [];
        for (var index = 0; index < d.length; index++) {
            this.positions[index] = { lat: d[index].latitude, lng: d[index].longitude };
            this.marker[index] = new google.maps.Marker({
                position: this.positions[index],
                map: this.map,
                icon: '/images/smalldragonicon.png',
                title: 'Dragon: ' + d[index].name

            });

        }
    }


    getMarkerDetailList() {
        return [
            {
                coord: this.origin,
                iconImage: '/images/dragonhomeicon.gif', //replace with dragon.img
                content: 'Name: ' + this.name + '<br>"I heard the churros from Spain are to die for!"'
            },
            {
                coord: { lat: 25.7617, lng: -80.1918 },
                iconImage: 'https://orig00.deviantart.net/8ea6/f/2012/224/2/f/pokemon_palm_tree_sprite_by_icefire82g-d5auju0.png', //replace with dragon.img
                content: '<h3><a href="/skyboat">Sky Boating</a></h3> <br> Sail through the air and collect as much booty as possible!'
            },
            {
                coord: { lat: 33.3943, lng: -104.5230 },
                iconImage: '/images/invadersicon.png',
                content: '<h3><a href="/invaders">JasInvaders</a></h3> <br> Protect the world from a swarm of hostile invaders'
            },
            {
                coord: { lat: 63.317778, lng: 147.433333 },
                iconImage: '/images/tanksicon.png',
                content: '<h3><a href="/tank">Dragon vs Tanks</a></h3> <br> Use your firepower to defeat your enemies'
            },
            {
                coord: { lat: 29.978827, lng: 31.134223 },
                iconImage: '/images/breakouticon.png',
                content: '<h3><a href="/breakout">Dragon Breath</a></h3> <br> Turn the bricks into dust'
            },

        ];
    }
    sendMessage() {
        this.connection.invoke('send',this.profile.displayName, this.message);
        document.getElementById('chatmessage').value = "";

    }
    getKm() {
        return this.window.sessionStorage.getItem("KmTravelled");
    }
    go() {
        this.state.go("grid");
    }
}



