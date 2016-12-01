(function () {
    'use strict';

    angular
        .module('movieApp')
        .controller('screeningsController', screeningsController);

    screeningsController.$inject = ['$scope', 'screeningsService'];

    function screeningsController($scope, screeningsService) {
        $scope.getFilmTheatres = function (minDate, maxDate) {
            var request = {
                StartDate: minDate,
                EndDate: maxDate,
                FilmId: $scope.film
            };

            $scope.getFilmTheatresPromise = screeningsService
                .getFilmTheatres(request)
                .then(function (response) {
                    $scope.filmTheatres = response.data;
                }, function (response) {
                    console.error("Something went wrong while getting theatres!");
                });
        };

        $scope.getFilms = function () {
            $scope.getFilmsPromise = screeningsService
                .getFilms()
                .then(function (response) {
                    $scope.films = response.data;
                    $scope.getFilmTheatres(null, null);
                }, function (response) {
                    console.error("Something went wrong while getting films!");
                });
        };

        $scope.getTheatres = function () {
            $scope.getTheatresPromise = screeningsService
                .getTheatres()
                .then(function (response) {
                    $scope.theatres = response.data;
                }, function (response) {
                    console.error("Something went wrong while getting theatres!");
                });
        };

        $scope.getFilmsInTheatre = function () {
            var request = {
                TheatreId: $scope.theatre,
                Day: $scope.day
            };

            $scope.getFilmsInTheatrePromise = screeningsService
                .getFilmsInTheatre(request)
                .then(function (response) {
                    $scope.filmsInTheatre = response.data;
                }, function (response) {
                    console.error("Something went wrong while getting films for selected theatre!");
                });
        };

        $scope.initFunctions = function () {
            $scope.getFilms();
            $scope.getTheatres();
            $scope.getFilmsInTheatre();
        };
    }
})();
