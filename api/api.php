<?php
include('include/conn.php');

$postdata = json_decode($_POST['sJsonInput'],true);
$apikey = "kkksjjshjhjewfndsafjnenfaeeadadw";


//gmail credentials
$G_hostname = '{imap.gmail.com:993/imap/ssl/novalidate-cert/norsh}Inbox';
$G_username = 'thomasp@abeo-electra.com'; # e.g somebody@gmail.com
$G_password = 'Peterrajt1';
//credentials end
if(json_last_error()!=0)
	error_resp("Invalid Input Json");
else if(count($postdata)==0)
	error_resp("No Request Found");
else if($postdata['apikey']!=$apikey)
	error_resp("API key invalid");
	



switch ($postdata['method']) {
 	case "Login":
        Login($postdata);
        break;
	case "SignUp":
        SignUp($postdata);
        break;
    case "GetAllCategory":
        GetAllCategory();
        break;
    case "GetCourses":
        GetCourses($postdata);
        break;
    case "GetChapter":
        GetChapter($postdata);
        break;
	 case "GetAllMail":
        GetAllMail($postdata);
        break;
	case "GetMailById":
        GetMailById($postdata);
        break;
	case "GetAllTickect":
        GetAllTickect($postdata);
        break;
	case "GetTickectDetail":
        GetTickectDetail($postdata);
        break;
	case "CreateTicketByNewUser":
        CreateTicketByNewUser($postdata);
        break;
	case "GetUserForAssignment":
        GetUserForAssignment($postdata);
        break;
	case "GetDeveloperForAssignment":
        GetDeveloperForAssignment($postdata);
        break;
	case "assignDeveloper":
        assignDeveloper($postdata);
        break;
	case "GetDevManagerList":
        GetDevManagerList($postdata);
        break;
	case "EscalateToDevManager":
        EscalateToDevManager($postdata);
        break;
	case "EscalateToSAP":
        EscalateToSAP($postdata);
        break;
	case "GetCustomerList":
        GetCustomerList($postdata);
        break;
	case "ReplyToTicket":
        ReplyToTicket($postdata);
        break;
	case "INReplyToTicket":
        INReplyToTicket($postdata);
        break;
	case "AssignUser":
        AssignUser($postdata);
        break;
	case "changeTicketOwner":
        changeTicketOwner($postdata);
        break;
	case "GetAllStatus":
        GetAllStatus($postdata);
        break;
	case "AddNewStatus":
        AddNewStatus($postdata);
        break;
	case "GetAccountType":
        GetAccountType($postdata);
        break;
	case "changeTicketStatus":
        changeTicketStatus($postdata);
        break;
	case "changestatusFlag":
        changestatusFlag($postdata);
        break;
	case "changeuserStatus":
        changeuserStatus($postdata);
        break;
	case "changeuserCompany":
        changeuserCompany($postdata);
        break;
	case "changeuserDepartment":
        changeuserDepartment($postdata);
        break;
	case "changeuserType":
        changeuserType($postdata);
        break;
	case "CreateNewUser":
        CreateNewUser($postdata);
        break;
	case "AdminGetAllUsers":
        AdminGetAllUsers($postdata);
        break;
	case "AdminGetAllStatus":
        AdminGetAllStatus($postdata);
        break;
	case "AdminGetAllCompany":
        AdminGetAllCompany($postdata);
        break;
	case "AdminGetDepartment":
        AdminGetDepartment($postdata);
        break;
	case "GetactivityData":
        GetactivityData($postdata);
        break;
	case "endActivity":
        endActivity($postdata);
        break;
	case "startActivity":
        startActivity($postdata);
        break;
	default:
		error_resp("Method Name Not Found");
		break;
}


//sending mail using gmail smtp
function gmailsend($to, $subject, $body)
{
$headers = "From: " . strip_tags("thomasp@abeo-electra.com") . "\r\n";
$headers .= "Reply-To: ". strip_tags("thomasp@abeo-electra.com") . "\r\n";
//$headers .= "CC: susan@example.com\r\n";
$headers .= "MIME-Version: 1.0\r\n";
$headers .= "Content-Type: text/html; charset=UTF-8\r\n";


$result =  mail($to,$subject,$body,$headers);

}

