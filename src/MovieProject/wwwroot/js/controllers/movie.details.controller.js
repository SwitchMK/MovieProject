(function () {
    'use strict';

    angular
        .module('movieApp')
        .controller('movieDetailsController', movieDetailsController);

    movieDetailsController.$inject = ['$scope', '$routeParams', 'movieService'];

    function movieDetailsController($scope, $routeParams, movieService) {
        $scope.getFilmDetails = function () {
            $scope.getFilmDetailsPromise = movieService
                .getFilmDetails($routeParams.filmId)
                .then(function (response) {
                    $scope.film = response.data;
                    getFeedbacks();
                }, function (response) {
                    console.error("Something went wrong while getting detailed information about movie!");
                    console.error(response);
                });
        };

        $scope.leaveFeedback = function (filmId) {
            var feedbackRequest = {
                Message: $scope.feedback,
                FilmId: filmId
            };

            $scope.leaveFeedbackPromise = movieService
                .leaveFeedback(feedbackRequest)
                .then(function (response) {
                    $scope.feedbacks = response.data;
                });
        };

        var getFeedbacks = function () {
            $scope.getFeedbacksPromise = movieService
                .getFeedbacks($scope.film.id)
                .then(function (response) {
                    $scope.feedbacks = response.data;
                });
        };
    }
})();