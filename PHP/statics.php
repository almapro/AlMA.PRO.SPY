<?php
    require 'config.php';
    function connectdb(){
  		global $dbhost;
  		global $dbuser;
  		global $dbpass;
  		global $dbname;
  		$con = new mysqli($dbhost,$dbuser,$dbpass,$dbname);
  		if($con->connect_error){return false;}
  		if($con->query("SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'")===false){return false;}
  		return $con;
  	}
    function RandColor(){
      $h = md5(time().rand(0,999));
      $r = substr($h,0,2);
      $g = substr($h,2,2);
      $b = substr($h,4,2);
      return "#".$r.$g.$b;
    }
    $dbcon = connectdb();
    if($dbcon === false){die("Error in connecting.");}
    $clmn="bots";
    $rcon = $dbcon->prepare("SELECT * FROM ".$clmn);
    $totbots = 0;
    if($rcon!==false){
        if($rcon->execute()){
            $rcon->store_result();
            if($rcon->affected_rows > 0){
              if($rcon->fetch()){
                $totbots = $rcon->affected_rows;
              }else{
                die("Error in fetching.");
              }
            }
        }else{
          die("Error in executing.");
        }
    }else{
      die("Error in preparing.");
    }
    ?>
<div class="pie_container">
<style>
  .pie_container{
    padding: 10px;
  }
  .pie{
    position:absolute;
    width:100px;
    height:200px;
    overflow:hidden;
    left:150px;
    margin-left: 10px;
    -moz-transform-origin:left center;
    -ms-transform-origin:left center;
    -o-transform-origin:left center;
    -webkit-transform-origin:left center;
    transform-origin:left center;
  }
  .pie:BEFORE {
    box-shadow: 0 0 5px 0 white;
    content:"";
    position:absolute;
    width:100px;
    height:200px;
    left:-100px;
    border-radius:100px 0 0 100px;
    -moz-transform-origin:right center;
    -ms-transform-origin:right center;
    -o-transform-origin:right center;
    -webkit-transform-origin:right center;
    transform-origin:right center;

  }
  .pie.big {
		width:200px;
		height:200px;
		left:50px;
		-moz-transform-origin:center center;
		-ms-transform-origin:center center;
		-o-transform-origin:center center;
		-webkit-transform-origin:center center;
		transform-origin:center center;
	}
	.pie.big:BEFORE {
		left:0px;
	}
  .pie.big:AFTER {
		content:"";
		position:absolute;
		width:100px;
		height:200px;
		left:100px;
		border-radius:0 100px 100px 0;
	}
  .pie.os:nth-of-type(1):BEFORE,
  .pie.os:nth-of-type(1):AFTER {
    background-color:black;
  }
  .pie.os:nth-of-type(2):AFTER,
  .pie.os:nth-of-type(2):BEFORE {
    background-color:silver;
  }
  .pie.os:nth-of-type(3):AFTER,
  .pie.os:nth-of-type(3):BEFORE {
    background-color:green;
  }
  .pie.os:nth-of-type(4):AFTER,
  .pie.os:nth-of-type(4):BEFORE {
    background-color:royalblue;
  }
  .pie.os:nth-of-type(5):AFTER,
  .pie.os:nth-of-type(5):BEFORE {
    background-color:gray;
  }
  .pie[data-start="n"] {
    display: none;
  }
  .pie[data-value="n"]:BEFORE {
    display: none;
  }
