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
      .when('/about', {
        templateUrl: 'views/about.html',
        controller: 'AboutCtrl'
      })
      .otherwise({
        redirectTo: '/'
      });
  });
