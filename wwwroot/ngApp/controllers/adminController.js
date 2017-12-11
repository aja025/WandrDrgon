class AdminController {
    constructor ($accountService, $profileService, $dragonService, $state) {
        this.message = "Welcome to the admin page!";
        this.accountService = $accountService;
        this.profileService = $profileService;
        this.dragonService = $dragonService;
        this.state = $state;
        this.profiles = $profileService.getProfiles();
    }

    deleteProfile(id, userId) {
        this.profileService.deleteProfile(id, res => {
            this.accountService.deleteUser(userId);
            this.state.reload();
        });
    }

    getUser(id) {
        this.state.go("otherdashboard", { id: id });
    }
}