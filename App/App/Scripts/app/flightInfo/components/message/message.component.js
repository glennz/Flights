(function (module) {
    'use strict';
    module.directive('message', function () {
        return {
            templateUrl: '/Scripts/app/flightInfo/components/message/message.template.html',
            restrict: 'E',
            scope: {
                msg:'='
            },
            bindToController: true,
            controller: 'messageController',
            controllerAs: 'ctrl'
        };
    });

    function messageController() {
        var ctrl = this;
    }

    module.controller('messageController', messageController);
    messageController.$inject = [];

})(angular.module('appFlightInfo'));
