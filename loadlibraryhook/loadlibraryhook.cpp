#include "loadlibraryhook.h"
#pragma comment (lib, "detours/detours")

const char *logFile = "D:\\log.txt";

HMODULE WINAPI FakeLoadLibraryA(
  LPCSTR lpLibFileName
) {
    FILE *fd = fopen(logFile, "a");
    std::string mytime = CurrentTime();

	LPCSTR cryptoApiLibs[7] = { "bcryptprimitives.dll","crypt32.dll","cryptbase.dll","cryptdll.dll", "cngaudit.dll","rsaenh.dll",
	"cryptsp.dll"};

	for(int i = 0; i < 7; ++i)
	{
		if (wcscmp(lpLibFileName, cryptoApiLibs[i]) == 0)
		{
			SentToPipe(std::to_string(GetCurrentProcessId()));
			break;
		}
	}
	
    fprintf(fd, "[LoadLibraryA] %s %s %d\n", mytime.c_str(), lpLibFileName, GetCurrentProcessId());
    fclose(fd);

    return RealLoadLibraryA(lpLibFileName);
}

void SentToPipe(std::string message) {

    HANDLE hPipe;
    DWORD dwWritten;
    hPipe =  CreateFileW(TEXT(L"\\\\.\\pipe\\my-very-cool-pipe-example"), 
		GENERIC_READ | GENERIC_WRITE, 
		FILE_SHARE_WRITE, 
		NULL, 
		OPEN_EXISTING, 
		0, 
		NULL);
    if (hPipe != INVALID_HANDLE_VALUE)
    {
		FILE *fd = fopen(logFile, "a");
		fprintf(fd, "Pipe opened1");
		fclose(fd);
        WriteFile(hPipe,
                  &message[0],
                  message.size() + 1,   // = length of string + terminating '\0' !!!
                  &dwWritten,
                  NULL);

        CloseHandle(hPipe);
    }
	else
	{
		FILE *fd = fopen(logFile, "a");
		fprintf(fd, "Can't open pipe1");
		fclose(fd);
	}
}

INT APIENTRY DllMain(HMODULE hModule, DWORD Reason, LPVOID lpReserved) {
    FILE *fd = fopen(logFile, "a");

    switch(Reason) {
        case DLL_PROCESS_ATTACH:
		
            DetourTransactionBegin();
            DetourUpdateThread(GetCurrentThread());

			DetourAttach(&(PVOID&)RealLoadLibraryA, FakeLoadLibraryA);


            DetourTransactionCommit();
            fprintf(fd, "[SUCCESS] Hooked CryptoAPI\n");
            break;

        case DLL_PROCESS_DETACH:

            DetourTransactionBegin();
            DetourUpdateThread(GetCurrentThread());

            DetourDetach(&(PVOID&)RealLoadLibraryA, FakeLoadLibraryA);

            DetourTransactionCommit();
            break;

        case DLL_THREAD_ATTACH:
            break;

        case DLL_THREAD_DETACH:
            break;
    }

    fclose(fd);
    return TRUE;
}

const std::string CurrentTime() {
    SYSTEMTIME st;
    GetSystemTime(&st);
    char currentTime[100] = "";
    sprintf(currentTime,"%d:%d:%d %d",st.wHour, st.wMinute, st.wSecond , st.wMilliseconds);
    return std::string(currentTime);
}