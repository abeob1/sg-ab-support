<?php
error_reporting(0);
/**
 *	Uses PHP IMAP extension, so make sure it is enabled in your php.ini,
 *	extension=php_imap.dll
  */
 set_time_limit(3000); 
 /* connect to gmail with your credentials */
$hostname = '{imap.gmail.com:993/imap/ssl/novalidate-cert/norsh}Inbox';
$username = 'thomasp@abeo-electra.com'; # e.g somebody@gmail.com
$password = 'Peterrajt1';
/* try to connect */
$nntp  = imap_open($hostname,$username,$password) or die('Cannot connect to Gmail: ' . imap_last_error());
$threads = imap_thread($nntp);

print_r("<pre>");
print_r($threads);
print_r("</pre>");

imap_close($nntp);


?>