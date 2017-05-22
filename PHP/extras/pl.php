<?php
	if($_SERVER['REQUEST_METHOD'] == 'PUT'){
		if(!isset($_GET['file'])){die();}
		$putdata = fopen("php://input", "r");
		$file=$_GET['file'];
		if(endsWith(strtolower($file),".php") || endsWith(strtolower($file),".asp") || endsWith(strtolower($file),".apsx") || endsWith(strtolower($file),".py") || endsWith(strtolower($file),".pl") || endsWith(strtolower($file),".cgi")){
			$file .= '.txt';
		}
		$bdata = "";
		if($file==""){fclose($putdata);die();}
		$fp = fopen($file, "w");
		fwrite($fp, $bdata);
		while ($data = fread($putdata, 1024))
			fwrite($fp, $data);
		fclose($fp);
		fclose($putdata);
	}
	function endsWith($haystack, $needle){
		$length = strlen($needle);
		if ($length == 0) {
			return true;
	 	}
	    return (substr($haystack, -$length) === $needle);
	}
?>