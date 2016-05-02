(function (module) {
    'use strict';
    module.directive('gates', function () {
        return {
            templateUrl: '/Scripts/app/flightInfo/components/gates/gates.template.html',
            restrict: 'E',
            scope: {
                gateIds: '=',
                selectGate: '=',
                selectedId: '='
            },
            bindToController: true,
            controller: 'gatesController',
            controllerAs: 'ctrl'
        };
    });

    function gatesController() {
        var ctrl = this;

        ctrl.clickGate = function (id) {
            ctrl.selectedId = id;
            ctrl.selectGate(id);
        };

        ctrl.getStyle = function (gid) {
            console.log(gid);
            console.log(ctrl.selectedId);
            if (gid === ctrl.selectedId) {
                return 'selected';
            }
            return '';
        };
    }

    module.controller('gatesController', gatesController);
    gatesController.$inject = [];

})(angular.module('appFlightInfo'));
