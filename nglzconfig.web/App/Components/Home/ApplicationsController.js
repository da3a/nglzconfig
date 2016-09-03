var ApplicationsController = function(lzconfigService) {
    this.title = "Applications View";
    var _this = this;

    handleResponse = function(response) {
        console.log("Applications Response Received");
        _this.applications = response.value;
    }

    getApplications = function() {
        lzconfigService.Applications().query(handleResponse);
    }

    getApplications();

    //$scope.$on('$routeChangeSuccess', function () {
    //    console.log("ApplicationsController initialization");
    //    getApplications();
    //});
}

var app = angular.module("lzconfig");
app.controller("ApplicationsController", ApplicationsController);