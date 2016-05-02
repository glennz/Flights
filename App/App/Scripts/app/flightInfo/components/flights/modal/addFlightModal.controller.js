
(function (module) {
    'use strict';

    module.controller('addFlightModalController', [
        '$scope', '$uibModalInstance', 'addFlight', '$filter', function ($scope, $uibModalInstance, addFlight, $filter) {
            $scope.flight = {
                flightNo: null,
                arrivalDateTime: null,
                departureDateTime: null,
                active: true
            };

            $scope.ok = function () {
                addFlight($scope.flight);
                $uibModalInstance.close();
            };
            $scope.cancel = function () {
                $uibModalInstance.dismiss();
            };
    }]);
})(angular.module('appFlightInfo'));
