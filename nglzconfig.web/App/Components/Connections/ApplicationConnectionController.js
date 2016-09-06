
var ApplicationConnectionController = function ($uibModalInstance, lzconfigService, connection) {
    var _this = this;
    this.errorMessage = "";
    _this.connection = connection;

    handleError = function (error) {
        _this.errorMessage = JSON.stringify(error);
    }

    this.connectionTypeChange = function()
    {
        console.log("connectionTypeChange called");
    }

    this.saveConnection = function()
    {
        console.log("saveConnection:" + JSON.stringify(this.connection));
        if (_this.connection.Password != "" && _this.connection.Password != this.verifyPassword)
            console.log("passwords set but do not verify");


        if (_this.connection.CreatedDate == null) {
            lzconfigService.ApplicationConnections(handleError).create({ id: connection.ApplicationId, Name:connection.Name },
            connection);
        } else {
            lzconfigService.ApplicationConnections(handleError).update({ id: connection.ApplicationId, Name: connection.Name },
            connection);
        };

    }

    this.cancel = function () {
        $uibModalInstance.dismiss("cancel");
    }

    var handleError = function (error) {
        _this.errorMessage = JSON.stringify(error);
    }

    var handleResponse = function (response) {
        console.log("ConnectionTypes Response Received");
        _this.connectionTypes = response.value;
    }

    lzconfigService.ConnectionTypes(handleError).query(null, handleResponse);
}

app = angular.module("lzconfig");

app.controller("ApplicationConnectionController", ApplicationConnectionController)