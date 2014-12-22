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
      {'title': 'Movies','name' : 'plex', 'img' : 'images/plex.png'},
      {'title': 'TV', 'name' : 'mediaportal', 'img' : 'images/mediaportal.png'},
      {'title': 'Stream', 'name' : 'kodi', 'img' : 'images/kodi.png'}
    ];
  });
