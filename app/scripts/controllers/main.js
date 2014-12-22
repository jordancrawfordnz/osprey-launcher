'use strict';

/**
 * @ngdoc function
 * @name ospreyLauncherApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the ospreyLauncherApp
 */
angular.module('ospreyLauncherApp')
  .controller('MainCtrl', function ($scope) {
    $scope.applications = [
      {'name': 'Movies'},
      {'name': 'TV'},
      {'name': 'Stream'},
      {'name': 'test'},
    ];
  });
