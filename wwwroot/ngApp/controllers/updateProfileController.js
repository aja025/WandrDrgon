class UpdateProfileController {
    constructor($profileService, $accountService, $state) {
        this.message = 'Welcome to your profile page! Lets get started (after intial set up, edits can be made on your Dashboard page)';
        this.state = $state;
        this.profileService = $profileService;        
        this.profileId = $accountService.getClaim("profileId");
        this.profile = $profileService.getProfile(this.profileId);
        // this.profile = this.newProfile();
        // this.addProfile = this.profileService.addProfile();
        // this.displayName = "";
    }
    // newProfile() {
    //     return {
    //         DisplayName: "",
    //         HomeTown: "" //remember to remove HomeTwon from backend so we can remove this.s
    //         // Dragon: ""
    //     };
    // }
    addProfile() {
        this.profileService.editProfile(this.profileId, this.profile, res => {
            this.state.go("addDragon");
        });
    } 

    

    // cancel() {
    //     this.state.go("home")
    // }
    // time(){
    //     var date = new Date();
    //    return date;
    // }
    // addAd() {
    //     this.adService.addAd(this.ad, res=> {
    //         this.state.reload();
    //     });
    // }

}
