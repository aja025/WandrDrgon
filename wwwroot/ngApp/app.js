var myApp = angular.module("myApp", [ "ui.router", "ngResource", "xeditable" ]);

myApp.controller("PlatformsController", PlatformsController);
myApp.controller("AccountController", AccountController);
myApp.controller("AllUsersController", AllUsersController);
myApp.controller("ConfirmEmailController", ConfirmEmailController);
myApp.controller("DashboardController", DashboardController);
myApp.controller("DynamicMapController", DynamicMapController);
myApp.controller("ExternalRegisterController", ExternalRegisterController);
myApp.controller("HomeController", HomeController);
myApp.controller("LoginController", LoginController);
myApp.controller("OtherDashboardController", OtherDashboardController);
myApp.controller("RegisterController", RegisterController);
myApp.controller("SecretController", SecretController);
myApp.controller("UpdateProfileController", UpdateProfileController);
myApp.controller("AddDragonController", AddDragonController);
myApp.controller("Part10Controller", Part10Controller);
myApp.controller("BreakoutController", BreakoutController);
myApp.controller("InvadersController", InvadersController);
myApp.controller("SteveController", SteveController);
myApp.controller("SkyboatController", SkyboatController);
myApp.controller("LeaderBoardController", LeaderBoardController);
myApp.controller("TankController", TankController);
myApp.controller("RandomEventController", RandomEventController);
myApp.controller("AdminController", AdminController);
myApp.controller("GridController", GridController);



myApp.service("$accountService", AccountService);
myApp.service("$dragonService", DragonService);
myApp.service("$profileService", ProfileService);
myApp.service("$staticMapService", StaticMapService);
myApp.service("$dashboardService", DashboardService);
myApp.service("$eventService", EventService);
myApp.service("$filepicker", function ($window) {
    return $window.filepicker;
});

