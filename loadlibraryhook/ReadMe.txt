Compile code in Developer Command Prompt for VS 2019

Build code:
cl /LD loadlibraryhook.cpp

Next you need to go to the registry (regedit.exe)
In chapter:
HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion\Windows
In AppInit_DLLs specify the path to loadlibraryhook.dll
For example, if the library is located on the C: drive, then specify C:\loadlibraryhook.dll there.

This is needed to intercept the LoadLibraryA function. This function is called when a program requests access to libraries.
If a process requests access to one of the cryptographic libraries (bcryptprimitives.dll crypt32.dll cryptbase.dll cryptdll.dll cngaudit.dll rsaenh.dll cryptsp.dll),
then this information is sent to the CryprojackerFinder program via a named pipe.