<?php
    if($totbots == 0){
      echo '
  .nobots{
  box-shadow: 0 0 15px 0 white;
  background: black;
  width: 200px;
  height: 200px;
  border-radius:100px 100px 100px 100px;
  font-size: 50px;
  color: white;
  }
</style>';
      die('<div class="nobots" title="No bots found"><span style="left: 95px;top: 75px;position: absolute;">0</span></div>');
    }
    $wg=0;
    $pie_chart="";
    $pie_explain='';
    $include_big=false;
    $big_included=false;
    $dfs=0;
    foreach(array('Linux','Mac','Android','Windows','IOS') as $os){
      $q = $dbcon->query("SELECT os,COUNT(*) AS alma FROM ".$clmn." WHERE os LIKE '%".strtolower($os)."%' GROUP BY os");
      if($q->num_rows > 0){$dfs+=1;}
    }
    if($dfs < 3){$include_big=true;}
    foreach(array('Linux','Mac','Android','Windows','IOS') as $os){
      $rcon = $dbcon->prepare("SELECT * FROM ".$clmn." WHERE os LIKE '%".strtolower($os)."%'");
	    if($rcon!==false){
	        if($rcon->execute()){
              $pie_perc='
  .pie.os[data-start="_ST_"]{
    -moz-transform: rotate(_ST_deg); /* Firefox */
    -ms-transform: rotate(_ST_deg); /* IE */
    -webkit-transform: rotate(_ST_deg); /* Safari and Chrome */
    -o-transform: rotate(_ST_deg); /* Opera */
    transform:rotate(_ST_deg);
  }
  .pie.os[data-value="_VL_"]:BEFORE{
    -moz-transform: rotate(_VL_deg); /* Firefox */
    -ms-transform: rotate(_VL_deg); /* IE */
    -webkit-transform: rotate(_VL_deg); /* Safari and Chrome */
    -o-transform: rotate(_VL_deg); /* Opera */
    transform:rotate(_VL_deg);
  }';
              $rcon->store_result();
              $div='<div class="pie os_BIG_" data-start="_ST_" data-value="_VL_"></div>';
              $pie_ex='<span style="float:left;padding: 10px;"><div style="width: 25px;height: 25px;background:_CLR_;" title="_OS_ - _TTL_"></div></span>';
              if($rcon->affected_rows > 0){
                if($rcon->fetch()){
                    $perc=($rcon->affected_rows/$totbots)*100;
                    $circ=(($rcon->affected_rows/$totbots)*360);
                    $pie_perc = str_replace("_ST_",$wg,$pie_perc);
                    $pie_perc = str_replace("_VL_",$circ,$pie_perc);
                    if($include_big){
                      if(!$big_included){
                        $div = str_replace("_BIG_"," big",$div);
                        $big_included=true;
                      }
                    }
                    $div = str_replace("_ST_",$wg,$div);
                    $div = str_replace("_VL_",$circ,$div);
                    $pie_ex = str_replace("_TTL_",$perc."%",$pie_ex);
                    $pie_ex = str_replace("_OS_",$os,$pie_ex);
                    $clr="transparent;display: none";
                    switch(strtolower($os)){
                      case 'linux':
                        $clr="black";
                        break;
                      case 'mac':
                        $clr="silver";
                        break;
                      case 'android':
                        $clr="green";
                        break;
                      case 'windows':
                        $clr="royalblue";
                        break;
                      case 'ios':
                        $clr="gray";
                        break;
                    }
                    $pie_ex = str_replace("_CLR_",$clr,$pie_ex);
                    $wg = $wg + $circ;
  	            }else{
  	                die("Error in fetching.");
  	            }
              }else{
                $pie_perc = "";
                $div = str_replace("_ST_","n",$div);
                $div = str_replace("_VL_","n",$div);
                $pie_ex = "";
              }
              $div = str_replace("_BIG_","",$div);
              echo $pie_perc;
              $pie_chart.=$div."
";
              $pie_explain.=$pie_ex;
	        }else{
	            die("Error in executing.");
	        }
	    }else{
	        die("Error in preparing.");
	    }
    }
?>
    .pie-explain{
      display: inline-block;
      background: #4c4c4c;
      color: white;
      padding: 5px;
      float: left;
      margin-left: 45px;
      margin-right: 5px;
      width: 150px;
      height: 100px;
      overflow-y: auto;
    }
    .ex{
      box-shadow: 0 0 15px 0 white;
    }
