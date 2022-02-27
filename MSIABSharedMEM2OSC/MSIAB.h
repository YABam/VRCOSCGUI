#pragma once
#include "stdafx.h"
#include "MAHMSharedMemory.h"
class MSIAB
{
protected:
	CString m_strInstallPath;
	LPVOID m_pMapAddr;//address of shared memory
	HANDLE m_hMapFile;

public:
	MSIAB();
	LPVOID GetMapAddress();

	void Connect();
	void Disconnect();

	CString GetMSIABInstallPath();
	BOOL IsSystemSource(DWORD id);
	CString DumpGpuEntry(LPMAHM_SHARED_MEMORY_GPU_ENTRY lpGpuEntry);
	CString DumpEntry(LPMAHM_SHARED_MEMORY_ENTRY lpEntry);
};