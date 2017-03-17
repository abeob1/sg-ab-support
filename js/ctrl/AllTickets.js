App.controller('AllTickets_ctrl', ['$scope', '$rootScope', '$http', '$window', '$cookies','util_SERVICE',

function ($scope, $rootScope, $http, $window, $cookies, US) {

  	US.islogin();
	
   
	$scope.userdata = JSON.parse($cookies.get('UserData'));
 //$scope.calltypeSelected = "";
 
 
 console.log($scope.userdata);
 
 
 $scope.GetAllCTickect = function () {

      
        var data ={
 "TICKETS": [{
  "UserID": $scope.userdata[0].id,
  "StatusId":""
 }]
}

        var parms = JSON.stringify(data);
        $http.post(US.url+'GetCustomerTickets', "sJasonInput=" + parms, US.config)
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
	
	$scope.assignConsultant = function(Tid)
	{
		
		$rootScope.CurrentTD = Tid;
		US.GetagentList().then(function (response){$scope.agentList=response.data.CONSULTANTS;});
		$("#assignModal").modal('show');
	}
	
	$scope.AssignUser = function(id)
	{
		US.AssignTicket($rootScope.CurrentTD,id).then(function (response){  if (response.data.VALIDATE[0].Status !=false) {
			  $("#assignModal").modal('hide');
             //alert(response.data.VALIDATE[0].Msg);
			 $scope.GetAllTickect();
			  
           }
            else
			{
				$("#assignModal").modal('hide');
               //alert(response.data.VALIDATE[0].Msg);
			}
			   
			   });
	}
	
	
	
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
	
	if($scope.userdata[0].account_type=="Customer")
	{
		$scope.GetAllCTickect();
	}
	else
	{
		$scope.GetAllTickect();
	}

} ]);
