(function () {
    'use strict';

    angular
        .module('movieApp')
        .service('screeningsService', screeningsService);

    screeningsService.$inject = ['$http'];

    function screeningsService($http) {
        var url = "api/Screening";

        this.getFilms = function () {
            return $http.get(url + "/GetFilms");
        };

        this.getTheatres = function () {
            return $http.get(url + '/GetTheatres');
        };

        this.getFilmTheatres = function (data) {
            return $http({
                method: 'POST',
                url: url + '/GetFilmTheatres',
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.getFilmsInTheatre = function (data) {
            return $http({
                method: 'POST',
                url: url + '/GetFilmsInTheatre',
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.submitTheatreInformation = function (data) {
            return $http({
                method: 'POST',
                url: url + '/SubmitTheatreInformation',
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.exportToXmlFile = function (data) {
            return $http({
                method: 'POST',
                url: url + '/ExportToXmlFile',
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.importFromXmlFile = function (data) {
            return $http({
                method: 'POST',
                url: url + '/ImportFromXmlFile',
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };
    }
})();