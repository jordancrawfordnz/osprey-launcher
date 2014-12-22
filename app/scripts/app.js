'use strict';

/**
 * @ngdoc overview
 * @name ospreyLauncherApp
 * @description
 * # ospreyLauncherApp
 *
 * Main module of the application.
 */
angular
  .module('ospreyLauncherApp', [
    'ngAnimate',
    'ngCookies',
    'ngResource',
    'ngRoute',
    'ngSanitize'
  ])
  .config(function ($routeProvider) {
    $routeProvider
      .when('/', {
        templateUrl: 'views/main.html',
        controller: 'MainCtrl'
      })
      .when('/info', {
        templateUrl: 'views/info.html',
        controller: 'InfoCtrl'
      })
      .otherwise({
        redirectTo: '/'
      });
  });
