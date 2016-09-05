var ApplicationsController = function(lzconfigService) {
    this.title = "Applications View";
    var _this = this;

    handleResponse = function(response) {
        console.log("Applications Response Received");
        _this.applications = response.value;
    }

    handleError = function(error) {
        _this.errorMessage = JSON.stringify(error);
    }

    getApplications = function() {
        lzconfigService.Applications(handleError).query(handleResponse);
    }

    getApplications();

    //$scope.$on('$routeChangeSuccess', function () {
    //    console.log("ApplicationsController initialization");
    //    getApplications();
    //});
}

var app = angular.module("lzconfig");
app.controller("ApplicationsController", ApplicationsController);