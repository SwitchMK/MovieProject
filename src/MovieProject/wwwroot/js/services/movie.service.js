(function () {
    'use strict';

    angular
        .module('movieApp')
        .service('movieService', movieService);

    movieService.$inject = ['$http'];

    function movieService($http) {
        var url = "api/Film";

        this.getFilms = function () {
            return $http.get(url + "/GetFilms");
        };

        this.getFilmDetails = function (data) {
            return $http({
                method: 'POST',
                url: url + '/GetFilmDetails',
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.rateMovie = function (data) {
            return $http({
                method: 'POST',
                url: url + '/RateMovie',
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.leaveFeedback = function (data) {
            return $http({
                method: 'POST',
                url: url + '/LeaveFeedback',
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.getFeedbacks = function (data) {
            return $http({
                method: 'POST',
                url: url + '/GetFeedbacks',
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };
    }
})();