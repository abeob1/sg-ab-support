App.controller('signup', ['$scope', '$rootScope', '$http', '$window', '$cookies','util_SERVICE',

function ($scope, $rootScope, $http, $window, $cookies, US) {
    // $scope.items = Data;
    $scope.userId = "";
    $scope.password = "";
    $cookies.put('MenuInfo', "");
    $cookies.put('UserData', "");
    $cookies.put('Islogin', "false");
	
	var url = US.url;
	
	US.GetAccountType().then(function (response){$scope.AccountType=response.data.ACCOUNTTYPE;});

	$scope.CreateNewUser = function () {
			
			var data = {
						 "USERS": [{
						  "user_name": $scope.FirstName,
						  "first_name": $scope.FirstName,
						  "last_name": $scope.LastName,
						  "gender": $scope.gender,
						  "email": $scope.email,
						  "password": $scope.password,
						  "country_code":$scope.country_code,
						  "phone_number": $scope.phone_number,
						  "account_type": $scope.account_type
						 }]
						}

        var parms = JSON.stringify(data);
        $http.post(url+'CreateUser', "sJasonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
           if (response.data.VALIDATE[0].Status !="False") {
              alert(response.data.VALIDATE[0].Msg);
			  window.location = "index.html";
           }
            else
               alert(response.data.VALIDATE[0].Msg);
       },
       function (response) {
           // failure callback

       }
    );

    }
	
	


//$scope.GetAccountType();
} ]);
