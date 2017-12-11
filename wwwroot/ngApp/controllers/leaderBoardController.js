class LeaderBoardController {
    constructor($dragonService, $profileService, $state) {
        this.dragons = $dragonService.getDragons();
        this.state = $state;

    }
    getUser(id) {
        this.state.go("otherdashboard", { id: id });
    }

}