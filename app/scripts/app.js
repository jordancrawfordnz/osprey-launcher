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
      .when('/loading', {
        templateUrl: 'views/loading.html',
        controller: 'LoadingCtrl'
      })
      .otherwise({
        redirectTo: '/'
      });
  });


// JavaScript related to selection of content.

var notifyOfMovement = null;
function registerMovementNotifyee(toNotify)
{
  notifyOfMovement = toNotify;
}

function moveLeft()
{
  if(notifyOfMovement !== null)
  {
    notifyOfMovement.moveLeft();
  }
}
function moveRight()
{
  if(notifyOfMovement !== null)
  {
    notifyOfMovement.moveRight();
  }
}
function moveUp()
{
  if(notifyOfMovement !== null)
  {
    notifyOfMovement.moveUp();
  }
}
function moveDown()
{
  if(notifyOfMovement !== null)
  {
    notifyOfMovement.moveDown();
  }
}
function selectKey()
{
  if(notifyOfMovement !== null)
  {
    notifyOfMovement.selectKey();
  }
}
