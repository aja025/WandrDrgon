class BreakoutController {
    constructor($window, $state, $timeout, $dragonService, $accountService) {
        this.message = 'Hello from part 12!';
        this.window = $window;
        this.profileId = $accountService.getClaim("profileId");
        this.dragonService = $dragonService;        
        this.dragon = $dragonService.getDragon(this.profileId);
        this.state = $state;
        this.timeout = $timeout;
        
        this.minigame = $dragonService.getDragon(this.profileId).$promise.then((dragon) => {
            this.origin = { lat: dragon.latitude, lng: dragon.longitude };
            this.radius = dragon.kmRadius;
            this.heck= this.gameCheck(29.978827, 31.134223); //insert here the coordinates of the markers, hardcoded
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
            this.state.go("breakout")
        }

    }
}

        // this.counter = 20;
// if (this.counter == 0) {
        //     this.score = this.getScore();
        //     console.log(this.score);
        //     this.travelled = this.score * 0.5;
        //     console.log(this.travelled);


        //     this.updateDragon = {
        //         XP: this.score,
        //         KmRadius: 300,
        //         KmTravelled: this.travelled,
        //         ChallengesWon: 1,
        //     };
        //     this.updateStats();
        // };
        // this.countDown();

        // getScore() {
    //     if (this.counter == 0) {
    //         return this.window.sessionStorage.getItem("gameScore");
    //     }

    // }
    // countDown() {
    //     this.timeout(() => {
    //         this.counter--;
    //         this.countDown();
    //     }, 1000);
    // }
    // clearStorage() {
    //     if (this.counter == 0) {
    //         this.window.sessionStorage.removeItem('gameScore');
    //     }
    // }
    // updateStats() {
    //     this.dragonService.updateDragon(this.profileId, this.updateDragon);
    // }