
(function (module) {
    'use strict';

    module.controller('assignToGateModalController', [
        '$scope', '$uibModalInstance', 'assignFlightToGate', 'flight', 'gateId', '$filter', function ($scope, $uibModalInstance, assignFlightToGate, flight, gateId, $filter) {

            $scope.ok = function () {
                assignFlightToGate($scope.gateId, $scope.flight);
                $uibModalInstance.close();
            };
            $scope.cancel = function () {
                $uibModalInstance.dismiss();
            };

            $scope.$watch('flight.arrivalDateTime', function (newValue) {
                $scope.flight.arrivalDateTime = $filter('date')(newValue, 'dd/MM/yyyy HH:mm');
            });

            $scope.$watch('flight.departureDateTime', function (newValue) {
                $scope.flight.departureDateTime = $filter('date')(newValue, 'dd/MM/yyyy HH:mm');
            });

            $scope.flight = flight;
            $scope.gateId = gateId;
    }]);
})(angular.module('appFlightInfo'));
