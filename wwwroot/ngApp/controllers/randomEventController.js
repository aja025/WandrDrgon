class RandomEventController {
    constructor($accountService, $dragonService, $state, $eventService) {
        this.state = $state;
        this.dragonService = $dragonService;
        this.eventService = $eventService;
        this.random = Math.floor((Math.random() * 10));  // Decide which random event happens 0-9
        console.log(this.random);
        this.event = this.eventService.getEvent(this.random);  // retrieve the random event details
        this.profileId = $accountService.getClaim("profileId");  // Get profileId from claims
        $dragonService.getDragon(this.profileId).$promise.then((data) => { // Get dragon object and resolve promise
            this.dragon = data;
            console.log(this.dragon);
            this.rXP = this.getRXP();  // get a random number of xp between 1-500
            console.log(this.rXP);
            this.checkXP();
            this.updateStats();
        });
    }

    ok() {
        this.state.go("dynamicMap");
    }

    getRXP() {
        if (this.dragon.xp < 5000) {
            return Math.floor((Math.random() * 250) + 1);
        } else {
            return Math.floor((Math.random() * 500) + 1);
        }
    }

    checkXP() {
        if (this.random == 1) {  // if you escape gwenvel you dont win or lose xp
            this.rXP = 0;
        } else if (this.rXP > this.dragon.xp) {  // makes sure you dont lose more xp than you have
            this.rXP = this.dragon.xp;
        } else {
            return;
        }
    }

    updateStats() {
        this.updateDragon = {
            xp: this.rXP * this.event.win,
            kmRadius: this.rXP * this.event.win * .2,
            kmTravelled: this.rXP * this.event.km,
            challengesWon: 0,
            tripsTaken: this.event.trips
        };
        console.log(this.updateDragon);
        console.log(this.event);
        this.dragonService.updateDragon(this.profileId, this.updateDragon);
    }

}