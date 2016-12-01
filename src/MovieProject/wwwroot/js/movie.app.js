(function () {
    'use strict';

    var app = angular.module('movieApp', ['ngRoute', 'cgBusy', 'ngMaterial', 'toaster', 'ngAnimate']);

    app.config(function ($routeProvider) {
        $routeProvider
        .when("/", {
            templateUrl: 'film/films',
            controller: 'movieController'
        })
        .when('/films/:filmId', {
            templateUrl: 'film/details',
            controller: 'movieDetailsController'
        })
        .when('/people', {
            templateUrl: 'people/people',
            controller: 'peopleController'
        })
        .when('/people/:personId', {
            templateUrl: 'people/details',
            controller: 'peopleDetailsController'
        })
        .when('/screenings', {
            templateUrl: 'screening/screenings',
            controller: 'screeningsController'
        })
        .when('/screenings/management', {
            templateUrl: 'screening/screeningsmanagement',
            controller: 'screeningsManagementController'
        })
    });
})();