﻿class InvadersController {
    constructor($window, $state, $timeout, $dragonService, $accountService) {
        this.message = 'Hello from part 13!';
        this.window = $window;
        this.profileId = $accountService.getClaim("profileId");
        this.dragon = $dragonService.getDragon(this.profileId);        
        this.state = $state;
        this.timeout = $timeout;
        this.dragonService = $dragonService;




        
        this.minigame = $dragonService.getDragon(this.profileId).$promise.then((dragon) => {
            this.origin = { lat: dragon.latitude, lng: dragon.longitude };
            this.radius = dragon.kmRadius;
            this.heck= this.gameCheck(33.3943, -104.5230); //insert here the coordinates of the markers, hardcoded
        });

    }

    gameCheck(laat, loon) {
        var lat1 = this.origin.lat;
        var lon1 = this.origin.lng;
        var lat2 = laat;
        var lon2 = loon;
        var p = 0.017453292519943295;    // Math.PI / 180
        var c = Math.cos;
        var a = 0.5 - c((lat2 - lat1) * p) / 2 +
            c(lat1 * p) * c(lat2 * p) *
            (1 - c((lon2 - lon1) * p)) / 2;

        var y = 12742 * Math.asin(Math.sqrt(a)); // 2 * R; R = 6371 km
        if (y > this.radius) {
            this.window.alert("This is a little too far! I don't think we can make it...");
            this.state.go("dynamicMap");
        }
        else {
            this.window.sessionStorage.setItem("trigger", true)            
            this.state.go("invaders")
        }
    }
    
    // clearStorage() {
    //     if (this.counter == 0) {
    //         this.window.sessionStorage.removeItem('gameScore');
    //     }
    // }
    // updateStats() {
    //     this.dragonService.updateDragon(this.profileId, this.updateDragon);
    // }
}