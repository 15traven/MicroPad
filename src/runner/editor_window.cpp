#include "pch.h"
#include "editor_window.h"

std::atomic_bool g_isLaunchInProgress = false;
DWORD g_editor_process_id = 0;

static void run_editor_window()
{
	g_isLaunchInProgress = true;

	STARTUPINFO startup_info{ sizeof(startup_info) };
	PROCESS_INFORMATION process_info = { 0 };
	LPCWSTR exe_path = L"MicroPad.Editor.exe";
	HANDLE hToken = nullptr;

	WCHAR command_line[MAX_PATH];
	wcscpy_s(command_line, exe_path);

	if (!CreateProcessW(nullptr,
		command_line,
		nullptr,
		nullptr,
		FALSE,
		0,
		nullptr,
		nullptr,
		&startup_info,
		&process_info))
	{
		goto LExit;
	}
	else
	{
		g_isLaunchInProgress = false;
	}

	if (!OpenProcessToken(GetCurrentProcess(), TOKEN_QUERY, &hToken))
	{
		goto LExit;
	}

	g_editor_process_id = process_info.dwProcessId;
	
	if (process_info.hProcess)
	{
		WaitForSingleObject(process_info.hProcess, INFINITE);
	}

LExit:
	if (process_info.hProcess)
	{
		CloseHandle(process_info.hProcess);
	}

	if (process_info.hThread)
	{
		CloseHandle(process_info.hThread);
	}

	if (hToken)
	{
		CloseHandle(hToken);
	}

	g_editor_process_id = 0;
}

void open_editor_window()
{
	if (g_editor_process_id == 0 && !g_isLaunchInProgress)
	{
		std::thread([]() {
			run_editor_window();
		}).detach();
	}
}

void close_editor_window()
{
	if (g_editor_process_id == 0)
	{
		return;
	}

	HANDLE proc{ OpenProcess(PROCESS_ALL_ACCESS, false, g_editor_process_id) };
	if (proc)
	{
		WaitForSingleObject(proc, 1500);
		TerminateProcess(proc, 0);
	}
}