class AllUsersController {
    constructor($accountService, $dashboardService, $dragonService, $profileService, $stateParams, $state) {
        this.state = $state;
        this.profileService = $profileService;
        this.users = this.profileService.getProfiles();
        this.myIndex = 1;
    }
    
    getUser(id) {
        this.state.go("otherdashboard", { id: id });
    }

}