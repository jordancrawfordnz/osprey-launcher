'use strict';
/**
 * @ngdoc function
 * @name ospreyLauncherApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the ospreyLauncherApp
 */
angular.module('ospreyLauncherApp')
  .controller('MainCtrl', ['$scope','$location','$rootScope',function($scope, $location,$rootScope) {

  	// == Setup buttons ==
    
    $rootScope.moveLeft = function()
  	{
      changePosition(currentRow, currentCol - 1);
  	};
  	
    $rootScope.moveRight = function()
  	{
      changePosition(currentRow, currentCol + 1);
  	};
  	
    $rootScope.moveUp = function()
  	{
      changePosition(currentRow - 1, currentCol);
  	};
  	
    $rootScope.moveDown = function()
  	{
  		changePosition(currentRow + 1, currentCol);
  	};

  	$rootScope.selectKey = function()
  	{
      $rootScope.currentSelectable.onSelect();
  	};

    $rootScope.context = function()
    {
      console.log('context menu');
    }

    // setup grid

    $scope.contextMenu = false;

    function changePosition(row, col)
    {
      if(grid[row] !== undefined)
      {
        var newPosition = grid[row][col];
        if(newPosition !== undefined)
        {
          currentRow = row;
          currentCol = col;
          $rootScope.makeCurrent(newPosition);
          $scope.$apply(); 
        }
      }
    };

    $scope.findSelectablePosition = function(findSelectable)
    {
      for(var rowKey = 0; rowKey < grid.length; rowKey++)
      {
        var currentRow = grid[rowKey];
        for(var colKey = 0; colKey < currentRow.length; colKey++)
        {
          var currentCol = currentRow[colKey];
          if(currentCol == $rootScope.currentSelectable)
          {
            return {'row' : rowKey, 'col' : colKey}; 
          }
        }
      }
    };

    $scope.selectables = $rootScope.applications;

    var perRow = 3;
    var grid = [];
    var currentRow = [];
    angular.forEach($scope.selectables, function(value, key)
    {
      if((key % perRow) == 0 && key != 0)
      {
        grid.push(currentRow);
        currentRow = [];
      }
      currentRow.push(value);
    });
    grid.push(currentRow);

    var currentPosition = $scope.findSelectablePosition($rootScope.currentSelectable);
    var currentRow = currentPosition.row;
    var currentCol = currentPosition.col;

  }]);