//Check Login And Log User
function Login($d)
{
	global $conn;
	
	if($d['UserName']=="")
		error_resp("Enter valid Username");
	if($d['PassWord']=="")
		error_resp("Enter valid Password");
	 $stSql = "SELECT * FROM users where email='".$d['UserName']."' and password='".md5($d['PassWord'])."' and active=1";
  	$result = $conn->query($stSql);
	
  	if($result->num_rows>0)
	{
		while($row = $result->fetch_assoc())
		{
			$rdata[] = $row;
		}
		success_resp($rdata);
	}
	else
	{
		error_resp("unauthorised access");
	}
	
}
//get last ticket id
function getLastTicketId()
{	
	global $conn;
	$Sql = "SELECT ticket_number FROM tickets ORDER BY id DESC LIMIT 0, 1";
  	$result = $conn->query($Sql);
	 
  	$row = $result->fetch_assoc();
	$lastnumber = preg_replace("/[^0-9,.]/", "", $row['ticket_number']);
	return $lastnumber;
		
}

//get User list For ticket owner change  
function GetCustomerList($d)
{	
	global $conn;
	$Sql = "SELECT users.id,users.email, organization.name FROM users LEFT JOIN organization ON users.company=organization.id WHERE users.active=1 AND users.account_type='Customer'";
  	$result = $conn->query($Sql);
	 
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	
	success_resp($rdata);
		
}


//get User list For ticket Assignment
function GetDeveloperForAssignment($d)
{	
	global $conn;
	$Sql = "SELECT users.id,users.email, department.name FROM users LEFT JOIN department ON users.primary_dpt=department.id WHERE users.active=1 AND users.account_type='developer'";
  	$result = $conn->query($Sql);
	 
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	
	success_resp($rdata);
		
}




//assign Developer to a ticket
function assignDeveloper($d)
{
	global $conn;
	if($d['AssignedDeveloper']=="")
		error_resp("Developer ID can not be null");
	if($d['email']=="")
		error_resp("Email ID can not be null");
	if($d['TID']=="")
		error_resp("Ticket ID can not be null");
		
	
	$updated_at = date("y-m-d h:i:s");
	$stSql = "UPDATE tickets SET Developer_to = '".$d['AssignedDeveloper']."', updated_at = '".$updated_at."' WHERE id =".$d['TID'];
  	$result = $conn->query($stSql);
  	if($result)
	{
		//$body = "Dear Agent<br/><br/> We assigned a ticket to you , Ticket ID : ".$TID."<br/>Thanks<br/>Abeo Support admin";
		//gmailsend($emailId,"New Ticket Assigned to you",$body);
		success_resp("Developer Assigned Successfully.");
	}
	else
	{
		error_resp("System can not Assign Error Code:24");
	}	
}



//get User list For ticket Assignment
function GetUserForAssignment($d)
{	
	global $conn;
	$Sql = "SELECT users.id,users.email, department.name FROM users LEFT JOIN department ON users.primary_dpt=department.id WHERE users.active=1 AND users.account_type='consultant'";
  	$result = $conn->query($Sql);
	 
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	
	success_resp($rdata);
		
}

//get Dev Manager list For escalation
function GetDevManagerList($d)
{	
	global $conn;
	$Sql = "SELECT users.id,users.email, department.name FROM users LEFT JOIN department ON users.primary_dpt=department.id WHERE users.active=1 AND users.account_type='developmentmanager'";
  	$result = $conn->query($Sql);
	 
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	
	success_resp($rdata);
		
}



//Escalate ticket to DevManager
function EscalateToDevManager($d)
{
	global $conn;
	if($d['DevManager']=="")
		error_resp("Development Manager ID can not be null");
	if($d['email']=="")
		error_resp("Email ID can not be null");
	if($d['TID']=="")
		error_resp("Ticket ID can not be null");
		
	
	$updated_at = date("y-m-d h:i:s");
	$stSql = "UPDATE tickets SET Escalate_to= '".$d['DevManager']."', Escalate_type='Development', updated_at = '".$updated_at."' WHERE id =".$d['TID'];
  	$result = $conn->query($stSql);
  	if($result)
	{
		//$body = "Dear Agent<br/><br/> We assigned a ticket to you , Ticket ID : ".$TID."<br/>Thanks<br/>Abeo Support admin";
		//gmailsend($emailId,"New Ticket Assigned to you",$body);
		success_resp("Ticket Escalated Successfully.");
	}
	else
	{
		error_resp("System can not Escalate Error Code:23");
	}	
}




