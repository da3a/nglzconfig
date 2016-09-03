var app = angular.module("lzconfig", ["ngRoute", "ngResource","ui.router", "ui.bootstrap"]);

app.config(function ($httpProvider) {
    $httpProvider.defaults.withCredentials = true;
});

var router = function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "./App/Components/Home/ApplicationsView.html",
            controller: "ApplicationsController",
            controllerAs: "vm"
        })
        .when("/application/:ID",
        {
            templateUrl: "./App/Components/Application/ApplicationView.html",
            controller: "ApplicationController",
            controllerAs: "vm"
        })
        .otherwise("/");
}

app.config(router);

//app.config(function ($stateProvider, $urlRouterProvider) {

//    $urlRouterProvider.otherwise("/main/tab1");

//    $stateProvider
//        .state("main", { abtract: true, url: "/main", templateUrl: "main.html" })
//            .state("main.tab1", { url: "/tab1", templateUrl: "tab1.html" })
//            .state("main.tab2", { url: "/tab2", templateUrl: "tab2.html" })
//            .state("main.tab3", { url: "/tab3", templateUrl: "tab3.html" });

//});