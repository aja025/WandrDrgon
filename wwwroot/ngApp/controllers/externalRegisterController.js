class ExternalRegisterController {
    constructor($accountService, $location) {
        this.registerUser;
        this.validationMessages;
        this.accountService = $accountService;
        this.location = $location;
    }

    register() {
        this.accountService.registerExternal(this.registerUser.email)
            .then((result) => {
                this.location.path('/updateProfile');
            }).catch((result) => {
                this.validationMessages = result;
            });
    }
}