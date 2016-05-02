
(function (module) {
    'use strict';

    module.controller('updateFlightModalController', [
        '$scope', '$uibModalInstance', 'flight', 'updateFlight', '$filter', function ($scope, $uibModalInstance, flight, updateFlight, $filter) {
            $scope.ok = function () {
                updateFlight(flight);
                $uibModalInstance.close();
            };
            $scope.cancel = function () {
                $uibModalInstance.dismiss();
            };

            $scope.flight = flight;
    }]);
})(angular.module('appFlightInfo'));
