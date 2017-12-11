class DashboardController {
    constructor($accountService, $dashboardService, $dragonService, $profileService, $filepicker) {
        this.message = 'Hello from the Dashboard!';
        this.profileId = $accountService.getClaim("profileId");  // Get profileId from claims
        this.dashboard = $dashboardService.getDashboard(this.profileId);  // Get dashboard object by profileId
        this.logs = $dragonService.getLog(this.profileId);
        this.profileService = $profileService;
        this.itemLog = $dragonService.getItemLog(this.profileId);
        console.log(this.itemLog);
        this.myIndex = 1;

    }
    editProfile() {
        this.profileService.editProfile2(this.profileId, this.dashboard.profile);
    }

    editProfile2(profile) {
        console.log(profile);
        this.profileService.editProfile2(profile.profileId, profile);
    }

    pickFile() {
        var profile = this.dashboard.profile;
        this.client = filestack.init('ACsY2WljRSubAvbaGgpgWz');
        this.client.pick({
            accept: 'image/*',
            imageMax: [320, 320],
            transformations: { crop: { aspectRatio: 1, force: true } }
            // transformations: { crop: { force: true } }
        }).then(function (result) {
            const fileUrl = result.filesUploaded[0].url;
            profile.image = fileUrl;
            return profile;
        }).then(profile => this.editProfile2(profile));
    }
}