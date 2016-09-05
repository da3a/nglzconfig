var app = angular.module("lzconfig", ["ngRoute", "ngResource", "ui.bootstrap"]);

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

//    $urlRouterProvider.otherwise("/");

//    $stateProvider
//        .state("main", { abtract: true, url: "/application/:ID", templateUrl: "./App/Components/Home/ApplicationsView.html" })
//            .state("main.tab1", { url: "/tab1", templateUrl: "./app/components/application/applicationDetailView.html" })
//            .state("main.tab2", { url: "/tab2", templateUrl: "./app/components/connections/applicationConnectionsView.html" })
//            .state("main.tab3", { url: "/tab3", templateUrl: "./app/components/variables/applicationVariablesView.html" });

//});