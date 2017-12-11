class SteveController {
    constructor($staticMapService, $stateParams, $timeout, $window) {
        // this.message = 'Hello from the old Steve page!';
        this.staticMapService = $staticMapService;
        this.counter = 20;
        this.timeout = $timeout;
        this.window = $window;
        this.countDown();
        
        // JLT - new code
        this.lat = $stateParams["latitude"];
        this.lng = $stateParams["longitude"];
        
        // JLT - new code
        this.coord = this.staticMapService.setMap(this.lat, this.lng); 
    }

    countDown() {
        this.timeout(() => {
            // if(this.counter === 0){
            //     this.window.alert("Times Up!");
            // }
            this.counter--;
            this.countDown();
        }, 1000);
     
    }
        
    }
    








 // JLT - old code
        // this.lat = lat;
        // this.lng = lng;

        // console.log(`JLT - latitude = ${this.lat}`);
        // console.log(`JLT - longitude = ${this.lng}`);

        // JLT - old code
        // this.coord = this.staticMapService.setMap(lat, lng);
