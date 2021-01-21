Компилировать код в Developer Command Prompt for VS 2019

Build code:
cl /LD loadlibraryhook.cpp

Далее нужно зайти в реестр (regedit.exe)
в разделе:
HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion\Windows
В AppInit_DLLs указать путь к loadlibraryhook.dll
Например, если библиотека лежит на диске C:, то указать там C:\loadlibraryhook.dll.

Это нужно, чтобы перехватывать функцию LoadLibraryA. Эта функции вызывается, когда программа запращивает доступ к библиотекам.
Если процесс запращивает доступ к одной из криптографических библиотек(bcryptprimitives.dll crypt32.dll cryptbase.dll cryptdll.dll cngaudit.dll rsaenh.dll cryptsp.dll),
то это информация отправляется программе CryprojackerFinder по именнованному пайпу.