'use strict';

angular.module('appFlightInfo').service('flightApiService', function ($http) {

    this.getGates = function () {
        return $http.get('/api/gate/list?rnd=' +new Date().getTime());
    };

    this.getGate = function (id) {
        return $http.get('/api/gate/' + id+ '?rnd=' +new Date().getTime());
    };

    this.cancelFlight = function (flight) {
        var url = '/api/gate/flight/cancel?rnd=' + new Date().getTime();
        return $http.put(url, flight);
    };

    this.updateFlight = function(flight) {
        var url = '/api/gate/flight/update?rnd=' +new Date().getTime();
        return $http.put(url, flight);
    };

    this.addFlight = function (gateId, flight) {
        var url = '/api/gate/flight/add/' + gateId + '?rnd=' +new Date().getTime();
        return $http.post(url, flight);
    };

    this.assignFlightToGate = function(gateId, flight) {
        var url = '/api/gate/flight/change-gate/' + gateId + '?rnd=' + new Date().getTime();
        return $http.put(url, flight);
    };
});