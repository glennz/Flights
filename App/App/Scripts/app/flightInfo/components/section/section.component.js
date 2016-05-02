(function (module) {
    'use strict';
    module.directive('section', function () {
        return {
            templateUrl: '/Scripts/app/flightInfo/components/section/section.template.html',
            restrict: 'E',
            bindToController: true,
            controller: 'sectionController',
            controllerAs: 'ctrl'
        };
    });



    function sectionController(flightApiService, $uibModal) {
        var ctrl = this;

        ctrl.selectedGateId = 0;

        //init function
        function Init() {
            //get gate ids
            flightApiService.getGates().then(function (response) {
                ctrl.gateIds = response.data;
            })
            .catch(function (response) {
                console.log(response);
            });
        }

        function SetMessage(response) {
            if (response.data.message) {
                ctrl.message = response.data.message;
            }
        }

        function ClearMessage() {
            ctrl.message = '';
        }

        //when gate is selected
        ctrl.selectGate = function (id) {
            flightApiService.getGate(id).then(function (response) {
                ctrl.selectedGateId = id;
                ctrl.gate = response.data;
                ClearMessage();
            })
            .catch(function (response) {
                SetMessage(response);
            });
        };

        //when cancel button click
        ctrl.cancelFlight = function(flight) {
            flightApiService.cancelFlight(flight).then(function (response) {
                if (ctrl.selectedGateId > 0) {
                    ctrl.selectGate(ctrl.selectedGateId);
                }
                ClearMessage();
            })
            .catch(function (response) {
                SetMessage(response);
            });
        };

        ctrl.updateFlight = function(flight) {
            flightApiService.updateFlight(flight).then(function (response) {
                if (ctrl.selectedGateId > 0) {
                    ctrl.selectGate(ctrl.selectedGateId);
                }

                ClearMessage();
             })
            .catch(function (response) {
                SetMessage(response);
            });
        };
        
        //create flight
        ctrl.addFlight = function(flight) {
            if (ctrl.selectedGateId > 0) {
                ctrl.selectGate(ctrl.selectedGateId);

                flightApiService.addFlight(ctrl.selectedGateId, flight).then(function(response) {
                        ctrl.selectGate(ctrl.selectedGateId);
                        ClearMessage();
                    })
                    .catch(function(response) {
                        SetMessage(response);
                    });
            }
        };

        ctrl.assignFlightToGate = function(gateId, flight) {
            flightApiService.assignFlightToGate(gateId, flight).then(function (response) {
                ctrl.selectGate(ctrl.selectedGateId);
                ClearMessage();
            })
            .catch(function (response) {
                SetMessage(response);
            });
        };
        ctrl.selectedGateId = 2;
        Init();
    }

    module.controller('sectionController', sectionController);
    sectionController.$inject = ['flightApiService', '$uibModal'];

})(angular.module('appFlightInfo'));
