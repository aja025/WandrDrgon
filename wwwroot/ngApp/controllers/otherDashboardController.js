class OtherDashboardController {
    constructor($accountService, $dashboardService, $dragonService, $profileService, $stateParams) {
        this.message = 'Hello from the Dashboard!';
        this.profileService = $profileService;
        this.profileId = $accountService.getClaim("profileId");
        let id = $stateParams["id"];
        this.dashboard = $dashboardService.getDashboard(id);  // Get dashboard object by profileId
        this.logs = $dragonService.getLog(id);
        this.myIndex=1;
        this.itemLog = $dragonService.getItemLog(id);

    }
    

}