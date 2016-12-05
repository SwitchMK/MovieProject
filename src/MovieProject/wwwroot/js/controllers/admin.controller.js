(function () {
    'use strict';

    angular
        .module('movieApp')
        .controller('adminController', adminController);

    adminController.$inject = ['$scope', '$mdDialog', 'adminService'];

    function adminController($scope, $mdDialog, adminService) {
        $scope.getUsers = function () {
            $scope.getUsersPromise = adminService
                .getUsers()
                .then(function (response) {
                    $scope.users = response.data;
                    if ($scope.users.length > 0) {
                        $scope.selectedUser = $scope.users[0];
                        getRemainingRoles();
                    }
                }, function (response) {
                });
        };

        $scope.selectUser = function (user) {
            $scope.selectedUser = user;
            getRemainingRoles();
        };

        $scope.deleteRole = function (roleName) {
            var request = {
                RoleName: roleName,
                UserId: $scope.selectedUser.id
            };

            $scope.deleteRolePromise = adminService
                .deleteRole(request)
                .then(function (response) {
                    $scope.selectedUser.roles = response.data;
                    getRemainingRoles();
                }, function (response) {
                });
        };

        $scope.addRole = function () {
            var request = {
                RoleName: $scope.roleToAdd,
                UserId: $scope.selectedUser.id
            };

            $scope.addRolePromise = adminService
                .addRole(request)
                .then(function (response) {
                    $scope.selectedUser.roles = response.data;
                    getRemainingRoles();
                }, function (response) {
                });
        };

        var getRemainingRoles = function () {
            var request = {
                UserId: $scope.selectedUser.id
            };

            $scope.getRemainingRolesPromise = adminService
                .getRemainingRoles(request)
                .then(function (response) {
                    $scope.remainingRoles = response.data;
                    if ($scope.remainingRoles.length > 0)
                        $scope.roleToAdd = $scope.remainingRoles[0].name;
                }, function (response) {
                });
        };
    }
})();
