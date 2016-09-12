(function() {
    console.log("iife running");
    var _this = this;

    this.BASEURL = "http://localhost/nglzconfig.services/odata/";

    var lzconfigService = function($resource) {

        var applications = function (resourceErrorHandler) {
            console.log("lzconfigService:Applications");

            var queryAction = {
                method: 'GET',
                url: _this.BASEURL + "Applications",
                isArray: false,
                interceptor : {responseError : resourceErrorHandler}
            };

            var getAction = {
                method: 'GET',
                url: _this.BASEURL + "Applications(:id)",
                params: { $expand: "tblApplicationConnection,tblApplicationVariable" }
            };

            var createAction = {
                method: 'POST',
                url: _this.BASEURL + "Applications",
                isArray: false
            };

            var updateAction = {
                method: 'PUT',
                url: _this.BASEURL + "Applications(:id)"
            };
            var deleteAction = {
                method: 'DELETE',
                url: _this.BASEURL + "Applications(:id)",
                params: { ID: "@ID" },
                isArray: false
            };
            return $resource(_this.BASEURL + "Applications", null, {
                get: getAction,
                query: queryAction,
                create: createAction,
                update: updateAction,
                delete: deleteAction
            });
        }

        var connectionTypes = function (resourceErrorHandler) {
            var queryAction = {
                method: 'GET',
                url: _this.BASEURL + "ConnectionTypes",
                isArray: false,
                interceptor: { responseError: resourceErrorHandler }
            };

            return $resource(_this.BASEURL + "ConnectionTypes(:id)", null, {
                query: queryAction
            });

        }

        var applicationVariables = function(resourceErrorHandler)
        {
            var createAction = {
                method: 'POST',
                url: _this.BASEURL + "ApplicationVariables",
                isArray: false
            };

            var updateAction = {
                method: 'PUT',
                url: _this.BASEURL + "ApplicationVariables(ApplicationID=:ID,Name=':Name')",
                params: { ID: "@ApplicationID", Name: "@Name" },
                interceptor: { responseError: resourceErrorHandler }
            };
            var deleteAction = {
                method: 'DELETE',
                url: _this.BASEURL + "ApplicationVariables(ApplicationID=:ApplicationID,Name=':Name')",
                isArray: false,
                interceptor: { responseError: resourceErrorHandler }
            };

            return $resource(_this.BASEURL + "ApplicationVariables(ApplicationID=:ID,Name=':Name')", null, {
                create: createAction,
                update: updateAction,
                delete: deleteAction
            });

        }
        var applicationConnections = function (resourceErrorHandler) {

            var createAction = {
                method: 'POST',
                url: _this.BASEURL + "ApplicationConnections",
                isArray: false
            };

            var updateAction = {
                method: 'PUT',
                url: _this.BASEURL + "ApplicationConnections(ApplicationID=:ID,Name=':Name')",
                params: { ID: "@ApplicationID", Name: "@Name" },
                interceptor: { responseError: resourceErrorHandler }
            };
            var deleteAction = {
                method: 'DELETE',
                url: _this.BASEURL + "ApplicationConnections(ApplicationID=:ApplicationID,Name=':Name')",
                isArray: false,
                interceptor: { responseError: resourceErrorHandler }
            };

            return $resource(_this.BASEURL + "ApplicationConnections(ApplicationID=:ID,Name=':Name')", null, {
                create: createAction,
                update: updateAction,
                delete: deleteAction
            });
        }

        return {
            Applications: applications,
            ConnectionTypes: connectionTypes,
            ApplicationVariables : applicationVariables,
            ApplicationConnections : applicationConnections
        };
    }

    var app = angular.module("lzconfig");
    app.factory("lzconfigService", lzconfigService);
}())