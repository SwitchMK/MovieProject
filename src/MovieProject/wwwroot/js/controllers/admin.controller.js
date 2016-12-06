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
                    console.error("Something went wrong while getting list of users!");
                    console.error(response);
                });
        };

        $scope.deleteRole = function (roleName) {
            var request = {
                RoleName: roleName,
                UserId: $scope.selectedUser.id
            };

            if (roleName == "Administrator") {
                showConfirm(request);
            } else {
                deleteRole(request);
            }
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
                    console.error("Something went wrong while adding new role!");
                    console.error(response);
                });
        };

        $scope.selectUser = function (user) {
            $scope.selectedUser = user;
            getRemainingRoles();
        };

        var deleteRole = function (request) {
            $scope.deleteRolePromise = adminService
                .deleteRole(request)
                .then(function (response) {
                    $scope.selectedUser.roles = response.data;
                    getRemainingRoles();
                }, function (response) {
                    console.error("Something went wrong while deleting role!");
                    console.error(response);
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
                    console.error("Something went wrong while getting roles for user!");
                    console.error(response);
                });
        };

        var showConfirm = function (request) {
            var confirm = $mdDialog.confirm()
                .title('Are your sure, you would like to detach administrator role?')
                .textContent('This user will be limited to her rights after deleting this role.')
                .ok('Yes')
                .cancel('Cancel');

            $mdDialog.show(confirm).then(function () {
                deleteRole(request);
            });
        };
    }
})();
