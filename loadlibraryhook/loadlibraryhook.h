#ifndef LOAD_LIBRARY_HOOK
#define LOAD_LIBRARY_HOOK

#include <stdio.h>
#include <windows.h>
#include <bcrypt.h>
#include <winioctl.h>
#include <Winternl.h>
#include <string>

#include "detours/detours.h"
#include <tchar.h>



static HMODULE (WINAPI *RealLoadLibraryA) (LPCSTR) = LoadLibraryA;
		
HMODULE WINAPI FakeLoadLibraryA(
  LPCSTR lpLibFileName
);

void SentToPipe(std::string message);

const std::string CurrentTime();

#endif //LOAD_LIBRARY_HOOK
