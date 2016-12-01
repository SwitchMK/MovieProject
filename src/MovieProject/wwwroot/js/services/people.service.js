(function () {
    'use strict';

    angular
        .module('movieApp')
        .service('peopleService', peopleService);

    peopleService.$inject = ['$http'];

    function peopleService($http) {
        var url = "api/People";

        this.getPeople = function () {
            return $http.get(url + "/GetPeople");
        };

        this.getPersonDetails = function (data) {
            return $http({
                method: 'POST',
                url: url + '/GetPersonDetails',
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };
    }
})();