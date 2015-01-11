
'use strict';

angular.module('ospreyLauncherApp')
  .controller('HeaderCtrl', ['$scope','$location','$rootScope',function($scope, $location,$rootScope) {
    $scope.time = Date.now();
}])
// Register the 'myCurrentTime' directive factory method.
// We inject $interval and dateFilter service since the factory method is DI.
.directive('currentTime', ['$interval', 'dateFilter',
  function($interval, dateFilter)
  {
    // return the directive link function.
    return function(scope, element, attrs)
    {  
      // used to update the UI
      function updateTime() {
        element.text(dateFilter(new Date(), 'h:mm a on EEEE, d MMMM yyyy'));
      }

      // watch the expression, and update the UI on change. $watch simply registers a callback.
      scope.$watch(attrs.currentTime, function() {
        updateTime();
      });

      var stopTime = $interval(updateTime, 1000); // store the promise so it can be canceled.

      // listen on DOM destroy (removal) event, and cancel the next UI update
      // to prevent updating time after the DOM element was removed.
      element.on('$destroy', function() {
        $interval.cancel(stopTime);
      });
    };
  }
])
.directive('todaysRecordings', ['$interval',
  function($interval)
  {
    // return the directive link function.
    return function(scope, element, attrs)
    {  
      // used to update the UI
      function updateRecordings() {
        $.getJSON( 'http://192.168.1.140:4322/MPExtended/TVAccessService/json/GetScheduledRecordingsForToday', function( data ) {
          if(Array.isArray(data))
          {
            if(data.length > 0)
            {
              if(data.length === 1)
              {
                element.text(data.length + ' recording scheduled today.');
              }
              else
              {
                element.text(data.length + ' recordings scheduled today.');
              }
            }
            else
            {
              element.text('No recordings today.');
            }
          }
        });
      }

      // watch the expression, and update the UI on change. $watch simply registers a callback.
      scope.$watch(attrs.todaysRecordings, function() {
        updateRecordings();
      });

      var automaticallyUpdateRecordings = $interval(updateRecordings, 60*60*1000); // store the promise so it can be canceled.

      // listen on DOM destroy (removal) event, and cancel the next UI update
      // to prevent updating time after the DOM element was removed.
      element.on('$destroy', function() {
        $interval.cancel(automaticallyUpdateRecordings);
      });
    };
  }
])
.directive('diskSpace', ['$interval',
  function($interval)
  {
    // return the directive link function.
    return function(scope, element, attrs)
    {  
      // used to update the UI
      function updateDiskSpace() {
        $.getJSON( 'http://192.168.1.140:80/DiskSpace', function( data ) {
          
          element.text((100-data.percentageUsed)+ '% of disk free.');
        });
      }

      // watch the expression, and update the UI on change. $watch simply registers a callback.
      scope.$watch(attrs.diskSpace, function() {
        updateDiskSpace();
      });

      var automaticallyUpdateDiskSpace = $interval(updateDiskSpace, 60*60*1000); // store the promise so it can be canceled.

      // listen on DOM destroy (removal) event, and cancel the next UI update
      // to prevent updating time after the DOM element was removed.
      element.on('$destroy', function() {
        $interval.cancel(automaticallyUpdateDiskSpace);
      });
   
    };
  }
]);