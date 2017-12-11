class Part10Controller {
    constructor($window, $state, $timeout, $dragonService, $accountService) {
        this.message = 'Hello from part 10!';
        this.window = $window;
        this.profileId = $accountService.getClaim("profileId");
        this.dragon = $dragonService.getDragon(this.profileId);
        this.counter = 20;
        
        this.state = $state;
        this.timeout = $timeout;
        this.dragonService = $dragonService;
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
        this.countDown();
    }
    // getScore() {
    //     if (this.counter == 0) {
    //         return this.window.sessionStorage.getItem("gameScore");
    //     }

    // }
    countDown() {
        this.timeout(() => {
            this.counter--;
            this.countDown();
        }, 2000);
    }
    clearStorage() {
        if (this.counter == 0) {
            this.window.sessionStorage.removeItem('gameScore');
        }
    }
    // updateStats() {
    //     this.dragonService.updateDragon(this.profileId, this.updateDragon);
    // }
}