myApp.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {

    $stateProvider
        .state("home", {
            url: "/",
            templateUrl: "/ngApp/views/home.html",
            controller: HomeController,
            controllerAs: "controller"
        }).state("secret", {
            url: "/secret",
            templateUrl: "/ngApp/views/secret.html",
            controller: SecretController,
            controllerAs: "controller"
        }).state("login", {
            url: "/login",
            templateUrl: "/ngApp/views/login.html",
            controller: LoginController,
            controllerAs: "controller"
        }).state("register", {
            url: "/register",
            templateUrl: "/ngApp/views/register.html",
            controller: RegisterController,
            controllerAs: "controller"
        }).state("externalRegister", {
            url: "/externalRegister",
            templateUrl: "/ngApp/views/externalRegister.html",
            controller: ExternalRegisterController,
            controllerAs: "controller"
        }).state("platforms", {
            url: "/platforms",
            templateUrl: "/ngApp/views/platforms.html",
            controller: PlatformsController,
            controllerAs: "controller"
        }).state("notFound", {
            url: "/notFound",
            templateUrl: "/ngApp/views/notFound.html"
        }).state("dynamicMap", {
            url: "/travelStart",
            templateUrl: "/ngApp/views/dynamicMap.html",
            controller: DynamicMapController,
            controllerAs: "controller"
        }).state("dashboard", {
            url: "/dashboard",
            templateUrl: "/ngApp/views/dashboard.html",
            controller: DashboardController,
            controllerAs: "controller"
        }).state("otherdashboard", {
            url: "/otherdashboard/:id",
            templateUrl: "/ngApp/views/otherDashboard.html",
            controller: OtherDashboardController,
            controllerAs: "controller"
        }).state("updateProfile", {
            url: "/updateProfile",
            templateUrl: "/ngApp/views/updateProfile.html",
            controller: UpdateProfileController,
            controllerAs: "controller"
        }).state("addDragon", {
            url: "/addDragon",
            templateUrl: "/ngApp/views/addDragon.html",
            controller: AddDragonController,
            controllerAs: "controller"
        }).state("part10", {
            url: "/part10",
            templateUrl: "/ngApp/views/part10.html",
            controller: Part10Controller,
            controllerAs: "controller"
        }).state("steve", {
            url: "/steve/:latitude/:longitude",         // JLT - modified to pass in latitude and longitude
            templateUrl: "/ngApp/views/steve.html",
            controller: SteveController,
            controllerAs: "controller"
        }).state("breakout", {
            url: "/breakout",
            templateUrl: "/ngApp/views/breakout.html",
            controller: BreakoutController,
            controllerAs: "controller"
        }).state("invaders", {
            url: "/invaders",            
            templateUrl: "/ngApp/views/invaders.html",
            controller: InvadersController,
            controllerAs: "controller"
        }).state("skyboat", {
            url: "/skyboat",            
            templateUrl: "/ngApp/views/skyboat.html",
            controller: SkyboatController,
            controllerAs: "controller"
        }).state("leaderboard", {
            templateUrl: "/ngApp/views/leaderBoard.html",
            controller: LeaderBoardController,
            controllerAs: "controller"
        }).state("tank", {
            url: "/tank",            
            templateUrl: "/ngApp/views/tank.html",
            controller: TankController,
            controllerAs: "controller"
        }).state("region1", {
            url: "/region1/:latitude/:longitude",
            templateUrl: "/ngApp/views/region1.html",
            controller: SteveController,
            controllerAs: "controller"
        }).state("region2", {
            url: "/region2/:latitude/:longitude",
            templateUrl: "/ngApp/views/region2.html",
            controller: SteveController,
            controllerAs: "controller"
        }).state("region3", {
            url: "/region3/:latitude/:longitude",
            templateUrl: "/ngApp/views/region3.html",
            controller: SteveController,
            controllerAs: "controller"
        }).state("region4", {
            url: "/region4/:latitude/:longitude",
            templateUrl: "/ngApp/views/region4.html",
            controller: SteveController,
            controllerAs: "controller"
        }).state("region5", {
            url: "/region5/:latitude/:longitude",
            templateUrl: "/ngApp/views/region5.html",
            controller: SteveController,
            controllerAs: "controller"
        }).state("region6",{
            url: "/region6/:latitude/:longitude",
            templateUrl: "/ngApp/views/region6.html",
            controller: SteveController,
            controllerAs: "controller"
        }).state("region7", {
            url: "/region7/:latitude/:longitude",
            templateUrl: "/ngApp/views/region7.html",
            controller: SteveController,
            controllerAs: "controller",
        }).state("region8", {
            url: "/region8/:latitude/:longitude",
            templateUrl: "/ngApp/views/region8.html",
            controller: SteveController,
            controllerAs: "controller",
        }).state("region9", {
            url: "/region9/:latitude/:longitude",
            templateUrl: "/ngApp/views/region9.html",
            controller: SteveController,
            controllerAs: "controller"
        }).state("randomEvent", {
            // url: "/randomEvent",  <-- This hides the url -->
            templateUrl: "/ngApp/views/randomEvent.html",
            controller: RandomEventController,
            controllerAs: "controller"
        }).state("allUsers", {
            url: "/search",
            templateUrl: "/ngApp/views/allUsers.html",
            controller: AllUsersController,
            controllerAs: "controller"
        }).state("admin", {
            url: "/admin",
            templateUrl: "/ngApp/views/admin.html",
            controller: AdminController,
            controllerAs: "controller"

        }).state("grid", {
            url: "/grid",
            templateUrl: "/ngApp/views/grid.html",
            controller: GridController,
            controllerAs: "controller"

        }).state("aboutUs", {
            url: "/aboutUs",
            templateUrl: "/ngApp/views/aboutUs.html",
            controller: SteveController,
            controllerAs: "controller"
        }).state("howTo", {
            url: "/howTo",
            templateUrl: "/ngApp/views/gamePlay.html"
        }).state("story", {
            url: "/story",
            templateUrl: "/ngApp/views/story.html"
        });

    // Handle request for non-existent route
    $urlRouterProvider.otherwise("/notFound");

    // Enable HTML5 navigation
    $locationProvider.html5Mode(true);
});

myApp.factory("authInterceptor", ($q, $window, $location) => ({
    request: function (config) {
        config.headers = config.headers || {};
        config.headers["X-Requested-With"] = "XMLHttpRequest";
        return config;
    },
    responseError: function (rejection) {
        if (rejection.status === 401 || rejection.status === 403) {
            $location.path("/login");
        }
        return $q.reject(rejection);
    }
}));

myApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push("authInterceptor");
});
