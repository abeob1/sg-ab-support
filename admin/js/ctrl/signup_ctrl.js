App.controller('signup', ['$scope', '$rootScope', '$http', '$window', '$cookies','util_SERVICE',

function ($scope, $rootScope, $http, $window, $cookies, US) {
    // $scope.items = Data;
    $scope.userId = "";
    $scope.password = "";
    $cookies.put('MenuInfo', "");
    $cookies.put('UserData', "");
    $cookies.put('Islogin', "false");
	
	var url = US.url;
	
	US.GetAccountType().then(function (response){$scope.AccountType=response.data.Data;console.log(response.data);});

	$scope.CreateNewUser = function () {

       
		var data = {
			"apikey" : US.APIKEY,
			'method':"CreateNewUser",
			"FirstName" : $scope.FirstName,
			"LastName" : $scope.LastName,
			"gender": $scope.gender,
			"email": $scope.email,
			"password":  $scope.password,
			"ConfirmPassword":  $scope.ConfirmPassword,
			"country_code":  $scope.country_code,
			"phone_number":  $scope.phone_number,
			"account_type": $scope.account_type
			}

        var parms = JSON.stringify(data);
        $http.post(url, "sJsonInput=" + parms, US.config)
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
	
	


//$scope.GetAccountType();
} ]);
