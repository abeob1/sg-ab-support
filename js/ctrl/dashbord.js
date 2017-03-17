App.controller('dashbord_ctrl', ['$scope', '$rootScope', '$http', '$window', '$cookies','util_SERVICE',

function ($scope, $rootScope, $http, $window, $cookies, US) {

  	US.islogin();
	
   
	$scope.userdata = JSON.parse($cookies.get('UserData'));
 //$scope.calltypeSelected = "";
 
 
 console.log($scope.userdata);
 
 $scope.GetAllTickect = function () {

        $http.post(US.url+'GetAllTickets', "", US.config)
   .then(
       function (response) {
           // success callback
           $scope.MAILLIST = response.data.TICKETS;
           
       },
       function (response) {
           // failure callback

       }
    );

    }
	
	
	
	
	$scope.GetAllMail = function () {

        var data = {"apikey" : US.APIKEY,'method':"GetAllMail"}

        var config = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
            }
        }

        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
          $scope.MAILLIST = response.data.Data;
           
       },
       function (response) {
           // failure callback

       }
    );

    }
	
	$scope.GetMailById = function(id)
	{
			

        var data = {"apikey" : US.APIKEY,'method':"GetMailById","MailId":id}

        var config = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
            }
        }

        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
          //$scope.MAILLIST = response.data.Data;
           
       },
       function (response) {
           // failure callback

       }
    );

    
	
	
	}
	

$scope.GetAllTickect();
} ]);