//Escalate ticket to SAP
function EscalateToSAP($d)
{
	global $conn;
	if($d['TID']=="")
		error_resp("Ticket ID can not be null");
		
	$created_at = date("y-m-d h:i:s");
	$ticket_id = $d['TID'];
	$user_id = $d['UserID'];
	$body = addslashes($d['body']);
	$ipaddress = $_SERVER['REMOTE_ADDR'];
	$is_internal = $d['is_internal'];
	
    $userQuery = "INSERT INTO ticket_thread (ticket_id, user_id, poster, source, reply_rating, rating_count, is_internal, title, bodyText, format, ip_address, created_at) VALUES ('".$ticket_id."', '".$user_id."', '', '2', '0', '0', '".$is_internal."', '', '".$body."', '', '".$ipaddress."', '".$created_at."')";
		
	
	$updated_at = date("y-m-d h:i:s");
	$stSql = "UPDATE tickets SET Escalate_to= '', Escalate_type='SAP', updated_at = '".$updated_at."' WHERE id =".$d['TID'];
  	$result = $conn->query($stSql);
  	if($result)
	{
			$INresult = $conn->query($userQuery);
		//$body = "Dear Agent<br/><br/> We assigned a ticket to you , Ticket ID : ".$TID."<br/>Thanks<br/>Abeo Support admin";
		//gmailsend($emailId,"New Ticket Assigned to you",$body);
		success_resp("Ticket Escalated Successfully.");
	}
	else
	{
		error_resp("System can not Escalate Error Code:23");
	}	
}



//get all status and icon
function GetAllStatus($d)
{	
	global $conn;
	$Sql = "SELECT id,name,icon_class FROM ticket_status where flags='0'";
  	$result = $conn->query($Sql);
	 
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	success_resp($rdata);
}


//create New Status 
function AddNewStatus($d)
{
	global $conn;
	
	if($d['StatusName']=="")
		error_resp("Enter valid Status Name");
	
		
	$StatusName = $d['StatusName'];
	$IconClass = $d['IconClass'];
	$Flag = $d['Flag'];
	$created_at = date("y-m-d h:i:s");
	
	//check if user Exist 
	$Sql = "SELECT * FROM ticket_status where name='".$StatusName."'";
  	$result = $conn->query($Sql);
	
  	if($result->num_rows>0)
	{
		error_resp("Status Name Already Exist");
	}
	else
	{
	
	 $userQuery = "INSERT INTO ticket_status (name, state, flags, icon_class, created_at,mode,message,sort,email_user,properties) VALUES ( '".$StatusName."', '".$StatusName."', '".$Flag."', '".$IconClass."', '".$created_at."','1','','1','1','')";
	$result = $conn->query($userQuery);
	if($result)
	{
	success_resp("Status Created Sucessfully");
	}
	else
	{
	error_resp("System can not create status Error Code:19");
	}
	
	}
	
  
	
}


//Admin Get All Status
function AdminGetAllStatus($d)
{	
	global $conn;
	$Sql = "SELECT * FROM ticket_status ORDER BY id DESC";
  	$result = $conn->query($Sql);
	 
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	success_resp($rdata);
}


//Admin Get All Users
function AdminGetAllUsers($d)
{	
	global $conn;
	$Sql = "SELECT a.*, b.name as 'DeptName',c.name as 'CompanyName' FROM users a LEFT JOIN department b ON b.id=a.primary_dpt LEFT JOIN organization c ON c.id=a.company ORDER BY id DESC";
  	$result = $conn->query($Sql);
	 
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	success_resp($rdata);
}

//Admin Get All Company
function AdminGetAllCompany($d)
{	
	global $conn;
	$Sql = "SELECT * From organization ORDER BY id DESC";
  	$result = $conn->query($Sql);
	 
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	success_resp($rdata);
}


//Admin Get All Department
function AdminGetDepartment($d)
{	
	global $conn;
	$Sql = "SELECT * From department ORDER BY id DESC";
  	$result = $conn->query($Sql);
	 
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	success_resp($rdata);
}



//update status flag
function changestatusFlag($d)
{
	global $conn;
	if($d['statusID']=="")
		error_resp("Status ID can not be null");
	if($d['UID']=="")
		error_resp("User ID can not be null");
		
	$UID = $d['UID'];
	$statusid = $d['statusID'];
	$updated_at = date("y-m-d h:i:s");
	$stSql = "UPDATE ticket_status SET flags = '".$statusid."', updated_at = '".$updated_at."' WHERE id =".$UID;
  	$result = $conn->query($stSql);
  	if($result)
	{
		success_resp("Status Updated Successfully.");
	}
	else
	{
		error_resp("System can not update staus Error Code:20");
	}	
}




//update user status
function changeuserStatus($d)
{
	global $conn;
	if($d['statusID']=="")
		error_resp("Status ID can not be null");
	if($d['UID']=="")
		error_resp("User ID can not be null");
		
	$UID = $d['UID'];
	$statusid = $d['statusID'];
	$updated_at = date("y-m-d h:i:s");
	$stSql = "UPDATE users SET active = '".$statusid."', updated_at = '".$updated_at."' WHERE id =".$UID;
  	$result = $conn->query($stSql);
  	if($result)
	{
		success_resp("User Status Updated Successfully.");
	}
	else
	{
		error_resp("System can not update staus Error Code:15");
	}	
}


