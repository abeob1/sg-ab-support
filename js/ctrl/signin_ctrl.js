App.controller('signin', ['$scope', '$rootScope', '$http', '$window', '$cookies','util_SERVICE',

function ($scope, $rootScope, $http, $window, $cookies, US) {
    // $scope.items = Data;
    $scope.userId = "";
    $scope.password = "";
    $cookies.put('MenuInfo', "");
    $cookies.put('UserData', "");
    $cookies.put('Islogin', "false");
	
	var url = US.url;
	$scope.company = "ADV_LATEST";
	
	$scope.loadcompany = function () {
		
		

        var data = {"sUserName" : $scope.userId, "sPassword" : $scope.password, "sCompany" : $scope.company}

        var config = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
            }
        }

        var parms = JSON.stringify(data);
        $http.post(url+"GetCompanyList", "sJsonInput=" + "", config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
           if (response.data !="") {
             $scope.companylist = response.data;
			 $scope.company = $scope.companylist[0].U_DBName;
           }
            else
               $scope.companylist = [];
       },
       function (response) {
           // failure callback

       }
    );

    
		
		
	}

    $scope.checklogin = function () {

var data = {
 "USERS": [{
  "UserName": $scope.userId,
  "PassWord": $scope.password
 }]
}
		//var data = {"apikey" : US.APIKEY,'method':"Login","UserName" : $scope.userId, "PassWord" : $scope.password}

        var config = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
            }
        }

        var parms = JSON.stringify(data);
        $http.post(url+"LogIn", "sJasonInput=" + parms, config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
           if (response.data.VALIDATE[0].Status !="False") {
               //$cookies.put('MenuInfo', JSON.stringify(response.data.MenuInfo));
               $cookies.put('UserData', JSON.stringify(response.data.USERINFO));
               $cookies.put('Islogin', "true");
			   if(response.data.USERINFO[0].account_type=="Customer")
              		window.location = "customerdashbord.html";
				else
					window.location = "dashbord.html";
           }
            else
               alert(response.data.VALIDATE[0].Msg);
       },
       function (response) {
           // failure callback

       }
    );

    }
	
	
	$scope.CreateTicketByNewUser = function () {

       
		var data = {
			"apikey" : US.APIKEY,
			'method':"CreateTicketByNewUser",
			"EmailId" : $scope.emailID,
			"Yourname" : $scope.Yourname,
			"priority_id":1,
			"help_topic_id":1,
			"Scenario": $scope.Scenario,
			"ExpectedScenario": $scope.ExpectedScenario,
			"ActualScenario": $scope.ActualScenario,
			"subject":$scope.subject
			}

        var config = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
            }
        }

        var parms = JSON.stringify(data);
        $http.post(url, "sJsonInput=" + parms, config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
           if (response.data.status !=false) {
              alert(response.data.Data);
           }
            else
               alert(response.data.MSG);
       },
       function (response) {
           // failure callback

       }
    );

    }
	
	


//$scope.loadcompany();
} ]);
