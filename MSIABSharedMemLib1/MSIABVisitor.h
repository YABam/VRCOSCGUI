#pragma once

#include "stdafx.h"
#include "MAHMSharedMemory.h"
#include <string>
using namespace std;
/*
enum HardwareData
{
	CPULoad,
	CPUTemp,
	GPULoad,
	GPUTemp
};

class MSIABVisitor
{
protected:
	CString m_strInstallPath;
	LPVOID m_pMapAddr;//address of shared memory
	HANDLE m_hMapFile;

	void Connect();
	void Disconnect();
public:
	MSIABVisitor();
	LPVOID GetMapAddress();

	CString GetMSIABInstallPath();
	BOOL IsSystemSource(DWORD id);

	CString DumpGpuEntry(LPMAHM_SHARED_MEMORY_GPU_ENTRY lpGpuEntry);
	CString DumpEntry(LPMAHM_SHARED_MEMORY_ENTRY lpEntry);

	//System::String^

	float GetHardwareData(HardwareData hardware);
};*/

#include <string>
public ref class Function
{
public:
	Function(void);
	~Function(void);
	int menber;
	int menberFuncAdd(int a, int b);
	System::String^ say(System::String^ str);
};