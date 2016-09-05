
var ApplicationConnectionController = function ($uibModalInstance, lzconfigService, connection) {
    var _this = this;
    _this.connection = connection;

    handleError = function (error) {
        _this.errorMessage = JSON.stringify(error);
    }

    var connectionTypeChange = function()
    {
        console.log("connectionTypeChange called");
    }

    var saveConnection = function()
    {
        console.log("saveConnection:" + JSON.stringify(this.connection));
    }

    var cancel = function () {
        $uibModalInstance.dismiss("cancel");
    }

    var handleResponse = function (response) {
        console.log("ConnectionTypes Response Received");
        _this.connectionTypes = response.value;
    }

    lzconfigService.ConnectionTypes(handleError).query(null, handleResponse);
}

app = angular.module("lzconfig");

app.controller("ApplicationConnectionController", ApplicationConnectionController)