//update user Company
function changeuserCompany($d)
{
	global $conn;
	if($d['CID']=="")
		error_resp("Company ID can not be null");
	if($d['UID']=="")
		error_resp("User ID can not be null");
		
	$UID = $d['UID'];
	$cid = $d['CID'];
	$updated_at = date("y-m-d h:i:s");
	$stSql = "UPDATE users SET company = '".$cid."', updated_at = '".$updated_at."' WHERE id =".$UID;
  	$result = $conn->query($stSql);
  	if($result)
	{
		success_resp("User Company Updated Successfully.");
	}
	else
	{
		error_resp("System can not update Company Error Code:16");
	}	
}


//update user Department
function changeuserDepartment($d)
{
	global $conn;
	if($d['CID']=="")
		error_resp("Department ID can not be null");
	if($d['UID']=="")
		error_resp("User ID can not be null");
		
	$UID = $d['UID'];
	$cid = $d['CID'];
	$updated_at = date("y-m-d h:i:s");
	$stSql = "UPDATE users SET primary_dpt = '".$cid."', updated_at = '".$updated_at."' WHERE id =".$UID;
  	$result = $conn->query($stSql);
  	if($result)
	{
		success_resp("User Department Updated Successfully.");
	}
	else
	{
		error_resp("System can not update Department Error Code:17");
	}	
}

//update user Type
function changeuserType($d)
{
	global $conn;
	//if($d['CID']=="")
		//error_resp("Account Type can not be null");
	if($d['UID']=="")
		error_resp("User ID can not be null");
		
	$UID = $d['UID'];
	$cid = $d['CID'];
	$updated_at = date("y-m-d h:i:s");
	$stSql = "UPDATE users SET account_type = '".$cid."', updated_at = '".$updated_at."' WHERE id =".$UID;
  	$result = $conn->query($stSql);
  	if($result)
	{
		success_resp("User Account Type Updated Successfully.");
	}
	else
	{
		error_resp("System can not update Account Type Error Code:18");
	}	
}


//Get All Account Type
function GetAccountType($d)
{	
	global $conn;
	$Sql = "select account_type from users group by account_type";
  	$result = $conn->query($Sql);
	 
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	success_resp($rdata);
}

//update ticket status
function changeTicketStatus($d)
{
	global $conn;
	if($d['statusID']=="")
		error_resp("Status ID can not be null");
	if($d['TID']=="")
		error_resp("Ticket ID can not be null");
		
	$TID = $d['TID'];
	$statusid = $d['statusID'];
	$updated_at = date("y-m-d h:i:s");
	$stSql = "UPDATE tickets SET status = '".$statusid."', updated_at = '".$updated_at."' WHERE id =".$TID;
  	$result = $conn->query($stSql);
  	if($result)
	{
		success_resp("Ticket Status Updated Successfully.");
	}
	else
	{
		error_resp("System can not update staus Error Code:14");
	}	
}


//Get all ticket
function GetAllTickect($d)
{
	global $conn;
	if($d["account_type"]=="Admin" ||  $d["account_type"]=="TicketManager")
	{
	$stSql = "SELECT a.*, b.*,c.first_name as 'AssignedTOName'  FROM tickets a LEFT JOIN ticket_priority b ON a.priority_id=b.priority_id  LEFT JOIN users c ON a.assigned_to=c.id ORDER BY a.ticket_number DESC";
	}
	else if($d["account_type"]=="Customer")
	{
	$stSql = "SELECT a.*, b.*,c.first_name as 'AssignedTOName'  FROM tickets a LEFT JOIN ticket_priority b ON a.priority_id=b.priority_id  LEFT JOIN users c ON a.assigned_to=c.id where a.user_id='".$d['UserID']."'  ORDER BY a.ticket_number DESC";
	}
	else if($d["account_type"]=="developmentmanager")
	{
	$stSql = "SELECT a.*, b.*,c.first_name as 'AssignedTOName'  FROM tickets a LEFT JOIN ticket_priority b ON a.priority_id=b.priority_id  LEFT JOIN users c ON a.Escalate_to=c.id where a.Escalate_to='".$d['UserID']."'  ORDER BY a.ticket_number DESC";
	}
	else if($d["account_type"]=="developer")
	{
	$stSql = "SELECT a.*, b.*,c.first_name as 'AssignedTOName'  FROM tickets a LEFT JOIN ticket_priority b ON a.priority_id=b.priority_id  LEFT JOIN users c ON a.Developer_to=c.id where a.Developer_to='".$d['UserID']."'  ORDER BY a.ticket_number DESC";
	}
	else
	{
	$stSql = "SELECT a.*, b.*,c.first_name as 'AssignedTOName' FROM tickets a LEFT JOIN ticket_priority b ON a.priority_id=b.priority_id  LEFT JOIN users c ON a.assigned_to=c.id where a.assigned_to='".$d['UserID']."' ORDER BY a.ticket_number DESC";
	}
	
  	$result = $conn->query($stSql);
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	
	success_resp($rdata);
}




