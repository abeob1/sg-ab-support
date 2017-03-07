<?php
error_reporting(0);
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "support";
$GLOBALS['UploadPath'] = "upload/";


/*$servername = "localhost";
$username = "root";
$password = "";
$dbname = "help";
$GLOBALS['UploadPath'] = "upload/";*/

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

//$sql = "SELECT * FROM user";
//$result = $conn->query($sql);
//$row=$result->fetch_assoc();
//print_r($row);
?>