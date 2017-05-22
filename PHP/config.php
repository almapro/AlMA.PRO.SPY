<?php
	date_default_timezone_set('Africa/Tripoli'); // Set your timezone according to PHP website, so you see correct time. [Default: Africa/Tripoli].
	$useragent=''; // Set UserAgent for restricting access to the ControlPanel [Default: '']. **Recommended**
	$dbhost='localhost'; // Database host.
	$dbuser='root'; // Database username.
	$dbpass=''; // Database password.
	$dbname='apsdb'; // AlMA.PRO.SPY database [Default: apsdb].
	$logins=array('almapro' => 'leader','' => ''); // AlMA.PRO.SPY logins' details (user => pass) [Default: almapro => leader].
	$loginneed=true; // Whether login is needed or not [Default: true = needed].
	$cookiename='aps_session'; // Cookie name for organizing active sessions [Default: aps_session].
	$cookietimeout=(86400 * 3); // Cookie timeout [Default: (86400 * 3)]. Means 3 days (86400 ==> 1 day).


	//almapro|dj0W69epQYG2b92mYBEj6qmhJVIvMx3Z|1495741054|Linux|FireFox

	//almapro|2JpXAUJlJoKst3QhwWTY3js33b7XepAh|1495746944|Windows 7|FireFox
?>
