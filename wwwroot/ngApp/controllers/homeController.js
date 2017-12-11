class HomeController {
    constructor($accountService, $state) {
        this.accountService = $accountService;
        this.state = $state;
        
    }
    isLoggedIn() {
        return this.accountService.isLoggedIn();
    }
    start(){
        if(this.isLoggedIn()){
            this.state.go("dashboard");
        }else{
            this.state.go("login")
        }
    }
}