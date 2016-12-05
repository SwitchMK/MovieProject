(function () {
    'use strict';

    angular
        .module('movieApp')
        .service('adminService', adminService);

    adminService.$inject = ['$http'];

    function adminService($http) {
        var url = "api/Admin";

        this.getUsers = function () {
            return $http.get(url + "/GetUsers");
        };

        this.addRole = function (data) {
            return $http({
                method: 'POST',
                url: url + '/AddRole',
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.deleteRole = function (data) {
            return $http({
                method: 'POST',
                url: url + '/DeleteRole',
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.getRemainingRoles = function (data) {
            return $http({
                method: 'POST',
                url: url + '/GetRemainingRoles',
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };
    }
})();