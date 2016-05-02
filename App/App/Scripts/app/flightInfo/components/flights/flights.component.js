(function (module) {
    'use strict';
    module.directive('flights', function () {
        return {
            templateUrl: '/Scripts/app/flightInfo/components/flights/flights.template.html',
            restrict: 'E',
            scope: {
                gate: '=',
                cancelFlight: '=',
                updateFlight: '=',
                addFlight: '=',
                assignFlightToGate: '='
            },
            bindToController: true,
            controller: 'flightsController',
            controllerAs: 'ctrl'
        };
    });

    function flightsController($scope, $uibModal) {
        var ctrl = this;

        ctrl.moveToGateId = 0;

        if (ctrl.gate) {
            ctrl.gateIds = ctrl.gate.id;
        }

        ctrl.openUpdateModal = function(flight) {
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: '/Scripts/app/flightInfo/components/flights/modal/updateFlight.modal.html',
                controller: 'updateFlightModalController',
                resolve: {
                    flight: function () {
                        return flight;
                    },
                    updateFlight: function () {
                        return ctrl.updateFlight;
                    }
                }
            });
        };

        ctrl.openAddModal = function (flight) {
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: '/Scripts/app/flightInfo/components/flights/modal/addFlight.modal.html',
                controller: 'addFlightModalController',
                resolve: {
                    addFlight: function () {
                        return ctrl.addFlight;
                    }
                }
            });
        };

        ctrl.openAssignToGateModal = function (flight) {
            ctrl.moveToGateId = ctrl.gate.id;

            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: '/Scripts/app/flightInfo/components/flights/modal/assignToGate.modal.html',
                controller: 'assignToGateModalController',
                resolve: {
                    assignFlightToGate: function () {
                        return ctrl.assignFlightToGate;
                    },
                    gateId: function() {
                        return ctrl.moveToGateId;
                    },
                    flight: function () {
                        return flight;
                    }
                }
            });
        };
    }

    module.controller('flightsController', flightsController);
    flightsController.$inject = ['$scope', '$uibModal'];

})(angular.module('appFlightInfo'));
