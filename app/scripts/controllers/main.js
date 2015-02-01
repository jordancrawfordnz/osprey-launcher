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
      if($scope.options)
        changeCurrentOption(-1);
      else
        changePosition(currentRow - 1, currentCol);
  	};
  	
    $rootScope.moveDown = function()
  	{
  		if($scope.options)
        changeCurrentOption(1);
      else
        changePosition(currentRow + 1, currentCol);
  	};

  	$rootScope.selectKey = function()
  	{
      if($scope.options)
        $scope.options[currentSelectedOption].action();
      else
        $rootScope.currentSelectable.onSelect();
  	};

    $rootScope.context = function()
    {
      showContextMenu();
    }

    // setup grid

    $scope.contextMenu = false;
    $scope.options = null;

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

    function changeCurrentOption(changeBy)
    {
      var newIndex = currentSelectedOption + changeBy;
      if(newIndex < 0)
      {
        newIndex = ($scope.options.length - 1);
      }
      if(newIndex >= $scope.options.length)
      {
        newIndex = 0;
      }
      setCurrentOption(newIndex);
    }

    var currentSelectedOption = null;
    function setCurrentOption(index)
    {
      if(currentSelectedOption !== null)
      {
        $scope.options[currentSelectedOption].selected = false;
      }
      currentSelectedOption = index;
      $scope.options[currentSelectedOption].selected = true;
      $scope.$apply();
    }

    function showContextMenu()
    {
      $scope.options = [];

      $scope.options.push({text : "Exit Launcher", action : $rootScope.exit});
      $scope.options.push({text : "Force Close [App] (Placeholder)", action : $rootScope.reset});
      $scope.options.push({text : "Keep [App] Open (Placeholder)", action : $rootScope.reset});
      $scope.options.push({text : "Cancel", action : hideContextMenu});

      setCurrentOption(0);
    }

    function hideContextMenu()
    {
      $scope.options = null;
      $scope.$apply();
    }


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