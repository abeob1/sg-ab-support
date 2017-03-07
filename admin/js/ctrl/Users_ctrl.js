App.controller('Users_ctrl', ['$scope', '$rootScope', '$http', '$window', '$cookies','util_SERVICE',

function ($scope, $rootScope, $http, $window, $cookies, US) {

  	US.islogin();
	
   
	$scope.userdata = JSON.parse($cookies.get('UserData'));
 //$scope.calltypeSelected = "";
 //alert($scope.userdata[0].id);
 
 console.log($scope.userdata);
 US.GetAllCompany().then(function (response){$scope.AllCompany=response.data.Data;});
 US.GetAllDepartment().then(function (response){$scope.AllDetartment=response.data.Data;});
 US.GetAccountType().then(function (response){$scope.AllType=response.data.Data;});
 
 $scope.AdminGetAllUsers = function () {

        var data = {"apikey" : US.APIKEY,'method':"AdminGetAllUsers","UserID":$scope.userdata[0].id}
        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
            if (response.data.status !=false) {
				console.log(response.data);
          $scope.AllUsers = response.data.Data;
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
	$scope.changecompany = function(id)
	{
			$rootScope.currentUID = id;
			$('#companyModal').modal('show');
	}
	$rootScope.Cchange = function()
	{
        var data = {"apikey" : US.APIKEY,'method':"changeuserCompany","UserID":$scope.userdata[0].id,"UID":$rootScope.currentUID,"CID":$scope.rootCompany}
        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
            if (response.data.status !=false) {
				$('#companyModal').modal('hide');
              US.info("Alert",response.data.Data,"success");
			  $scope.AdminGetAllUsers();
           }
            else
               US.info("Alert",response.data.MSG,"danger");
			   
			   
           
       },
       function (response) {
           // failure callback

       }
    );
}
    
	//change department of user
	$scope.changedepartment = function(id)
	{
			$rootScope.currentUID = id;
			$('#departmentModal').modal('show');
	}
	$rootScope.Dchange = function()
	{
        var data = {"apikey" : US.APIKEY,'method':"changeuserDepartment","UserID":$scope.userdata[0].id,"UID":$rootScope.currentUID,"CID":$scope.rootDepartment}
        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
            if (response.data.status !=false) {
				$('#departmentModal').modal('hide');
              US.info("Alert",response.data.Data,"success");
			  $scope.AdminGetAllUsers();
           }
            else
               US.info("Alert",response.data.MSG,"danger");
			   
			   
           
       },
       function (response) {
           // failure callback

       }
    );
   }
   
   //change user account type 
   $scope.changeType = function(id)
	{
			$rootScope.currentUID = id;
			$('#typeModal').modal('show');
	}
	$rootScope.Tchange = function()
	{
        var data = {"apikey" : US.APIKEY,'method':"changeuserType","UserID":$scope.userdata[0].id,"UID":$rootScope.currentUID,"CID":$scope.rootType}
        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
            if (response.data.status !=false) {
				$('#typeModal').modal('hide');
              US.info("Alert",response.data.Data,"success");
			  $scope.AdminGetAllUsers();
           }
            else
               US.info("Alert",response.data.MSG,"danger");
			   
			   
           
       },
       function (response) {
           // failure callback

       }
    );
}
		
	
	$scope.changeuserStatus = function (status,id) {

        var data = {"apikey" : US.APIKEY,'method':"changeuserStatus","UserID":$scope.userdata[0].id,"UID":id,"statusID":status}
        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
            if (response.data.status !=false) {
              US.info("Alert",response.data.Data,"success");
			  $scope.AdminGetAllUsers();
           }
            else
               US.info("Alert",response.data.MSG,"danger");
			   
			   
           
       },
       function (response) {
           // failure callback

       }
    );

    }
	
	
	

$scope.AdminGetAllUsers();
} ]);