//change Ticket Owner
function changeTicketOwner($d)
{
	global $conn;
	if($d['NewOwner']=="")
		error_resp("User ID can not be null");
	if($d['email']=="")
		error_resp("Email ID can not be null");
	if($d['TID']=="")
		error_resp("Ticket ID can not be null");
		
	$usser = $d['NewOwner'];
	$TID = $d['TID'];
	$emailId = $d['email'];
	$updated_at = date("y-m-d h:i:s");
	$stSql = "UPDATE tickets SET user_id = '".$usser."', updated_at = '".$updated_at."' WHERE id =".$TID;
  	$result = $conn->query($stSql);
  	if($result)
	{
		$body = "Dear Customer<br/><br/> Ticket Owner hasbeen changed to you , Ticket ID : ".$TID."<br/><br/>Thanks<br/>Abeo Support admin";
		gmailsend($emailId,"Ticket Owner hasbeen changed",$body);
		success_resp("Ticket Owner changed Successfully.");
	}
	else
	{
		error_resp("System can not change owner Error Code:14");
	}	
}


//Get all ticket to admin 
function AssignUser($d)
{
	global $conn;
	if($d['AssignedUserId']=="")
		error_resp("User ID can not be null");
	if($d['email']=="")
		error_resp("Email ID can not be null");
	if($d['TID']=="")
		error_resp("Ticket ID can not be null");
		
	$usser = $d['AssignedUserId'];
	$TID = $d['TID'];
	$emailId = $d['email'];
	$updated_at = date("y-m-d h:i:s");
	$stSql = "UPDATE tickets SET assigned_to = '".$usser."', updated_at = '".$updated_at."' WHERE id =".$TID;
  	$result = $conn->query($stSql);
  	if($result)
	{
		$body = "Dear Agent<br/><br/> We assigned a ticket to you , Ticket ID : ".$TID."<br/>Thanks<br/>Abeo Support admin";
		gmailsend($emailId,"New Ticket Assigned to you",$body);
		success_resp("Ticket Assigned Successfully.");
	}
	else
	{
		error_resp("System can not Assign Error Code:13");
	}	
}


//get ticket details by id 
function GetTickectDetail($d)
{
	global $conn;
	//$stSql = "SELECT tickets.Escalate_type,tickets.id,tickets.ticket_number,tickets.Scenario,tickets.ExpectedScenario,tickets.ActualScenario, ticket_priority.priority,ticket_source.name as 'sourceName',department.name, users.email,users.mobile, help_topic.topic, ticket_status.name  as 'statusName' FROM tickets LEFT JOIN ticket_priority ON tickets.priority_id=ticket_priority.priority_id LEFT JOIN users ON tickets.user_id=users.id LEFT JOIN department ON tickets.dept_id=department.id LEFT JOIN ticket_source ON tickets.source=ticket_source.id LEFT JOIN help_topic ON tickets.help_topic_id=help_topic.id LEFT JOIN ticket_status ON tickets.status=ticket_status.id WHERE tickets.id='".$d['ID']."'";
	
	$stSql = "SELECT u1.first_name as assignedUserName, u2.first_name as assignedDeveloperName, u3.first_name as assignedDevManagerName,t.Escalate_type,t.id,t.ticket_number,t.Scenario,t.ExpectedScenario,t.ActualScenario, tp.priority,ticket_source.name as 'sourceName',d.name, U.email,U.mobile, help_topic.topic, ticket_status.name  as 'statusName' FROM tickets as t 
	LEFT JOIN ticket_priority as TP ON t.priority_id=TP.priority_id 
	LEFT JOIN users as U ON t.user_id=U.id 
	LEFT JOIN users as u1 ON t.assigned_to=u1.id 
	LEFT JOIN users as u2 ON t.Developer_to=u2.id 
	LEFT JOIN users as u3 ON t.Escalate_to=u3.id 
	LEFT JOIN department as d ON t.dept_id=d.id 
	LEFT JOIN ticket_source ON t.source=ticket_source.id 
	LEFT JOIN help_topic ON t.help_topic_id=help_topic.id 
	LEFT JOIN ticket_status ON t.status=ticket_status.id WHERE t.id='".$d['ID']."'";
  	$result = $conn->query($stSql);
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	
	
	//get ticket thread by id
	$ttSql = "SELECT * FROM ticket_thread  LEFT JOIN users ON ticket_thread.user_id=users.id  where ticket_thread.ticket_id='".$d['ID']."' ORDER BY ticket_thread.id DESC";
  	$ttresult = $conn->query($ttSql);
  	while($ttrow = $ttresult->fetch_assoc())
	{
		$ttrow["bodyText"] = stripslashes($ttrow["bodyText"]);
		$ttrdata[] = $ttrow;
	}
	
	$arr = array("Details"=>$rdata,"thread"=>$ttrdata);
	
	success_resp($arr);

}

