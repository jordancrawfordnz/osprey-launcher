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
      if(!$scope.options)
      {
        changePosition(currentRow, currentCol - 1);
      }
  	};
  	
    $rootScope.moveRight = function()
  	{
      if(!$scope.options)
      {
       changePosition(currentRow, currentCol + 1);
      }
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
      if(!$scope.options)
        showContextMenu($scope.currentSelectable);
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

          var item = $("#" + newPosition.name);
          var topBarPush = parseInt($('#launchableContainer').css('margin-top'), 10);
          if(!item.visible())
          {
            $('html, body').stop().animate({
              scrollTop: item.offset().top - topBarPush
            }, 300);
          }

          $rootScope.makeCurrent(newPosition);
          $scope.$apply(); 
        }
      }
    };

    $scope.findSelectablePosition = function(findSelectable)
    {
      for(var rowKey = 0; rowKey < grid.length; rowKey++)
      {
        var row = grid[rowKey];
        for(var colKey = 0; colKey < row.length; colKey++)
        {
          var col = row[colKey];
          if(col == $rootScope.currentSelectable)
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

    function showContextMenu(selectable)
    {
      $scope.options = [];

      $scope.options.push({text : "Exit Launcher", action : $rootScope.exit});
      $scope.options.push({text : "Force Close " + selectable.name, action : $rootScope.forceCloseCurrent});
      $scope.options.push({text : "Cancel", action : hideContextMenu});

      setCurrentOption(0);
    }

    function hideContextMenu()
    {
      $scope.options = null;
      $scope.$apply();
    }


    $scope.selectables = $rootScope.applications;
    var grid;
    var currentRow;
    var currentCol;
    var perRow;

    $scope.resetGrid = function()
    {
      $('#launchableContainer').css('margin-top', ($('#launcher-headerbar').height() + 30));
      var perRowOld = perRow;
      var winWidth =  $(window).width();
       if(winWidth < 768 ){
          perRow = 1;
       }else if( winWidth >= 768 && winWidth <= 991){
          perRow = 2;
       }else if( winWidth >= 992 && winWidth <= 1199){
          perRow = 2;
       }else if( winWidth >= 1200 ){
          perRow = 3;
       }

      if(perRow != perRowOld)
      {
        grid = [];
        var row = [];
        angular.forEach($scope.selectables, function(value, key)
        {
          if((key % perRow) == 0 && key != 0)
          {
            grid.push(row);
            row = [];
          }
          row.push(value);
        });
        grid.push(row);

        var currentPosition = $scope.findSelectablePosition($rootScope.currentSelectable);
        currentRow = currentPosition.row;
        currentCol = currentPosition.col;
      }
    };

    $scope.resetGrid();
    $(window).resize($scope.resetGrid); // re-caluate the grid when the browser re-sizes

  }]);