</style>
<?php
  echo $pie_chart;
  $main_explain='<div class="pie-explain ex">'.$pie_explain.'</div>';
  $wg=0;
  $lft=0;
  $pie_chart="";
  $pie_explain='';
  $totstatics=0;
  $q = $dbcon->query("SELECT * FROM `statics`"); $totstatics=$q->num_rows;
  if($totstatics==0){die('<div style="float: left;position:absolute;top:250px;padding:10px;">'.$main_explain.'</div></div>');}
  $pad=3;
  foreach(array('Country','AntiVirus') as $var){
    $rcon = $dbcon->prepare("SELECT ".strtolower($var).",COUNT(*) AS alma FROM statics GROUP BY ".strtolower($var)." ORDER BY alma DESC");
    $include_big=false;
    $big_included=false;
    $dfs=0;
    if($rcon!==false){
        if($rcon->execute()){
            $rcon->store_result();
            echo '<style>
.pie.'.strtolower($var).'{
left: '.$pad.'50px;
}';
            if($rcon->affected_rows > 0){
              if($rcon->affected_rows < 3){$include_big=true;}
              $res='';
              $alma=0;
              $rcon->bind_result($res,$alma);
              while($rcon->fetch()){
                    $div='<div class="pie '.strtolower($var).'_BIG_" _CLR_ data-start="_ST_" data-value="_VL_"></div>';
                    $pie_ex='<span style="float:left;padding: 10px;"><div style="width: 25px;height: 25px;background:_CLR_;" title="_VAR_ - _TTL_"></div></span>';
                    $pie_perc='
    .pie.'.strtolower($var).'[data-start="_ST_"]{
      -moz-transform: rotate(_ST_deg); /* Firefox */
      -ms-transform: rotate(_ST_deg); /* IE */
      -webkit-transform: rotate(_ST_deg); /* Safari and Chrome */
      -o-transform: rotate(_ST_deg); /* Opera */
      transform:rotate(_ST_deg);
    }
    .pie.'.strtolower($var).'[data-value="_VL_"]:BEFORE{
      -moz-transform: rotate(_VL_deg); /* Firefox */
      -ms-transform: rotate(_VL_deg); /* IE */
      -webkit-transform: rotate(_VL_deg); /* Safari and Chrome */
      -o-transform: rotate(_VL_deg); /* Opera */
      transform:rotate(_VL_deg);
    }
    .pie.'.strtolower($var).'['.strtolower($var).'="'.$res.'"]:BEFORE,
    .pie.'.strtolower($var).'['.strtolower($var).'="'.$res.'"]:AFTER {
      background-color:_CLR_;
    }
    ';
                  $perc=($alma/$totstatics)*100;
                  $circ=(($alma/$totstatics)*360);
                  $pie_perc = str_replace("_ST_",$wg,$pie_perc);
                  $pie_perc = str_replace("_VL_",$circ,$pie_perc);
                  $clr=RandColor();
                  $pie_perc = str_replace("_CLR_",$clr,$pie_perc);
                  if($include_big){
                    if(!$big_included){
                      $div = str_replace("_BIG_"," big\" style='left: ".($pad-1)."50px;'\"",$div);
                      $big_included=true;
                    }
                  }
                  $div = str_replace("_CLR_",strtolower($var)."=\"".$res."\"",$div);
                  $div = str_replace("_ST_",$wg,$div);
                  $div = str_replace("_VL_",$circ,$div);
                  $pie_ex = str_replace("_TTL_",$perc."%",$pie_ex);
                  $pie_ex = str_replace("_VAR_",$res,$pie_ex);
                  $pie_ex = str_replace("_CLR_",$clr,$pie_ex);
                  $wg = $wg + $circ;
                  $div = str_replace("_BIG_","",$div);
                  echo $pie_perc.'/* AlMA: '.$totstatics.' - '.$alma.' - '.$wg.'*/
';
                  $pie_chart.=$div."
";
                  $pie_explain.=$pie_ex;
              }
              $pad+=2;
              $lft+=2;
            }else{
              $pie_perc = "";
              $div = "";
              $pie_ex = "";
            }
            echo '</style>
'.$pie_chart;
            $main_explain.='<div class="pie-explain ex">'.$pie_explain.'</div>';
            $pie_explain="";
            $pie_chart="";
        }else{
            die("Error in executing.");
        }
    }else{
        die("Error in preparing.");
    }
    $wg=0;
  }
  $rcon = $dbcon->prepare("SELECT status,COUNT(*) AS alma FROM statics GROUP BY status ORDER BY alma DESC");
  $include_big=false;
  $big_included=false;
  $dfs=0;
  if($rcon!==false){
      if($rcon->execute()){
          $rcon->store_result();
          echo '<style>
.pie.status{
left: '.$pad.'50px;
}';
          //
          if($rcon->affected_rows > 0){
            if($rcon->affected_rows < 3){$include_big=true;}
            $res=0;
            $status='';
            $rcon->bind_result($status,$res);
            while($rcon->fetch()){
                  $div='<div class="pie status_BIG_" clr="_S_" data-start="_ST_" data-value="_VL_"></div>';
                  $pie_ex='<span style="float:left;padding: 10px;"><div style="width: 25px;height: 25px;background:_CLR_;" title="_TTL_"></div>_VAR_</span>';
                  $pie_perc='
  .pie.status[data-start="_ST_"]{
    -moz-transform: rotate(_ST_deg); /* Firefox */
    -ms-transform: rotate(_ST_deg); /* IE */
    -webkit-transform: rotate(_ST_deg); /* Safari and Chrome */
    -o-transform: rotate(_ST_deg); /* Opera */
    transform:rotate(_ST_deg);
  }
  .pie.status[data-value="_VL_"]:BEFORE{
    -moz-transform: rotate(_VL_deg); /* Firefox */
    -ms-transform: rotate(_VL_deg); /* IE */
    -webkit-transform: rotate(_VL_deg); /* Safari and Chrome */
    -o-transform: rotate(_VL_deg); /* Opera */
    transform:rotate(_VL_deg);
  }
  .pie.status[clr="0"]:BEFORE,
  .pie.status[clr="0"]:AFTER {
    background-color:red;
  }
  .pie.status[clr="1"]:BEFORE,
  .pie.status[clr="1"]:AFTER {
    background-color:green;
  }
  .pie.status[clr="2"]:BEFORE,
  .pie.status[clr="2"]:AFTER {
    background-color:gray;
  }
  ';
                $perc=($res/$totstatics)*100;
                $circ=(($res/$totstatics)*360);
                $pie_perc = str_replace("_ST_",$wg,$pie_perc);
                $pie_perc = str_replace("_VL_",$circ,$pie_perc);
                if($include_big){
                  if(!$big_included){
                    $div = str_replace("_BIG_"," big\" style='left: ".($pad-1)."50px;'\"",$div);
                    $big_included=true;
                  }
                }
                $div = str_replace("_ST_",$wg,$div);
                $div = str_replace("_VL_",$circ,$div);
                $div = str_replace("_S_",$status,$div);
                $pie_ex = str_replace("_TTL_",$perc."%",$pie_ex);
                $pie_st="";
                $clr="transparent;display:none";
                switch($status){
                  case '0':
                    $clr="red";
                    $pie_st="Offline";
                    break;
                  case '1':
                    $clr="green";
                    $pie_st="Online";
                    break;
                  case '2':
                    $clr="gray";
                    $pie_st="Dead";
                    break;
                }
                $pie_ex = str_replace("_VAR_",$pie_st,$pie_ex);
                $pie_ex = str_replace("_CLR_",$clr,$pie_ex);
                $wg = $wg + $circ;
                $div = str_replace("_BIG_","",$div);
                echo $pie_perc.'/* AlMA: '.$totstatics.' - '.$res.' - '.$wg.'*/
';
                $pie_chart.=$div."
";
                $pie_explain.=$pie_ex;
            }
            $pad+=2;
            $lft+=2;
          }else{
            $pie_perc = "";
            $div = "";
            $pie_ex = "";
          }
          echo '</style>
'.$pie_chart;
          $main_explain.='<div class="pie-explain ex">'.$pie_explain.'</div>';
          $pie_explain="";
          $pie_chart="";
      }else{
          die("Error in executing.");
      }
  }else{
      die("Error in preparing.");
  }
  $wg=0;
  echo '<div style="float: left;position:absolute;top:250px;padding:10px;">'.$main_explain.'</div></div>';
?>
