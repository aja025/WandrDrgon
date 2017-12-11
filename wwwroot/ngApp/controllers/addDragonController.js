class AddDragonController {
    constructor($dragonService, $state) {
        this.dragonService = $dragonService;
        // this.origin = codeAddress(hometown);
        this.state = $state;
        this.dragon = this.newDragon();

    }

    newDragon() {
        return {
            name: "",
            dragonSpriteId: 0,
            hometown: "",
            latitude: 0,
            longitude: 0,
            kmRadius: 25
        }
    }

    codeAddress(address) {
        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ "address": address }, (results, status) => {
            if (status == google.maps.GeocoderStatus.OK && results.length > 0) {
                var location = results[0].geometry.location,
                    lat = location.lat(),
                    lng = location.lng();
                // document.getElementById("latitude").value = lat;
                // document.getElementById("longitude").value = lng;
                this.dragon.latitude = lat;
                this.dragon.longitude = lng;
            }
            else {
                alert("I'm not sure where that is... Can you check your spelling? Some hometown examples would be Los Angeles, CA; London, England; New York, New York, USA; etc...")
            }
        });
    }

    addDragon() {
        if (this.dragon.latitude == 0 && this.dragon.longitude == 0) {
            alert("Please click the select town button")
        } else {
            this.dragonService.addDragon(this.dragon, res => {
                this.state.go("story");
            });
        }
      
        // if (confirm("Are you sure you want this dragon? You can't change your mind after it's born!") == true) {
        //     this.dragonService.addDragon(this.dragon, res => {
        //         this.state.go("story");
        //     })
        // }
        // else {
        //     this.state.reload;
        // }
    }

}