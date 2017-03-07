App.controller('Status_ctrl', ['$scope', '$rootScope', '$http', '$window', '$cookies','util_SERVICE',

function ($scope, $rootScope, $http, $window, $cookies, US) {

  	US.islogin();
	
   
	$scope.userdata = JSON.parse($cookies.get('UserData'));
 //$scope.calltypeSelected = "";
 //alert($scope.userdata[0].id);
 
 
 
 $scope.AdminGetAllStatus = function () {

        var data = {"apikey" : US.APIKEY,'method':"AdminGetAllStatus","UserID":$scope.userdata[0].id}
        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
            if (response.data.status !=false) {
				console.log(response.data);
          $scope.AllStatus = response.data.Data;
              //US.info("Alert",response.data.Data,"success");
           }
            else
               US.info("Alert",response.data.MSG,"danger");
			   
			   
           
       },
       function (response) {
           // failure callback

       }
    );

    }
	$scope.AddNew = function()
	{
		 var data = {"apikey" : US.APIKEY,'method':"AddNewStatus","UserID":$scope.userdata[0].id,"StatusName":$scope.rootName,"IconClass":$scope.rootIC,"Flag":$scope.rootFlag}
        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
            if (response.data.status !=false) {
				$('#Addnew').modal('hide');
              US.info("Alert",response.data.Data,"success");
			  $scope.AdminGetAllStatus();
           }
            else
               US.info("Alert",response.data.MSG,"danger");
			   
			   
           
       },
       function (response) {
           // failure callback

       }
    );
	}
	
	
	$scope.changeStatus = function (status,id) {

        var data = {"apikey" : US.APIKEY,'method':"changestatusFlag","UserID":$scope.userdata[0].id,"UID":id,"statusID":status}
        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
            if (response.data.status !=false) {
              US.info("Alert",response.data.Data,"success");
			  $scope.AdminGetAllStatus();
           }
            else
               US.info("Alert",response.data.MSG,"danger");
			   
			   
           
       },
       function (response) {
           // failure callback

       }
    );

    }
	
	
	

$scope.AdminGetAllStatus();
} ]);