//reply to the tickect by id 
function ReplyToTicket($d)
{	
	global $conn;
	
	if(!filter_var($d['email'], FILTER_VALIDATE_EMAIL))
		error_resp("Email id not valid");
	if($d['TID']=="")
		error_resp("Ticket Id Missing");
	if($d['body']=="")
		error_resp("Enter valid reply text");
	if($d['UserID']=="")
		error_resp("User ID not valid");
		
	$created_at = date("y-m-d h:i:s");
	$ticket_id = $d['TID'];
	$user_id = $d['UserID'];
	$body = addslashes($d['body']);
	$ipaddress = $_SERVER['REMOTE_ADDR'];
	$email = $d['email'];
	$is_internal = $d['is_internal'];
	
    $userQuery = "INSERT INTO ticket_thread (ticket_id, user_id, poster, source, reply_rating, rating_count, is_internal, title, bodyText, format, ip_address, created_at) VALUES ('".$ticket_id."', '".$user_id."', '', '2', '0', '0', '".$is_internal."', '', '".$body."', '', '".$ipaddress."', '".$created_at."')";
	$result = $conn->query($userQuery);
	if($result)
	{
		gmailsend($email,"Ticket Reply",$body);
		success_resp("Reply Sent Sucessfully");
	}
	else
	{
		error_resp("Reply Not Sent Error Code:12");
	}
}


//reply to the tickect by id 
function INReplyToTicket($d)
{	
	global $conn;
	
	
	if($d['TID']=="")
		error_resp("Ticket Id Missing");
	if($d['body']=="")
		error_resp("Enter valid reply text");
	if($d['UserID']=="")
		error_resp("User ID not valid");
		
	$created_at = date("y-m-d h:i:s");
	$ticket_id = $d['TID'];
	$user_id = $d['UserID'];
	$body = addslashes($d['body']);
	$ipaddress = $_SERVER['REMOTE_ADDR'];
	$is_internal = $d['is_internal'];
	
    $userQuery = "INSERT INTO ticket_thread (ticket_id, user_id, poster, source, reply_rating, rating_count, is_internal, title, bodyText, format, ip_address, created_at) VALUES ('".$ticket_id."', '".$user_id."', '', '2', '0', '0', '".$is_internal."', '', '".$body."', '', '".$ipaddress."', '".$created_at."')";
	$result = $conn->query($userQuery);
	if($result)
	{
		//gmailsend($email,"Ticket Reply",$body);
		success_resp("Reply Sent Sucessfully");
	}
	else
	{
		error_resp("Reply Not Sent Error Code:12");
	}
}



//create user by mail id 
function createUser($email,$username)
{
	global $conn;
	
	$userID = 0;
	//check if user exsist 
	$Sql = "SELECT * FROM users where email='".$email."'";
  	$result = $conn->query($Sql);
	
  	if($result->num_rows>0)
	{
	
		while($row = $result->fetch_assoc())
		{
			$userID = $row['id'];
		}
	}
	else
	{
	$password = sprintf("%06d", mt_rand(1, 999999));
	
	  $userQuery = "INSERT INTO users (user_name, first_name, last_name, gender, email, ban, password, active, ext, country_code, phone_number, mobile, agent_sign, account_type, account_status, assign_group, primary_dpt, agent_tzone, limit_access, company, role, internal_note, profile_pic, remember_token, created_at, updated_at) VALUES ( '".$username."', '', '', '1', '".$email."', '', '".md5($password)."', '1', '', '', '', NULL, '', 'Customer', 'active', NULL, '1', '', '', '', '', '', '', NULL, '".date("m-d-y")."', NULL)";
	$result = $conn->query($userQuery);
	if($result)
	{
	$userID = $conn->insert_id;
		
	gmailsend($email,"Your Profile is Ready","Hi<br/> Your Username: ".$email."<br/>  Your Password: ".$password."<br/>  Thanks<br/> Abeo Team");
	}
	
	}
	
  	return $userID;
	
}

