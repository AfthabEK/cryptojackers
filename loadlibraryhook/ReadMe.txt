������������� ��� � Developer Command Prompt for VS 2019

Build code:
cl /LD loadlibraryhook.cpp

����� ����� ����� � ������ (regedit.exe)
� �������:
HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion\Windows
� AppInit_DLLs ������� ���� � loadlibraryhook.dll
��������, ���� ���������� ����� �� ����� C:, �� ������� ��� C:\loadlibraryhook.dll.

��� �����, ����� ������������� ������� LoadLibraryA. ��� ������� ����������, ����� ��������� ����������� ������ � �����������.
���� ������� ����������� ������ � ����� �� ����������������� ���������(bcryptprimitives.dll crypt32.dll cryptbase.dll cryptdll.dll cngaudit.dll rsaenh.dll cryptsp.dll),
�� ��� ���������� ������������ ��������� CryprojackerFinder �� ������������� �����.