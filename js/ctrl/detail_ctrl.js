App.controller('detail_ctrl', ['$scope', '$rootScope', '$http', '$window', '$cookies','util_SERVICE',

function ($scope, $rootScope, $http, $window, $cookies, US) {
	
	$scope.reply_body = "";
	$scope.reply_cannedResponce = "";
  	US.islogin();
	
   $rootScope.notification = false;
	$scope.userdata = JSON.parse($cookies.get('UserData'));
 //$scope.calltypeSelected = "";
 
 
 console.log($scope.userdata);
 US.GetAllStatus().then(function (response){$scope.AllStatus=response.data.TICKETSTATUS;});
 
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
  
  
  
  $scope.openDuedatePop = function()
	{
		$("#DDPOPUP").modal('show');
	}
	
	$scope.setDueDate = function(DDate)
	{
		console.log(DDate);
		US.SetDueDate($scope.details.id,DDate).then(function (response){  if (response.data.VALIDATE[0].Status !=false) {
			  $("#DDPOPUP").modal('hide');
             //alert(response.data.VALIDATE[0].Msg);
			$scope.GetTickectDetail($scope.details.id);
			  
           }
            else
			{
				$("#DDPOPUP").modal('hide');
               //alert(response.data.VALIDATE[0].Msg);
			}
			   
			   });
	}
	
	
  $scope.assignConsultant = function(Tid)
	{
		
		$rootScope.CurrentTD = Tid;
		US.GetagentList().then(function (response){$scope.agentList=response.data.CONSULTANTS;});
		$("#assignModal").modal('show');
	}
	
	$scope.AssignUser = function(id)
	{
		US.AssignTicket($scope.details.id,id).then(function (response){  if (response.data.VALIDATE[0].Status !=false) {
			  $("#assignModal").modal('hide');
             //alert(response.data.VALIDATE[0].Msg);
			$scope.GetTickectDetail($scope.details.id);
			  
           }
            else
			{
				$("#assignModal").modal('hide');
               //alert(response.data.VALIDATE[0].Msg);
			}
			   
			   });
	}
	
	
  
  $scope.closeTicket = function(TID)
  {
	  var notification = $("#notification").is(':checked') ? "True" : "False";
	    var data = { "TICKET": [{  "TID": TID, "StatusID":"2","Notification":notification }]}

        var parms = JSON.stringify(data);
        $http.post(US.url+'UpdateTicketStatus', "sJasonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
             if (response.data.VALIDATE[0].Status !=false) {
				 $scope.GetTickectDetail($scope.details.id);
              //US.info("Alert",response.data.Data,"success");
           }
            else
               alert(response.data.VALIDATE[0].MSG);
			   
		  },
       function (response) {});
  }
 
 $scope.GetTickectDetail = function (id) {

        var data = { "TICKET": [{  "TID": id }]}


        var parms = JSON.stringify(data);
        $http.post(US.url+'GetTicketDetails', "sJasonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
          $scope.details = response.data.TICKETS[0];
		  $scope.thread = response.data.TICKET_THREAD;
		  //get activity data
		  //US.GetactivityData(id,$scope.userdata[0].id).then(function (response){$scope.GetactivityData=response.data.Data;});
		  
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
	//end active activity 
	$scope.endActivity = function(id)
	{
		
	   var data = {"apikey" : US.APIKEY,
					'method':"endActivity",
					"UserID":$scope.userdata[0].id,
					"AID":id
					}

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
	
	//startactivity by ticket
	$scope.startActivity = function(id)
	{
		alert(id);
		
	   var data = {"apikey" : US.APIKEY,
					'method':"startActivity",
					"UserID":$scope.userdata[0].id,
					"Desc":"",
					"TID":id
					}

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
	
	
	
	//get dev manager list for escalate
	 $scope.GetDeveloperForAssignment = function (id) {

        var data = {"apikey" : US.APIKEY,'method':"GetDeveloperForAssignment","UserID":$scope.userdata[0].id}

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
           $rootScope.Developers = response.data.Data;
		  },
       function (response) {
           // failure callback

       }
    );

    }
	
	
	
		//assign ticket to Developer by manager
	 $scope.assignDeveloper = function (id,email) {
        var data = {"apikey" : US.APIKEY,
					'method':"assignDeveloper",
					"UserID":$scope.userdata[0].id,
					"TID":$scope.details.id,
					"email":email,
					"AssignedDeveloper":id}

        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
             if (response.data.status !=false) {
				  $('#DevelopersModal').modal('toggle');
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
	//get dev manager list for escalate
	 $scope.EscalateSAP = function (id) {
		 $('#SAPManagerModal').modal('show');
		 
		 $('#SAPManagerModal').on('shown.bs.modal', function () {
				$('#textareaID').focus();
			})  
		 
	 }
	
	//get dev manager list for escalate
	 $scope.GetDevManagerList = function (id) {

        var data = {"apikey" : US.APIKEY,'method':"GetDevManagerList","UserID":$scope.userdata[0].id}

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
           $rootScope.DevManagerList = response.data.Data;
		  },
       function (response) {
           // failure callback

       }
    );

    }
	
	
	//Escalate ticket to DevManager
	 $scope.EscalateToDevManager = function (id,email) {
        var data = {"apikey" : US.APIKEY,
					'method':"EscalateToDevManager",
					"UserID":$scope.userdata[0].id,
					"TID":$scope.details.id,
					"email":email,
					"DevManager":id}

        var parms = JSON.stringify(data);
        $http.post(US.url, "sJsonInput=" + parms, US.config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
             if (response.data.status !=false) {
				  $('#devManagerModal').modal('toggle');
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
           $rootScope.agentListddd = response.data.Data;
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
			$("#Rainc").css("display","block");
			
			$("#subc"+elementID).css("display","none");
			$("#tdcss"+elementID).css("background-color","#fff");
			}
		else
		{
			$("#mainc"+elementID).css("display","none");
			$("#Rainc").css("display","none");
			$("#homeReply").css("display","none");
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
	
	$scope.INreplyBody = "";
	
	//reply to the ticket
	$scope.INreply = function()
	{
		
				  var data = {
					"apikey" : US.APIKEY,
				  	'method':"INReplyToTicket",
					"UserID":$scope.userdata[0].id,
					"TID":$scope.details.id,
					"body": encodeURIComponent($scope.INreplyBody),
					"source":2,
					"is_internal":1
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
	
	$rootScope.ETOSAPBody = "ssssss";
	
	//reply to the ticket
	$rootScope.INreplyETOSAP = function()
	{
		
				  var data = {
					"apikey" : US.APIKEY,
				  	'method':"EscalateToSAP",
					"UserID":$scope.userdata[0].id,
					"TID":$scope.details.id,
					"body": encodeURIComponent($scope.INreplyBody),
					"source":2,
					"is_internal":1
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