//create user by signup form
function CreateNewUser($d)
{
	global $conn;
	if(!filter_var($d['email'], FILTER_VALIDATE_EMAIL))
		error_resp("Enter valid Email id");
	if(strlen($d['password'])<6)
		error_resp("Password length must be more than 6");
	if(!($d['ConfirmPassword']==$d['password']))
		error_resp("Password and Confirm Password must be same");
	if($d['gender']=="")
		error_resp("Select Gender");
	if($d['FirstName']=="")
		error_resp("Enter your First Name");
	
		
	$email = $d['email'];
	$password = $d['password'];
	$user_name = $d['FirstName']." ".$d['LastName'];
	$FirstName = $d['FirstName'];
	$LastName = $d['LastName'];
	$gender = $d['gender'];
	$country_code = $d['country_code'];
	$phone_number = $d['phone_number'];
	$account_type = $d['account_type'];
	$created_at = date("y-m-d h:i:s");
	
	//check if user Exist 
	$Sql = "SELECT * FROM users where email='".$email."'";
  	$result = $conn->query($Sql);
	
  	if($result->num_rows>0)
	{
		error_resp("User Details Already Exist");
	}
	else
	{
	
	 $userQuery = "INSERT INTO users (user_name, first_name, last_name, gender, email, ban, password, active, country_code, phone_number, account_type, account_status, created_at) VALUES ( '".$user_name."', '".$FirstName."', '".$LastName."', '".$gender."', '".$email."', '', '".md5($password)."', '0', '".$country_code."', '".$phone_number."',  '".$account_type."', 'Inactive','".$created_at."')";
	$result = $conn->query($userQuery);
	if($result)
	{
	$userID = $conn->insert_id;
		
	gmailsend($email,"Your Profile is Ready","Hi<br/> Your Username: ".$email."<br/>  Your Password: ".$password."<br/><br/>  Thanks<br/> Abeo Support Team");
	success_resp("Profile Created Sucessfully");
	}
	
	}
	
  
	
}

//Create Ticke tBy New User
function CreateTicketByNewUser($d)
{
	global $conn;
	
	if(!filter_var($d['EmailId'], FILTER_VALIDATE_EMAIL))
		error_resp("Enter valid Email id");
	if($d['Yourname']=="")
		error_resp("Enter a valid Name");
	if($d['priority_id']=="")
		error_resp("Select a valid priority");
	if($d['help_topic_id']=="")
		error_resp("Select a valid topic");
	if($d['Scenario']=="")
		error_resp("Enter a valid Scenario");
	if($d['ExpectedScenario']=="")
		error_resp("Enter a valid Expected Scenario");
	if($d['ActualScenario']=="")
		error_resp("Enter a valid ActualScenario");
		
		
	
	$userid = createUser($d['EmailId'],$d['Yourname']);
	if($userid==0)
		error_resp("User Not Created!");
		
		
		
		$ticket_number = "AAA".(getLastTicketId()+1);
		$user_id = $userid;
		$dept_id = "";
		$team_id = "";
		$priority_id = $d['priority_id'];
		$help_topic_id = $d['help_topic_id'];
		$status = 1;
		$rating = "";
		$ip_address = $_SERVER['REMOTE_ADDR'];
		$assigned_to = 1;
		$created_at = date("y-m-d h:i:s");
		$Scenario = $d['Scenario'];
		$ExpectedScenario = $d['ExpectedScenario'];
		$ActualScenario = $d['ActualScenario'];
		$subject = $d['subject'];
	
	$InsertSql = "INSERT INTO tickets ( ticket_number, user_id, dept_id, team_id, priority_id, help_topic_id, status, rating, ip_address, assigned_to, created_at, Scenario, ExpectedScenario, ActualScenario,Subject,source) VALUES ( '".$ticket_number."', '".$user_id."', '".$dept_id."', '".$team_id."', '".$priority_id."', '".$help_topic_id."', '".$status."', '".$rating."', '".$ip_address."', '".$assigned_to."', '".$created_at."', '".$Scenario."', '".$ExpectedScenario."', '".$ActualScenario."', '".$subject."','1');";
  	$result = $conn->query($InsertSql);
	
  	if($result=== TRUE)
	{
		success_resp("Ticket Created Sucessfully");
	}
	else
	{
		error_resp("Something error access");
	}
	
}


//give all category to api caller no login requird
function GetAllCategory()
{
	global $conn;
	$stSql = "SELECT * FROM category where active='true' ORDER BY id ASC";
  	$result = $conn->query($stSql);
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	
	success_resp($rdata);
}




//give courses to api caller no login requird,category id need
function GetCourses($d)
{
	global $conn;
	$rdata= [] ;
	$stSql = "SELECT * FROM courses where active='true' and category_id='".$d['catID']."' ORDER BY id ASC";
  	$result = $conn->query($stSql);
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	
	success_resp($rdata);
}

