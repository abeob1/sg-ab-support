var App = angular.module('myApp', ['ngCookies','ui.router', 'oc.lazyLoad']);

App.config(['$ocLazyLoadProvider', '$stateProvider', '$urlRouterProvider' , function($ocLazyLoadProvider, $stateProvider, $urlRouterProvider) {
	$urlRouterProvider.otherwise("/state1");
	
	
	//Config/states of UI Router
	$stateProvider
	.state('state1', {
		url: "/state1",
		views : {
			"" : {
				templateUrl:"js/tpl/dashbord.html"
			}
		}
	})
	.state('statusMaster', {
		url: "/statusMaster",
		views : {
			"" : {
				templateUrl:"js/tpl/statusMaster.html"
			}
		}
	})
	.state('users', {
		url: "/users",
		views : {
			"" : {
				templateUrl:"js/tpl/users.html"
			}
		}
	});
}]);

App.filter('cut', function () {
        return function (value, wordwise, max, tail) {
            if (!value) return '';

            max = parseInt(max, 10);
            if (!max) return value;
            if (value.length <= max) return value;

            value = value.substr(0, max);
            if (wordwise) {
                var lastspace = value.lastIndexOf(' ');
                if (lastspace != -1) {
                  //Also remove . and , so its gives a cleaner result.
                  if (value.charAt(lastspace-1) == '.' || value.charAt(lastspace-1) == ',') {
                    lastspace = lastspace - 1;
                  }
                  value = value.substr(0, lastspace);
                }
            }

            return value + (tail || ' …');
        };
    });

App.filter("trust", ['$sce', function($sce) {
  return function(htmlCode){
    return $sce.trustAsHtml(htmlCode);
  }
}]);
App.filter('decodeURIComponent', function($window) {
    return $window.decodeURIComponent;
});

App.filter('htmlToPlaintext', function() {
    return function(text) {
      return  text ? String(text).replace(/<[^>]+>/gm, '') : '';
    };
  }
);

App.directive('loadingss', ['$http', function ($http) {
      return {
          restrict: 'A',
          link: function (scope, element, attrs) {
              scope.isLoading = function () {
                  return $http.pendingRequests.length > 0;
              };
              scope.$watch(scope.isLoading, function (value) {
                  if (value) {
                      $("#loadingss").animate({top: '0px'},800);
                  } else {
                      $("#loadingss").animate({ top: '-100px' },800);
                  }
              });
          }
      };
  } ]);

  App.directive('stringToNumber', function () {
      return {
          require: 'ngModel',
          link: function (scope, element, attrs, ngModel) {
              ngModel.$parsers.push(function (value) {
                  return '' + value;
              });
              ngModel.$formatters.push(function (value) {
                  return parseFloat(value, 10);
              });
          }
      }
  })


  App.directive('ngEnter', function () {
      return function (scope, element, attrs) {
          element.bind("keydown keypress", function (event) {
              if (event.which === 13) {
                  scope.$apply(function () {
                      scope.$eval(attrs.ngEnter, { 'event': event });
                  });

                  event.preventDefault();
              }
          });
      };
  });
