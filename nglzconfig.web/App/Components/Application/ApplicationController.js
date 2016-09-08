
var ApplicationController = function ($routeParams, $location,  $uibModal,  lzconfigService) {
    var _this = this;
    var emptyGuid = "00000000-0000-0000-0000-000000000000";
    this.title = "Application View";
    this.errorMessage = "";

    this.tabs = [
        { title: "Application", url: "app/components/application/applicationDetailView.html"},
        { title: "Connection Strings", url: "app/components/connections/applicationConnectionsView.html" },
        { title: "Application Variables", url: "app/components/variables/applicationVariablesView.html"}
    ];

    //this.tabs = [
    //{ heading: "Application", route: "main.tab1", active: false },
    //{ heading: "Connection Stringa", route: "main.tab2", active: false },
    //{ heading: "Application Variables", route: "main.tab3", active: false }
    //];

    //this.go = function (route) {
    //    this.go(route);
    //};

    this.currentTab = "app/components/application/applicationDetailView.html";
    this.selectedVariable = null;
    this.onClickTab = function (tab) {
        _this.currentTab = tab.url;
    }


    var handleError = function (error) {
        _this.errorMessage = JSON.stringify(error);
    }

    var handleResponse = function (response) {
        console.log("Application Response Received");
        _this.application = response;
    }

    var getApplication = function () {
        if ($routeParams.ID !== emptyGuid) {
            console.log("Application:getApplication");
            lzconfigService.Applications(handleError).get({ id: $routeParams.ID }, handleResponse);
        }
    };

    this.getTemplate = function(variable) {
        if (!_this.selectedVariable)
            return 'display';
        if (variable.Name === this.selectedVariable.Name)
            return 'edit';
        else
            return 'display';
    }

    //application 
    this.deleteApplication = function () {
        if (!confirm("Are you sure you want to delete the application?"))
            return;
        console.log("Application:deleteApplication:" + JSON.stringify(_this.application));

        lzconfigService.Applications(handleError).delete({ id: this.application.ID });
        $location.path("/applicationList");
    }

    this.saveApplication = function() {
        console.log("Application:SaveApplication");

        if ($routeParams.ID === emptyGuid) {
            lzconfigService.Applications().create({ id: $routeParams.ID },
            {
                ID: _this.application.ID,
                Name: this.application.Name,
                Description: this.application.Description,
                URL: this.application.URL,
                CreatedBy: "user",
                ModifiedBy: "user" //set in service
            });
        } else {
            lzconfigService.Applications().update({ id: $routeParams.ID },
            {
                ID: _this.application.ID,
                Name: this.application.Name,
                Description: this.application.Description,
                URL: this.application.URL,
                CreatedBy: this.application.CreatedBy,
                CreatedDate: this.application.CreatedDate,
                ModifiedBy: "user" //set in service
            });
        };
    }
    //application end

   //variables
    this.cancelApplication = function() {
        $location.path("/");
    }
    
    this.editVariable = function(variable) {
        console.log("editVariable:" + JSON.stringify(variable));
        this.selectedVariable = angular.copy(variable);
    }

    this.cancelEditVariable = function(index)
    {
     console.log("cncelEditVariable:" + index);
    this.application.tblApplicationVariable.splice(index, 1);
    this.selected = null;
    }
    //variables end


    //connections

    this.editConnection = function (connection) {
        if (connection == null)
            connection = {
                ApplicationID: _this.application.ID,
                Name: "",
                ConnectionString: "",
                VirtualConnectionString: "",
                Password: null,
                CommandTimeout: null,
                ProviderName : null,
                CreatedDate : null,
                CreatedBy : "",
                ModifiedDate :null,
                ModifiedBy: ""
            }
        else
            console.log("editConnection:" + JSON.stringify(connection));

        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'app/components/connections/applicationConnectionView.html',
            controller: 'ApplicationConnectionController',
            controllerAs: 'vm',
            size: null,
            resolve: { connection: connection }
        }).result.then(() => {getApplication()});
    }

    this.deleteConnection = function(index) {
        if (!confirm("Are you sure you want to delte the connection?"))
            return;

        var connection = _this.application.tblApplicationConnection[index];
        console.log("deleteConnection" + JSON.stringify(connection));
        lzconfigService.ApplicationConnections(handleError).delete(connection);
        _this.application.tblApplicationConnection.splice(index, 1);
    }
    //connections end 

    getApplication();
}

var app = angular.module("lzconfig");
app.controller("ApplicationController", ApplicationController);