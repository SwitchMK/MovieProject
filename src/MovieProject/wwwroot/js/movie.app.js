(function () {
    'use strict';

    var app = angular.module('movieApp', ['ngRoute', 'cgBusy', 'ngMaterial', 'toaster', 'ngAnimate']);

    app.value('cgBusyDefaults', {
        message: 'Loading',
        templateUrl: '/js/templates/loadingTemplate.html',
        minDuration: 500
    });

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