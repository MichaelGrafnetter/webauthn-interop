// This is a wrapper for the webauthn.h header file,
// which for some reason does not include windows.h and thus cannot be compiled by itself.
#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include "..\webauthn\webauthn.h"
