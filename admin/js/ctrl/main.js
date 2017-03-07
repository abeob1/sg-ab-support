App.controller('main_ctrl', ['$scope', '$rootScope', '$http', '$window', '$cookies','util_SERVICE',

function ($scope, $rootScope, $http, $window, $cookies, US) {

  	US.islogin();
	
   
	$scope.userdata = JSON.parse($cookies.get('UserData'));
 //$scope.calltypeSelected = "";
 var expandstatus = 1;
 $scope.expandtoggle = function()
 {
	 if(expandstatus==1)
	 {
	$( "#sidebar-collapse" ).css( "left", "-230px" ); 
	$( "#content" ).css( "width", "100%" ); 
	$( "#content" ).css( "margin-left", "0%" );
	expandstatus = 2;
	 }
	 else
	 {
	$( "#sidebar-collapse" ).css( "left", "-0px" ); 
	$( "#content" ).css( "width", "83.33333333%" ); 
	$( "#content" ).css( "margin-left", "16.66666667%" );
	expandstatus = 1;
	 }
 }
 
 console.log($scope.userdata);
 
 $scope.GetAllTickect = function () {

        var data = {"apikey" : US.APIKEY,'method':"GetAllTickect","UserID":$scope.userdata[0].id}

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
$scope.expandtoggle();
} ]);
