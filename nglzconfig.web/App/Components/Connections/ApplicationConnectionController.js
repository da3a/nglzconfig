
var ApplicationConnectionController = function ($uibModalInstance, lzconfigService, connection) {
    var _this = this;
    this.errorMessage = "";

    _this.connection = connection;

    var handleError = function (error) {
        _this.errorMessage = JSON.stringify(error);
    }

    var handleResponse = function (response) {
        console.log("ConnectionTypes Response Received");
        _this.connectionTypes = response.value;
    }

    this.connectionTypeChange = function()
    {
        console.log("connectionTypeChange called");
        console.log(this.connectionTypeId);
        var connectionType = _this.connectionTypes.filter(x=> x.ID === _this.connectionTypeId)[0];
        _this.connection.ConnectionString = connectionType.DefaultConnectionString;
        _this.connection.ProviderName = connectionType.ProviderName;
    }

    this.saveConnection = function()
    {
        console.log("saveConnection:" + JSON.stringify(this.connection));
        if (_this.connection.Password != "" && _this.connection.Password != this.verifyPassword)
            console.log("passwords set but do not verify");

        _this.connection.VirtualConnectionString = _this.connection.ConnectionString;

        if (_this.connection.CreatedDate == null) {
            _this.connection.CreatedDate = new Date();
            _this.connection.CreatedBy = "user";
            _this.connection.ModifiedDate = new Date();
            _this.connection.ModifiedBy = "user";
            lzconfigService.ApplicationConnections(handleError).create({ id: connection.ApplicationId, Name: connection.Name },
            connection);
        } else {
            lzconfigService.ApplicationConnections(handleError).update({ id: connection.ApplicationId, Name: connection.Name },
            connection);
        };
        $uibModalInstance.close();
    }

    this.cancel = function () {
        $uibModalInstance.dismiss("cancel");
    }


    lzconfigService.ConnectionTypes(handleError).query(null, handleResponse);
}

app = angular.module("lzconfig");

app.controller("ApplicationConnectionController", ApplicationConnectionController)