(function () {
    'use strict';

    angular
        .module('movieApp')
        .controller('screeningsManagementController', screeningsManagementController);

    screeningsManagementController.$inject = ['$scope', 'screeningsService', 'toaster'];

    function screeningsManagementController($scope, screeningsService, toaster) {
        $scope.getTheatres = function () {
            $scope.getTheatresPromise = screeningsService
                .getTheatres()
                .then(function (response) {
                    $scope.theatres = response.data;
                }, function (response) {
                    console.error("Something went wrong while getting theatres!");
                });
        };

        $scope.getFilms = function () {
            $scope.getFilmsPromise = screeningsService
                .getFilms()
                .then(function (response) {
                    $scope.films = response.data;
                }, function (response) {
                    console.error("Something went wrong while getting films!");
                });
        };

        $scope.submitTheatreInformation = function () {
            var request = getUpdateFilmTheatreRequest();

            $scope.submitTheatreInformationPromise = screeningsService
                .submitTheatreInformation(request)
                .then(function (response) {
                    if (response.data.status == "Success")
                        toaster.pop('success', "Success", response.data.message);
                    else
                        toaster.pop('error', "Error", response.data.message);
                }, function (response) {
                    console.error("Something went wrong while getting response!");
                });
        };

        $scope.exportToXmlFile = function () {
            var request = {
                Path: $scope.file,
                UpdateFilmTheatreRequest: getUpdateFilmTheatreRequest()
            };

            $scope.exportToXmlFilePromise = screeningsService
                .exportToXmlFile(request)
                .then(function (response) {
                    if (response.data.status == "Success")
                        toaster.pop('success', "Success", response.data.message);
                    else
                        toaster.pop('error', "Error", response.data.message);
                }, function (response) {
                });
        };

        $scope.chooseFile = function () {
            $scope.file = $('#exportToXmlFileInput')[0].value;
        };

        $scope.importFromXmlFile = function () {
            var request = {
                Path: $scope.file
            };

            $scope.exportToXmlFilePromise = screeningsService
                .importFromXmlFile(request)
                .then(function (response) {
                    if (response.data.status == "Success")
                        toaster.pop('success', "Success", response.data.message);
                    else if (response.data.status == "Error")
                        toaster.pop('error', "Error", response.data.message);
                    else if (response.data.status == "Info")
                        toaster.pop('note', "Information", response.data.message);
                }, function (response) {
                });
        };

        $scope.initFunctions = function () {
            $scope.getFilms();
            $scope.getTheatres();
        };

        var getUpdateFilmTheatreRequest = function () {
            return {
                TheatreId: $scope.theatre,
                FilmId: $scope.film,
                StartDistributionDate: $scope.minDate,
                EndDistributionDate: $scope.maxDate,
                BoxOffice: $scope.boxOffice,
                AmountOfPeople: $scope.amountOfPeople
            };
        };

        var mainTemplate = '{preview}<div class="input-group {class}"><div class="input-group-btn">{browse}{upload}{remove}</div>{caption}</div>';

        $("#exportToXmlFileInput").fileinput({
            previewFileType: "text",
            allowedFileExtensions: ["xml"],
            previewClass: "bg-warning",
            showRemove: false,
            showPreview: false,
            layoutTemplates: {
                main1: mainTemplate
            }
        });

        $("#exportToXmlFileInput").on("filebrowse", function () {
            $("#exportToXmlFileInput").fileinput('clear');
        });
    }
})();
