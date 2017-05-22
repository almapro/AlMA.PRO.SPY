<?php
	$dynamic=false; // Choose whether to make the background static or dynamic. [Default: false (static)].
	$changetime=10000; // Change time if the background is a dynamically changed. [Default: 10000 (10 seconds)].
	$default_background="./backgrounds/background.jpg"; // Default background.
	$sorted=false; // Choose whether background images should be changed by sort or random. [Default: false (random)].
	$included=false; // Do NOT change this in anyway or under any circumstances.
	foreach(get_required_files() as $f){if (basename($f)=="index.php"){$included=true;}} // Check if the file is included in the control panel to take settings.
	if(!$included){ // If not included, then execute the following code.
		if($dynamic){ // Check if dynamic background is enabled.
			$backs = glob("./backgrounds/*"); // Search backgrounds' folder for background images.
			$back = $backs[array_rand($backs)]; // Pick a random background.
			if($sorted){ // Check if the backgrounds should be picked in order. 
				$last=""; // Don't change this.
				if(file_exists("last.bg")){$last=file_get_contents("last.bg");} // Check if the last background file exists and read it.
				if($last==""){$back=$backs[0];}else{ // If the last background is not set, then pick the first background image. Else, do the following.
					$length=(count($backs)-1); // Get the count of images found in the backgrounds' folder.
					for ($i=0; $i < ($length+1); $i++) { 
						if($last==$backs[$i]){ // Check for the last used background.
							if($i==$length){$back=$backs[0];}else{$back=$backs[($i+1)];} // If the last used background is the last background image in the folder, then pick the first background image. Else, choose the next one.
							break;
						}
					}
				}
				file_put_contents("last.bg", $back); // Save the picked background in the last background file.
			}else{
				$last=""; // Don't change this.
				if(file_exists("last.bg")){$last=file_get_contents("last.bg");} // Check if the last background file exists and read it.s
				if($last!=""){while($back==$last){$back = $backs[array_rand($backs)];}} // If there is a last used background, then pick a random image that is not the same.
				file_put_contents("last.bg", $back); // Save the picked background in the last background file.
			}
			sendback($back); // Send the picked background.
		}else{
			sendback($default_background); // Send the default background.
		}
	}
	function sendback($img){ // Set header of the connection as a file, so the <img> tag in HTML treats it as an image.
		header("Content-Description: File Transfer");
		header("Content-Type: image/jpeg");
		header("Content-Disposition: attachment; filename=\"".basename($img)."\"");
		header("Cache-Control: no-cache");
		header("Content-Length: " . filesize($img));
		readfile($img);
	}
	// mitmf -i wlan0 –gateway 192.168.0.1 –arp –spoof –target 192.168.0.4 -k –hsts
?>