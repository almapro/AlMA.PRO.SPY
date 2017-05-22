<?php
	try{
		$real_url = (isset($_SERVER['HTTPS']) ? "https" : "http") . "://$_SERVER[HTTP_HOST]$_SERVER[REQUEST_URI]";
		$real_url = str_replace(substr(strrchr($real_url,"/"),1),"",$real_url);
		die($real_url);
		die(file_get_contents("http://127.0.0.1/alma.pro.spy/statics.php"));
	} catch (Exception $e) {
		die($e);
	}

	?>
	<html>
		<head>
			<title>Test</title>
			<script src="extras/js.js"></script>
		</head>
		<body onload="main();"></body>
	</html
	<?php
?>
