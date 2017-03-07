App.controller('detail_ctrl', ['$scope', '$rootScope', '$http', '$window', '$cookies','util_SERVICE',

function ($scope, $rootScope, $http, $window, $cookies, US) {
	
	$scope.reply_body = "";
	$scope.reply_cannedResponce = "";
  	US.islogin();
	
   
	$scope.userdata = JSON.parse($cookies.get('UserData'));
 //$scope.calltypeSelected = "";
 
 
 console.log($scope.userdata);
 US.GetAllStatus().then(function (response){$scope.AllStatus=response.data.Data;console.log(response.data);});
 
 //change ticket status 
  $scope.changeTicketStatus = function(id)
  {
	   var data = {"apikey" : US.APIKEY,
					'method':"changeTicketStatus",
					"UserID":$scope.userdata[0].id,
					"TID":$scope.details.id,
					"statusID":id}

        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
             if (response.data.status !=false) {
				 $scope.GetTickectDetail($scope.details.id);
              US.info("Alert",response.data.Data,"success");
           }
            else
               US.info("Alert",response.data.MSG,"danger");
			   
		  },
       function (response) {});
  }
 
 $scope.GetTickectDetail = function (id) {

        var data = {"apikey" : US.APIKEY,'method':"GetTickectDetail","UserID":$scope.userdata[0].id,"ID":id}

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
          $scope.details = response.data.Data.Details[0];
		  $scope.thread = response.data.Data.thread;
		  
		  for(var i = 0;i<$scope.thread.length;i++)
		  {
			  $scope.thread[i].bodyTextSub = decodeEntities($scope.thread[i].bodyText);
			  
		  }
           
       },
       function (response) {
           // failure callback

       }
    );

    }
	
	
	//get Customer list to change owner 
	$scope.GetOwnersList = function (id) {

        var data = {"apikey" : US.APIKEY,'method':"GetCustomerList","UserID":$scope.userdata[0].id}

        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
           $rootScope.CustomersList = response.data.Data;
		   $('#customerModal').modal('show'); 
		  },
       function (response) {
           // failure callback

       }
    );

    }
	
	//change ticket owner
	 $scope.changeOwner = function (id,email) {
        var data = {"apikey" : US.APIKEY,
					'method':"changeTicketOwner",
					"UserID":$scope.userdata[0].id,
					"TID":$scope.details.id,
					"email":email,
					"NewOwner":id}

        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
             if (response.data.status !=false) {
				  $('#customerModal').modal('toggle');
				  $scope.GetTickectDetail($scope.details.id);
			  US.info("Alert",response.data.Data,"success");
			  
               
           }
            else
               US.info("Alert",response.data.MSG,"danger");
			   
		  },
       function (response) {
           // failure callback

       }
    );

    }
	
	
	
	
	//get agent list for assignment
	 $scope.GetUserForAssignment = function (id) {

        var data = {"apikey" : US.APIKEY,'method':"GetUserForAssignment","UserID":$scope.userdata[0].id}

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
           $rootScope.agentList = response.data.Data;
		  },
       function (response) {
           // failure callback

       }
    );

    }
	
	//assign ticket to agent
	 $scope.AssignUser = function (id,email) {
        var data = {"apikey" : US.APIKEY,
					'method':"AssignUser",
					"UserID":$scope.userdata[0].id,
					"TID":$scope.details.id,
					"email":email,
					"AssignedUserId":id}

        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
             if (response.data.status !=false) {
				  $('#assignModal').modal('toggle');
              US.info("Alert",response.data.Data,"success");
			  
               
           }
            else
               US.info("Alert",response.data.MSG,"danger");
			   
		  },
       function (response) {
           // failure callback

       }
    );

    }
	
	
	
	$scope.opwn = function(elementID)
	{
		var status = $("#mainc"+elementID).css( "display" );
		if(status=='none')
		{
			$("#mainc"+elementID).css("display","block");
			$("#subc"+elementID).css("display","none");
			$("#tdcss"+elementID).css("background-color","#fff");
			}
		else
		{
			$("#mainc"+elementID).css("display","none");
			$("#subc"+elementID).css("display","block");
			$("#tdcss"+elementID).css("background-color","#f9f9f9");
			}
	}
	
	//reply to the ticket
	$scope.reply = function()
	{
		var bodytext= CKEDITOR.instances['editor1'].getData();
		
				  var data = {
					"apikey" : US.APIKEY,
				  	'method':"ReplyToTicket",
					"UserID":$scope.userdata[0].id,
					"TID":$scope.details.id,
					"body": encodeURIComponent(bodytext),
					"email":$scope.details.email,
					"source":2,
					"is_internal":0
					}

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
						    if (response.data.status !=false) {
							  US.info("Alert",response.data.Data,"success");
						   }
							else
							   US.info("Alert",response.data.MSG,"danger");
					
						   
					   },
					   function (response) {
						   // failure callback
				
					   }
					);
			
	}
	

$scope.GetTickectDetail(getParameterByName('id'));
} ]);

function getParameterByName(name, url) {
    if (!url) {
      url = window.location.href;
    }
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}
var decodeEntities = (function () {
        //create a new html document (doesn't execute script tags in child elements)
        var doc = document.implementation.createHTMLDocument("");
        var element = doc.createElement('div');

        function getText(str) {
            element.innerHTML = str;
            str = element.textContent;
            element.textContent = '';
            return str;
        }

        function decodeHTMLEntities(str) {
            if (str && typeof str === 'string') {
                var x = getText(str);
                while (str !== x) {
                    str = x;
                    x = getText(x);
                }
                return x;
            }
        }
        return decodeHTMLEntities;
    })();