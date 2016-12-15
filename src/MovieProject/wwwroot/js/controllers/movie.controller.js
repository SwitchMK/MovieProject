(function () {
    'use strict';

    angular
        .module('movieApp')
        .controller('movieController', movieController);

    movieController.$inject = ['$scope', '$routeParams', 'movieService'];

    function movieController($scope, $routeParams, movieService) {
        var ratedMovieId = null;

        $scope.getFilms = function () {
            $scope.getFilmsPromise = movieService
                .getFilms()
                .then(function (response) {
                    $scope.films = response.data;
                }, function (response) {
                    console.error("Something went wrong while getting films!");
                    console.error(response);
                });
        };

        $scope.rateMovie = function () {
            var rateRequest = {
                FilmId: ratedMovieId,
                Rating: $scope.rating
            };

            $scope.rateMoviePromise = movieService
                .rateMovie(rateRequest)
                .then(function (response) {
                    $scope.films = response.data;
                });
        };

        $scope.selectRateableMovie = function (film) {
            ratedMovieId = film.id;
            $('#input-id').rating('update', film.personalRating);
        };

        $("#input-id").rating({ size: 'xs' });

        $('#input-id').on('rating.change', function (event, value, caption) {
            $scope.rating = value;
        });

        $('#input-id').on('rating.clear', function(event) {
            $scope.rating = 0;
        });
    }
})();
