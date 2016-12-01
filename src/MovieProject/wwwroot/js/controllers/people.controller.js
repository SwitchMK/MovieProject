(function () {
    'use strict';

    angular
        .module('movieApp')
        .controller('peopleController', peopleController);

    peopleController.$inject = ['$scope', '$routeParams', 'peopleService'];

    function peopleController($scope, $routeParams, peopleService) {
        $scope.getPeople = function () {
            $scope.getPeoplePromise = peopleService
                .getPeople()
                .then(function (response) {
                    $scope.people = response.data;
                }, function (response) {
                    console.error("Something went wrong while getting people!");
                    console.error(response);
                });
        };
    }
})();
