(function () {
    'use strict';

    angular
        .module('movieApp')
        .controller('peopleDetailsController', peopleDetailsController);

    peopleDetailsController.$inject = ['$scope', '$routeParams', 'peopleService'];

    function peopleDetailsController($scope, $routeParams, peopleService) {
        $scope.getPersonDetails = function () {
            $scope.getFilmDetailsPromise = peopleService
                .getPersonDetails($routeParams.personId)
                .then(function (response) {
                    $scope.person = response.data;
                }, function (response) {
                    console.error("Something went wrong while getting detailed information about person!");
                    console.error(response);
                });
        };
    }
})();