//give Chapter to api caller no login requird,courses id need
function GetChapter($d)
{
	global $conn;
	$rdata= [] ;
	$stSql = "SELECT * FROM chapter where active='true' and course_id='".$d['ID']."' ORDER BY id ASC";
  	$result = $conn->query($stSql);
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	
	success_resp($rdata);
}

//Get activity Data by ticket id 
function GetactivityData($d)
{
	global $conn;
	$rdata= [] ;
	$stSql = "SELECT * FROM activity where EndTime='0000-00-00 00:00:00' and ticket_id='".$d['TID']."' and activity_by='".$d['UserID']."' ORDER BY activity_id ASC";
  	$result = $conn->query($stSql);
  	while($row = $result->fetch_assoc())
	{
		$rdata[] = $row;
	}
	
	success_resp($rdata);
}

//endActivity

function endActivity($d)
{

	global $conn;
	if($d['AID']=="")
		error_resp("User ID can not be null");
		
	$updated_at = date("y-m-d h:i:s");
	$stSql = "UPDATE activity SET EndTime = '".$updated_at."' WHERE activity_id =".$d['AID'];
  	$result = $conn->query($stSql);
  	if($result)
	{
		success_resp("Ticket Task Ended Successfully.");
	}
	else
	{
		error_resp("System can not change Activity Code:21");
	}	

}

//startActivity

function startActivity($d)
{

	global $conn;
	if($d['TID']=="")
		error_resp("Ticket ID can not be null");
	if($d['UserID']=="")
		error_resp("User ID can not be null");
		
	$updated_at = date("y-m-d h:i:s");
 $stSql = "INSERT INTO activity (ticket_id, activity_by, StartTime, EndTime, Description) VALUES ('".$d['TID']."', '".$d['UserID']."', '".$updated_at ."', '', '".$d['Desc']."')";
  	$result = $conn->query($stSql);
  	if($result)
	{
		success_resp("Ticket Task Started Successfully.");
	}
	else
	{
		error_resp("System can not start Activity Code:22");
	}	

}


//Check Login And Log User
function GetAllMail($d)
{
	global $G_hostname,$G_username,$G_password;
	$nntp  = @imap_open($G_hostname,$G_username,$G_password) or die('Cannot connect to Gmail: ' . imap_last_error());
	$threads = imap_thread($nntp);
	
  	
	if($threads!="")
	{
	$branches = array();
$currenttree = null;
foreach($threads as $key => $value){
/*
    list($num,$type) = explode('.',$key);
    switch($type){
        case 'num':
            //nothing
            break;
        case 'next':
            if(is_null($currenttree)) $currenttree = &$branches[$threads[$value.'.num']];
            if($value && isset($threads[$value.'.num'])) $currenttree[] = $threads[$value.'.num'];
            break;
        case 'branch':
            unset($currenttree);
            if($value && $threads[$value.'.num']){
                $branches[$threads[$value.'.num']] = array($threads[$value.'.num']);
                $currenttree =& $branches[$threads[$value.'.num']];
            }
    }
	*/
 list($num,$type) = explode('.',$key);
 if($type=="num")
 {
 	$header = imap_headerinfo($nntp, $value);
	$branches[$value] = array("from"=>$header->fromaddress,"subject"=>$header->subject,"Val"=>$value);
 }

}

imap_close($nntp);
		success_resp($branches);
		}
		else
		{
	imap_close($nntp);
			error_resp("Cannot connect to Gmail");
		}
		
	
	
}



//Check Login And Log User
function GetMailById($d)
{
	$mailDetails = array();
	$IDlist = $d['MailId'];
	global $G_hostname,$G_username,$G_password;
	$nntp  = @imap_open($G_hostname,$G_username,$G_password) or die('Cannot connect to Gmail: ' . imap_last_error());
	
	//for ($x = 0; $x < count($IDlist); $x++) 
	//{
    	$header = imap_headerinfo($nntp, $IDlist);
		$mailDetails[$x] = $header->fromaddress;
	//} 
	
  	imap_close($nntp);
	
	
	if(count($mailDetails)!=0)
	{
		success_resp($mailDetails);
	}
	else
	{
		error_resp("Cannot Read Mail list");
		
	}
		
		
	
	
}




function error_resp($errormsg)
{
	$response = ["status"=>false,"MSG"=>$errormsg];
	header('Content-Type: application/json');
	echo json_encode($response);
	exit();
}

function success_resp($data)
{
	$response = ["status"=>true,"MSG"=>"","Data"=>$data];
	header('Content-Type: application/json');
	echo json_encode($response);
	
}

?>
